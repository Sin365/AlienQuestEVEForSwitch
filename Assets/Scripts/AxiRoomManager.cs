using System.Collections.Generic;
using UnityEngine;

public static class AxiRoomManager
{
    enum E_RoomLoadingDataState
    {
        None = 0,
        Ready = 1,
        Loading = 2,
        Faild = 3,
        Success = 4,
    }
    static int CurrRoomID = -1;
    static Dictionary<int, List<int>> mDicRoomId2RoomIds = new Dictionary<int, List<int>>()
    { {0,new List<int>(){26,1                }},
    {1,new List<int>(){0,2                 }},
    {2,new List<int>(){1,7,3               }},
    {3,new List<int>(){4,2                 }},
    {4,new List<int>(){3,5                 }},
    {5,new List<int>(){6,34,4,7,53         }},
    {6,new List<int>(){30,5                }},
    {7,new List<int>(){2,8,5,9             }},
    {8,new List<int>(){13,7                }},
    {9,new List<int>(){4,11,7,7,7,10,11    }},
    {10,new List<int>(){9                  }},
    {11,new List<int>(){12,9,12,13,54      }},
    {12,new List<int>(){11,11              }},
    {13,new List<int>(){76,8,14,11         }},
    {14,new List<int>(){23,15,13           }},
    {15,new List<int>(){72,16,14,77        }},
    {16,new List<int>(){17,15              }},
    {17,new List<int>(){18,16              }},
    {18,new List<int>(){19,17              }},
    {19,new List<int>(){20,18              }},
    {20,new List<int>(){24,71,19,21        }},
    {21,new List<int>(){25,20,22           }},
    {22,new List<int>(){21,23              }},
    {23,new List<int>(){22,14              }},
    {24,new List<int>(){20                 }},
    {25,new List<int>(){21,122,26          }},
    {26,new List<int>(){27,25,0            }},
    {27,new List<int>(){26,39,28           }},
    {28,new List<int>(){27,29,31           }},
    {29,new List<int>(){28,30              }},
    {30,new List<int>(){29,6               }},
    {31,new List<int>(){37,28,35,32        }},
    {32,new List<int>(){36,31,33           }},
    {33,new List<int>(){32,34              }},
    {34,new List<int>(){33,5,52            }},
    {35,new List<int>(){31                 }},
    {36,new List<int>(){32,37,51           }},
    {37,new List<int>(){31,38,36,45,49     }},
    {38,new List<int>(){39,37              }},
    {39,new List<int>(){27,40,38           }},
    {40,new List<int>(){41,39,42           }},
    {41,new List<int>(){40,42              }},
    {42,new List<int>(){41,40,45,43        }},
    {43,new List<int>(){45,44,42,46        }},
    {44,new List<int>(){43,43              }},
    {45,new List<int>(){43,42,37           }},
    {46,new List<int>(){43,47              }},
    {47,new List<int>(){46,48              }},
    {48,new List<int>(){47,49              }},
    {49,new List<int>(){48,148,37,50       }},
    {50,new List<int>(){49                 }},
    {51,new List<int>(){36,57              }},
    {52,new List<int>(){34,58              }},
    {53,new List<int>(){5,60               }},
    {54,new List<int>(){11,67              }},
    {55,new List<int>(){76,68              }},
    {56,new List<int>(){78,70              }},
    {57,new List<int>(){58,51,61           }},
    {58,new List<int>(){59,57,52,62        }},
    {59,new List<int>(){60,60,58           }},
    {60,new List<int>(){67,59,59,53,63     }},
    {61,new List<int>(){57,135             }},
    {62,new List<int>(){58,133             }},
    {63,new List<int>(){60,63,60,117       }},
    {64,new List<int>(){67,112             }},
    {65,new List<int>(){68,104             }},
    {66,new List<int>(){70,101             }},
    {67,new List<int>(){68,60,54,64        }},
    {68,new List<int>(){69,67,55,65        }},
    {69,new List<int>(){70,68              }},
    {70,new List<int>(){85,69,56,66        }},
    {71,new List<int>(){20,72,81           }},
    {72,new List<int>(){15,71,73           }},
    {73,new List<int>(){79,72,74           }},
    {74,new List<int>(){76,73,75           }},
    {75,new List<int>(){74                 }},
    {76,new List<int>(){74,13,77,55        }},
    {77,new List<int>(){15,76              }},
    {78,new List<int>(){84,79,56           }},
    {79,new List<int>(){73,80,78           }},
    {80,new List<int>(){81,79              }},
    {81,new List<int>(){95,82,71,80        }},
    {82,new List<int>(){81,83              }},
    {83,new List<int>(){82,84              }},
    {84,new List<int>(){78,83,85           }},
    {85,new List<int>(){94,70,84,86        }},
    {86,new List<int>(){85,87              }},
    {87,new List<int>(){86,88              }},
    {88,new List<int>(){87,89              }},
    {89,new List<int>(){88,96,90,91        }},
    {90,new List<int>(){89                 }},
    {91,new List<int>(){92,89              }},
    {92,new List<int>(){93,91              }},
    {93,new List<int>(){94,92              }},
    {94,new List<int>(){85,93              }},
    {95,new List<int>(){81                 }},
    {96,new List<int>(){101,89,97          }},
    {97,new List<int>(){100,96,98          }},
    {98,new List<int>(){99,97              }},
    {99,new List<int>(){98,100,109         }},
    {100,new List<int>(){97,102,101,99     }},
    {101,new List<int>(){96,66,100         }},
    {102,new List<int>(){103,106,100       }},
    {103,new List<int>(){102,104,105       }},
    {104,new List<int>(){65,103            }},
    {105,new List<int>(){115,103,106,111,113}},
    {106,new List<int>(){102,113,105,107   }},
    {107,new List<int>(){106,108,109       }},
    {108,new List<int>(){114,107,125       }},
    {109,new List<int>(){99,107,110        }},
    {110,new List<int>(){109               }},
    {111,new List<int>(){112,105           }},
    {112,new List<int>(){64,111            }},
    {113,new List<int>(){106,105,114       }},
    {114,new List<int>(){108,113,126       }},
    {115,new List<int>(){105,116           }},
    {116,new List<int>(){117,118,115       }},
    {117,new List<int>(){63,116,127        }},
    {118,new List<int>(){116,119           }},
    {119,new List<int>(){118,120           }},
    {120,new List<int>(){119,121           }},
    {121,new List<int>(){120,123           }},
    {122,new List<int>(){25                }},
    {123,new List<int>(){121,124,128       }},
    {124,new List<int>(){123,126,125       }},
    {125,new List<int>(){108,124           }},
    {126,new List<int>(){114,124           }},
    {127,new List<int>(){132,117           }},
    {128,new List<int>(){123,129,138       }},
    {129,new List<int>(){130,128           }},
    {130,new List<int>(){131,129           }},
    {131,new List<int>(){134,132,130       }},
    {132,new List<int>(){127,133,131       }},
    {133,new List<int>(){62,132            }},
    {134,new List<int>(){131,139,135,136   }},
    {135,new List<int>(){61,134            }},
    {136,new List<int>(){134,137           }},
    {137,new List<int>(){138,136           }},
    {138,new List<int>(){137,128           }},
    {139,new List<int>(){134,140,141       }},
    {140,new List<int>(){139               }},
    {141,new List<int>(){139,142,143       }},
    {142,new List<int>(){141               }},
    {143,new List<int>(){144,141,145,141   }},
    {144,new List<int>(){145,143           }},
    {145,new List<int>(){149,146,143       }},
    {146,new List<int>(){148,145           }},
    {148,new List<int>(){49,146            }},
    {149,new List<int>(){145,150,150       }},
    {150,new List<int>(){149,149           }}, };
    static HashSet<int> NeedLoadRooms = new HashSet<int>();

