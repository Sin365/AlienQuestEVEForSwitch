using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class Map_Control : MonoBehaviour
{
    private Vector3 pos_Center;

    public Vector2 pos_Cursor;

    private bool cursor_UP;

    private bool cursor_Down;

    private float inputX;

    private float prevX;

    private float inputY;

    private float prevY;

    private float Life_Timer;

    private float SelCursor_Timer;

    private float SelCursor_Size = 1f;

    private GameObject map_Cursor;

    private GameObject minimap_Cursor;

    private GameObject map_CursorBox;

    GameManager GM => GameManager.instance;

    RectTransform MiniMap_SaveFont_Rect;
    RectTransform MiniMap_TeleportFont_Rect;
    public Dictionary<int, GameObject> dictMapPos = new Dictionary<int, GameObject>();
    public Dictionary<int, SpriteRenderer> dictMapPosSp = new Dictionary<int, SpriteRenderer>();
    public Dictionary<int, RectTransform> dictMapPosRect = new Dictionary<int, RectTransform>();
    public Dictionary<int, SpriteRenderer> dictMapPosMap_BorderSP = new Dictionary<int, SpriteRenderer>();
    public Dictionary<int, RectTransform> dictMapPosMap_BorderRect = new Dictionary<int, RectTransform>();
    public Dictionary<int, UnityEngine.UI.Text> dictMapSaveFont_Text = new Dictionary<int, UnityEngine.UI.Text>();

    public Dictionary<int, GameObject> dictMiniMapPos = new Dictionary<int, GameObject>();
    public Dictionary<int, SpriteRenderer> dictMiniMapPosSp = new Dictionary<int, SpriteRenderer>();
    public Dictionary<int, RectTransform> dictMiniMapPosRect = new Dictionary<int, RectTransform>();
    public Dictionary<int, SpriteRenderer> dictMiniMapPosMap_BorderSP = new Dictionary<int, SpriteRenderer>();
    public Dictionary<int, RectTransform> dictMiniMapPosMap_BorderRect = new Dictionary<int, RectTransform>();

    private void Start()
    {
        PreLoadAllMapObj();
        //GM = GameObject.Find("GameManager").GetComponent<GameManager>();
        map_Cursor = GameObject.Find("MapPos_Cursor");
        minimap_Cursor = GameObject.Find("MiniMap_Cursor");
        map_CursorBox = GameObject.Find("MapPos_CursorBox");
        pos_Center = new Vector3(0f, 0f, 0f);
        Reset_MiniMap();
        Reset_PosToCursor();
    }

    void PreLoadAllMapObj()
    {
        MiniMap_SaveFont_Rect = GameObject.Find("MiniMap_SaveFont").GetComponent<RectTransform>();
        MiniMap_TeleportFont_Rect = GameObject.Find("MiniMap_TeleportFont").GetComponent<RectTransform>();
        dictMapPos.Clear();
        dictMapPosSp.Clear();
        dictMapPosRect.Clear();
        dictMapPosMap_BorderSP.Clear();
        dictMapPosMap_BorderRect.Clear();
        dictMapSaveFont_Text.Clear();
        Transform trans = this.transform;
        int count = trans.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform gobj = trans.GetChild(i);
            if (!gobj.name.Contains("MapPos_"))
                continue;
            string[] temp = gobj.name.Split("_");
            if (temp.Length != 3)
                continue;
            int key = GetPosKey(Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]));
            dictMapPos[key] = gobj.gameObject;
            dictMapPosSp[key] = gobj.GetComponent<SpriteRenderer>();
            dictMapPosRect[key] = gobj.GetComponent<RectTransform>();
            int count_sub = gobj.transform.childCount;
            for (int j = 0; j < count_sub; j++)
            {
                Transform sub_gobj = gobj.transform.GetChild(j);
                if (sub_gobj.name.Contains("MapBorder_"))
                {
                    string[] tempborder = sub_gobj.name.Split("_");
                    if (tempborder.Length != 3)
                        continue;
                    int keysub = GetPosKey(Convert.ToInt32(tempborder[1]), Convert.ToInt32(tempborder[2]));
                    dictMapPosMap_BorderSP[keysub] = sub_gobj.GetComponent<SpriteRenderer>();
                    dictMapPosMap_BorderRect[keysub] = sub_gobj.GetComponent<RectTransform>();
                }
                else if (sub_gobj.name.Contains("MapSaveFont_"))
                {
                    string[] tempborder = sub_gobj.name.Split("_");
                    if (tempborder.Length != 3)
                        continue;
                    int keysub = GetPosKey(Convert.ToInt32(tempborder[1]), Convert.ToInt32(tempborder[2]));
                    dictMapSaveFont_Text[keysub] = sub_gobj.GetComponent<UnityEngine.UI.Text>();
                }
            }
        }

        dictMiniMapPos.Clear();
        dictMiniMapPosSp.Clear();
        dictMiniMapPosRect.Clear();
        dictMiniMapPosMap_BorderSP.Clear();
        dictMiniMapPosMap_BorderRect.Clear();
        trans = trans.parent.Find("MiniMap");
        count = trans.childCount;
        for (int i = 0; i < count; i++)
        {
            Transform gobj = trans.GetChild(i);
            if (!gobj.name.Contains("MiniMap_"))
                continue;
            string[] temp = gobj.name.Split("_");
            if (temp.Length != 3)
                continue;
            int key = GetPosKey(Convert.ToInt32(temp[1]), Convert.ToInt32(temp[2]));
            dictMiniMapPos[key] = gobj.gameObject;
            dictMiniMapPosSp[key] = gobj.GetComponent<SpriteRenderer>();
            dictMiniMapPosRect[key] = gobj.GetComponent<RectTransform>();
            int count_sub = gobj.transform.childCount;
            for (int j = 0; j < count_sub; j++)
            {
                Transform sub_gobj = gobj.transform.GetChild(j);
                if (!sub_gobj.name.Contains("MiniMapBorder_"))
                    continue;
                string[] tempborder = sub_gobj.name.Split("_");
                if (tempborder.Length != 3)
                    continue;
                int keysub = GetPosKey(Convert.ToInt32(tempborder[1]), Convert.ToInt32(tempborder[2]));
                dictMiniMapPosMap_BorderSP[keysub] = sub_gobj.GetComponent<SpriteRenderer>();
                dictMiniMapPosMap_BorderRect[keysub] = sub_gobj.GetComponent<RectTransform>();
            }
        }
    }
    public static int GetPosKey(int item1, int item2)
    {
        return 100000000 + (item1 * 1000 + item2);
    }

    private void Reset_PosToCursor()
    {
        pos_Center = map_Cursor.GetComponent<RectTransform>().localPosition;
        if (pos_Center.x < -1650f)
        {
            pos_Center.x = 1650f;
        }
        else if (pos_Center.x > 1650f)
        {
            pos_Center.x = -1650f;
        }
        else
        {
            pos_Center.x = 0f - pos_Center.x;
        }
        if (pos_Center.y < -630f)
        {
            pos_Center.y = 630f;
        }
        else if (pos_Center.y > 630f)
        {
            pos_Center.y = -630f;
        }
        else
        {
            pos_Center.y = 0f - pos_Center.y;
        }
    }

    //private void Update()
    private void Update_Logic()
    {
        if (GM.onEvent || GM.GameOver)
        {
            return;
        }
        if (GM.Paused)
        {
            if (GM.onMap)
            {
                Life_Timer += Time.deltaTime;
                inputX = 0f;
                inputY = 0f;
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    inputX = 1f;
                }
                else if (Input.GetKey(KeyCode.LeftArrow))
                {
                    inputX = -1f;
                }
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    inputY = 1f;
                }
                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    inputY = -1f;
                }
                if (Input.GetAxis("L_X") != 0f)
                {
                    inputX = Input.GetAxis("L_X");
                }
                if (Input.GetAxis("L_Y") != 0f)
                {
                    inputY = Input.GetAxis("L_Y");
                }
                if ((inputX > 0f && pos_Center.x > -1650f) || (inputX < 0f && pos_Center.x < 1650f))
                {
                    pos_Center.x += 800f * (0f - inputX) * Time.deltaTime;
                }
                if ((inputY > 0f && pos_Center.y > -630f) || (inputY < 0f && pos_Center.y < 630f))
                {
                    pos_Center.y += 800f * (0f - inputY) * Time.deltaTime;
                }
                GetComponent<RectTransform>().localPosition = Vector3.Lerp(GetComponent<RectTransform>().localPosition, pos_Center, Time.deltaTime * 3f);
                map_CursorBox.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.6f + Mathf.Sin(Life_Timer * 5f) * 0.4f);
                SelCursor_Timer += Time.deltaTime;
                SelCursor_Size = 1f + (1f + Mathf.Sin(SelCursor_Timer * 10f)) * 0.05f;
                GameObject.Find("Ellen_MapCursor").GetComponent<RectTransform>().localScale = new Vector3(SelCursor_Size, SelCursor_Size, 1f);
                if (GM.EventState == 200)
                {
                    GameObject.Find("Mission_4").GetComponent<RectTransform>().localPosition = new Vector3(-1050f, 170f + Mathf.Sin(SelCursor_Timer * 8f) * 3f, 1f);
                }
                if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Start") || Input.GetButtonDown("_B") || Input.GetButtonDown("Back"))
                {
                    GM.Game_Resume();
                    GetComponent<RectTransform>().localPosition = new Vector3(-2500f, 2500f, 0f);
                    GameObject.Find("MissionBriefing").SendMessage("Hide_BriefingPos");
                }
            }
        }
        else if (!GM.onMenu && !GM.onGatePass && !GM.onSave)
        {
            Life_Timer += Time.deltaTime;
            minimap_Cursor.GetComponent<SpriteRenderer>().color = new Color(0.6f, 1f, 1f, 0.6f + Mathf.Sin(Life_Timer * 5f) * 0.4f);
            //if (GameObject.Find("MapPos_" + pos_Cursor.x + "_" + pos_Cursor.y) != null)
            int mapKey = GetPosKey((int)pos_Cursor.x, (int)pos_Cursor.y);
            if (dictMapPos[mapKey] != null)
            {
                map_Cursor.GetComponent<RectTransform>().localPosition = dictMapPosRect[mapKey].localPosition;
                // GameObject.Find("MapPos_" + pos_Cursor.x + "_" + pos_Cursor.y).GetComponent<RectTransform>().localPosition;
            }
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Back"))
            {
                GM.onMap = true;
                cursor_UP = false;
                cursor_Down = false;
                GM.Game_Pause();
                GetComponent<RectTransform>().localPosition = pos_Center;
                Reset_PosToCursor();
                GameObject.Find("MissionBriefing").SendMessage("Set_BriefingPos_Map");
                GameObject.Find("Menu").SendMessage("Sound_MapOn");
            }
        }
    }

    /// <summary>
    /// 隐藏地图
    /// </summary>
    public void Axi_HideMap()
    {
        GM.onMap = false;
        this.gameObject.SetActive(false);
    }

    public void Update_GameManager()
    {
        if (GM.onEvent || GM.GameOver)
            return;

        if (GM.Paused)
        {
            if (GM.onMap)
            {
            }
        }
        else if (!GM.onMenu && !GM.onGatePass && !GM.onSave)
        {
            if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Back"))
            {
                this.gameObject.SetActive(true);
            }
        }

        if (this.gameObject.activeSelf == true)
            Update_Logic();
    }

    private void Reset_MiniMap()
    {
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                //GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<SpriteRenderer>().enabled = false;
                //GameObject.Find("MiniMapBorder_" + i + "_" + j).GetComponent<SpriteRenderer>().enabled = false;
                int mapPosKey = GetPosKey(i, j);
                dictMiniMapPosSp[mapPosKey].enabled = false;
                dictMiniMapPosMap_BorderSP[mapPosKey].enabled = false;
            }
        }
    }

    public void Change_MiniMap()
    {
        //string text = "--";

        //string text_border = text;
        //string MinimapBorder = text;
        bool flag = false;
        bool flag2 = false;
        for (int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                int MapPosKey = GetPosKey((int)(pos_Cursor.x - (float)(2 - i)), (int)(pos_Cursor.y + (float)(1 - j)));
                int MinimapPosKey = GetPosKey(i, j);
                //text = "MapPos_" + (pos_Cursor.x - (float)(2 - i)) + "_" + (pos_Cursor.y + (float)(1 - j));
                //text_border = "MapBorder_" + (pos_Cursor.x - (float)(2 - i)) + "_" + (pos_Cursor.y + (float)(1 - j));
                //MinimapBorder = "MiniMapBorder_" + i + "_" + j;
                //if (GameObject.Find(text) != null && GameObject.Find(text).GetComponent<SpriteRenderer>().enabled)
                if (dictMapPos.ContainsKey(MapPosKey) && dictMapPos[MapPosKey] != null && dictMapPosSp[MapPosKey].enabled)
                {
                    //GameObject.Find(MinimapBorder).GetComponent<SpriteRenderer>().sprite = GameObject.Find(text_border).GetComponent<SpriteRenderer>().sprite;
                    dictMiniMapPosMap_BorderSP[MinimapPosKey].sprite = dictMapPosMap_BorderSP[MapPosKey].sprite;
                    //GameObject.Find(MinimapBorder).GetComponent<RectTransform>().localRotation = GameObject.Find(text_border).GetComponent<RectTransform>().localRotation;
                    dictMiniMapPosMap_BorderRect[MinimapPosKey].localRotation = dictMapPosMap_BorderRect[MapPosKey].localRotation;
                    //GameObject.Find(MinimapBorder).GetComponent<RectTransform>().localScale = GameObject.Find(text_border).GetComponent<RectTransform>().localScale;
                    dictMiniMapPosMap_BorderRect[MinimapPosKey].localScale = dictMapPosMap_BorderRect[MapPosKey].localScale;
                    //GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<SpriteRenderer>().enabled = true;
                    dictMiniMapPosSp[MinimapPosKey].enabled = true;
                    //GameObject.Find(MinimapBorder).GetComponent<SpriteRenderer>().enabled = true;
                    dictMiniMapPosMap_BorderSP[MinimapPosKey].enabled = true;
                    //if (GameObject.Find(text).GetComponent<SpriteRenderer>().sortingOrder == 85)
                    if (dictMapPosSp[MapPosKey].sortingOrder == 85)
                    {
                        //GameObject.Find("MiniMap_SaveFont").GetComponent<RectTransform>().localPosition = GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<RectTransform>().localPosition;
                        MiniMap_SaveFont_Rect.localPosition = dictMiniMapPosRect[MinimapPosKey].localPosition;
                        flag = true;
                    }
                    //if (GameObject.Find(text).GetComponent<SpriteRenderer>().sortingOrder == 86)
                    if (dictMapPosSp[MapPosKey].sortingOrder == 86)
                    {
                        //GameObject.Find("MiniMap_TeleportFont").GetComponent<RectTransform>().localPosition = GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<RectTransform>().localPosition;
                        MiniMap_TeleportFont_Rect.localPosition = dictMiniMapPosRect[MinimapPosKey].localPosition;
                        flag2 = true;
                    }
                }
                else
                {
                    //GameObject.Find("MiniMap_" + i + "_" + j).GetComponent<SpriteRenderer>().enabled = false;
                    dictMiniMapPosSp[MinimapPosKey].enabled = false;
                    //GameObject.Find(MinimapBorder).GetComponent<SpriteRenderer>().enabled = false;
                    dictMiniMapPosMap_BorderSP[MinimapPosKey].enabled = false;
                }
            }
        }
        if (flag)
        {
            //GameObject.Find("MiniMap_SaveFont").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            MiniMap_SaveFont_Rect.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else
        {
            //GameObject.Find("MiniMap_SaveFont").GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
            MiniMap_SaveFont_Rect.localScale = new Vector3(0f, 0f, 0f);
        }
        if (flag2)
        {
            //GameObject.Find("MiniMap_TeleportFont").GetComponent<RectTransform>().localScale = new Vector3(0.5f, 0.5f, 1f);
            MiniMap_TeleportFont_Rect.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else
        {
            //GameObject.Find("MiniMap_TeleportFont").GetComponent<RectTransform>().localScale = new Vector3(0f, 0f, 0f);
            MiniMap_TeleportFont_Rect.localScale = new Vector3(0f, 0f, 0f);
        }
    }
}
