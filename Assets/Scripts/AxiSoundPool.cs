using System.Collections.Generic;
using UnityEngine;

public static class AxiSoundPool
{
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound = new Dictionary<string, Queue<AxiSoundBase>>();
    //Sound_Inv
    static Dictionary<string, Queue<AxiSoundBase>> mPool_Sound_Inv = new Dictionary<string, Queue<AxiSoundBase>>();

    public static void AddSound(GameObject src, Vector3? targetPos = null)
    {
        Dictionary<string, Queue<AxiSoundBase>> dictPool = null;
        AxiSoundBase src_axi = src.GetComponent<AxiSoundBase>();
        if (src_axi is Sound)
        {
            dictPool = mPool_Sound;
        }
        else if (src_axi is Sound_Inv)
        {
            dictPool = mPool_Sound_Inv;
        }

        GameObject go;
        if (dictPool.ContainsKey(src.name) && dictPool[src.name].Count > 0)
        {
            go = dictPool[src.name].Dequeue().gameObject;
        }
        else
        {
            go = (global::UnityEngine.Object.Instantiate(src) as global::UnityEngine.GameObject);
            go.GetComponent<AxiSoundBase>().resourceName = src.name;
        }
        if (targetPos.HasValue)
        {
            go.transform.position = targetPos.Value;
        }
    }

    public static void ReleaseSound(AxiSoundBase go)
    {
        go.gameObject.SetActive(false);
        go.transform.parent = null;
        Dictionary<string, Queue<AxiSoundBase>> dictPool = null;
        if (go is Sound)
        {
            dictPool = mPool_Sound;
        }
        else if (go is Sound_Inv)
        {
            dictPool = mPool_Sound_Inv;
        }
        dictPool[go.resourceName].Enqueue(go);
    }
}