    static Dictionary<int, RoomLoadedData> mDicLoadedRoomRes = new Dictionary<int, RoomLoadedData>();
    static Dictionary<int, RoomLoadingData> mDictLoadingRoomRes = new Dictionary<int, RoomLoadingData>();

    /// <summary>
    /// 角色切换房间时调用
    /// </summary>
    /// <param name="RoomID"></param>
    /// <returns></returns>
    public static GameObject CloneRoom(int RoomID)
    {
        RoomLoadedData loadedsrc;
        if (mDicLoadedRoomRes.ContainsKey(RoomID))
        {
            loadedsrc = mDicLoadedRoomRes[RoomID];
#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]使用已加载的RoomID=>" + RoomID);
#endif
        }
        else
        {
            GameObject gobj = Resources.Load<GameObject>(GetRoomResourceID(RoomID));
            //现同步加载的资源，加入已加载
            loadedsrc = new RoomLoadedData(RoomID, gobj);
#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]直接加载RoomID=>" + RoomID);
#endif
            mDicLoadedRoomRes[RoomID] = loadedsrc;
            loadedsrc.SetNeed();
        }
        GameObject gameObject = AxiObject.Instantiate(loadedsrc.gobj);
        PreLoadNearRoom(RoomID);
        if (CurrRoomID != RoomID)
        {
            CurrRoomID = RoomID;
        }
        return gameObject;
    }

