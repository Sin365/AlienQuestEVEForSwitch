using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public static class AxiRoomManager
{
    enum E_RoomLoadingDataState
    {
        None = 0,
        Loading = 1,
        Faild = 2,
        Success = 3,
    }
    static int CurrRoomID = -1;
    static Dictionary<int, List<int>> mDicRoomId2RoomIds = new Dictionary<int, List<int>>();
    static HashSet<int> NeedLoadRooms = new HashSet<int>();

    static Dictionary<int, GameObject> mDicLoadedRoomRes = new Dictionary<int, GameObject>();
    static Dictionary<int, RoomLoadingData> mDictLoadingRoomRes = new Dictionary<int, RoomLoadingData>();

    public static GameObject CloneRoom(int RoomID)
    {
        GameObject src;
        if (mDicLoadedRoomRes.ContainsKey(RoomID))
            src = mDicLoadedRoomRes[RoomID];
        else
        {
            src = Resources.Load<GameObject>(GameManager.instance.sm_StageManager.GetRoomResourceID(RoomID));
            //现同步加载的资源，加入已加载
            mDicLoadedRoomRes[RoomID] = src;
        }
        GameObject gameObject = AxiObject.Instantiate(src);
        if (CurrRoomID != RoomID)
        {
            PreLoadNearRoom(RoomID);
            CurrRoomID = RoomID;
            //释放掉已经没有依赖的资源
            Resources.UnloadUnusedAssets();
        }
        return gameObject;
    }
    public static void Update_Logic()
    {
        var keys = mDictLoadingRoomRes.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            RoomLoadingData loading = mDictLoadingRoomRes[keys[i]];
            loading.Update_logic();
            switch (loading.state)
            {
                case E_RoomLoadingDataState.Loading:
                    break;
                case E_RoomLoadingDataState.Success:
                    //先移除队列
                    mDictLoadingRoomRes.Remove(keys[i]);

                    //如果不需要
                    if (!NeedLoadRooms.Contains(loading.RoomID))
                        break;
                    //如果已经加载，则不要了
                    if (!mDicLoadedRoomRes.ContainsKey(loading.RoomID))
                        break;
                    else//保留加载的结果
                        mDicLoadedRoomRes[loading.RoomID] = (GameObject)loading.GetLoaded();
                    break;
            }
            loading.Release();
        }
    }

    static void PreLoadNearRoom(int CenterRoomID)
    {
        NeedLoadRooms.Clear();
        NeedLoadRooms.Add(CenterRoomID);
        for (int i = 0; i < mDicRoomId2RoomIds[CenterRoomID].Count; i++)
        {
            int roomid = mDicRoomId2RoomIds[CenterRoomID][i];
            if (!NeedLoadRooms.Contains(roomid))
                NeedLoadRooms.Add(roomid);
        }
        //检查新增的需要加载的Room
        foreach (var needloadroomid in NeedLoadRooms)
        {
            if (mDicLoadedRoomRes.ContainsKey(needloadroomid))
                continue;
            if (mDictLoadingRoomRes.ContainsKey(needloadroomid))
                continue;
            mDictLoadingRoomRes[needloadroomid] = new RoomLoadingData(needloadroomid);
        }

        //释放已经加载的，不再需要的Room
        var keys = mDicLoadedRoomRes.Keys.ToArray();
        for (int i = 0; i < keys.Length; i++)
        {
            int roomid = keys[i];
            if (!NeedLoadRooms.Contains(roomid))
            {
                //释放
                mDicLoadedRoomRes.Remove(roomid);
            }
        }
    }

    class RoomLoadingData
    {
        public int RoomID;
        ResourceRequest resourceRequest;
        public E_RoomLoadingDataState state;
        public RoomLoadingData(int RoomID)
        {
            this.RoomID = RoomID;
            resourceRequest = Resources.LoadAsync<GameObject>(GameManager.instance.sm_StageManager.GetRoomResourceID(RoomID));
            state = E_RoomLoadingDataState.None;
        }

        public void Update_logic()
        {
            if (state > E_RoomLoadingDataState.Loading)
                return;

            if (!resourceRequest.isDone)
                return;

            if (resourceRequest.isDone)
            {
                state = E_RoomLoadingDataState.Success;
            }
        }

        public Object GetLoaded()
        {
            if (state != E_RoomLoadingDataState.Success)
                return null;
            return resourceRequest.asset;
        }

        public void Release()
        {
            RoomID = -1;
            state = E_RoomLoadingDataState.None;
            resourceRequest = null;
        }
    }
}

