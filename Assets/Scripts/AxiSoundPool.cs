using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class AxiSoundPool
{
    static Dictionary<string, List<AxiSoundBase>> mPool_Sound = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Sound_Inv = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Sound_Moan = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, List<AxiSoundBase>> mPool_Sound_Shield = new Dictionary<string, List<AxiSoundBase>>();
    static Dictionary<string, float> mDictLastLoadTime = new Dictionary<string, float>();
    static HashSet<long> hashsetInPool = new HashSet<long>();

    static long mSeed = 1;
    const float soundkeep_time = 20f;
    static long GetNextSeed()
    {
        return mSeed++;
    }

    public static Dictionary<string, List<AxiSoundBase>> GetPoolByType(AxiSoundBase src_axi)
    {
        Dictionary<string, List<AxiSoundBase>> dictPool = null;
        if (src_axi is Sound)
            dictPool = mPool_Sound;
        else if (src_axi is Sound_Inv)
            dictPool = mPool_Sound_Inv;
        else if (src_axi is Sound_Moan)
            dictPool = mPool_Sound_Moan;
        else if (src_axi is Sound_Shield)
            dictPool = mPool_Sound_Shield;
        return dictPool;
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
            hashsetInPool.Remove(sound.Seed);
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
        mDictLastLoadTime[src.name] = Time.time;
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
    public static AxiSoundBase AddSoundForTrans(string path, Transform trans = null)
    {
        GameObject src = Resources.Load<GameObject>(path);
        return AddSoundForTrans(src, trans);
    }
    public static AxiSoundBase AddSoundForPosRot(string path, Vector3 targetPos, Quaternion targetRotation)
    {
        GameObject src = Resources.Load<GameObject>(path);
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
        if (!hashsetInPool.Contains(Seed))
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
        if (go.InRelease)
            return;
        if (go.Seed == 0)
            return;
        if (string.IsNullOrEmpty(go.resourceName))
            return;
        if (!hashsetInPool.Contains(go.Seed))
            return;
        hashsetInPool.Remove(go.Seed);
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
        if (hashsetInPool.Contains(go.Seed))
        {
#if UNITY_EDITOR
            Debug.LogError($"[AxiSoundPool] InPool HashSet 已存在,{go.name}");
#endif
            return;
        }
        hashsetInPool.Add(go.Seed);
        Dictionary<string, List<AxiSoundBase>> dictPool = GetPoolByType(go);
        dictPool[go.resourceName].Add(go);
#if UNITY_EDITOR
        Debug.Log($"[AxiSoundPool]入{go.resourceName}池，当前{go.resourceName}池{dictPool[go.resourceName].Count}个");
#endif
    }

    public static void UpdateLogic()
    {
        bool bHad = false;
        foreach (var key in mDictLastLoadTime.Keys.ToArray())//TODO 为什么这里迭代器会被改变？报错，暂时先ToArray
        {
            if (mDictLastLoadTime[key] != -1 && Time.time - mDictLastLoadTime[key] > soundkeep_time)
            {
                ReleaseToPool(mPool_Sound, key);
                ReleaseToPool(mPool_Sound_Inv, key);
                ReleaseToPool(mPool_Sound_Moan, key);
                ReleaseToPool(mPool_Sound_Shield, key);
                mDictLastLoadTime[key] = -1;
                bHad = true;
            }
        }

        if (bHad)
        {
            System.GC.Collect();
        }
    }

    static void ReleaseToPool(Dictionary<string, List<AxiSoundBase>> pool, string key)
    {
        if (!pool.ContainsKey(key))
            return;
        foreach (var item in pool[key])
        {
            item.InRelease = true;
            GameObject.Destroy(item.gameObject);
        }
        pool[key].Clear();
    }
}