    static bool bNeedClearDirty = false;
    static float mLastSetClearTime = 0;
    public static void SetClearDirty()
    {
        if (bNeedClearDirty)
            return;
        mLastSetClearTime = Time.time;
        bNeedClearDirty = true;
    }
    static bool CheckClearDirty()
    {
        if (!bNeedClearDirty)
            return false;

        //有正在加载的资源时不处理
        if (mDictLoadingRoomRes.Count > 0)
            return false;

        if (bNeedClearDirty && Time.time - mLastSetClearTime < 10)
            return false;

        //释放掉已经没有依赖的资源
        Resources.UnloadUnusedAssets();
        Debug.Log("[AxiRoomManager] UnloadUnusedAssets!");
#if UNITY_2019_1_OR_NEWER
        System.GC.Collect();
        Debug.Log("[AxiRoomManager] System.GC.Collect()!");
#endif
        bNeedClearDirty = false;
        return true;
    }

    static long lastUpdateCheck = 0;

    static List<int> tempRemoveLoading = new List<int>();
    static List<int> tempRemoveLoaded = new List<int>();
    /// <summary>
    /// 外部MonoBehavior 调用
    /// </summary>
    public static void Update_Logic()
    {
        lastUpdateCheck++;
        if (lastUpdateCheck < 10)
            return;
        lastUpdateCheck = 0;

        if (CheckClearDirty())
            return;

        tempRemoveLoading.Clear();
        var iterator = mDictLoadingRoomRes.GetEnumerator();
        while (iterator.MoveNext())
        {
            RoomLoadingData loading = iterator.Current.Value;
            loading.Update_logic();
            bool bflag_HadStart = false;
            switch (loading.state)
            {
                case E_RoomLoadingDataState.Ready:
                    loading.StartLoad();
                    bflag_HadStart = true;
                    break;
                case E_RoomLoadingDataState.Loading:
                    break;
                case E_RoomLoadingDataState.Faild:
                    {
                        //先移除队列
                        tempRemoveLoading.Add(iterator.Current.Key);
#if UNITY_EDITOR
                        Debug.Log("[AxiRoomManager]Faild RoomID=>" + loading.RoomID);
#endif
                        loading.Release();
                    }
                    break;
                case E_RoomLoadingDataState.Success:
                    {
                        //先移除队列
                        tempRemoveLoading.Add(iterator.Current.Key);
                        //如果不需要
                        if (!NeedLoadRooms.Contains(loading.RoomID))
                        {
#if UNITY_EDITOR
                            Debug.Log("[AxiRoomManager]预加载抛弃RoomID=>" + loading.RoomID);
#endif
                        }
                        //如果已经加载，则不要了
                        else if (mDicLoadedRoomRes.ContainsKey(loading.RoomID))
                        {
#if UNITY_EDITOR
                            Debug.Log("[AxiRoomManager]预加载重复RoomID=>" + loading.RoomID);
#endif
                        }
                        else//保留加载的结果
                        {
                            mDicLoadedRoomRes[loading.RoomID] = new RoomLoadedData(loading.RoomID, (GameObject)loading.GetLoaded());
#if UNITY_EDITOR
                            Debug.Log("[AxiRoomManager]预加载完毕RoomID=>" + loading.RoomID);
#endif
                        }
                        loading.Release();
                    }
                    break;
            }
            if (bflag_HadStart)
                break;
        }
        iterator.Dispose();
        for (int i = 0; i < tempRemoveLoading.Count; i++)
        {
            mDictLoadingRoomRes.Remove(tempRemoveLoading[i]);
        }

        //释放已经加载的，不再需要的Room
        tempRemoveLoaded.Clear();
        var loadediterator = mDicLoadedRoomRes.GetEnumerator();
        while (loadediterator.MoveNext())
        {
            RoomLoadedData loaded = loadediterator.Current.Value;
            if (loaded.CheckCanRelease())
                tempRemoveLoaded.Add(loaded.RoomId);
        }
        loadediterator.Dispose();

        for (int i = 0; i < tempRemoveLoaded.Count; i++)
        {

#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]释放RoomID=>" + tempRemoveLoaded[i]);
#endif
            mDicLoadedRoomRes.Remove(tempRemoveLoaded[i]);
        }

        if (tempRemoveLoaded.Count > 0)
        {

#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]SetClearDirty");
#endif
            SetClearDirty();
        }
    }

