public class StageManager : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Room;

	private global::UnityEngine.Vector3 Target_Pos;

	public global::UnityEngine.GameObject Current_Room;

	public global::UnityEngine.GameObject _Lift_Reactor;

	private global::UnityEngine.GameObject Current_Lift;

	private float RoomCam_L;

	private float RoomCam_R;

	private float RoomCam_Top;

	private float RoomCam_Bot;

	private global::UnityEngine.Vector2 PrevCursor = new global::UnityEngine.Vector2(0f, 0f);

	private int pos_Num;

	private float dist_X;

	private float dist_Y;

	private bool isFadeIn;

	private bool isFadeOut;

	private float FadeOpacity;

	private float Fade_Timer;

	private global::UnityEngine.GameObject GateFade;

	private bool isMapChanged;

	private bool isCheatRoom;

	private global::UnityEngine.Vector2 player_Velocity;

    GameManager GM => GameManager.instance;

    private global::UnityEngine.GameObject Player;

	private Map_Control MC;

	private int[] MapArray_Num = new int[200]
	{
		6, 2, 4, 3, 1, 6, 4, 6, 3, 3,
		1, 8, 2, 7, 7, 6, 1, 2, 2, 2,
		5, 7, 7, 3, 1, 3, 8, 6, 8, 5,
		4, 6, 8, 4, 7, 1, 8, 8, 8, 6,
		6, 6, 6, 6, 1, 6, 1, 2, 2, 8,
		1, 5, 2, 4, 3, 4, 4, 6, 3, 9,
		9, 5, 2, 4, 3, 4, 4, 3, 6, 3,
		6, 7, 9, 7, 6, 1, 8, 4, 8, 5,
		8, 6, 5, 9, 9, 9, 1, 2, 4, 7,
		1, 2, 4, 2, 3, 1, 7, 6, 3, 8,
		7, 8, 8, 7, 9, 8, 7, 9, 5, 6,
		1, 9, 6, 8, 8, 5, 9, 9, 6, 1,
		2, 6, 3, 9, 5, 1, 6, 3, 8, 8,
		6, 7, 8, 8, 7, 6, 8, 9, 3, 6,
		1, 5, 1, 8, 0, 5, 8, 0, 8, 3,
		6, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0,
		0, 0, 0, 0, 0, 0, 0, 0, 0, 0
	};

	private int[,,] MapArray_Pos = new int[151, 9, 2]
	{
		{
			{ 8, 18 },
			{ 9, 18 },
			{ 10, 18 },
			{ 8, 19 },
			{ 9, 19 },
			{ 10, 19 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 11, 18 },
			{ 12, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 13, 18 },
			{ 14, 18 },
			{ 13, 17 },
			{ 14, 17 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 17 },
			{ 16, 17 },
			{ 17, 17 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 18, 17 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 19, 16 },
			{ 20, 16 },
			{ 19, 17 },
			{ 19, 18 },
			{ 20, 18 },
			{ 20, 17 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 17, 15 },
			{ 18, 15 },
			{ 17, 16 },
			{ 18, 16 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 18 },
			{ 16, 19 },
			{ 16, 18 },
			{ 17, 19 },
			{ 17, 18 },
			{ 18, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 21 },
			{ 15, 20 },
			{ 15, 19 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 18, 19 },
			{ 18, 20 },
			{ 19, 20 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 17, 20 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 20, 20 },
			{ 21, 20 },
			{ 22, 20 },
			{ 21, 21 },
			{ 20, 22 },
			{ 21, 22 },
			{ 22, 22 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 23, 20 },
			{ 23, 21 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 22 },
			{ 16, 22 },
			{ 17, 22 },
			{ 18, 22 },
			{ 19, 22 },
			{ 17, 23 },
			{ 18, 23 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 11, 22 },
			{ 12, 22 },
			{ 13, 22 },
			{ 14, 22 },
			{ 11, 23 },
			{ 12, 23 },
			{ 13, 23 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 10, 23 },
			{ 8, 24 },
			{ 9, 24 },
			{ 10, 24 },
			{ 9, 25 },
			{ 10, 25 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 7, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 5, 24 },
			{ 6, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 3, 24 },
			{ 4, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 2, 24 },
			{ 2, 23 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 1, 22 },
			{ 1, 23 },
			{ 1, 24 },
			{ 1, 25 },
			{ 1, 26 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 2, 22 },
			{ 2, 21 },
			{ 2, 20 },
			{ 2, 19 },
			{ 3, 21 },
			{ 3, 20 },
			{ 3, 19 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 4, 21 },
			{ 5, 21 },
			{ 6, 21 },
			{ 7, 21 },
			{ 5, 22 },
			{ 6, 22 },
			{ 7, 22 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 8, 22 },
			{ 9, 22 },
			{ 10, 22 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 0, 26 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 1, 18 },
			{ 2, 18 },
			{ 3, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 4, 18 },
			{ 5, 19 },
			{ 5, 18 },
			{ 5, 17 },
			{ 6, 19 },
			{ 6, 18 },
			{ 6, 17 },
			{ 7, 18 },
			{ 0, 0 }
		},
		{
			{ 5, 16 },
			{ 5, 15 },
			{ 5, 14 },
			{ 6, 16 },
			{ 6, 15 },
			{ 7, 15 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 8, 15 },
			{ 9, 15 },
			{ 8, 14 },
			{ 9, 14 },
			{ 8, 13 },
			{ 9, 13 },
			{ 10, 13 },
			{ 11, 13 },
			{ 0, 0 }
		},
		{
			{ 10, 14 },
			{ 11, 14 },
			{ 12, 14 },
			{ 11, 15 },
			{ 12, 15 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 13, 15 },
			{ 14, 15 },
			{ 15, 15 },
			{ 16, 15 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 12, 13 },
			{ 13, 13 },
			{ 14, 13 },
			{ 12, 12 },
			{ 13, 12 },
			{ 14, 12 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 13 },
			{ 16, 13 },
			{ 17, 13 },
			{ 18, 13 },
			{ 19, 13 },
			{ 20, 13 },
			{ 17, 12 },
			{ 18, 12 },
			{ 0, 0 }
		},
		{
			{ 21, 13 },
			{ 22, 13 },
			{ 21, 12 },
			{ 22, 12 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 21, 16 },
			{ 22, 16 },
			{ 23, 16 },
			{ 23, 15 },
			{ 23, 14 },
			{ 23, 13 },
			{ 23, 12 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 11, 12 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 16, 10 },
			{ 17, 10 },
			{ 18, 10 },
			{ 19, 10 },
			{ 16, 11 },
			{ 17, 11 },
			{ 18, 11 },
			{ 19, 11 },
			{ 0, 0 }
		},
		{
			{ 13, 11 },
			{ 11, 10 },
			{ 12, 10 },
			{ 13, 10 },
			{ 14, 10 },
			{ 15, 10 },
			{ 13, 9 },
			{ 13, 8 },
			{ 0, 0 }
		},
		{
			{ 6, 10 },
			{ 7, 10 },
			{ 8, 10 },
			{ 9, 10 },
			{ 10, 10 },
			{ 7, 11 },
			{ 8, 11 },
			{ 9, 11 },
			{ 0, 0 }
		},
		{
			{ 4, 10 },
			{ 5, 10 },
			{ 4, 11 },
			{ 5, 11 },
			{ 5, 12 },
			{ 5, 13 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 2, 10 },
			{ 3, 10 },
			{ 2, 9 },
			{ 3, 9 },
			{ 3, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 2, 7 },
			{ 3, 7 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 4, 7 },
			{ 5, 7 },
			{ 6, 7 },
			{ 4, 8 },
			{ 5, 8 },
			{ 6, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 7, 6 },
			{ 8, 6 },
			{ 9, 6 },
			{ 7, 7 },
			{ 8, 7 },
			{ 9, 7 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 6, 6 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 7, 8 },
			{ 8, 8 },
			{ 9, 8 },
			{ 10, 8 },
			{ 11, 8 },
			{ 12, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 10, 6 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 11, 6 },
			{ 12, 6 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 13, 6 },
			{ 14, 6 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 6 },
			{ 16, 6 },
			{ 17, 6 },
			{ 17, 7 },
			{ 14, 8 },
			{ 15, 8 },
			{ 16, 8 },
			{ 17, 8 },
			{ 0, 0 }
		},
		{
			{ 18, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 20, 10 },
			{ 21, 10 },
			{ 22, 10 },
			{ 23, 10 },
			{ 24, 10 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 24, 13 },
			{ 25, 13 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 21, 18 },
			{ 22, 18 },
			{ 23, 18 },
			{ 24, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 23, 22 },
			{ 24, 22 },
			{ 25, 22 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 21, 24 },
			{ 22, 24 },
			{ 23, 24 },
			{ 24, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 21, 29 },
			{ 22, 29 },
			{ 23, 29 },
			{ 24, 29 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 25, 10 },
			{ 26, 10 },
			{ 27, 10 },
			{ 25, 11 },
			{ 26, 11 },
			{ 27, 11 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 26, 12 },
			{ 26, 13 },
			{ 26, 14 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 25, 15 },
			{ 26, 15 },
			{ 27, 15 },
			{ 25, 16 },
			{ 26, 16 },
			{ 27, 16 },
			{ 25, 17 },
			{ 26, 17 },
			{ 27, 17 }
		},
		{
			{ 25, 18 },
			{ 26, 18 },
			{ 27, 18 },
			{ 25, 19 },
			{ 26, 19 },
			{ 27, 19 },
			{ 25, 20 },
			{ 26, 20 },
			{ 27, 20 }
		},
		{
			{ 28, 10 },
			{ 29, 10 },
			{ 30, 10 },
			{ 31, 10 },
			{ 32, 10 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 27, 13 },
			{ 28, 13 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 28, 18 },
			{ 29, 18 },
			{ 30, 18 },
			{ 31, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 27, 22 },
			{ 28, 22 },
			{ 29, 22 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 28, 24 },
			{ 29, 24 },
			{ 30, 24 },
			{ 31, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 28, 29 },
			{ 29, 29 },
			{ 30, 29 },
			{ 31, 29 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 26, 21 },
			{ 26, 22 },
			{ 26, 23 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 25, 24 },
			{ 26, 24 },
			{ 27, 24 },
			{ 25, 25 },
			{ 26, 25 },
			{ 27, 25 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 26, 26 },
			{ 26, 27 },
			{ 26, 28 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 25, 29 },
			{ 26, 29 },
			{ 27, 29 },
			{ 25, 30 },
			{ 26, 30 },
			{ 27, 30 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 2, 26 },
			{ 3, 26 },
			{ 4, 26 },
			{ 5, 26 },
			{ 3, 27 },
			{ 3, 28 },
			{ 4, 28 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 6, 26 },
			{ 7, 26 },
			{ 8, 26 },
			{ 9, 26 },
			{ 10, 26 },
			{ 11, 26 },
			{ 9, 27 },
			{ 10, 27 },
			{ 11, 27 }
		},
		{
			{ 12, 26 },
			{ 13, 26 },
			{ 14, 26 },
			{ 15, 26 },
			{ 13, 27 },
			{ 14, 27 },
			{ 15, 27 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 16, 27 },
			{ 17, 27 },
			{ 18, 27 },
			{ 19, 27 },
			{ 17, 26 },
			{ 18, 26 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 20, 27 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 24 },
			{ 16, 24 },
			{ 17, 24 },
			{ 18, 24 },
			{ 19, 24 },
			{ 20, 24 },
			{ 17, 25 },
			{ 18, 25 },
			{ 0, 0 }
		},
		{
			{ 11, 24 },
			{ 12, 24 },
			{ 13, 24 },
			{ 14, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 15, 29 },
			{ 16, 29 },
			{ 17, 29 },
			{ 18, 29 },
			{ 19, 29 },
			{ 20, 29 },
			{ 17, 30 },
			{ 18, 30 },
			{ 0, 0 }
		},
		{
			{ 11, 29 },
			{ 12, 29 },
			{ 13, 29 },
			{ 14, 29 },
			{ 13, 28 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 6, 29 },
			{ 7, 29 },
			{ 8, 29 },
			{ 9, 29 },
			{ 10, 29 },
			{ 7, 30 },
			{ 8, 30 },
			{ 9, 30 },
			{ 0, 0 }
		},
		{
			{ 5, 28 },
			{ 5, 29 },
			{ 5, 30 },
			{ 4, 31 },
			{ 5, 31 },
			{ 6, 31 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 7, 31 },
			{ 8, 31 },
			{ 9, 31 },
			{ 10, 31 },
			{ 11, 31 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 12, 30 },
			{ 13, 30 },
			{ 14, 30 },
			{ 12, 31 },
			{ 13, 31 },
			{ 14, 31 },
			{ 12, 32 },
			{ 13, 32 },
			{ 14, 32 }
		},
		{
			{ 15, 31 },
			{ 16, 31 },
			{ 17, 31 },
			{ 18, 31 },
			{ 19, 31 },
			{ 15, 32 },
			{ 16, 32 },
			{ 17, 32 },
			{ 18, 32 }
		},
		{
			{ 20, 31 },
			{ 21, 31 },
			{ 22, 31 },
			{ 23, 31 },
			{ 20, 32 },
			{ 21, 32 },
			{ 22, 32 },
			{ 23, 32 },
			{ 24, 32 }
		},
		{
			{ 25, 32 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 26, 32 },
			{ 27, 32 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 28, 32 },
			{ 29, 32 },
			{ 28, 33 },
			{ 29, 33 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 30, 32 },
			{ 31, 32 },
			{ 32, 32 },
			{ 33, 32 },
			{ 31, 33 },
			{ 30, 34 },
			{ 31, 34 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 30, 33 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 28, 34 },
			{ 29, 34 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 25, 34 },
			{ 26, 34 },
			{ 27, 34 },
			{ 26, 35 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 23, 34 },
			{ 24, 34 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 21, 33 },
			{ 21, 34 },
			{ 22, 34 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 3, 31 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 34, 32 },
			{ 35, 32 },
			{ 36, 32 },
			{ 37, 32 },
			{ 35, 31 },
			{ 36, 31 },
			{ 37, 31 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 38, 31 },
			{ 39, 31 },
			{ 40, 31 },
			{ 38, 32 },
			{ 39, 32 },
			{ 40, 32 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 41, 31 },
			{ 42, 31 },
			{ 43, 31 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 42, 29 },
			{ 43, 29 },
			{ 44, 29 },
			{ 45, 29 },
			{ 42, 30 },
			{ 43, 30 },
			{ 44, 30 },
			{ 45, 30 },
			{ 0, 0 }
		},
		{
			{ 37, 29 },
			{ 38, 29 },
			{ 39, 29 },
			{ 40, 29 },
			{ 41, 29 },
			{ 39, 30 },
			{ 39, 28 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 32, 29 },
			{ 33, 29 },
			{ 34, 29 },
			{ 35, 29 },
			{ 36, 29 },
			{ 33, 30 },
			{ 34, 30 },
			{ 35, 30 },
			{ 0, 0 }
		},
		{
			{ 36, 27 },
			{ 37, 27 },
			{ 38, 27 },
			{ 39, 27 },
			{ 40, 27 },
			{ 41, 27 },
			{ 42, 27 },
			{ 39, 26 },
			{ 0, 0 }
		},
		{
			{ 35, 24 },
			{ 36, 24 },
			{ 37, 24 },
			{ 35, 25 },
			{ 36, 25 },
			{ 37, 25 },
			{ 36, 26 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 32, 24 },
			{ 33, 24 },
			{ 34, 24 },
			{ 32, 25 },
			{ 33, 25 },
			{ 34, 25 },
			{ 32, 26 },
			{ 33, 26 },
			{ 34, 26 }
		},
		{
			{ 38, 24 },
			{ 39, 24 },
			{ 40, 24 },
			{ 41, 24 },
			{ 39, 23 },
			{ 40, 23 },
			{ 39, 22 },
			{ 40, 22 },
			{ 0, 0 }
		},
		{
			{ 42, 24 },
			{ 43, 24 },
			{ 44, 24 },
			{ 42, 25 },
			{ 43, 25 },
			{ 44, 25 },
			{ 42, 26 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 45, 24 },
			{ 46, 24 },
			{ 47, 24 },
			{ 45, 25 },
			{ 46, 25 },
			{ 47, 25 },
			{ 45, 26 },
			{ 46, 26 },
			{ 47, 26 }
		},
		{
			{ 48, 24 },
			{ 49, 24 },
			{ 50, 24 },
			{ 48, 25 },
			{ 49, 25 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 46, 29 },
			{ 47, 29 },
			{ 48, 29 },
			{ 48, 28 },
			{ 48, 27 },
			{ 48, 26 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 49, 28 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 34, 22 },
			{ 35, 22 },
			{ 36, 22 },
			{ 37, 22 },
			{ 38, 22 },
			{ 35, 23 },
			{ 36, 23 },
			{ 37, 23 },
			{ 38, 23 }
		},
		{
			{ 30, 22 },
			{ 31, 22 },
			{ 32, 22 },
			{ 33, 22 },
			{ 31, 21 },
			{ 32, 21 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 41, 22 },
			{ 42, 22 },
			{ 43, 22 },
			{ 44, 22 },
			{ 41, 23 },
			{ 42, 23 },
			{ 43, 23 },
			{ 44, 23 },
			{ 0, 0 }
		},
		{
			{ 45, 22 },
			{ 46, 22 },
			{ 47, 22 },
			{ 48, 22 },
			{ 45, 23 },
			{ 46, 23 },
			{ 47, 23 },
			{ 48, 23 },
			{ 0, 0 }
		},
		{
			{ 38, 20 },
			{ 39, 20 },
			{ 40, 20 },
			{ 39, 21 },
			{ 40, 21 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 35, 18 },
			{ 36, 18 },
			{ 37, 18 },
			{ 35, 19 },
			{ 36, 19 },
			{ 37, 19 },
			{ 35, 20 },
			{ 36, 20 },
			{ 37, 20 }
		},
		{
			{ 32, 17 },
			{ 33, 17 },
			{ 34, 17 },
			{ 32, 18 },
			{ 33, 18 },
			{ 34, 18 },
			{ 32, 19 },
			{ 33, 19 },
			{ 34, 19 }
		},
		{
			{ 38, 18 },
			{ 39, 18 },
			{ 40, 18 },
			{ 41, 18 },
			{ 39, 19 },
			{ 40, 19 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 42, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 43, 18 },
			{ 44, 18 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 45, 18 },
			{ 46, 18 },
			{ 47, 18 },
			{ 45, 19 },
			{ 46, 19 },
			{ 47, 19 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 0, 18 },
			{ 0, 17 },
			{ 1, 17 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 48, 18 },
			{ 49, 18 },
			{ 50, 18 },
			{ 51, 18 },
			{ 52, 18 },
			{ 49, 19 },
			{ 50, 19 },
			{ 51, 19 },
			{ 52, 19 }
		},
		{
			{ 52, 24 },
			{ 52, 23 },
			{ 52, 22 },
			{ 52, 21 },
			{ 52, 20 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 51, 24 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 49, 22 },
			{ 50, 22 },
			{ 51, 22 },
			{ 49, 23 },
			{ 50, 23 },
			{ 51, 23 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 35, 17 },
			{ 35, 16 },
			{ 35, 15 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 49, 17 },
			{ 50, 17 },
			{ 49, 16 },
			{ 50, 16 },
			{ 49, 15 },
			{ 50, 15 },
			{ 49, 14 },
			{ 50, 14 },
			{ 0, 0 }
		},
		{
			{ 45, 13 },
			{ 46, 13 },
			{ 47, 13 },
			{ 48, 13 },
			{ 45, 14 },
			{ 46, 14 },
			{ 47, 14 },
			{ 48, 14 },
			{ 0, 0 }
		},
		{
			{ 41, 13 },
			{ 42, 13 },
			{ 43, 13 },
			{ 44, 13 },
			{ 41, 14 },
			{ 42, 14 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 38, 13 },
			{ 39, 13 },
			{ 40, 13 },
			{ 39, 12 },
			{ 40, 12 },
			{ 39, 14 },
			{ 40, 14 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 34, 13 },
			{ 35, 13 },
			{ 36, 13 },
			{ 37, 13 },
			{ 34, 14 },
			{ 35, 14 },
			{ 36, 14 },
			{ 37, 14 },
			{ 0, 0 }
		},
		{
			{ 29, 13 },
			{ 30, 13 },
			{ 31, 13 },
			{ 32, 13 },
			{ 33, 13 },
			{ 31, 14 },
			{ 32, 14 },
			{ 33, 14 },
			{ 0, 0 }
		},
		{
			{ 37, 10 },
			{ 38, 10 },
			{ 39, 10 },
			{ 40, 10 },
			{ 41, 10 },
			{ 39, 9 },
			{ 39, 11 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 33, 10 },
			{ 34, 10 },
			{ 35, 10 },
			{ 36, 10 },
			{ 34, 11 },
			{ 35, 11 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 42, 10 },
			{ 43, 10 },
			{ 44, 10 },
			{ 45, 10 },
			{ 42, 11 },
			{ 43, 11 },
			{ 44, 11 },
			{ 45, 11 },
			{ 0, 0 }
		},
		{
			{ 46, 10 },
			{ 47, 10 },
			{ 48, 10 },
			{ 46, 11 },
			{ 47, 11 },
			{ 48, 11 },
			{ 49, 11 },
			{ 50, 11 },
			{ 51, 11 }
		},
		{
			{ 51, 12 },
			{ 51, 13 },
			{ 51, 14 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 38, 8 },
			{ 39, 8 },
			{ 40, 8 },
			{ 41, 8 },
			{ 42, 8 },
			{ 43, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 37, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 44, 6 },
			{ 44, 7 },
			{ 45, 7 },
			{ 44, 8 },
			{ 45, 8 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 46, 7 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 36, 6 },
			{ 37, 6 },
			{ 38, 6 },
			{ 39, 6 },
			{ 40, 6 },
			{ 41, 6 },
			{ 42, 6 },
			{ 43, 6 },
			{ 0, 0 }
		},
		{
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 31, 6 },
			{ 32, 6 },
			{ 33, 6 },
			{ 34, 6 },
			{ 35, 6 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 23, 6 },
			{ 24, 6 },
			{ 25, 6 },
			{ 26, 6 },
			{ 27, 6 },
			{ 28, 6 },
			{ 29, 6 },
			{ 30, 6 },
			{ 0, 0 }
		},
		{
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 18, 6 },
			{ 19, 6 },
			{ 20, 6 },
			{ 21, 6 },
			{ 22, 6 },
			{ 19, 5 },
			{ 20, 5 },
			{ 21, 5 },
			{ 0, 0 }
		},
		{
			{ 33, 5 },
			{ 33, 4 },
			{ 33, 3 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		},
		{
			{ 32, 1 },
			{ 33, 1 },
			{ 34, 1 },
			{ 32, 2 },
			{ 33, 2 },
			{ 34, 2 },
			{ 0, 0 },
			{ 0, 0 },
			{ 0, 0 }
		}
	};

	private void Start()
	{
		//GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
		Player = global::UnityEngine.GameObject.Find("Player");
		GateFade = global::UnityEngine.GameObject.Find("GateFade");
		MC = global::UnityEngine.GameObject.Find("Menu_Map").GetComponent<Map_Control>();
	}

	public void First_Game()
	{
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Room[0]) as global::UnityEngine.GameObject;
		StartCoroutine("Start_Map_Pos");
	}

	public void Load_Game()
	{
		if (GM.Room_Num > Room.Length)
		{
			return;
		}
		if (Current_Room != null)
		{
			global::UnityEngine.Object.Destroy(Current_Room.gameObject);
		}
		global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Room[GM.Room_Num]) as global::UnityEngine.GameObject;
		if (gameObject.GetComponent<Room_Control>().Save_Pos != null)
		{
			Target_Pos = gameObject.GetComponent<Room_Control>().Save_Pos.position;
		}
		else
		{
			Target_Pos = gameObject.GetComponent<Room_Control>().targetPos[0].position;
		}
		Player.transform.position = Target_Pos;
		global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(gameObject.transform.position.x, gameObject.transform.position.y, -10f);
		StartCoroutine("Start_Map_Pos");
		if (GM.Room_Num == 145 || GM.Room_Num == 149)
		{
			if (Current_Lift == null)
			{
				global::UnityEngine.GameObject current_Lift = global::UnityEngine.Object.Instantiate(_Lift_Reactor) as global::UnityEngine.GameObject;
				Current_Lift = current_Lift;
			}
			if (GM.Room_Num == 149)
			{
				global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(gameObject.transform.position.x, -228.1147f, -10f);
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 8f;
				global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Start_Cam_Size(8f);
			}
		}
		else if (GM.Room_Num == 10 || GM.Room_Num == 16 || GM.Room_Num == 35 || GM.Room_Num == 46 || GM.Room_Num == 75 || GM.Room_Num == 86 || GM.Room_Num == 95 || GM.Room_Num == 110 || GM.Room_Num == 119 || GM.Room_Num == 140)
		{
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<UnityEngine.Camera>().orthographicSize = 8f;
			global::UnityEngine.GameObject.Find("Main Camera").GetComponent<Camera_Control>().Set_Start_Cam_Size(8f);
		}
		else if (GM.Room_Num == 60)
		{
			global::UnityEngine.GameObject.Find("Main Camera").transform.position = new global::UnityEngine.Vector3(732.317f, 3.2f, -10f);
		}
	}

	public global::System.Collections.IEnumerator Start_Map_Pos()
	{
		yield return new global::UnityEngine.WaitForSeconds(0.5f);
		Set_Cam_LRTopBot();
		Room_Map_Pos();
	}

	public void Go_Room(int roomNum, int posNum, float dist_x, float dist_y, bool cheatRoom)
	{
		if (roomNum <= Room.Length)
		{
			GM.onGatePass = true;
			GM.Room_Num = roomNum;
			pos_Num = posNum;
			dist_X = dist_x;
			dist_Y = dist_y;
			isFadeOut = true;
			GateFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			isCheatRoom = cheatRoom;
		}
	}

	private void Update()
	{
		if (GM.Paused)
		{
			return;
		}
		if (Fade_Timer > 0f)
		{
			Fade_Timer -= global::UnityEngine.Time.deltaTime;
			if (Fade_Timer <= 0f)
			{
				isFadeIn = true;
			}
			if (!isMapChanged)
			{
				isMapChanged = true;
				Set_Cam_LRTopBot();
				Room_Map_Pos();
			}
			Player.transform.position = global::UnityEngine.Vector3.Lerp(Player.transform.position, Target_Pos, global::UnityEngine.Time.deltaTime * 5f);
		}
		if (isFadeIn)
		{
			FadeOpacity -= global::UnityEngine.Time.deltaTime * 3f;
			if (FadeOpacity <= 0f)
			{
				isFadeIn = false;
				FadeOpacity = 0f;
				GateFade.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				GM.onGatePass = false;
				isCheatRoom = false;
				Player.GetComponent<Player_Control>().UnLock_GatePass();
			}
			GateFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
			if (!isCheatRoom)
			{
				Player.transform.position = global::UnityEngine.Vector3.Lerp(Player.transform.position, Target_Pos, global::UnityEngine.Time.deltaTime * 6f);
				if (GM.Room_Num == 149 && Current_Lift != null && Current_Lift.transform.position.y > -206.35f)
				{
					Current_Lift.transform.position = new global::UnityEngine.Vector3(Current_Lift.transform.position.x, Player.transform.position.y - 0.85f, 0f);
				}
			}
		}
		else if (isFadeOut)
		{
			FadeOpacity += global::UnityEngine.Time.deltaTime * 4f;
			if (FadeOpacity >= 1f)
			{
				isFadeOut = false;
				FadeOpacity = 1f;
				Fade_Timer = 0.35f;
				if (Current_Room != null)
				{
					global::UnityEngine.Object.Destroy(Current_Room.gameObject);
				}
				global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Room[GM.Room_Num]) as global::UnityEngine.GameObject;
				if (!isCheatRoom)
				{
					Target_Pos = gameObject.GetComponent<Room_Control>().targetPos[pos_Num].position;
					Target_Pos = new global::UnityEngine.Vector3(Target_Pos.x + dist_X, Target_Pos.y + dist_Y, 0f);
				}
				else
				{
					global::UnityEngine.GameObject.Find("Main Camera").transform.position = gameObject.transform.position;
					if (GM.Room_Num == 60)
					{
						Player.transform.position = (Target_Pos = gameObject.GetComponent<Room_Control>().targetPos[5].position);
						global::UnityEngine.GameObject.Find("Main Camera").transform.position = Target_Pos;
					}
					else
					{
						Player.transform.position = (Target_Pos = gameObject.GetComponent<Room_Control>().Save_Pos.position);
					}
				}
				if ((GM.Room_Num == 145 || GM.Room_Num == 149) && Current_Lift == null)
				{
					global::UnityEngine.GameObject current_Lift = global::UnityEngine.Object.Instantiate(_Lift_Reactor) as global::UnityEngine.GameObject;
					Current_Lift = current_Lift;
				}
				isMapChanged = false;
			}
			GateFade.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(0f, 0f, 0f, FadeOpacity);
		}
		else if (!GM.onGatePass && !GM.onSave && !GM.onEvent)
		{
			Move_Map_Pos();
		}
	}

	private void See_Map(int num)
	{
		string empty = string.Empty;
		for (int i = 0; i < MapArray_Num[num]; i++)
		{
			empty = MapArray_Pos[num, i, 0] + "_" + MapArray_Pos[num, i, 1];
			global::UnityEngine.GameObject.Find("MapPos_" + empty).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			global::UnityEngine.GameObject.Find("MapBorder_" + empty).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
			if (global::UnityEngine.GameObject.Find("MapSaveFont_" + empty) != null)
			{
				global::UnityEngine.GameObject.Find("MapSaveFont_" + empty).GetComponent<global::UnityEngine.UI.Text>().enabled = true;
			}
		}
	}

	public void Hide_UnsightMap(int[] map)
	{
		string empty = string.Empty;
		for (int i = 1; i < MapArray_Num.Length; i++)
		{
			if (map[i] != 0)
			{
				continue;
			}
			for (int j = 0; j < MapArray_Num[i]; j++)
			{
				empty = MapArray_Pos[i, j, 0] + "_" + MapArray_Pos[i, j, 1];
				global::UnityEngine.GameObject.Find("MapPos_" + empty).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				global::UnityEngine.GameObject.Find("MapBorder_" + empty).GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
				if (global::UnityEngine.GameObject.Find("MapSaveFont_" + empty) != null)
				{
					global::UnityEngine.GameObject.Find("MapSaveFont_" + empty).GetComponent<global::UnityEngine.UI.Text>().enabled = false;
				}
			}
		}
	}

	private void Reset_Map()
	{
		global::UnityEngine.GameObject[] array = global::UnityEngine.GameObject.FindGameObjectsWithTag("MapBlock");
		global::UnityEngine.GameObject[] array2 = array;
		foreach (global::UnityEngine.GameObject gameObject in array2)
		{
			gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
		}
	}

	private void Room_Map_Pos()
	{
		if (!GM.Check_WatchMap())
		{
			See_Map(GM.Room_Num);
		}
		switch (GM.Room_Num)
		{
		case 0:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 18f);
			}
			break;
		case 1:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 18f);
			}
			break;
		case 2:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 17f);
			}
			break;
		case 3:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 17f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 17f);
			}
			break;
		case 4:
			MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 17f);
			break;
		case 5:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 16f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 16f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 17f);
			}
			else if (pos_Num == 3)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 18f);
			}
			break;
		case 6:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 15f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 16f);
			}
			break;
		case 7:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 18f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(16f, 19f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 19f);
			}
			break;
		case 8:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 19f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 21f);
			}
			break;
		case 9:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 19f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 20f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 20f);
			}
			break;
		case 10:
			MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 20f);
			break;
		case 11:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 20f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(22f, 20f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 22f);
			}
			else if (pos_Num == 3)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(22f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 21f);
			}
			break;
		case 12:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 20f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 21f);
			}
			break;
		case 13:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 22f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 22f);
			}
			else if (Player.transform.position.x > 502.6f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 23f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 23f);
			}
			break;
		case 14:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 23f);
			}
			break;
		case 15:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 23f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 24f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 25f);
			}
			break;
		case 16:
			MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 24f);
			break;
		case 17:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 24f);
			}
			break;
		case 18:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 24f);
			}
			break;
		case 19:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 23f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 24f);
			}
			break;
		case 20:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(1f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(1f, 23f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(1f, 26f);
			}
			break;
		case 21:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 21f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 19f);
			}
			break;
		case 22:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 21f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 22f);
			}
			break;
		case 23:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 22f);
			}
			break;
		case 24:
			MC.pos_Cursor = new global::UnityEngine.Vector2(0f, 26f);
			break;
		case 25:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(1f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 18f);
			}
			break;
		case 26:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 18f);
			}
			else if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 17f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 17f);
			}
			break;
		case 27:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 14f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 15f);
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 16f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 16f);
			}
			break;
		case 28:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 15f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(9f, 14f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 13f);
			}
			break;
		case 29:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 14f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 15f);
			}
			break;
		case 30:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 15f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(16f, 15f);
			}
			break;
		case 31:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 12f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 13f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 12f);
			}
			break;
		case 32:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 13f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 13f);
			}
			else if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 12f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 12f);
			}
			break;
		case 33:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(22f, 12f);
			}
			break;
		case 34:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 12f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 16f);
			}
			break;
		case 35:
			MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 12f);
			break;
		case 36:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(16f, 10f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 10f);
			}
			else if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 10f);
			}
			break;
		case 37:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 10f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 10f);
			}
			else if (pos_Num == 2 || pos_Num == 3)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 8f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 11f);
			}
			break;
		case 38:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 10f);
			}
			break;
		case 39:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 10f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 13f);
			}
			break;
		case 40:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 8f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 8f);
			}
			break;
		case 41:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 7f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 7f);
			}
			break;
		case 42:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 8f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 8f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 7f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 7f);
			}
			break;
		case 43:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 6f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(9f, 6f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 7f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 7f);
			}
			break;
		case 44:
			MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 6f);
			break;
		case 45:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 8f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 8f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(8f, 8f);
			}
			break;
		case 46:
			MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 6f);
			break;
		case 47:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 6f);
			}
			break;
		case 48:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 6f);
			}
			break;
		case 49:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 6f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 6f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 8f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 8f);
			}
			break;
		case 50:
			MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 8f);
			break;
		case 51:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 10f);
			}
			break;
		case 61:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(32f, 10f);
			}
			break;
		case 52:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 13f);
			}
			break;
		case 62:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 13f);
			}
			break;
		case 53:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 18f);
			}
			break;
		case 63:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(31f, 18f);
			}
			break;
		case 54:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 22f);
			}
			break;
		case 64:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(29f, 22f);
			}
			break;
		case 55:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 24f);
			}
			break;
		case 65:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(31f, 24f);
			}
			break;
		case 56:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 29f);
			}
			break;
		case 66:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(31f, 29f);
			}
			break;
		case 57:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 10f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 11f);
			}
			break;
		case 58:
			if (pos_Num == 0 || pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 13f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 12f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 14f);
			}
			break;
		case 59:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 15f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 17f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 17f);
			}
			break;
		case 60:
			if (pos_Num == 0 || pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 18f);
			}
			else if (pos_Num == 1 || pos_Num == 3)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 18f);
			}
			else if (pos_Num == 4)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 20f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 18f);
			}
			break;
		case 67:
			if (pos_Num == 0 || pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 22f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 21f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 23f);
			}
			break;
		case 68:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 24f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 24f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 25f);
			}
			break;
		case 69:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 26f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 28f);
			}
			break;
		case 70:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 29f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 31f);
			}
			break;
		case 71:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(2f, 26f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 26f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 28f);
			}
			break;
		case 72:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 26f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 26f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 26f);
			}
			break;
		case 73:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 26f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 27f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 27f);
			}
			break;
		case 74:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(16f, 27f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 27f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 26f);
			}
			break;
		case 75:
			MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 27f);
			break;
		case 76:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 24f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 24f);
			}
			else if (pos_Num == 2)
			{
				if (Player.transform.position.x > Get_Room_Half_X())
				{
					MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 24f);
				}
				else
				{
					MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 24f);
				}
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 25f);
			}
			break;
		case 77:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 24f);
			}
			break;
		case 78:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 29f);
			}
			else if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 30f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 30f);
			}
			break;
		case 79:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(13f, 28f);
			}
			break;
		case 80:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(10f, 29f);
			}
			break;
		case 81:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 28f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(5f, 29f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(4f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(6f, 31f);
			}
			break;
		case 82:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(7f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(11f, 31f);
			}
			break;
		case 83:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(12f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(14f, 31f);
			}
			break;
		case 84:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(15f, 31f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(19f, 31f);
			}
			else if (Player.transform.position.x > 497.84f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(17f, 31f);
			}
			break;
		case 85:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(20f, 31f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 32f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 32f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 31f);
			}
			break;
		case 86:
			MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 32f);
			break;
		case 87:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(26f, 32f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 32f);
			}
			break;
		case 88:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 32f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(29f, 32f);
			}
			break;
		case 89:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(30f, 32f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 32f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(31f, 33f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(30f, 34f);
			}
			break;
		case 90:
			MC.pos_Cursor = new global::UnityEngine.Vector2(30f, 33f);
			break;
		case 91:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(28f, 34f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(29f, 34f);
			}
			break;
		case 92:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(25f, 34f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(27f, 34f);
			}
			break;
		case 93:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 34f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(24f, 34f);
			}
			break;
		case 94:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(21f, 33f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(22f, 34f);
			}
			break;
		case 95:
			MC.pos_Cursor = new global::UnityEngine.Vector2(3f, 31f);
			break;
		case 96:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 32f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 31f);
			}
			break;
		case 97:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 31f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(40f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 31f);
			}
			break;
		case 98:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 31f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 31f);
			}
			break;
		case 99:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 30f);
			}
			break;
		case 100:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 29f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 30f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 28f);
			}
			break;
		case 101:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(32f, 29f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(36f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 30f);
			}
			break;
		case 102:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(36f, 27f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 27f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 27f);
			}
			break;
		case 103:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 24f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(36f, 26f);
			}
			break;
		case 104:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(32f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 24f);
			}
			break;
		case 105:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(40f, 22f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 24f);
			}
			else if (pos_Num == 3)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 24f);
			}
			else if (Player.transform.position.x < 1123.71f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(40f, 22f);
			}
			break;
		case 106:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 24f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 24f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 26f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 24f);
			}
			break;
		case 107:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 24f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(47f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(47f, 26f);
			}
			break;
		case 108:
			if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(50f, 24f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 24f);
			}
			break;
		case 109:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 26f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(46f, 29f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 28f);
			}
			break;
		case 110:
			MC.pos_Cursor = new global::UnityEngine.Vector2(49f, 28f);
			break;
		case 111:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 22f);
			}
			break;
		case 112:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(30f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 22f);
			}
			break;
		case 113:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 22f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 23f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 23f);
			}
			break;
		case 114:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 23f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 23f);
			}
			break;
		case 115:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 20f);
			}
			else if (Player.transform.position.x < 1123.71f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 21f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(40f, 21f);
			}
			break;
		case 116:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 20f);
			}
			break;
		case 117:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(32f, 18f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 17f);
			}
			break;
		case 118:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 18f);
			}
			break;
		case 119:
			MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 18f);
			break;
		case 120:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 18f);
			}
			break;
		case 121:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(47f, 18f);
			}
			break;
		case 122:
			MC.pos_Cursor = new global::UnityEngine.Vector2(0f, 18f);
			break;
		case 123:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 18f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(52f, 19f);
			}
			else if (Player.transform.position.x < 1408.193f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(49f, 18f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(50f, 18f);
			}
			break;
		case 124:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(52f, 20f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(52f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(52f, 24f);
			}
			break;
		case 125:
			MC.pos_Cursor = new global::UnityEngine.Vector2(51f, 24f);
			break;
		case 126:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(49f, 22f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(51f, 22f);
			}
			break;
		case 127:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 17f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 15f);
			}
			break;
		case 128:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(49f, 14f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(50f, 14f);
			}
			else if (Player.transform.position.x < 1408.193f)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(49f, 17f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(50f, 17f);
			}
			break;
		case 129:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(48f, 14f);
			}
			break;
		case 130:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 13f);
			}
			break;
		case 131:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 12f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(40f, 13f);
			}
			break;
		case 132:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(34f, 13f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 14f);
			}
			break;
		case 133:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(29f, 13f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 13f);
			}
			break;
		case 134:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 9f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 10f);
			}
			else if (pos_Num == 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(41f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 11f);
			}
			break;
		case 135:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(36f, 10f);
			}
			break;
		case 136:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(42f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 10f);
			}
			break;
		case 137:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(46f, 10f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(51f, 11f);
			}
			break;
		case 138:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(51f, 12f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(51f, 14f);
			}
			break;
		case 139:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(38f, 8f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 8f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(39f, 8f);
			}
			break;
		case 140:
			MC.pos_Cursor = new global::UnityEngine.Vector2(37f, 8f);
			break;
		case 141:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 6f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(45f, 7f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(44f, 8f);
			}
			break;
		case 142:
			MC.pos_Cursor = new global::UnityEngine.Vector2(46f, 7f);
			break;
		case 143:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(36f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(43f, 6f);
			}
			break;
		case 145:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(31f, 6f);
			}
			else if (pos_Num == 1)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(35f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 6f);
			}
			break;
		case 146:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(23f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(30f, 6f);
			}
			break;
		case 148:
			if (pos_Num == 0)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(18f, 6f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(22f, 6f);
			}
			break;
		case 149:
			if (pos_Num < 2)
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 3f);
			}
			else
			{
				MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 5f);
			}
			break;
		case 150:
			MC.pos_Cursor = new global::UnityEngine.Vector2(33f, 2f);
			break;
		}
		MC.Change_MiniMap();
	}

	private void Move_Map_Pos()
	{
		PrevCursor = MC.pos_Cursor;
		switch (GM.Room_Num)
		{
		case 0:
			if (Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 9f;
			}
			else
			{
				MC.pos_Cursor.x = 8f;
			}
			if (Player.transform.position.y > 4f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 1:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 12f;
			}
			else
			{
				MC.pos_Cursor.x = 11f;
			}
			MC.pos_Cursor.y = 18f;
			break;
		case 2:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 14f;
			}
			else
			{
				MC.pos_Cursor.x = 13f;
			}
			if (Player.transform.position.y > -11f)
			{
				MC.pos_Cursor.y = 18f;
			}
			else
			{
				MC.pos_Cursor.y = 17f;
			}
			break;
		case 3:
			if (Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			MC.pos_Cursor.y = 17f;
			break;
		case 5:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 20f;
			}
			else
			{
				MC.pos_Cursor.x = 19f;
			}
			if (Player.transform.position.y > -8.4f)
			{
				MC.pos_Cursor.y = 18f;
			}
			else if (Player.transform.position.y > -23f)
			{
				MC.pos_Cursor.y = 17f;
			}
			else
			{
				MC.pos_Cursor.y = 16f;
			}
			break;
		case 6:
			if (Player.transform.position.x > 496f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else
			{
				MC.pos_Cursor.x = 17f;
			}
			if (Player.transform.position.y > -45f)
			{
				MC.pos_Cursor.y = 16f;
			}
			else
			{
				MC.pos_Cursor.y = 15f;
			}
			break;
		case 7:
			if (Player.transform.position.y > 7.4f)
			{
				if (Player.transform.position.x > Get_Room_Half_X())
				{
					MC.pos_Cursor.x = 17f;
				}
				else
				{
					MC.pos_Cursor.x = 16f;
				}
				MC.pos_Cursor.y = 19f;
				break;
			}
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			MC.pos_Cursor.y = 18f;
			break;
		case 8:
			if (Player.transform.position.y > 42.5f)
			{
				MC.pos_Cursor.y = 21f;
			}
			else if (Player.transform.position.y > 21.5f)
			{
				MC.pos_Cursor.y = 20f;
			}
			else
			{
				MC.pos_Cursor.y = 19f;
			}
			MC.pos_Cursor.x = 15f;
			break;
		case 9:
			if (Player.transform.position.y > 24.4f)
			{
				if (Player.transform.position.x > 526.5f)
				{
					MC.pos_Cursor.x = 19f;
				}
				else
				{
					MC.pos_Cursor.x = 18f;
				}
				MC.pos_Cursor.y = 20f;
			}
			else
			{
				MC.pos_Cursor.x = 18f;
				MC.pos_Cursor.y = 19f;
			}
			break;
		case 11:
			if (Player.transform.position.y > 57f)
			{
				if (Player.transform.position.x > Get_Room_AThird_X(2))
				{
					MC.pos_Cursor.x = 22f;
				}
				else if (Player.transform.position.x > Get_Room_AThird_X(1))
				{
					MC.pos_Cursor.x = 21f;
				}
				else
				{
					MC.pos_Cursor.x = 20f;
				}
				MC.pos_Cursor.y = 22f;
			}
			else if (Player.transform.position.y > 36.5f)
			{
				MC.pos_Cursor.x = 21f;
				MC.pos_Cursor.y = 21f;
			}
			else
			{
				if (Player.transform.position.x > Get_Room_AThird_X(2))
				{
					MC.pos_Cursor.x = 22f;
				}
				else if (Player.transform.position.x > Get_Room_AThird_X(1))
				{
					MC.pos_Cursor.x = 21f;
				}
				else
				{
					MC.pos_Cursor.x = 20f;
				}
				MC.pos_Cursor.y = 20f;
			}
			break;
		case 13:
			if (Player.transform.position.y > 75.5f)
			{
				if (Player.transform.position.x > 502.6f)
				{
					MC.pos_Cursor.x = 18f;
				}
				else
				{
					MC.pos_Cursor.x = 17f;
				}
				MC.pos_Cursor.y = 23f;
				break;
			}
			if (Player.transform.position.x > 528f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > 502.6f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > 476f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > 445f)
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			MC.pos_Cursor.y = 22f;
			break;
		case 14:
			if (Player.transform.position.x > 384f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > 355.6f)
			{
				MC.pos_Cursor.x = 13f;
			}
			else if (Player.transform.position.x > 327.15f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else
			{
				MC.pos_Cursor.x = 11f;
			}
			if (Player.transform.position.y > 71f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 15:
			if (Player.transform.position.y > 86.5f)
			{
				if (Player.transform.position.x > Get_Room_AThird_X(2))
				{
					MC.pos_Cursor.x = 10f;
				}
				else if (Player.transform.position.x > Get_Room_AThird_X(1))
				{
					MC.pos_Cursor.x = 9f;
				}
				else
				{
					MC.pos_Cursor.x = 8f;
				}
				if (Player.transform.position.y > 106f)
				{
					MC.pos_Cursor.y = 25f;
				}
				else
				{
					MC.pos_Cursor.y = 24f;
				}
			}
			else
			{
				MC.pos_Cursor.x = 10f;
				MC.pos_Cursor.y = 23f;
			}
			break;
		case 17:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 6f;
			}
			else
			{
				MC.pos_Cursor.x = 5f;
			}
			MC.pos_Cursor.y = 24f;
			break;
		case 18:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 4f;
			}
			else
			{
				MC.pos_Cursor.x = 3f;
			}
			MC.pos_Cursor.y = 24f;
			break;
		case 19:
			MC.pos_Cursor.x = 2f;
			if (Player.transform.position.y > 91f)
			{
				MC.pos_Cursor.y = 24f;
			}
			else
			{
				MC.pos_Cursor.y = 23f;
			}
			break;
		case 20:
			MC.pos_Cursor.x = 1f;
			if (Player.transform.position.y > Get_Room_AFifth_Y(4))
			{
				MC.pos_Cursor.y = 26f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(3))
			{
				MC.pos_Cursor.y = 25f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(2))
			{
				MC.pos_Cursor.y = 24f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(1))
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 21:
			if (Player.transform.position.x > 73f)
			{
				MC.pos_Cursor.x = 3f;
			}
			else
			{
				MC.pos_Cursor.x = 2f;
			}
			if (Player.transform.position.y > Get_Room_AQuarter_Y(3) && MC.pos_Cursor.x == 2f)
			{
				MC.pos_Cursor.y = 22f;
			}
			else if (Player.transform.position.y > Get_Room_AQuarter_Y(2))
			{
				MC.pos_Cursor.y = 21f;
			}
			else if (Player.transform.position.y > Get_Room_AQuarter_Y(1))
			{
				MC.pos_Cursor.y = 20f;
			}
			else
			{
				MC.pos_Cursor.y = 19f;
			}
			break;
		case 22:
			if (Player.transform.position.y > Get_Room_Half_Y())
			{
				MC.pos_Cursor.y = 22f;
			}
			else
			{
				MC.pos_Cursor.y = 21f;
			}
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 7f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(2))
			{
				MC.pos_Cursor.x = 6f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 5f;
			}
			else
			{
				MC.pos_Cursor.x = 4f;
			}
			break;
		case 23:
			if (Player.transform.position.x > 270.4f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.7f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else
			{
				MC.pos_Cursor.x = 8f;
			}
			MC.pos_Cursor.y = 22f;
			break;
		case 25:
			if (Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 3f;
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 2f;
			}
			else
			{
				MC.pos_Cursor.x = 1f;
			}
			MC.pos_Cursor.y = 18f;
			break;
		case 26:
			if (Player.transform.position.y > Get_Room_AThird_Y(2))
			{
				MC.pos_Cursor.y = 19f;
			}
			else if (Player.transform.position.y > Get_Room_AThird_Y(1))
			{
				MC.pos_Cursor.y = 18f;
			}
			else
			{
				MC.pos_Cursor.y = 17f;
			}
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 7f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(2))
			{
				MC.pos_Cursor.x = 6f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 5f;
			}
			else
			{
				MC.pos_Cursor.x = 4f;
			}
			break;
		case 27:
			if (Player.transform.position.y > -44.8f)
			{
				MC.pos_Cursor.y = 16f;
			}
			else if (Player.transform.position.y > -60f)
			{
				MC.pos_Cursor.y = 15f;
			}
			else
			{
				MC.pos_Cursor.y = 14f;
			}
			if (MC.pos_Cursor.y == 15f && Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 7f;
			}
			else if (MC.pos_Cursor.y > 14f && Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 6f;
			}
			else
			{
				MC.pos_Cursor.x = 5f;
			}
			break;
		case 28:
			if (Player.transform.position.y > -57.5f)
			{
				MC.pos_Cursor.y = 15f;
			}
			else if (Player.transform.position.y > -73.3f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else
			{
				MC.pos_Cursor.y = 13f;
			}
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 11f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(2))
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 9f;
			}
			else
			{
				MC.pos_Cursor.x = 8f;
			}
			break;
		case 29:
			if (Player.transform.position.y > -57.5f)
			{
				MC.pos_Cursor.y = 15f;
			}
			else
			{
				MC.pos_Cursor.y = 14f;
			}
			if (Player.transform.position.x > 327.5f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else if (Player.transform.position.x > 295.7f)
			{
				MC.pos_Cursor.x = 11f;
			}
			else
			{
				MC.pos_Cursor.x = 10f;
			}
			break;
		case 30:
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 16f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(2))
			{
				MC.pos_Cursor.x = 15f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 14f;
			}
			else
			{
				MC.pos_Cursor.x = 13f;
			}
			break;
		case 31:
			if (Player.transform.position.y > -88.7f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			if (Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 13f;
			}
			else
			{
				MC.pos_Cursor.x = 12f;
			}
			break;
		case 32:
			if (Player.transform.position.y > -89.5f)
			{
				if (Player.transform.position.x > Get_Room_ASixth_X(5))
				{
					MC.pos_Cursor.x = 20f;
				}
				else if (Player.transform.position.x > Get_Room_ASixth_X(4))
				{
					MC.pos_Cursor.x = 19f;
				}
				else if (Player.transform.position.x > Get_Room_ASixth_X(3))
				{
					MC.pos_Cursor.x = 18f;
				}
				else if (Player.transform.position.x > Get_Room_ASixth_X(2))
				{
					MC.pos_Cursor.x = 17f;
				}
				else if (Player.transform.position.x > Get_Room_ASixth_X(1))
				{
					MC.pos_Cursor.x = 16f;
				}
				else
				{
					MC.pos_Cursor.x = 15f;
				}
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				if (Player.transform.position.x > Get_Room_Half_X())
				{
					MC.pos_Cursor.x = 18f;
				}
				else
				{
					MC.pos_Cursor.x = 17f;
				}
				MC.pos_Cursor.y = 12f;
			}
			break;
		case 33:
			if (Player.transform.position.y > -90.5f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 22f;
			}
			else
			{
				MC.pos_Cursor.x = 21f;
			}
			break;
		case 34:
			if (Player.transform.position.y > Get_Room_AFifth_Y(4))
			{
				if (Player.transform.position.x > Get_Room_AThird_X(2))
				{
					MC.pos_Cursor.x = 23f;
				}
				else if (Player.transform.position.x > Get_Room_AThird_X(1))
				{
					MC.pos_Cursor.x = 22f;
				}
				else
				{
					MC.pos_Cursor.x = 21f;
				}
				MC.pos_Cursor.y = 16f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(3))
			{
				MC.pos_Cursor.y = 15f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(2))
			{
				MC.pos_Cursor.y = 14f;
			}
			else if (Player.transform.position.y > Get_Room_AFifth_Y(1))
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			break;
		case 36:
			if (Player.transform.position.y > -122.5f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			if (Player.transform.position.x > Get_Room_AQuarter_X(3))
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(2))
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > Get_Room_AQuarter_X(1))
			{
				MC.pos_Cursor.x = 17f;
			}
			else
			{
				MC.pos_Cursor.x = 16f;
			}
			break;
		case 37:
			MC.pos_Cursor.x = 13f;
			if (Player.transform.position.y > -120f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else if (Player.transform.position.y > -138f)
			{
				if (Player.transform.position.x > Get_Room_AFifth_X(4))
				{
					MC.pos_Cursor.x = 15f;
				}
				else if (Player.transform.position.x > Get_Room_AFifth_X(3))
				{
					MC.pos_Cursor.x = 14f;
				}
				else if (Player.transform.position.x > Get_Room_AFifth_X(2))
				{
					MC.pos_Cursor.x = 13f;
				}
				else if (Player.transform.position.x > Get_Room_AFifth_X(1))
				{
					MC.pos_Cursor.x = 12f;
				}
				else
				{
					MC.pos_Cursor.x = 11f;
				}
				MC.pos_Cursor.y = 10f;
			}
			else if (Player.transform.position.y > -152.5f)
			{
				MC.pos_Cursor.y = 9f;
			}
			else
			{
				MC.pos_Cursor.y = 8f;
			}
			break;
		case 38:
			if (Player.transform.position.y > -121.5f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			if (Player.transform.position.x > 271f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else if (Player.transform.position.x > 184.2f)
			{
				MC.pos_Cursor.x = 7f;
			}
			else
			{
				MC.pos_Cursor.x = 6f;
			}
			break;
		case 39:
			if (Player.transform.position.x > 128f)
			{
				MC.pos_Cursor.x = 5f;
			}
			else
			{
				MC.pos_Cursor.x = 4f;
			}
			if (Player.transform.position.y > -85.4f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else if (Player.transform.position.y > -96f)
			{
				MC.pos_Cursor.y = 12f;
			}
			else if (Player.transform.position.y > -116f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			break;
		case 40:
			if (Player.transform.position.x > 71.12f)
			{
				MC.pos_Cursor.x = 3f;
			}
			else
			{
				MC.pos_Cursor.x = 2f;
			}
			if (Player.transform.position.y > -136.4f)
			{
				MC.pos_Cursor.y = 10f;
			}
			else if (Player.transform.position.y > -152f)
			{
				MC.pos_Cursor.y = 9f;
			}
			else
			{
				MC.pos_Cursor.y = 8f;
			}
			break;
		case 41:
			if (Player.transform.position.x > 71.12f)
			{
				MC.pos_Cursor.x = 3f;
			}
			else
			{
				MC.pos_Cursor.x = 2f;
			}
			MC.pos_Cursor.y = 7f;
			break;
		case 42:
			if (Player.transform.position.x > 156.46f)
			{
				MC.pos_Cursor.x = 6f;
			}
			else if (Player.transform.position.x > 128f)
			{
				MC.pos_Cursor.x = 5f;
			}
			else
			{
				MC.pos_Cursor.x = 4f;
			}
			if (Player.transform.position.y > -168f)
			{
				MC.pos_Cursor.y = 8f;
			}
			else
			{
				MC.pos_Cursor.y = 7f;
			}
			break;
		case 43:
			if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else
			{
				MC.pos_Cursor.x = 7f;
			}
			if (Player.transform.position.y > -185.8f)
			{
				MC.pos_Cursor.y = 7f;
			}
			else
			{
				MC.pos_Cursor.y = 6f;
			}
			break;
		case 45:
			if (Player.transform.position.x > 327.15f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else if (Player.transform.position.x > 298.7f)
			{
				MC.pos_Cursor.x = 11f;
			}
			else if (Player.transform.position.x > 270.26f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else
			{
				MC.pos_Cursor.x = 7f;
			}
			MC.pos_Cursor.y = 8f;
			break;
		case 47:
			if (Player.transform.position.x > 327.26f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else
			{
				MC.pos_Cursor.x = 11f;
			}
			MC.pos_Cursor.y = 6f;
			break;
		case 48:
			if (Player.transform.position.x > 384.05f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else
			{
				MC.pos_Cursor.x = 13f;
			}
			MC.pos_Cursor.y = 6f;
			break;
		case 49:
			if (Player.transform.position.x > 464f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > 439f)
			{
				MC.pos_Cursor.x = 16f;
			}
			else if (Player.transform.position.x > 412f)
			{
				MC.pos_Cursor.x = 15f;
			}
			else
			{
				MC.pos_Cursor.x = 14f;
			}
			if (Player.transform.position.y > -169f)
			{
				MC.pos_Cursor.y = 8f;
			}
			else if (Player.transform.position.y > -184.9f)
			{
				MC.pos_Cursor.y = 7f;
			}
			else
			{
				MC.pos_Cursor.y = 6f;
			}
			break;
		case 51:
			if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else if (Player.transform.position.x > 640.08f)
			{
				MC.pos_Cursor.x = 23f;
			}
			else if (Player.transform.position.x > 611.64f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else if (Player.transform.position.x > 583.19f)
			{
				MC.pos_Cursor.x = 21f;
			}
			else
			{
				MC.pos_Cursor.x = 20f;
			}
			MC.pos_Cursor.y = 10f;
			break;
		case 61:
			if (Player.transform.position.x > 896.12f)
			{
				MC.pos_Cursor.x = 32f;
			}
			else if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else if (Player.transform.position.x > 810.78f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			MC.pos_Cursor.y = 10f;
			break;
		case 52:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 25f;
			}
			else
			{
				MC.pos_Cursor.x = 24f;
			}
			MC.pos_Cursor.y = 13f;
			break;
		case 62:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 28f;
			}
			else
			{
				MC.pos_Cursor.x = 27f;
			}
			MC.pos_Cursor.y = 13f;
			break;
		case 53:
			if (Player.transform.position.x > 668.6f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else if (Player.transform.position.x > 640.09f)
			{
				MC.pos_Cursor.x = 23f;
			}
			else if (Player.transform.position.x > 611.57f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else
			{
				MC.pos_Cursor.x = 21f;
			}
			MC.pos_Cursor.y = 18f;
			break;
		case 63:
			if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else if (Player.transform.position.x > 810.78f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			MC.pos_Cursor.y = 18f;
			break;
		case 54:
			if (Player.transform.position.x > 696.98f)
			{
				MC.pos_Cursor.x = 25f;
			}
			else if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else
			{
				MC.pos_Cursor.x = 23f;
			}
			MC.pos_Cursor.y = 22f;
			break;
		case 64:
			if (Player.transform.position.x > 810.77f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else if (Player.transform.position.x > 782.32f)
			{
				MC.pos_Cursor.x = 28f;
			}
			else
			{
				MC.pos_Cursor.x = 27f;
			}
			MC.pos_Cursor.y = 22f;
			break;
		case 55:
			if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else if (Player.transform.position.x > 640.08f)
			{
				MC.pos_Cursor.x = 23f;
			}
			else if (Player.transform.position.x > 611.64f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else
			{
				MC.pos_Cursor.x = 21f;
			}
			MC.pos_Cursor.y = 24f;
			break;
		case 65:
			if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else if (Player.transform.position.x > 810.78f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			MC.pos_Cursor.y = 24f;
			break;
		case 56:
			if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else if (Player.transform.position.x > 640.08f)
			{
				MC.pos_Cursor.x = 23f;
			}
			else if (Player.transform.position.x > 611.64f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else
			{
				MC.pos_Cursor.x = 21f;
			}
			MC.pos_Cursor.y = 29f;
			break;
		case 66:
			if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else if (Player.transform.position.x > 810.78f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			MC.pos_Cursor.y = 29f;
			break;
		case 57:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > -121.7f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			break;
		case 58:
			if (Player.transform.position.y > -72f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else if (Player.transform.position.y > -88f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			MC.pos_Cursor.x = 26f;
			break;
		case 59:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > -21f)
			{
				MC.pos_Cursor.y = 17f;
			}
			else if (Player.transform.position.y > -40.5f)
			{
				MC.pos_Cursor.y = 16f;
			}
			else
			{
				MC.pos_Cursor.y = 15f;
			}
			break;
		case 60:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > 23.2f)
			{
				MC.pos_Cursor.y = 20f;
			}
			else if (Player.transform.position.y > 8f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 67:
			if (Player.transform.position.y > 72f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else if (Player.transform.position.y > 56f)
			{
				MC.pos_Cursor.y = 22f;
			}
			else
			{
				MC.pos_Cursor.y = 21f;
			}
			MC.pos_Cursor.x = 26f;
			break;
		case 68:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > 103f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 69:
			if (Player.transform.position.y > 152f)
			{
				MC.pos_Cursor.y = 28f;
			}
			else if (Player.transform.position.y > 136f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			MC.pos_Cursor.x = 26f;
			break;
		case 70:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > 198f)
			{
				MC.pos_Cursor.y = 31f;
			}
			else if (Player.transform.position.y > 182.2f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else
			{
				MC.pos_Cursor.y = 29f;
			}
			break;
		case 71:
			if (Player.transform.position.x > 128.01f)
			{
				MC.pos_Cursor.x = 5f;
			}
			else if (Player.transform.position.x > 99.57f)
			{
				MC.pos_Cursor.x = 4f;
			}
			else if (Player.transform.position.x > 71.12f)
			{
				MC.pos_Cursor.x = 3f;
			}
			else
			{
				MC.pos_Cursor.x = 2f;
			}
			if (Player.transform.position.y > 150.3f)
			{
				MC.pos_Cursor.y = 28f;
			}
			else if (Player.transform.position.y > 135.5f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			break;
		case 72:
			if (Player.transform.position.x > 298.7f)
			{
				MC.pos_Cursor.x = 11f;
			}
			else if (Player.transform.position.x > 270.26f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else if (Player.transform.position.x > 184.91f)
			{
				MC.pos_Cursor.x = 7f;
			}
			else
			{
				MC.pos_Cursor.x = 6f;
			}
			if (Player.transform.position.y > 133.5f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			break;
		case 73:
			if (Player.transform.position.x > 412.5f)
			{
				MC.pos_Cursor.x = 15f;
			}
			else if (Player.transform.position.x > 384.05f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > 355.6f)
			{
				MC.pos_Cursor.x = 13f;
			}
			else
			{
				MC.pos_Cursor.x = 12f;
			}
			if (Player.transform.position.y > 134f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			break;
		case 74:
			if (Player.transform.position.x > 536.29f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > 504.84f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > 469.39f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else
			{
				MC.pos_Cursor.x = 16f;
			}
			if (Player.transform.position.y > 134.8f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			break;
		case 76:
			if (Player.transform.position.x > 554.74f)
			{
				MC.pos_Cursor.x = 20f;
			}
			else if (Player.transform.position.x > 531.5f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > 497.84f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > 469.39f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > 440.95f)
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			if (Player.transform.position.y > 102f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 77:
			if (Player.transform.position.x > 384.05f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > 355.6f)
			{
				MC.pos_Cursor.x = 13f;
			}
			else if (Player.transform.position.x > 327.15f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else
			{
				MC.pos_Cursor.x = 11f;
			}
			MC.pos_Cursor.y = 24f;
			break;
		case 78:
			if (Player.transform.position.x > 554.74f)
			{
				MC.pos_Cursor.x = 20f;
			}
			else if (Player.transform.position.x > 526.29f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > 497.84f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > 469.39f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > 440.95f)
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			if (Player.transform.position.y > 183.5f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else
			{
				MC.pos_Cursor.y = 29f;
			}
			break;
		case 79:
			if (Player.transform.position.x > 384.05f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > 355.6f)
			{
				MC.pos_Cursor.x = 13f;
			}
			else if (Player.transform.position.x > 327.15f)
			{
				MC.pos_Cursor.x = 12f;
			}
			else
			{
				MC.pos_Cursor.x = 11f;
			}
			if (Player.transform.position.y > 165.5f)
			{
				MC.pos_Cursor.y = 29f;
			}
			else
			{
				MC.pos_Cursor.y = 28f;
			}
			break;
		case 80:
			if (Player.transform.position.x > 270.26f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else if (Player.transform.position.x > 184.91f)
			{
				MC.pos_Cursor.x = 7f;
			}
			else
			{
				MC.pos_Cursor.x = 6f;
			}
			if (Player.transform.position.y > 181f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else
			{
				MC.pos_Cursor.y = 29f;
			}
			break;
		case 81:
			MC.pos_Cursor.x = 5f;
			if (Player.transform.position.y > 193f)
			{
				if (Player.transform.position.x > 156.4f)
				{
					MC.pos_Cursor.x = 6f;
				}
				else if (Player.transform.position.x > 128f)
				{
					MC.pos_Cursor.x = 5f;
				}
				else
				{
					MC.pos_Cursor.x = 4f;
				}
				MC.pos_Cursor.y = 31f;
			}
			else if (Player.transform.position.y > 182f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else if (Player.transform.position.y > 166f)
			{
				MC.pos_Cursor.y = 29f;
			}
			else
			{
				MC.pos_Cursor.y = 28f;
			}
			break;
		case 82:
			if (Player.transform.position.x > 298.7f)
			{
				MC.pos_Cursor.x = 11f;
			}
			else if (Player.transform.position.x > 270.26f)
			{
				MC.pos_Cursor.x = 10f;
			}
			else if (Player.transform.position.x > 241.81f)
			{
				MC.pos_Cursor.x = 9f;
			}
			else if (Player.transform.position.x > 213.36f)
			{
				MC.pos_Cursor.x = 8f;
			}
			else
			{
				MC.pos_Cursor.x = 7f;
			}
			MC.pos_Cursor.y = 31f;
			break;
		case 83:
			if (Player.transform.position.x > 384.05f)
			{
				MC.pos_Cursor.x = 14f;
			}
			else if (Player.transform.position.x > 355.6f)
			{
				MC.pos_Cursor.x = 13f;
			}
			else
			{
				MC.pos_Cursor.x = 12f;
			}
			if (Player.transform.position.y > 213.8f)
			{
				MC.pos_Cursor.y = 32f;
			}
			else if (Player.transform.position.y > 200.5f)
			{
				MC.pos_Cursor.y = 31f;
			}
			else
			{
				MC.pos_Cursor.y = 30f;
			}
			break;
		case 84:
			if (Player.transform.position.x > 526.29f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else if (Player.transform.position.x > 497.84f)
			{
				MC.pos_Cursor.x = 18f;
			}
			else if (Player.transform.position.x > 469.39f)
			{
				MC.pos_Cursor.x = 17f;
			}
			else if (Player.transform.position.x > 440.95f)
			{
				MC.pos_Cursor.x = 16f;
			}
			else
			{
				MC.pos_Cursor.x = 15f;
			}
			if (Player.transform.position.y > 213.8f)
			{
				MC.pos_Cursor.y = 32f;
			}
			else if (Player.transform.position.y > 200.5f)
			{
				MC.pos_Cursor.y = 31f;
			}
			else
			{
				MC.pos_Cursor.y = 30f;
			}
			break;
		case 85:
			if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else if (Player.transform.position.x > 640.08f)
			{
				MC.pos_Cursor.x = 23f;
			}
			else if (Player.transform.position.x > 611.64f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else if (Player.transform.position.x > 583.19f)
			{
				MC.pos_Cursor.x = 21f;
			}
			else
			{
				MC.pos_Cursor.x = 20f;
			}
			if (Player.transform.position.y > 215.2f)
			{
				MC.pos_Cursor.y = 32f;
			}
			else
			{
				MC.pos_Cursor.y = 31f;
			}
			break;
		case 87:
			if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else
			{
				MC.pos_Cursor.x = 26f;
			}
			MC.pos_Cursor.y = 32f;
			break;
		case 88:
			if (Player.transform.position.x > 810.77f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			if (Player.transform.position.y > 228f)
			{
				MC.pos_Cursor.y = 33f;
			}
			else
			{
				MC.pos_Cursor.y = 32f;
			}
			break;
		case 89:
			if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else if (Player.transform.position.x > 899.5f)
			{
				MC.pos_Cursor.x = 32f;
			}
			else if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else
			{
				MC.pos_Cursor.x = 30f;
			}
			if (Player.transform.position.y > 244.8f)
			{
				MC.pos_Cursor.y = 34f;
			}
			else if (Player.transform.position.y > 229f)
			{
				MC.pos_Cursor.y = 33f;
			}
			else
			{
				MC.pos_Cursor.y = 32f;
			}
			break;
		case 91:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 29f;
			}
			else
			{
				MC.pos_Cursor.x = 28f;
			}
			MC.pos_Cursor.y = 34f;
			break;
		case 92:
			if (Player.transform.position.x > Get_Room_AThird_X(2))
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > Get_Room_AThird_X(1))
			{
				MC.pos_Cursor.x = 26f;
			}
			else
			{
				MC.pos_Cursor.x = 25f;
			}
			if (Player.transform.position.y > 261f)
			{
				MC.pos_Cursor.y = 35f;
			}
			else
			{
				MC.pos_Cursor.y = 34f;
			}
			break;
		case 93:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 24f;
			}
			else
			{
				MC.pos_Cursor.x = 23f;
			}
			MC.pos_Cursor.y = 34f;
			break;
		case 94:
			if (Player.transform.position.x > Get_Room_Half_X())
			{
				MC.pos_Cursor.x = 22f;
			}
			else
			{
				MC.pos_Cursor.x = 21f;
			}
			if (Player.transform.position.y > 248.5f)
			{
				MC.pos_Cursor.y = 34f;
			}
			else
			{
				MC.pos_Cursor.y = 33f;
			}
			break;
		case 96:
			if (Player.transform.position.x > 1038.365f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else if (Player.transform.position.x > 1009.916f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else if (Player.transform.position.x < 981.468f && Player.transform.position.y > 212f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else
			{
				MC.pos_Cursor.x = 35f;
			}
			if (Player.transform.position.y > 212f)
			{
				MC.pos_Cursor.y = 32f;
			}
			else
			{
				MC.pos_Cursor.y = 31f;
			}
			break;
		case 97:
			if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else
			{
				MC.pos_Cursor.x = 38f;
			}
			if (Player.transform.position.y > 212.5f)
			{
				MC.pos_Cursor.y = 32f;
			}
			else
			{
				MC.pos_Cursor.y = 31f;
			}
			break;
		case 98:
			if (Player.transform.position.x > 1209.055f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else if (Player.transform.position.x > 1180.607f)
			{
				MC.pos_Cursor.x = 42f;
			}
			else
			{
				MC.pos_Cursor.x = 41f;
			}
			break;
		case 99:
			if (Player.transform.position.x > 1265.95f)
			{
				MC.pos_Cursor.x = 45f;
			}
			else if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else if (Player.transform.position.x > 1209.055f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else
			{
				MC.pos_Cursor.x = 42f;
			}
			if (Player.transform.position.y > 183f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else
			{
				MC.pos_Cursor.y = 29f;
			}
			break;
		case 100:
			if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1130.3f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1089f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else if (Player.transform.position.x > 1066.81f)
			{
				MC.pos_Cursor.x = 38f;
			}
			else
			{
				MC.pos_Cursor.x = 37f;
			}
			if (Player.transform.position.y > 185f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else if (Player.transform.position.y > 167.5f)
			{
				MC.pos_Cursor.y = 29f;
			}
			else
			{
				MC.pos_Cursor.y = 28f;
			}
			break;
		case 101:
			if (Player.transform.position.x > 1009.916f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else if (Player.transform.position.x > 979.5f)
			{
				MC.pos_Cursor.x = 35f;
			}
			else if (Player.transform.position.x > 953.02f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else
			{
				MC.pos_Cursor.x = 32f;
			}
			if (Player.transform.position.y > 184f)
			{
				MC.pos_Cursor.y = 30f;
			}
			else
			{
				MC.pos_Cursor.y = 29f;
			}
			break;
		case 102:
			if (Player.transform.position.x > 1180.607f)
			{
				MC.pos_Cursor.x = 42f;
				MC.pos_Cursor.y = 27f;
				break;
			}
			if (Player.transform.position.x < 1038.365f)
			{
				MC.pos_Cursor.x = 36f;
				MC.pos_Cursor.y = 27f;
				break;
			}
			if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1090.6f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else if (Player.transform.position.x > 1066.813f)
			{
				MC.pos_Cursor.x = 38f;
			}
			else
			{
				MC.pos_Cursor.x = 37f;
			}
			if (Player.transform.position.y > 135.5f)
			{
				MC.pos_Cursor.y = 27f;
				break;
			}
			MC.pos_Cursor.x = 39f;
			MC.pos_Cursor.y = 26f;
			break;
		case 103:
			if (Player.transform.position.x > 1038.365f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else if (Player.transform.position.x > 1009.916f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else
			{
				MC.pos_Cursor.x = 35f;
			}
			if (Player.transform.position.y > 119.5f)
			{
				MC.pos_Cursor.y = 26f;
			}
			else if (Player.transform.position.y > 104f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 104:
			if (Player.transform.position.x > 953.02f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else
			{
				MC.pos_Cursor.x = 32f;
			}
			if (Player.transform.position.y > 120f)
			{
				MC.pos_Cursor.y = 26f;
			}
			else if (Player.transform.position.y > 104f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 105:
			if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else
			{
				MC.pos_Cursor.x = 38f;
			}
			if (Player.transform.position.y > 88f)
			{
				MC.pos_Cursor.y = 24f;
			}
			else if (Player.transform.position.y > 72f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 106:
			if (Player.transform.position.y > 124f)
			{
				MC.pos_Cursor.x = 42f;
				MC.pos_Cursor.y = 26f;
				break;
			}
			if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else if (Player.transform.position.x > 1209.055f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else
			{
				MC.pos_Cursor.x = 42f;
			}
			if (Player.transform.position.y > 105f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 107:
			if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else if (Player.transform.position.x > 1294.4f)
			{
				MC.pos_Cursor.x = 46f;
			}
			else
			{
				MC.pos_Cursor.x = 45f;
			}
			if (Player.transform.position.y > 119f)
			{
				MC.pos_Cursor.y = 26f;
			}
			else if (Player.transform.position.y > 103.5f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 108:
			if (Player.transform.position.x > 1408.19f)
			{
				MC.pos_Cursor.x = 50f;
			}
			else if (Player.transform.position.x > 1379.745f)
			{
				MC.pos_Cursor.x = 49f;
			}
			else
			{
				MC.pos_Cursor.x = 48f;
			}
			if (Player.transform.position.y > 104f)
			{
				MC.pos_Cursor.y = 25f;
			}
			else
			{
				MC.pos_Cursor.y = 24f;
			}
			break;
		case 109:
			if (Player.transform.position.x > 1351.3f)
			{
				MC.pos_Cursor.x = 48f;
			}
			else if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else
			{
				MC.pos_Cursor.x = 46f;
			}
			if (Player.transform.position.y > 167.5f)
			{
				MC.pos_Cursor.y = 29f;
			}
			else if (Player.transform.position.y > 152f)
			{
				MC.pos_Cursor.y = 28f;
			}
			else if (Player.transform.position.y > 136f)
			{
				MC.pos_Cursor.y = 27f;
			}
			else
			{
				MC.pos_Cursor.y = 26f;
			}
			break;
		case 111:
			if (Player.transform.position.x > 1066.81f)
			{
				MC.pos_Cursor.x = 38f;
			}
			else if (Player.transform.position.x > 1038.365f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else if (Player.transform.position.x > 1009.916f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else if (Player.transform.position.x > 981.47f)
			{
				MC.pos_Cursor.x = 35f;
			}
			else
			{
				MC.pos_Cursor.x = 34f;
			}
			if (Player.transform.position.y > 72f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 112:
			if (Player.transform.position.y > 54f)
			{
				if (Player.transform.position.x > 924.57f)
				{
					MC.pos_Cursor.x = 33f;
				}
				else if (Player.transform.position.x > 896.12f)
				{
					MC.pos_Cursor.x = 32f;
				}
				else if (Player.transform.position.x > 867.675f)
				{
					MC.pos_Cursor.x = 31f;
				}
				else
				{
					MC.pos_Cursor.x = 30f;
				}
				MC.pos_Cursor.y = 22f;
			}
			else
			{
				if (Player.transform.position.x > 896.12f)
				{
					MC.pos_Cursor.x = 32f;
				}
				else
				{
					MC.pos_Cursor.x = 31f;
				}
				MC.pos_Cursor.y = 21f;
			}
			break;
		case 113:
			if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else if (Player.transform.position.x > 1209.055f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else if (Player.transform.position.x > 1180.61f)
			{
				MC.pos_Cursor.x = 42f;
			}
			else
			{
				MC.pos_Cursor.x = 41f;
			}
			if (Player.transform.position.y > 72f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 114:
			if (Player.transform.position.x > 1351.3f)
			{
				MC.pos_Cursor.x = 48f;
			}
			else if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else if (Player.transform.position.x > 1294.4f)
			{
				MC.pos_Cursor.x = 46f;
			}
			else
			{
				MC.pos_Cursor.x = 45f;
			}
			if (Player.transform.position.y > 68.9f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 115:
			if (Player.transform.position.x < 1095.26f)
			{
				MC.pos_Cursor.x = 38f;
				MC.pos_Cursor.y = 20f;
				break;
			}
			if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else
			{
				MC.pos_Cursor.x = 39f;
			}
			if (Player.transform.position.y > 40f)
			{
				MC.pos_Cursor.y = 21f;
			}
			else
			{
				MC.pos_Cursor.y = 20f;
			}
			break;
		case 116:
			if (Player.transform.position.x > 1038.365f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else if (Player.transform.position.x > 1009.916f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else
			{
				MC.pos_Cursor.x = 35f;
			}
			if (Player.transform.position.y > 24f)
			{
				MC.pos_Cursor.y = 20f;
			}
			else if (Player.transform.position.y > 8f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 117:
			if (Player.transform.position.x > 953.02f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else
			{
				MC.pos_Cursor.x = 32f;
			}
			if (Player.transform.position.y > 8f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else if (Player.transform.position.y > -8.5f)
			{
				MC.pos_Cursor.y = 18f;
			}
			else
			{
				MC.pos_Cursor.y = 17f;
			}
			break;
		case 118:
			if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else
			{
				MC.pos_Cursor.x = 38f;
			}
			if (Player.transform.position.y > 6f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 120:
			if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else
			{
				MC.pos_Cursor.x = 43f;
			}
			break;
		case 121:
			if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else if (Player.transform.position.x > 1294.4f)
			{
				MC.pos_Cursor.x = 46f;
			}
			else
			{
				MC.pos_Cursor.x = 45f;
			}
			if (Player.transform.position.y > 7.5f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 122:
			if (Player.transform.position.x > 14.5f)
			{
				MC.pos_Cursor.x = 1f;
			}
			else
			{
				MC.pos_Cursor.x = 0f;
			}
			if (Player.transform.position.y > -12f)
			{
				MC.pos_Cursor.y = 18f;
			}
			else
			{
				MC.pos_Cursor.y = 17f;
			}
			break;
		case 123:
			if (Player.transform.position.x > 1465.09f)
			{
				MC.pos_Cursor.x = 52f;
			}
			else if (Player.transform.position.x > 1436.64f)
			{
				MC.pos_Cursor.x = 51f;
			}
			else if (Player.transform.position.x > 1408.19f)
			{
				MC.pos_Cursor.x = 50f;
			}
			else if (Player.transform.position.x > 1379.745f)
			{
				MC.pos_Cursor.x = 49f;
			}
			else
			{
				MC.pos_Cursor.x = 48f;
			}
			if (Player.transform.position.y > 8.5f)
			{
				MC.pos_Cursor.y = 19f;
			}
			else
			{
				MC.pos_Cursor.y = 18f;
			}
			break;
		case 124:
			if (Player.transform.position.y > 88f)
			{
				MC.pos_Cursor.y = 24f;
			}
			else if (Player.transform.position.y > 72f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else if (Player.transform.position.y > 56f)
			{
				MC.pos_Cursor.y = 22f;
			}
			else if (Player.transform.position.y > 40f)
			{
				MC.pos_Cursor.y = 21f;
			}
			else
			{
				MC.pos_Cursor.y = 20f;
			}
			break;
		case 126:
			if (Player.transform.position.x > 1436.64f)
			{
				MC.pos_Cursor.x = 51f;
			}
			else if (Player.transform.position.x > 1408.19f)
			{
				MC.pos_Cursor.x = 50f;
			}
			else
			{
				MC.pos_Cursor.x = 49f;
			}
			if (Player.transform.position.y > 71.5f)
			{
				MC.pos_Cursor.y = 23f;
			}
			else
			{
				MC.pos_Cursor.y = 22f;
			}
			break;
		case 127:
			if (Player.transform.position.y > -24f)
			{
				MC.pos_Cursor.y = 17f;
			}
			else if (Player.transform.position.y > -40f)
			{
				MC.pos_Cursor.y = 16f;
			}
			else
			{
				MC.pos_Cursor.y = 15f;
			}
			break;
		case 128:
			if (Player.transform.position.x > 1408.19f)
			{
				MC.pos_Cursor.x = 50f;
			}
			else
			{
				MC.pos_Cursor.x = 49f;
			}
			if (Player.transform.position.y > -23f)
			{
				MC.pos_Cursor.y = 17f;
			}
			else if (Player.transform.position.y > -38f)
			{
				MC.pos_Cursor.y = 16f;
			}
			else if (Player.transform.position.y > -53f)
			{
				MC.pos_Cursor.y = 15f;
			}
			else
			{
				MC.pos_Cursor.y = 14f;
			}
			break;
		case 129:
			if (Player.transform.position.x > 1351.3f)
			{
				MC.pos_Cursor.x = 48f;
			}
			else if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else if (Player.transform.position.x > 1294.4f)
			{
				MC.pos_Cursor.x = 46f;
			}
			else
			{
				MC.pos_Cursor.x = 45f;
			}
			if (Player.transform.position.y > -71.5f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else
			{
				MC.pos_Cursor.y = 13f;
			}
			break;
		case 130:
			if (Player.transform.position.y > -72f)
			{
				if (Player.transform.position.x > 1180.61f)
				{
					MC.pos_Cursor.x = 42f;
				}
				else
				{
					MC.pos_Cursor.x = 41f;
				}
				MC.pos_Cursor.y = 14f;
				break;
			}
			if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else if (Player.transform.position.x > 1209.05f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else if (Player.transform.position.x > 1180.61f)
			{
				MC.pos_Cursor.x = 42f;
			}
			else
			{
				MC.pos_Cursor.x = 41f;
			}
			MC.pos_Cursor.y = 13f;
			break;
		case 131:
			if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1094.5f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else
			{
				MC.pos_Cursor.x = 38f;
			}
			if (Player.transform.position.y > -72f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else if (Player.transform.position.y > -88f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			break;
		case 132:
			if (Player.transform.position.x > 1038.36f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else if (Player.transform.position.x > 1009.91f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else if (Player.transform.position.x > 981.47f)
			{
				MC.pos_Cursor.x = 35f;
			}
			else
			{
				MC.pos_Cursor.x = 34f;
			}
			if (Player.transform.position.y > -72f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else
			{
				MC.pos_Cursor.y = 13f;
			}
			break;
		case 133:
			if (Player.transform.position.y > -72f)
			{
				if (Player.transform.position.x > 924.57f)
				{
					MC.pos_Cursor.x = 33f;
				}
				else if (Player.transform.position.x > 896.12f)
				{
					MC.pos_Cursor.x = 32f;
				}
				else
				{
					MC.pos_Cursor.x = 31f;
				}
				MC.pos_Cursor.y = 14f;
				break;
			}
			if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else if (Player.transform.position.x > 896.12f)
			{
				MC.pos_Cursor.x = 32f;
			}
			else if (Player.transform.position.x > 867.67f)
			{
				MC.pos_Cursor.x = 31f;
			}
			else if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else
			{
				MC.pos_Cursor.x = 29f;
			}
			MC.pos_Cursor.y = 13f;
			break;
		case 134:
			if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else if (Player.transform.position.x > 1066.81f)
			{
				MC.pos_Cursor.x = 38f;
			}
			else
			{
				MC.pos_Cursor.x = 37f;
			}
			if (Player.transform.position.y > -120f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else if (Player.transform.position.y > -139f)
			{
				MC.pos_Cursor.y = 10f;
			}
			else
			{
				MC.pos_Cursor.y = 9f;
			}
			break;
		case 135:
			if (Player.transform.position.x > 1009.92f)
			{
				MC.pos_Cursor.x = 36f;
			}
			else if (Player.transform.position.x > 981.47f)
			{
				MC.pos_Cursor.x = 35f;
			}
			else if (Player.transform.position.x > 953.02f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else
			{
				MC.pos_Cursor.x = 33f;
			}
			if (Player.transform.position.y > -120f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			break;
		case 136:
			if (Player.transform.position.x > 1265.95f)
			{
				MC.pos_Cursor.x = 45f;
			}
			else if (Player.transform.position.x > 1237.5f)
			{
				MC.pos_Cursor.x = 44f;
			}
			else if (Player.transform.position.x > 1209.05f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else
			{
				MC.pos_Cursor.x = 42f;
			}
			if (Player.transform.position.y > -120f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			break;
		case 137:
			if (Player.transform.position.x > 1436.64f)
			{
				MC.pos_Cursor.x = 51f;
				MC.pos_Cursor.y = 11f;
				break;
			}
			if (Player.transform.position.x > 1408.19f)
			{
				MC.pos_Cursor.x = 50f;
				MC.pos_Cursor.y = 11f;
				break;
			}
			if (Player.transform.position.x > 1380f)
			{
				MC.pos_Cursor.x = 49f;
				MC.pos_Cursor.y = 11f;
				break;
			}
			if (Player.transform.position.x > 1351.3f)
			{
				MC.pos_Cursor.x = 48f;
			}
			else if (Player.transform.position.x > 1322.85f)
			{
				MC.pos_Cursor.x = 47f;
			}
			else
			{
				MC.pos_Cursor.x = 46f;
			}
			if (Player.transform.position.y > -120.5f)
			{
				MC.pos_Cursor.y = 11f;
			}
			else
			{
				MC.pos_Cursor.y = 10f;
			}
			break;
		case 138:
			MC.pos_Cursor.x = 51f;
			if (Player.transform.position.y > -72f)
			{
				MC.pos_Cursor.y = 14f;
			}
			else if (Player.transform.position.y > -95f)
			{
				MC.pos_Cursor.y = 13f;
			}
			else
			{
				MC.pos_Cursor.y = 12f;
			}
			break;
		case 139:
			if (Player.transform.position.x > 1209.05f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else if (Player.transform.position.x > 1180.61f)
			{
				MC.pos_Cursor.x = 42f;
			}
			else if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else
			{
				MC.pos_Cursor.x = 38f;
			}
			MC.pos_Cursor.y = 8f;
			break;
		case 141:
			if (Player.transform.position.x > 1265.95f && Player.transform.position.y > -185.2f)
			{
				MC.pos_Cursor.x = 45f;
			}
			else
			{
				MC.pos_Cursor.x = 44f;
			}
			if (Player.transform.position.y > -167.5f)
			{
				MC.pos_Cursor.y = 8f;
			}
			else if (Player.transform.position.y > -185.2f)
			{
				MC.pos_Cursor.y = 7f;
			}
			else
			{
				MC.pos_Cursor.y = 6f;
			}
			break;
		case 143:
			if (Player.transform.position.x > 1209.05f)
			{
				MC.pos_Cursor.x = 43f;
			}
			else if (Player.transform.position.x > 1180.61f)
			{
				MC.pos_Cursor.x = 42f;
			}
			else if (Player.transform.position.x > 1152.16f)
			{
				MC.pos_Cursor.x = 41f;
			}
			else if (Player.transform.position.x > 1123.71f)
			{
				MC.pos_Cursor.x = 40f;
			}
			else if (Player.transform.position.x > 1095.26f)
			{
				MC.pos_Cursor.x = 39f;
			}
			else if (Player.transform.position.x > 1066.81f)
			{
				MC.pos_Cursor.x = 38f;
			}
			else if (Player.transform.position.x > 1038.36f)
			{
				MC.pos_Cursor.x = 37f;
			}
			else
			{
				MC.pos_Cursor.x = 36f;
			}
			MC.pos_Cursor.y = 6f;
			break;
		case 145:
			if (Player.transform.position.x > 981.47f)
			{
				MC.pos_Cursor.x = 35f;
			}
			else if (Player.transform.position.x > 953.02f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else if (Player.transform.position.x > 924.57f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else if (Player.transform.position.x > 896.12f)
			{
				MC.pos_Cursor.x = 32f;
			}
			else
			{
				MC.pos_Cursor.x = 31f;
			}
			MC.pos_Cursor.y = 6f;
			break;
		case 146:
			if (Player.transform.position.x > 839.22f)
			{
				MC.pos_Cursor.x = 30f;
			}
			else if (Player.transform.position.x > 810.78f)
			{
				MC.pos_Cursor.x = 29f;
			}
			else if (Player.transform.position.x > 782.33f)
			{
				MC.pos_Cursor.x = 28f;
			}
			else if (Player.transform.position.x > 753.88f)
			{
				MC.pos_Cursor.x = 27f;
			}
			else if (Player.transform.position.x > 725.43f)
			{
				MC.pos_Cursor.x = 26f;
			}
			else if (Player.transform.position.x > 696.98f)
			{
				MC.pos_Cursor.x = 25f;
			}
			else if (Player.transform.position.x > 668.53f)
			{
				MC.pos_Cursor.x = 24f;
			}
			else
			{
				MC.pos_Cursor.x = 23f;
			}
			MC.pos_Cursor.y = 6f;
			break;
		case 148:
			if (Player.transform.position.x > 611.64f && Player.transform.position.y > -199f)
			{
				MC.pos_Cursor.x = 22f;
			}
			else if (Player.transform.position.x > 583.19f)
			{
				MC.pos_Cursor.x = 21f;
			}
			else if (Player.transform.position.x > 554.74f)
			{
				MC.pos_Cursor.x = 20f;
			}
			else if (Player.transform.position.x > 526.29f)
			{
				MC.pos_Cursor.x = 19f;
			}
			else
			{
				MC.pos_Cursor.x = 18f;
			}
			if (Player.transform.position.y > -199f)
			{
				MC.pos_Cursor.y = 6f;
			}
			else
			{
				MC.pos_Cursor.y = 5f;
			}
			break;
		case 149:
			MC.pos_Cursor.x = 33f;
			if (Player.transform.position.y > -216f)
			{
				MC.pos_Cursor.y = 5f;
			}
			else if (Player.transform.position.y > -236.3f)
			{
				MC.pos_Cursor.y = 4f;
			}
			else
			{
				MC.pos_Cursor.y = 3f;
			}
			break;
		case 150:
			if (Player.transform.position.x > 960.12f)
			{
				MC.pos_Cursor.x = 34f;
			}
			else if (Player.transform.position.x > 917.46f)
			{
				MC.pos_Cursor.x = 33f;
			}
			else
			{
				MC.pos_Cursor.x = 32f;
			}
			if (Player.transform.position.y > -273f)
			{
				MC.pos_Cursor.y = 2f;
			}
			else
			{
				MC.pos_Cursor.y = 1f;
			}
			break;
		}
		if (MC.pos_Cursor != PrevCursor)
		{
			MC.Change_MiniMap();
		}
	}

	private void Debug_Show_XY()
	{
		global::UnityEngine.Debug.Log("====== Room : " + GM.Room_Num + "\n");
		global::UnityEngine.Debug.Log("1/2_X : " + Get_Room_Half_X() + ",     1/2 : " + Get_Room_Half_Y());
		global::UnityEngine.Debug.Log(" \n");
		global::UnityEngine.Debug.Log("2/3 X : " + Get_Room_AThird_X(2) + ",     2/3 : " + Get_Room_AThird_Y(2));
		global::UnityEngine.Debug.Log("1/3 X : " + Get_Room_AThird_X(1) + ",     1/3 : " + Get_Room_AThird_Y(1));
		global::UnityEngine.Debug.Log(" \n");
		global::UnityEngine.Debug.Log("3/4 X : " + Get_Room_AQuarter_X(3) + ",     3/4 : " + Get_Room_AQuarter_Y(3));
		global::UnityEngine.Debug.Log("2/4 X : " + Get_Room_AQuarter_X(2) + ",     2/4 : " + Get_Room_AQuarter_Y(2));
		global::UnityEngine.Debug.Log("1/4 X : " + Get_Room_AQuarter_X(1) + ",     1/4 : " + Get_Room_AQuarter_Y(1));
		global::UnityEngine.Debug.Log(" \n");
		global::UnityEngine.Debug.Log("4/5 X : " + Get_Room_AFifth_X(4) + ",     4/5 : " + Get_Room_AFifth_Y(4));
		global::UnityEngine.Debug.Log("3/5 X : " + Get_Room_AFifth_X(3) + ",     3/5 : " + Get_Room_AFifth_Y(3));
		global::UnityEngine.Debug.Log("2/5 X : " + Get_Room_AFifth_X(2) + ",     2/5 : " + Get_Room_AFifth_Y(2));
		global::UnityEngine.Debug.Log("1/5 X : " + Get_Room_AFifth_X(1) + ",     1/5 : " + Get_Room_AFifth_Y(1));
		global::UnityEngine.Debug.Log(" \n");
		global::UnityEngine.Debug.Log("5/6 X : " + Get_Room_ASixth_X(5) + ",     5/6 : " + Get_Room_ASixth_Y(5));
		global::UnityEngine.Debug.Log("4/6 X : " + Get_Room_ASixth_X(4) + ",     4/6 : " + Get_Room_ASixth_Y(4));
		global::UnityEngine.Debug.Log("3/6 X : " + Get_Room_ASixth_X(3) + ",     3/6 : " + Get_Room_ASixth_Y(3));
		global::UnityEngine.Debug.Log("2/6 X : " + Get_Room_ASixth_X(2) + ",     2/6 : " + Get_Room_ASixth_Y(2));
		global::UnityEngine.Debug.Log("1/6 X : " + Get_Room_ASixth_X(1) + ",     1/6 : " + Get_Room_ASixth_Y(1));
		global::UnityEngine.Debug.Log("======\n\n");
	}

	private void Set_Cam_LRTopBot()
	{
		RoomCam_L = Current_Room.GetComponent<Room_Control>().cam_Left.position.x;
		RoomCam_R = Current_Room.GetComponent<Room_Control>().cam_Right.position.x;
		RoomCam_Top = Current_Room.GetComponent<Room_Control>().cam_Top.position.y;
		RoomCam_Bot = Current_Room.GetComponent<Room_Control>().cam_Bot.position.y;
	}

	private float Get_Room_Half_X()
	{
		return (RoomCam_R + RoomCam_L) / 2f;
	}

	private float Get_Room_Half_Y()
	{
		return (RoomCam_Top + RoomCam_Bot) / 2f;
	}

	private float Get_Room_AThird_X(int num)
	{
		return (RoomCam_R - RoomCam_L) / 3f * (float)num + RoomCam_L;
	}

	private float Get_Room_AThird_Y(int num)
	{
		return (RoomCam_Top - RoomCam_Bot) / 3f * (float)num + RoomCam_Bot;
	}

	private float Get_Room_AQuarter_X(int num)
	{
		return (RoomCam_R - RoomCam_L) / 4f * (float)num + RoomCam_L;
	}

	private float Get_Room_AQuarter_Y(int num)
	{
		return (RoomCam_Top - RoomCam_Bot) / 4f * (float)num + RoomCam_Bot;
	}

	private float Get_Room_AFifth_X(int num)
	{
		return (RoomCam_R - RoomCam_L) / 5f * (float)num + RoomCam_L;
	}

	private float Get_Room_AFifth_Y(int num)
	{
		return (RoomCam_Top - RoomCam_Bot) / 5f * (float)num + RoomCam_Bot;
	}

	private float Get_Room_ASixth_X(int num)
	{
		return (RoomCam_R - RoomCam_L) / 6f * (float)num + RoomCam_L;
	}

	private float Get_Room_ASixth_Y(int num)
	{
		return (RoomCam_Top - RoomCam_Bot) / 6f * (float)num + RoomCam_Bot;
	}
}
