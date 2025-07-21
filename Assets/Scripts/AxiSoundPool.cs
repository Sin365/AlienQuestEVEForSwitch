using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public static class AxiSoundPool
{
    //音频pool优化完毕
    static long mSeed = 1;
    const float soundkeep_time = 20f;
    static HashSet<string> mHashSetKeepIn_Name = new HashSet<string>();
    static HashSet<long> mHashSetInPool_Seed = new HashSet<long>();
    static Dictionary<string, float> mDictName2LastLoadTime = new Dictionary<string, float>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Name2SoundClone = new Dictionary<string, List<AxiSoundBase>>();
    //static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound = new Dictionary<string, List<AxiSoundBase>>();
    //static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Inv = new Dictionary<string, List<AxiSoundBase>>();
    //static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Moan = new Dictionary<string, List<AxiSoundBase>>();
    //static Dictionary<string, List<AxiSoundBase>> mPool_Name2Sound_Shield = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, LoadedAudioSrc> mDictPath2Resource = new Dictionary<string, LoadedAudioSrc>();

    static long GetNextSeed()
    {
        return mSeed++;
    }
    public static void PreLoadAudio()
    {
        List<string> temp = new List<string>();
        //玩家
        temp.Add(Player_SoundList.Attack_1);
        temp.Add(Player_SoundList.Attack_2);
        temp.Add(Player_SoundList.Attack_3);
        temp.Add(Player_SoundList.Jump);
        temp.Add(Player_SoundList.Slide);
        temp.Add(Player_SoundList.Spin);
        temp.Add(Player_SoundList.Down);
        temp.Add(Player_SoundList.FootStep);
        temp.Add(Player_SoundList.voiceDamage_1);
        temp.Add(Player_SoundList.voiceDamage_2);
        temp.Add(Player_SoundList.voiceDamage_3);
        temp.Add(Player_SoundList.voiceDamage_4);
        temp.Add(Player_SoundList.voiceDeath_1);
        //开关门
        temp.Add(UI_SoundList.Gate_Open);
        temp.Add(UI_SoundList.Gate_Close);
        //战斗
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
                //加载之后隐藏回池
                //PS:原则上进出池 应该重置播放进度，但这里是初始化之后，直接回收了
                AxiSoundBase clone = AddSound(path);
                ReleaseSound(clone);

                if (!mHashSetKeepIn_Name.Contains(src.gobj.name))
                    mHashSetKeepIn_Name.Add(src.gobj.name);
            }
            else
                mDictPath2Resource[path].SetKeep(true);
        }
        Debug.Log($"[AxiSoundPool]音频预加载完毕{mDictPath2Resource.Count}个");
    }
    static AxiSoundBase AddSound(string path)
    {
		AxiSoundBase clonego;
        string srcname = Path.GetFileNameWithoutExtension(path);

        if (!mPool_Name2SoundClone.ContainsKey(srcname))
            mPool_Name2SoundClone[srcname] = new List<AxiSoundBase>();

        if (mPool_Name2SoundClone.ContainsKey(srcname) && mPool_Name2SoundClone[srcname].Count > 0)
        {
            AxiSoundBase sound = mPool_Name2SoundClone[srcname][mPool_Name2SoundClone[srcname].Count - 1];
            mPool_Name2SoundClone[srcname].RemoveAt(mPool_Name2SoundClone[srcname].Count - 1);
            sound.Init();
            clonego = sound;
            clonego.gameObject.SetActive(true);
#if UNITY_EDITOR
            Debug.Log($"[AxiSoundPool]出{srcname}池，当前{srcname}池{mPool_Name2SoundClone[srcname].Count}个");
#endif
            mHashSetInPool_Seed.Remove(sound.Seed);
        }
        else
        {
            GameObject src = GetAxiSoundSrcByPath(path);
            clonego = AxiObject.Instantiate(src).GetComponent<AxiSoundBase>();
#if UNITY_EDITOR
            Debug.Log($"[AxiSoundPool]实例化新的[{srcname}]");
#endif
            clonego.resourceName = srcname;
            clonego.Seed = GetNextSeed();
        }
        mDictName2LastLoadTime[srcname] = Time.time;
        return clonego;
    }
    static GameObject GetAxiSoundSrcByPath(string path)
    {
        if (!mDictPath2Resource.ContainsKey(path))
            mDictPath2Resource.Add(path, new LoadedAudioSrc(path, false));
        mDictPath2Resource[path].ResetTime();
        return mDictPath2Resource[path].gobj.gameObject;
    }
    public static AxiSoundBase AddSoundForTrans(string path, Transform trans = null)
    {
        AxiSoundBase clonego = AddSound(path);
        Transform target = null;
        if (trans != null)
            target = trans;
        else if (GameManager.instance != null)
        {
            target = Camera.main.transform;// GameObject.Find("Main Camera").transform;
        }

        if (target != null)
        {
            clonego.transform.parent = target;
            clonego.transform.localPosition = Vector3.zero;
            clonego.transform.localEulerAngles = Vector3.zero;
        }
        //go.transform.parent = null;
        return clonego;
    }
    public static AxiSoundBase AddSoundForPosRot(string path, Vector3 targetPos, Quaternion targetRotation)
    {
        AxiSoundBase go = AddSound(path);
        go.transform.position = targetPos;
        go.transform.rotation = targetRotation;
        return go;
    }

    #region 计划废弃
    static AxiSoundBase AddSound(GameObject src)
    {
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        AxiSoundBase go;
        if (!mPool_Name2SoundClone.ContainsKey(src.name))
            mPool_Name2SoundClone[src.name] = new List<AxiSoundBase>();

        if (mPool_Name2SoundClone.ContainsKey(src.name) && mPool_Name2SoundClone[src.name].Count > 0)
        {
            AxiSoundBase sound = mPool_Name2SoundClone[src.name][mPool_Name2SoundClone[src.name].Count - 1];
            mPool_Name2SoundClone[src.name].RemoveAt(mPool_Name2SoundClone[src.name].Count - 1);
            sound.Init();
            go = sound;
            go.gameObject.SetActive(true);
#if UNITY_EDITOR
            Debug.Log($"[AxiSoundPool]出{src.name}池，当前{src.name}池{mPool_Name2SoundClone[src.name].Count}个");
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
    public static AxiSoundBase AddSoundForPosRot(GameObject src, Vector3 targetPos, Quaternion targetRotation)
    {
        AxiSoundBase go = AddSound(src);
        go.transform.position = targetPos;
        go.transform.rotation = targetRotation;
        return go;
    }
    #endregion

    public static void ReleaseBySeed(GameObject src, int Seed)
    {
        if (!mHashSetInPool_Seed.Contains(Seed))
            return;
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        if (!mPool_Name2SoundClone.ContainsKey(src.name))
            return;
        for (int i = mPool_Name2SoundClone[src.name].Count - 1; i >= 0; i++)
        {
            if (mPool_Name2SoundClone[src.name][i].Seed == Seed)
                CheckNeedRemoveFormPool(mPool_Name2SoundClone[src.name][i]);
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
        if (!mPool_Name2SoundClone.ContainsKey(go.resourceName))
            return;
        for (int i = mPool_Name2SoundClone[go.resourceName].Count - 1; i >= 0; i--)
        {
            if (mPool_Name2SoundClone[go.resourceName][i].Seed == go.Seed)
            {
                mPool_Name2SoundClone[go.resourceName].RemoveAt(i);
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
        mPool_Name2SoundClone[go.resourceName].Add(go);
#if UNITY_EDITOR
        Debug.Log($"[AxiSoundPool]入{go.resourceName}池，当前{go.resourceName}池{mPool_Name2SoundClone[go.resourceName].Count}个");
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
            if (sourceiterator.Current.Value.CheckNeedRemove())
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
                continue;

            if (mDictName2LastLoadTime[key] != -1 && Time.time - mDictName2LastLoadTime[key] > soundkeep_time)
            {
                //ReleaseToPool(mPool_Name2Sound, key);
                //ReleaseToPool(mPool_Name2Sound_Inv, key);
                //ReleaseToPool(mPool_Name2Sound_Moan, key);
                //ReleaseToPool(mPool_Name2Sound_Shield, key);  
                ReleaseToPool(mPool_Name2SoundClone, key);
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