    static List<int> temp = new List<int>();
    /// <summary>
    /// 预加载临近房间资源
    /// </summary>
    /// <param name="CenterRoomID"></param>
    static void PreLoadNearRoom(int CenterRoomID)
    {
        temp.Clear();
        NeedLoadRooms.Clear();
        NeedLoadRooms.Add(CenterRoomID);
        for (int i = 0; i < mDicRoomId2RoomIds[CenterRoomID].Count; i++)
        {
            int roomid = mDicRoomId2RoomIds[CenterRoomID][i];
            if (!NeedLoadRooms.Contains(roomid))
                NeedLoadRooms.Add(roomid);
            /*
            //第二层预加载
            for (int j = 0; j < mDicRoomId2RoomIds[roomid].Count; j++)
            {
                int subRoomid = mDicRoomId2RoomIds[roomid][j];
                if (!NeedLoadRooms.Contains(subRoomid))
                    NeedLoadRooms.Add(subRoomid);
            }*/
        }

        //检查新增的需要加载的Room
        foreach (var needloadroomid in NeedLoadRooms)
        {
            if (mDicLoadedRoomRes.ContainsKey(needloadroomid))
            {
                //标记为需要
                mDicLoadedRoomRes[needloadroomid].SetNeed();
                continue;
            }
            if (mDictLoadingRoomRes.ContainsKey(needloadroomid))
                continue;
            mDictLoadingRoomRes[needloadroomid] = new RoomLoadingData(needloadroomid);
        }

        //释放已经加载的，不再需要的Room
        //var keys = mDicLoadedRoomRes.Keys.ToArray();
        //for (int i = 0; i < keys.Length; i++)
        foreach (var loadedroom in mDicLoadedRoomRes)
        {
            int roomid = loadedroom.Key;
            if (!NeedLoadRooms.Contains(roomid))
            {

#if UNITY_EDITOR
                Debug.Log("[AxiRoomManager]标记未使用RoomID=>" + roomid);
#endif
                //释放
                //mDicLoadedRoomRes.Remove(roomid);
                loadedroom.Value.SetUnneed();
            }
        }
    }

