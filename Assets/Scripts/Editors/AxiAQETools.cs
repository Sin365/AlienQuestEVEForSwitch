#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class AxiAQETools : EditorWindow
{
    static Dictionary<int,List<int>> temp = new Dictionary<int,List<int>>();
    [MenuItem("AxiAQETools/收集房间关联")]
    public static void GetAllRoomTarget()
    {
        temp.Clear();
        string[] prefabGuids = AssetDatabase.FindAssets("t:Prefab");
        foreach (string guid in prefabGuids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            if (!path.Contains("/level"))
                continue;
            GetPrefab(path);
        }
        string str = string.Empty;
        foreach (var roomid in temp.Keys.ToList().OrderBy(w=>w))
        {
            str += "\r\nRoomID:" + roomid + "=>";
            for (int i = 0; i < temp[roomid].Count; i++)
            {
                if(i > 0)
                    str += ",";
                str += temp[roomid][i];
            }
        }
        Debug.Log(str);
    }

    static void GetPrefab(string path)
    {
#if UNITY_4_6
		GameObject prefab = AssetDatabase.LoadAssetAtPath(path,typeof(GameObject)) as GameObject;
#else
        GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);
#endif
        Room_Control room = prefab.GetComponent<Room_Control>();
        if (room == null)
            return;
        LoopPrefabNode(room.Room_Num, path, prefab.gameObject, 0);
    }
    static void LoopPrefabNode(int RoomID, string rootPath, GameObject trans, int depth)
    {
        //		#if UNITY_2018_4_OR_NEWER
        string nodename = rootPath + trans.name;
        GameObject prefabRoot = trans.gameObject;
        Component[] components = prefabRoot.GetComponents<Component>();
        for (int i = 0; i < components.Length; i++)
        {
            var com = components[i];

            if (com == null)
                continue;

            Gate_Control gate = com as Gate_Control;
            if (gate != null)
            {
                if (!temp.ContainsKey(RoomID))
                    temp[RoomID] = new List<int>();
                temp[RoomID].Add(gate.targetRoom_Num);
                continue;
            }

            Gate_Passage passage = com as Gate_Passage;
            if (passage != null)
            {
                if (!temp.ContainsKey(RoomID))
                    temp[RoomID] = new List<int>();
                temp[RoomID].Add(passage.targetRoom_Num);
                continue;
            }
        }

        //遍历
        foreach (Transform child in trans.transform)
            LoopPrefabNode(RoomID, nodename, child.gameObject, depth + 1);
    }

}
#endif