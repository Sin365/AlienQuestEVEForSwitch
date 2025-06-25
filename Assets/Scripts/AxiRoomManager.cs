using UnityEngine;
using System.Linq;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

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

    static Dictionary<int, GameObject> mDicLoadedRoomRes = new Dictionary<int, GameObject>();
    static Dictionary<int, RoomLoadingData> mDictLoadingRoomRes = new Dictionary<int, RoomLoadingData>();

    public static GameObject CloneRoom(int RoomID)
    {
        GameObject src;
        if (mDicLoadedRoomRes.ContainsKey(RoomID))
        {
            src = mDicLoadedRoomRes[RoomID];
            Debug.Log("[AxiRoomManager]使用已加载的RoomID=>" + RoomID);
        }
        else
        {
            src = Resources.Load<GameObject>(GameManager.instance.sm_StageManager.GetRoomResourceID(RoomID));
            //现同步加载的资源，加入已加载
            mDicLoadedRoomRes[RoomID] = src;
            Debug.Log("[AxiRoomManager]直接加载RoomID=>" + RoomID);
        }
        GameObject gameObject = AxiObject.Instantiate(src);
        PreLoadNearRoom(RoomID);
        if (CurrRoomID != RoomID)
        {
            CurrRoomID = RoomID;
            //释放掉已经没有依赖的资源
            Resources.UnloadUnusedAssets();
            System.GC.Collect();
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
                    {
                        Debug.Log("[AxiRoomManager]预加载抛弃RoomID=>" + loading.RoomID);
                    }
                    //如果已经加载，则不要了
                    else if (mDicLoadedRoomRes.ContainsKey(loading.RoomID))
                    {
                        Debug.Log("[AxiRoomManager]预加载重复RoomID=>" + loading.RoomID);
                    }
                    else//保留加载的结果
                    { 
                        mDicLoadedRoomRes[loading.RoomID] = (GameObject)loading.GetLoaded();
                        Debug.Log("[AxiRoomManager]预加载完毕RoomID=>" + loading.RoomID);
                    }
                    loading.Release();
                    break;
            }
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
                Debug.Log("[AxiRoomManager]释放RoomID=>" + roomid);
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
            Debug.Log("[AxiRoomManager]预加载开始RoomID=>" + RoomID);
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