    static string[] mRoomPathList { get; set; } = new string[151]
        {
"prefabs/level_1_2/Room_0",
"prefabs/level_1_2/Room_1",
"prefabs/level_1_2/Room_2",
"prefabs/level_1_2/Room_3_N",
"prefabs/level_1_2/Room_4",
"prefabs/level_1_2/Room_5",
"prefabs/level_1_2/Room_6",
"prefabs/level_1_2/Room_7",
"prefabs/level_1_2/Room_8",
"prefabs/level_1_2/Room_9",
"prefabs/level_1_2/Room_10 Save",
"prefabs/level_1_2/Room_11",
"prefabs/level_1_2/Room_12",
"prefabs/level_1_2/Room_13",
"prefabs/level_1_2/Room_14",
"prefabs/level_1_2/Room_15",
"prefabs/level_1_2/Room_16 Save",
"prefabs/level_1_2/Room_17",
"prefabs/level_1_2/Room_18 Boss_1",
"prefabs/level_1_2/Room_19",
"prefabs/level_1_2/Room_20",
"prefabs/level_1_2/Room_21",
"prefabs/level_1_2/Room_22",
"prefabs/level_1_2/Room_23",
"prefabs/level_1_2/Room_24 T",
"prefabs/level_1_2/Room_25",
"prefabs/level_1_2/Room_26",
"prefabs/level_1_2/Room_27",
"prefabs/level_1_2/Room_28",
"prefabs/level_1_2/Room_29",
"prefabs/level_1_2/Room_30",
"prefabs/level_1_2/Room_31",
"prefabs/level_1_2/Room_32",
"prefabs/level_1_2/Room_33",
"prefabs/level_1_2/Room_34",
"prefabs/level_1_2/Room_35 Save",
"prefabs/level_1_2/Room_36",
"prefabs/level_1_2/Room_37",
"prefabs/level_1_2/Room_38",
"prefabs/level_1_2/Room_39",
"prefabs/level_1_2/Room_40",
"prefabs/level_1_2/Room_41",
"prefabs/level_1_2/Room_42",
"prefabs/level_1_2/Room_43",
"prefabs/level_1_2/Room_44 T",
"prefabs/level_1_2/Room_45",
"prefabs/level_1_2/Room_46 Save",
"prefabs/level_1_2/Room_47",
"prefabs/level_1_2/Room_48 Boss_2",
"prefabs/level_1_2/Room_49",
"prefabs/level_1_2/Room_50",
"prefabs/level_3_c/Room_51",
"prefabs/level_3_c/Room_52",
"prefabs/level_3_c/Room_53",
"prefabs/level_3_c/Room_54",
"prefabs/level_3_c/Room_55",
"prefabs/level_3_c/Room_56",
"prefabs/level_3_c/Room_57",
"prefabs/level_3_c/Room_58",
"prefabs/level_3_c/Room_59",
"prefabs/level_3_c/Room_60 ST",
"prefabs/level_3_c/Room_61",
"prefabs/level_3_c/Room_62",
"prefabs/level_3_c/Room_63",
"prefabs/level_3_c/Room_64",
"prefabs/level_3_c/Room_65",
"prefabs/level_3_c/Room_66",
"prefabs/level_3_c/Room_67",
"prefabs/level_3_c/Room_68",
"prefabs/level_3_c/Room_69",
"prefabs/level_3_c/Room_70",
"prefabs/level_3_c/Room_71",
"prefabs/level_3_c/Room_72",
"prefabs/level_3_c/Room_73",
"prefabs/level_3_c/Room_74",
"prefabs/level_3_c/Room_75 Save",
"prefabs/level_3_c/Room_76",
"prefabs/level_3_c/Room_77",
"prefabs/level_3_c/Room_78",
"prefabs/level_3_c/Room_79",
"prefabs/level_3_c/Room_80",
"prefabs/level_3_c/Room_81",
"prefabs/level_3_c/Room_82",
"prefabs/level_3_c/Room_83",
"prefabs/level_3_c/Room_84",
"prefabs/level_3_c/Room_85",
"prefabs/level_3_c/Room_86 Save",
"prefabs/level_3_c/Room_87",
"prefabs/level_3_c/Room_88 Boss_3",
"prefabs/level_3_c/Room_89",
"prefabs/level_3_c/Room_90 T",
"prefabs/level_3_c/Room_91",
"prefabs/level_3_c/Room_92 EVE",
"prefabs/level_3_c/Room_93",
"prefabs/level_3_c/Room_94",
"prefabs/level_3_c/Room_95 Save",
"prefabs/level_4_5/Room_96",
"prefabs/level_4_5/Room_97",
"prefabs/level_4_5/Room_98",
"prefabs/level_4_5/Room_99",
"prefabs/level_4_5/Room_100",
"prefabs/level_4_5/Room_101",
"prefabs/level_4_5/Room_102",
"prefabs/level_4_5/Room_103",
"prefabs/level_4_5/Room_104",
"prefabs/level_4_5/Room_105",
"prefabs/level_4_5/Room_106",
"prefabs/level_4_5/Room_107",
"prefabs/level_4_5/Room_108",
"prefabs/level_4_5/Room_109",
"prefabs/level_4_5/Room_110 Save",
"prefabs/level_4_5/Room_111",
"prefabs/level_4_5/Room_112",
"prefabs/level_4_5/Room_113",
"prefabs/level_4_5/Room_114",
"prefabs/level_4_5/Room_115",
"prefabs/level_4_5/Room_116",
"prefabs/level_4_5/Room_117",
"prefabs/level_4_5/Room_118",
"prefabs/level_4_5/Room_119 Save",
"prefabs/level_4_5/Room_120",
"prefabs/level_4_5/Room_121 Boss_4",
"prefabs/level_4_5/Room_122",
"prefabs/level_4_5/Room_123",
"prefabs/level_4_5/Room_124",
"prefabs/level_4_5/Room_125 T",
"prefabs/level_4_5/Room_126",
"prefabs/level_4_5/Room_127",
"prefabs/level_4_5/Room_128",
"prefabs/level_4_5/Room_129",
"prefabs/level_4_5/Room_130",
"prefabs/level_4_5/Room_131",
"prefabs/level_4_5/Room_132",
"prefabs/level_4_5/Room_133",
"prefabs/level_4_5/Room_134",
"prefabs/level_4_5/Room_135",
"prefabs/level_4_5/Room_136",
"prefabs/level_4_5/Room_137",
"prefabs/level_4_5/Room_138",
"prefabs/level_4_5/Room_139",
"prefabs/level_4_5/Room_140 Save",
"prefabs/level_4_5/Room_141",
"prefabs/level_4_5/Room_142 T",
"prefabs/level_4_5/Room_143_N",
"prefabs/level_4_5/Room_143_N",
"prefabs/level_4_5/Room_145",
"prefabs/level_4_5/Room_146",
"prefabs/level_4_5/Room_146",
"prefabs/level_4_5/Room_148",
"prefabs/level_4_5/Room_149",
"prefabs/level_4_5/Room_150 Queen",
        };

