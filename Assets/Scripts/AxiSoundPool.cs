using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public static class AxiSoundPool
{
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound = new Dictionary<string, Queue<AxiSoundBase>>();
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound_Inv = new Dictionary<string, Queue<AxiSoundBase>>();
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound_Moan = new Dictionary<string, Queue<AxiSoundBase>>();
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound_Shield = new Dictionary<string, Queue<AxiSoundBase>>();
    static HashSet<long> hashsetInPool = new HashSet<long>();

    static long mSeed = 1;

    static long GetNextSeed()
    {
        return mSeed++;
    }

    public static Dictionary<string, Queue<AxiSoundBase>> GetPoolByType(AxiSoundBase src_axi)
    {
        Dictionary<string, Queue<AxiSoundBase>> dictPool = null;
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

    public static void AddSoundForTrans(GameObject src, Transform trans = null)
    {
        GameObject go = AddSound(src);

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
    }

    public static void AddSoundForPosRot(GameObject src, Vector3 targetPos, Quaternion targetRotation)
    {
        GameObject go = AddSound(src);
        go.transform.position = targetPos;
        go.transform.rotation = targetRotation;
    }

    static GameObject AddSound(GameObject src)
    {
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        Dictionary<string, Queue<AxiSoundBase>> dictPool = GetPoolByType(src_axi);

        GameObject go;

        if (!dictPool.ContainsKey(src.name))
            dictPool[src.name] = new Queue<AxiSoundBase>();

        if (dictPool.ContainsKey(src.name) && dictPool[src.name].Count > 0)
        {
            AxiSoundBase sound = dictPool[src.name].Dequeue();
            sound.Init();
            go = sound.gameObject;
            go.SetActive(true);
            Debug.Log($"[AxiSoundPool]出{go.name}池，当前{src.name}池{dictPool[src.name].Count}个");
            hashsetInPool.Remove(sound.Seed);
        }
        else
        {
            go = (global::UnityEngine.Object.Instantiate(src) as global::UnityEngine.GameObject);
            Debug.Log($"[AxiSoundPool]实例化新的[{src.name}]");
            go.GetComponent<AxiSoundBase>().resourceName = src.name;
            go.GetComponent<AxiSoundBase>().Seed = GetNextSeed();
        }

        return go;
    }

    public static void ReleaseSound(AxiSoundBase go)
    {
        if (hashsetInPool.Contains(go.Seed))
        {
            Debug.LogError($"[AxiSoundPool] InPool HashSet 已存在,{go.name}");
            return;
        }
        hashsetInPool.Add(go.Seed);
        go.gameObject.SetActive(false);
        go.transform.parent = null;
        Dictionary<string, Queue<AxiSoundBase>> dictPool = GetPoolByType(go);
        if (string.IsNullOrEmpty(go.resourceName))
        {
            Debug.LogError($"[AxiSoundPool] go.resourceName 为空,{go.name}");
            global::UnityEngine.Object.Destroy(go.gameObject);
            return;
        }
        dictPool[go.resourceName].Enqueue(go);
        Debug.Log($"[AxiSoundPool]入{go.resourceName}池，当前{go.resourceName}池{dictPool[go.resourceName].Count}个");
    }
}
