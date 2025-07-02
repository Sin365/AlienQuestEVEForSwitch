using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AxiSoundPool
{
    static long mSeed = 1;
    const float soundkeep_time = 20f;
    static HashSet<string> mHashSetKeepIn_Name = new HashSet<string>();
    static HashSet<long> mHashSetInPool_Seed = new HashSet<long>();
    static Dictionary<string, float> mDictName2LastLoadTime = new Dictionary<string, float>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Inv = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Moan = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Shield = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, LoadedAudioSrc> mDictPath2Resource = new Dictionary<string, LoadedAudioSrc>();

    static long GetNextSeed()
    {
        return mSeed++;
    }
    static Dictionary<string, List<AxiSoundBase>> GetPoolByType(AxiSoundBase src_axi)
    {
        Dictionary<string, List<AxiSoundBase>> dictPool = null;
        if (src_axi is Sound)
            dictPool = mPool_Name2Sound;
        else if (src_axi is Sound_Inv)
            dictPool = mPool_Name2Sound_Inv;
        else if (src_axi is Sound_Moan)
            dictPool = mPool_Name2Sound_Moan;
        else if (src_axi is Sound_Shield)
            dictPool = mPool_Name2Sound_Shield;
        return dictPool;
    }
    public static void PreLoadAudio()
    {
        List<string> temp = new List<string>();
        temp.Add(Sound_Control.Sound_Magic_1);
        temp.Add(Sound_Control.Sound_Magic_2);
        temp.Add(Sound_Control.Sound_Magic_3_Explo_1);
        temp.Add(Sound_Control.Sound_Magic_3_Explo_2);
        temp.Add(Sound_Control.Sound_Magic_3_Explo_3);
        temp.Add(Sound_Control.Sound_Hit_1);
        temp.Add(Sound_Control.Sound_Hit_2);
        temp.Add(Sound_Control.Sound_Hit_3);
        temp.Add(Sound_Control.Sound_Hit_4);
        temp.Add(Sound_Control.Sound_Hit_5);
        temp.Add(Sound_Control.Sound_Hit_6);
        temp.Add(Sound_Control.Sound_Hit_11);
        temp.Add(Sound_Control.Sound_Hit_12);
        temp.Add(Sound_Control.Sound_Hit_Explo);
        temp.Add(Sound_Control.Sound_Footstep_Mon_1);
        temp.Add(Sound_Control.Sound_Footstep_Mon_2);
        temp.Add(Sound_Control.Sound_MonAtk_1);
        temp.Add(Sound_Control.Sound_MonAtk_2);
        temp.Add(Sound_Control.Sound_MonAtk_3);
        temp.Add(Sound_Control.Sound_MonAtk_4);
        temp.Add(Sound_Control.Sound_MonAtk_5);
        temp.Add(Sound_Control.Sound_MonAtk_6);
        temp.Add(Sound_Control.Sound_MonAtk_7);
        temp.Add(Sound_Control.Sound_Elec);
        temp.Add(Sound_Control.Sound_Plasma);
        temp.Add(Sound_Control.Mon_10_Growling_1);
        temp.Add(Sound_Control.Mon_10_Growling_2);
        temp.Add(Sound_Control.Mon_10_Growling_3);
        temp.Add(Sound_Control.Mon_10_Growling_4);
        temp.Add(Sound_Control.Alien_Growling_1);
        temp.Add(Sound_Control.Alien_Growling_2);
        temp.Add(Sound_Control.Alien_Growling_3);
        temp.Add(Sound_Control.Alien_Growling_4);
        temp.Add(Sound_Control.Alien_Growling_5);
        temp.Add(Sound_Control.Alien_Dash_1);
        temp.Add(Sound_Control.Alien_Dash_2);
        temp.Add(Sound_Control.Alien_Dash_3);
        temp.Add(Sound_Control.Alien_Dash_4);
        temp.Add(Sound_Control.Alien_Dmg_1);
        temp.Add(Sound_Control.Alien_Dmg_2);
        temp.Add(Sound_Control.Alien_Death_1);
        temp.Add(Sound_Control.Alien_Death_2);
        temp.Add(Sound_Control.Alien_Death_3);
        temp.Add(Sound_Control.Alien_Death_4);
        temp.Add(Sound_Control.Alien_Death_5);
        temp.Add(Sound_Control.Mon_8_Dmg);
        temp.Add(Sound_Control.Mon_7_Atk);
        temp.Add(Sound_Control.Mon_7_Dmg);
        temp.Add(Sound_Control.Mon_6_Dmg);
        temp.Add(Sound_Control.Mon_5_Dmg);
        temp.Add(Sound_Control.Mon_4_Dmg);
        temp.Add(Sound_Control.Mon_3_Dmg);
        temp.Add(Sound_Control.Mon_2_Dmg);
        temp.Add(Sound_Control.Mon_1_Dmg);
        temp.Add(Sound_Control.Mob_Dmg);
        temp.Add(Sound_Control.Mon_10_Atk);
        temp.Add(Sound_Control.Mon_10_Dmg1);
        temp.Add(Sound_Control.Mon_10_Dmg2);
        temp.Add(Sound_Control.Mon_10_Dmg3);
        for (int i = 0; i < Sound_Control.s_List.Length; i++)
            temp.Add(Sound_Control.s_List[i]);
        foreach (var path in temp)
        {
            if (!mDictPath2Resource.ContainsKey(path))
            {
                LoadedAudioSrc src = new LoadedAudioSrc(path, true);
                mDictPath2Resource[path] = src;
                if (!mHashSetKeepIn_Name.Contains(src.gobj.name))
                    mHashSetKeepIn_Name.Add(src.gobj.name);
            }
            else
                mDictPath2Resource[path].SetKeep(true);
        }
        Debug.Log($"[AxiSoundPool]音频预加载完毕{mDictPath2Resource.Count}个");
    }
    static AxiSoundBase AddSound(GameObject src)
    {
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        Dictionary<string, List<AxiSoundBase>> dictPool = GetPoolByType(src_axi);
        AxiSoundBase go;
        if (!dictPool.ContainsKey(src.name))
            dictPool[src.name] = new List<AxiSoundBase>();

        if (dictPool.ContainsKey(src.name) && dictPool[src.name].Count > 0)
        {
            AxiSoundBase sound = dictPool[src.name][dictPool[src.name].Count - 1];
            dictPool[src.name].RemoveAt(dictPool[src.name].Count - 1);
            sound.Init();
            go = sound;
            go.gameObject.SetActive(true);
#if UNITY_EDITOR
            Debug.Log($"[AxiSoundPool]出{src.name}池，当前{src.name}池{dictPool[src.name].Count}个");
#endif
            mHashSetInPool_Seed.Remove(sound.Seed);
        }
        else
        {
            go = AxiObject.Instantiate(src).GetComponent<AxiSoundBase>();
#if UNITY_EDITOR
            Debug.Log($"[AxiSoundPool]实例化新的[{src.name}]");
#endif
            go.resourceName = src.name;
            go.Seed = GetNextSeed();
        }
        mDictName2LastLoadTime[src.name] = Time.time;
        return go;
    }
    static AxiSoundBase AddSoundForTrans(GameObject src, Transform trans = null)
    {
        AxiSoundBase go = AddSound(src);

        Transform target = null;
        if (trans != null)
            target = trans;
        else if (GameManager.instance != null)
        {
            target = GameObject.Find("Main Camera").transform;
        }

        if (target != null)
        {
            go.transform.parent = target;
            go.transform.localPosition = Vector3.zero;
            go.transform.localEulerAngles = Vector3.zero;
        }
        //go.transform.parent = null;
        return go;

    }
    static AxiSoundBase GetAxiSoundByPath(string path)
    {
        if (!mDictPath2Resource.ContainsKey(path))
            mDictPath2Resource.Add(path, new LoadedAudioSrc(path, false));
        mDictPath2Resource[path].ResetTime();
        return mDictPath2Resource[path].gobj;
    }
    public static AxiSoundBase AddSoundForTrans(string path, Transform trans = null)
    {
        GameObject src = GetAxiSoundByPath(path).gameObject;
        return AddSoundForTrans(src, trans);
    }
    public static AxiSoundBase AddSoundForPosRot(string path, Vector3 targetPos, Quaternion targetRotation)
    {
        GameObject src = GetAxiSoundByPath(path).gameObject;
        return AddSoundForPosRot(src, targetPos, targetRotation);
    }
    public static AxiSoundBase AddSoundForPosRot(GameObject src, Vector3 targetPos, Quaternion targetRotation)
    {
        AxiSoundBase go = AddSound(src);
        go.transform.position = targetPos;
        go.transform.rotation = targetRotation;
        return go;
    }

    public static void ReleaseBySeed(GameObject src, int Seed)
    {
        if (!mHashSetInPool_Seed.Contains(Seed))
            return;
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        Dictionary<string, List<AxiSoundBase>> dictPool = GetPoolByType(src_axi);
        if (!dictPool.ContainsKey(src.name))
            return;
        for (int i = dictPool[src.name].Count - 1; i >= 0; i++)
        {
            if (dictPool[src.name][i].Seed == Seed)
                CheckNeedRemoveFormPool(dictPool[src.name][i]);
        }
    }

    public static void CheckNeedRemoveFormPool(AxiSoundBase go)
    {
        if (go == null)
            return;
        //主动释放，不再回收
        if (go.AutoReleaseForTimeOut)
            return;
        if (go.Seed == 0)
            return;
        if (string.IsNullOrEmpty(go.resourceName))
            return;
        if (!mHashSetInPool_Seed.Contains(go.Seed))
            return;
        mHashSetInPool_Seed.Remove(go.Seed);
        Dictionary<string, List<AxiSoundBase>> dictPool = GetPoolByType(go);
        if (!dictPool.ContainsKey(go.resourceName))
            return;
        for (int i = dictPool[go.resourceName].Count - 1; i >= 0; i--)
        {
            if (dictPool[go.resourceName][i].Seed == go.Seed)
            {
                dictPool[go.resourceName].RemoveAt(i);
                return;
            }
        }
    }

    public static void ReleaseSound(AxiSoundBase go)
    {
        if (go.AutoReleaseForTimeOut)
            return;
        if (go.Seed == 0)
        {
#if UNITY_EDITOR
            Debug.LogError($"[AxiSoundPool] 并不来自对象池创建,{go.name}");
#endif
            global::UnityEngine.Object.Destroy(go.gameObject);
            return;
        }
        if (string.IsNullOrEmpty(go.resourceName))
        {
#if UNITY_EDITOR
            Debug.LogError($"[AxiSoundPool] go.resourceName 为空,{go.name}");
#endif
            global::UnityEngine.Object.Destroy(go.gameObject);
            return;
        }
        go.gameObject.SetActive(false);
        go.transform.parent = null;
        if (mHashSetInPool_Seed.Contains(go.Seed))
        {
#if UNITY_EDITOR
            Debug.LogError($"[AxiSoundPool] InPool HashSet 已存在,{go.name}");
#endif
            return;
        }
        mHashSetInPool_Seed.Add(go.Seed);
        Dictionary<string, List<AxiSoundBase>> dictPool = GetPoolByType(go);
        dictPool[go.resourceName].Add(go);
#if UNITY_EDITOR
        Debug.Log($"[AxiSoundPool]入{go.resourceName}池，当前{go.resourceName}池{dictPool[go.resourceName].Count}个");
#endif
    }

    static List<string> tempRemoveLoadedSrc = new List<string>();
    public static void UpdateLogic()
    {
        bool bHad = false;

        tempRemoveLoadedSrc.Clear();
        var sourceiterator = mDictPath2Resource.GetEnumerator();
        while (sourceiterator.MoveNext())
        {
            if(sourceiterator.Current.Value.CheckNeedRemove())
                tempRemoveLoadedSrc.Add(sourceiterator.Current.Key);
        }
        sourceiterator.Dispose();

        for (int i = 0; i < tempRemoveLoadedSrc.Count; i++)
        {
            mDictPath2Resource[tempRemoveLoadedSrc[i]].Release();
            mDictPath2Resource.Remove(tempRemoveLoadedSrc[i]);
        }

        foreach (var key in mDictName2LastLoadTime.Keys.ToArray())//TODO 为什么这里迭代器会被改变？报错，暂时先ToArray
        {
            //常驻音频不清理
            if (mHashSetKeepIn_Name.Contains(key))
                return;

            if (mDictName2LastLoadTime[key] != -1 && Time.time - mDictName2LastLoadTime[key] > soundkeep_time)
            {
                ReleaseToPool(mPool_Name2Sound, key);
                ReleaseToPool(mPool_Name2Sound_Inv, key);
                ReleaseToPool(mPool_Name2Sound_Moan, key);
                ReleaseToPool(mPool_Name2Sound_Shield, key);
                mDictName2LastLoadTime[key] = -1;
                bHad = true;
            }
        }

        if (bHad)
        {
            //这里不用清理，算了，靠切换房间
            //AxiRoomManager.SetClearDirty();
        }
    }

    static void ReleaseToPool(Dictionary<string, List<AxiSoundBase>> pool, string key)
    {
        if (!pool.ContainsKey(key))
            return;
        foreach (var item in pool[key])
        {
            item.AutoReleaseForTimeOut = true;
            GameObject.Destroy(item.gameObject);
        }
        pool[key].Clear();
    }

    public class LoadedAudioSrc
    {
        bool needKeep;
        string path;
        public AxiSoundBase gobj;
        float mLastUseTime;
        public LoadedAudioSrc(string path, bool keep)
        {
            this.path = path;
            SetKeep(keep);
            gobj = Resources.Load<GameObject>(path).GetComponent<AxiSoundBase>();
        }

        public void SetKeep(bool keep)
        {
            needKeep = keep;
            ResetTime();
        }

        public void ResetTime()
        {
            mLastUseTime = Time.time;
        }

        public bool CheckNeedRemove()
        {
            return !needKeep && Time.time - mLastUseTime > soundkeep_time;
        }

        public void Release()
        {
            gobj = null;
        }
    }
}