    static string GetRoomResourceID(int Roomid)
    {
        return mRoomPathList[Roomid];
    }

    public static int RoomPathListCount => mRoomPathList.Length;
    class RoomLoadingData
    {
        public int RoomID;
        ResourceRequest resourceRequest;
        public E_RoomLoadingDataState state;
        public RoomLoadingData(int RoomID)
        {
            this.RoomID = RoomID;
            state = E_RoomLoadingDataState.Ready;
            resourceRequest = Resources.LoadAsync<GameObject>(GetRoomResourceID(RoomID));
#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]Ready load Data RoomID=>" + RoomID);
#endif
        }
        public void StartLoad()
        {
            resourceRequest = Resources.LoadAsync<GameObject>(GetRoomResourceID(RoomID));
#if UNITY_EDITOR
            Debug.Log("[AxiRoomManager]预加载开始RoomID=>" + RoomID);
#endif
            state = E_RoomLoadingDataState.Loading;
        }

        public void Update_logic()
        {
            if (state < E_RoomLoadingDataState.Loading)
                return;

            if (!resourceRequest.isDone)
                return;

            if (resourceRequest.isDone)
            {
                if (resourceRequest.asset == null)
                    state = E_RoomLoadingDataState.Faild;
                else
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

    class RoomLoadedData
    {
        public bool Unneed { get; private set; }
        public float UnneedTime { get; private set; }
        public int RoomId { get; private set; }
        public GameObject gobj { get; private set; }
        public RoomLoadedData(int roomid, GameObject go)
        {
            RoomId = roomid;
            gobj = go;
            SetNeed();
        }

        public void SetUnneed()
        {
            Unneed = true;
            UnneedTime = Time.time;
        }

        public void SetNeed()
        {
            if (Unneed == true)
            {
#if UNITY_EDITOR
                Debug.Log("[AxiRoomManager]标记重新使用RoomID=>" + RoomId);
#endif
            }
            Unneed = false;
            UnneedTime = 0f;
        }

        public bool CheckCanRelease()
        {
            if (Unneed && Time.time - UnneedTime > 10f)
            {
                gobj = null;
                Unneed = false;
                UnneedTime = -1;
                return true;
            }
            return false;
        }
    }
}

