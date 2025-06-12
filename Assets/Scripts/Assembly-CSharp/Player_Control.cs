using UnityEngine;

public class Player_Control : global::UnityEngine.MonoBehaviour
{
    public static Player_Control instance { get; set; }
    public enum AniState
    {
        Idle = 0,
        Run = 1,
        Sit = 2,
        Jump = 3,
        Spin = 4,
        Slide = 5,
        BackDash = 6,
        Damage = 7,
        Down = 8,
        Scene = 9
    }

    public Player_Control.AniState State;

    public int facingRight = 1;

    public bool onAttack;

    public bool grounded_Now;

    private bool grounded_Last;

    private bool grounded_Front;

    private bool grounded_Rear;

    private bool grounded_Slide;

    private bool grounded_Dash;

    private bool grounded_Dash2;

    public int Jump_Num;

    public bool onRolling;

    public bool onJumpDrop;

    public float Jump_Pos_Y;

    private float Drop_Pos_Y;

    private float ErrorJump_Timer;

    private float ErrorDrop_Timer;

    private float DropJump_Delay;

    private float ErrorGround_Timer;

    public float Lock_Timer;

    private bool onSitLock;

    private float Attack_Timer;

    private float AttackBelow_Timer;

    private float Attack_Delay;

    public int Attack_Num;

    private float Damage_Timer;

    public bool onFlicker;

    private float Flicker_Timer;

    private float Flicker_Delay;

    private float Slide_Timer;

    private float SlideJump_Speed;

    private float BackDash_Timer;

    private float BackDash_Speed;

    private float BackDashDelay;

    private float Spin_Timer;

    private bool onDash_Pad;

    private bool onDash_Keyboard;

    private float Dash_Timer;

    private float Lag_Timer;

    private float BD_Lag_Timer;

    private float HJ_Lag_Timer;

    private float HighJump_Timer;

    public bool onHighJump;

    private bool onPoison;

    private float color_Timer;

    private global::UnityEngine.Color color_Player = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

    private float color_R = 1f;

    private global::UnityEngine.Color color_On = new global::UnityEngine.Color(1f, 1f, 1f, 1f);

    private float BoxCol_SizeY = 5f;

    public bool onScrewAttack;

    public float Screw_Opacity;

    private float Screw_Speed = 1f;

    private float Screw_Delay;

    private float Screw_Timer;

    private float inputX;

    private float prevX;

    private float pre_Pos_X;

    private float inputY;

    private float prevY;

    private float Move_Speed = 1f;

    private float Move_FrRate = 0.03f;

    private float Accel;

    private float Down_Timer;

    private int Num_Down_Impact = 1;

    private float DownUp_Timer;

    private global::UnityEngine.Vector3 prePosition = new global::UnityEngine.Vector3(0f, 0f, 0f);

    private global::UnityEngine.Vector2 velocity = new global::UnityEngine.Vector2(0f, 0f);

    public float Speed_X;

    public float Speed_Y;

    public bool[] Lock_Lift = new bool[2];

    public float[] Pos_Lift = new float[2];

    public global::UnityEngine.Sprite Spr_Effect_Rolling;

    public global::UnityEngine.GameObject[] Effect_Attack;

    public global::UnityEngine.GameObject Effect_Lag;

    public global::UnityEngine.GameObject Effect_BackDash;

    public global::UnityEngine.GameObject Magic_1;

    public global::UnityEngine.GameObject Magic_2;

    public global::UnityEngine.GameObject Magic_3;

    public global::UnityEngine.GameObject Magic_4;

    public global::UnityEngine.GameObject Magic_5;

    public global::UnityEngine.Transform groundedStart;

    public global::UnityEngine.Transform groundedEnd;

    public global::UnityEngine.Transform groundedEndDeath;

    public global::UnityEngine.Transform frontStart;

    public global::UnityEngine.Transform frontEnd;

    public global::UnityEngine.Transform rearStart;

    public global::UnityEngine.Transform rearEnd;

    public global::UnityEngine.Transform posSitLock_1;

    public global::UnityEngine.Transform posSitLock_2;

    public global::UnityEngine.Transform posSitLock_1C;

    public global::UnityEngine.Transform posSitLock_2C;

    public global::UnityEngine.Transform checkFront;

    public global::UnityEngine.Transform checkBack;

    public global::UnityEngine.Transform checkBack2;

    public global::UnityEngine.GameObject player_ani;

    public global::UnityEngine.GameObject Ani_rolling;

    public global::UnityEngine.GameObject Effect_rolling;

    public global::UnityEngine.GameObject Glow_rolling;

    public global::UnityEngine.GameObject Border_rolling;

    public global::UnityEngine.Material Ani_Down_Mat;

    public global::UnityEngine.SpriteRenderer Ani_Down_Foot;

    public global::UnityEngine.SpriteRenderer Ani_Down_Mouth;

    public global::UnityEngine.SpriteRenderer Ani_Eye;

    public global::UnityEngine.BoxCollider2D Col_Top;

    public global::UnityEngine.BoxCollider2D Col_Bot;

    public global::UnityEngine.BoxCollider2D Col_Jump2;

    private global::UnityEngine.GameObject PlayerSound;

    private H_SoundControl H_Sound;

    private int Moan_Num;

    GameManager GM => GameManager.instance;

    private Custom_Key CK => GameManager.instance.CK;

    private Player_Ani Ani;

    private void Awake()
    {
        instance = this;
    }

    private void OnDestroy()
    {
        instance = null;
    }

    private void Start()
    {
        //GM = global::UnityEngine.GameObject.Find("GameManager").GetComponent<GameManager>();
        //CK = global::UnityEngine.GameObject.Find("GameManager").GetComponent<Custom_Key>();
        PlayerSound = global::UnityEngine.GameObject.Find("Player_SoundList");
        H_Sound = global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>();
        Ani = player_ani.GetComponent<Player_Ani>();
        base.transform.position = global::UnityEngine.GameObject.Find("Player_Locker").transform.position;
        prePosition = base.transform.position;
    }

    private void FixedUpdate()
    {
        if (GM.Paused || GM.onGatePass || GM.onSave || GM.onTeleport || GM.onEvent || GM.onConsole || GM.GameOver || State == Player_Control.AniState.Down)
        {
            if (GM.Paused && !base.GetComponent<UnityEngine.Rigidbody2D>().IsSleeping())
            {
                base.GetComponent<UnityEngine.Rigidbody2D>().Sleep();
            }
            return;
        }
        if (State != Player_Control.AniState.Scene && State != Player_Control.AniState.Damage && State != Player_Control.AniState.Slide && State != Player_Control.AniState.BackDash)
        {
            if (inputX != 0f && State == Player_Control.AniState.Run && !onAttack)
            {
                if (!grounded_Slide)
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 18f * inputX * Move_Speed);
                }
            }
            else if (inputX != 0f && State == Player_Control.AniState.Spin)
            {
                if (!grounded_Slide)
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 13f * facingRight * Move_Speed);
                }
            }
            else if ((global::UnityEngine.Input.GetButton("Jump") || global::UnityEngine.Input.GetKey(CK.Jump)) && State == Player_Control.AniState.Jump && onRolling && onScrewAttack)
            {
                if (inputX != 0f && !grounded_Slide)
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 30f * facingRight * Move_Speed);
                }
                if (GM.onScrew && GM.MP > 10 && Screw_Opacity > 0f)
                {
                    if (inputY > 0f)
                    {
                        if (onJumpDrop)
                        {
                            if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y < 0f)
                            {
                                base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f);
                            }
                            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 70f * global::UnityEngine.Time.deltaTime, global::UnityEngine.ForceMode2D.Impulse);
                        }
                        else
                        {
                            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 30f * global::UnityEngine.Time.deltaTime, global::UnityEngine.ForceMode2D.Impulse);
                        }
                    }
                    else if (onJumpDrop)
                    {
                        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 2f);
                    }
                }
            }
            else if (inputX != 0f && State == Player_Control.AniState.Jump && !grounded_Slide)
            {
                if (Accel < 0.1f)
                {
                    if ((global::UnityEngine.Input.GetAxis("L_Y") == 0f || !(global::UnityEngine.Input.GetAxis("L_Y") < -0.8f)) && !global::UnityEngine.Input.GetKey(CK.Down))
                    {
                        base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 13f * facingRight * Move_Speed);
                    }
                }
                else
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 13f * facingRight * Move_Speed);
                }
            }
        }
        if (inputX != 0f)
        {
            GM.Velcocity.x = global::UnityEngine.Mathf.Lerp(GM.Velcocity.x, Move_Speed, global::UnityEngine.Time.deltaTime * 2f * Move_Speed);
        }
        else
        {
            GM.Velcocity.x = global::UnityEngine.Mathf.Lerp(GM.Velcocity.x, 0f, global::UnityEngine.Time.deltaTime * 2f);
        }
        GM.Velcocity.y = base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y;
        prePosition = base.transform.position;
        if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x != 0f)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(global::UnityEngine.Mathf.Lerp(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f, global::UnityEngine.Time.deltaTime * 3f), base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
        }
        if (grounded_Now)
        {
            if (State != Player_Control.AniState.Jump)
            {
                global::UnityEngine.RaycastHit2D raycastHit2D = global::UnityEngine.Physics2D.Linecast(groundedStart.position, groundedEnd.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
                if (raycastHit2D.collider != null)
                {
                    if (raycastHit2D.collider.GetComponent<Tile_Lift>() != null)
                    {
                        if (raycastHit2D.collider.GetComponent<Tile_Lift>().Type != 1)
                        {
                            base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f);
                            base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x, raycastHit2D.collider.transform.position.y + 0.35f, 0f);
                            Lock_Lift[1] = true;
                            Pos_Lift[1] = raycastHit2D.collider.transform.position.y;
                        }
                        else if (inputX == 0f)
                        {
                            base.transform.position = new global::UnityEngine.Vector3(base.transform.position.x + raycastHit2D.collider.GetComponent<Tile_Lift>().Speed_for_PC * global::UnityEngine.Time.deltaTime, base.transform.position.y, 0f);
                            Lock_Lift[0] = true;
                            Pos_Lift[0] = raycastHit2D.collider.transform.position.x;
                        }
                        global::UnityEngine.Debug.Log("hit.collider.GetComponent<Tile_Lift>() != null");
                    }
                    else
                    {
                        Lock_Lift[0] = (Lock_Lift[1] = false);
                    }
                }
                else
                {
                    Lock_Lift[0] = (Lock_Lift[1] = false);
                }
            }
            else
            {
                Lock_Lift[0] = (Lock_Lift[1] = false);
            }
            if (Jump_Num > 1 && Screw_Opacity <= 0f)
            {
                ErrorGround_Timer += global::UnityEngine.Time.deltaTime;
                if (ErrorGround_Timer > 0.8f)
                {
                    Jump_Num = 0;
                    ErrorGround_Timer = 0f;
                    global::UnityEngine.Debug.Log("ERROR GROUND Jump_Num!!!!!");
                }
            }
            else
            {
                ErrorGround_Timer = 0f;
            }
        }
        else
        {
            Lock_Lift[0] = (Lock_Lift[1] = false);
        }
    }

    private void Set_Poison_Color()
    {
        if (!onPoison)
        {
            onPoison = true;
        }
        color_Timer += global::UnityEngine.Time.deltaTime;
        color_Player = new global::UnityEngine.Color(0.9f + global::UnityEngine.Mathf.Sin(color_Timer * 5f) * 0.1f, 0.75f, 1f, player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color.a);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Player;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Player;
        Ani_Down_Mat.color = color_Player;
        Ani_Down_Foot.color = color_Player;
        Ani_Down_Mouth.color = color_Player;
        Ani_Eye.color = color_Player;
    }

    private void End_Poison_Color()
    {
        onPoison = false;
        color_Player = new global::UnityEngine.Color(1f, 1f, 1f, 1f);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Player;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = color_Player;
        Ani_Down_Mat.color = color_Player;
        Ani_Down_Foot.color = color_Player;
        Ani_Down_Mouth.color = color_Player;
        Ani_Eye.color = color_Player;
    }

    private void Update()
    {
        if (GM.onPoison)
        {
            Set_Poison_Color();
        }
        else if (onPoison)
        {
            End_Poison_Color();
        }
        if (GM.Paused || GM.onGatePass || GM.onSave || GM.onTeleport || GM.onConsole)
        {
            return;
        }
        if (State == Player_Control.AniState.Down)
        {
            Down_Timer += global::UnityEngine.Time.deltaTime;
            if (!GM.onDown && !GM.onHscene && GM.HP > 0 && DownUp_Timer > 0.2f)
            {
                GetUp();
            }
            else if (!GM.onHscene && Down_Timer > 0.5f)
            {
                grounded_Now = global::UnityEngine.Physics2D.Linecast(groundedStart.position, groundedEnd.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
                if (Num_Down_Impact > 0 && grounded_Now && (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y == 0f || global::UnityEngine.Mathf.Abs(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y) < 0.05f))
                {
                    base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.x, 0f);
                    base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 7f * Num_Down_Impact, global::UnityEngine.ForceMode2D.Impulse);
                    Num_Down_Impact--;
                    Ani.Set_Down_Bounce();
                }
                else if (grounded_Now && base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y == 0f)
                {
                    Ani.Set_Down_Down();
                    DownUp_Timer += global::UnityEngine.Time.deltaTime;
                }
            }
            return;
        }
        if (Attack_Timer > 0f)
        {
            Attack_Timer -= global::UnityEngine.Time.deltaTime;
        }
        else
        {
            Attack_Num = 0;
        }
        if (Attack_Delay > 0f)
        {
            Attack_Delay -= global::UnityEngine.Time.deltaTime;
        }
        if (AttackBelow_Timer > 0f)
        {
            AttackBelow_Timer -= global::UnityEngine.Time.deltaTime;
        }
        if (BackDashDelay > 0f)
        {
            BackDashDelay -= global::UnityEngine.Time.deltaTime;
        }
        if (DropJump_Delay > 0f)
        {
            DropJump_Delay -= global::UnityEngine.Time.deltaTime;
        }
        if (HighJump_Timer > 0f)
        {
            HighJump_Timer -= global::UnityEngine.Time.deltaTime;
        }
        if (Damage_Timer > 0f)
        {
            Damage_Timer -= global::UnityEngine.Time.deltaTime;
        }
        if (onDash_Pad)
        {
            Check_SpeedUp_Pad();
        }
        else if (GM.onSpeedUp && GM.MP > 5 && (global::UnityEngine.Input.GetButton("R_B") || global::UnityEngine.Input.GetKey(CK.RB)))
        {
            onDash_Pad = true;
            onDash_Keyboard = false;
            Dash_Timer = 0f;
            Move_Speed = 1.6f;
            Move_FrRate = 0.01f;
            Ani.Set_Run_Speed(Move_FrRate);
        }
        if (onFlicker)
        {
            if (Flicker_Timer > 1.5f)
            {
                Damage_Flicker();
            }
            else if (Flicker_Timer > 0f)
            {
                Damage_Flicker_2();
            }
            else
            {
                End_Flicker();
            }
        }
        inputX = 0f;
        inputY = 0f;
        if (global::UnityEngine.Input.GetKey(CK.Right))
        {
            inputX = 1f;
        }
        else if (global::UnityEngine.Input.GetKey(CK.Left))
        {
            inputX = -1f;
        }
        if (global::UnityEngine.Input.GetKey(CK.Up))
        {
            inputY = 1f;
        }
        else if (global::UnityEngine.Input.GetKey(CK.Down))
        {
            inputY = -1f;
        }
        if (global::UnityEngine.Input.GetAxis("L_X") != 0f)
        {
            inputX = global::UnityEngine.Input.GetAxis("L_X");
        }
        if (global::UnityEngine.Input.GetAxis("L_Y") != 0f)
        {
            inputY = global::UnityEngine.Input.GetAxis("L_Y");
        }
        if (!GM.onEvent && State != Player_Control.AniState.Scene && State != Player_Control.AniState.Damage && State != Player_Control.AniState.Slide && !Ani.Check_FlipLock())
        {
            if (facingRight > 0 && inputX < 0f)
            {
                Flip();
            }
            else if (facingRight < 0 && inputX > 0f)
            {
                Flip();
            }
        }
        if (State != Player_Control.AniState.Scene && State != Player_Control.AniState.Damage && State != Player_Control.AniState.Slide && State != Player_Control.AniState.Spin && State != Player_Control.AniState.BackDash)
        {
            if (!GM.onEvent && GM.MP >= 10 && State != Player_Control.AniState.Sit && State != Player_Control.AniState.Spin && (global::UnityEngine.Input.GetAxis("L_Trigger") > 0f || global::UnityEngine.Input.GetKey(CK.Spin)))
            {
                State = Player_Control.AniState.Spin;
                Ani.Set_Spin();
                Spin_Timer = 0.3f;
                if (Jump_Num == 2)
                {
                    Jump2_End();
                }
            }
            else
            {
                if (!GM.onEvent && (State == Player_Control.AniState.Idle || State == Player_Control.AniState.Run || State == Player_Control.AniState.Jump))
                {
                    if (!onHighJump && GM.onHighJump && inputY > 0f && (global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetKeyDown(CK.LB)))
                    {
                        High_Jump();
                    }
                    else if ((global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetKeyDown(CK.Jump)) && Lock_Timer <= 0f && Jump_Num < 2)
                    {
                        if (Jump_Num < 1)
                        {
                            Jump();
                        }
                        else if (Jump_Num == 1 && DropJump_Delay > 0f)
                        {
                            Jump_Num = 0;
                            DropJump_Delay = 0f;
                            Jump();
                        }
                        else if (GM.onDBJump)
                        {
                            Jump();
                        }
                    }
                }
                if (Lock_Timer > 0f)
                {
                    Lock_Timer -= global::UnityEngine.Time.deltaTime;
                }
                if (!GM.onEvent && (global::UnityEngine.Input.GetButtonDown("_X") || global::UnityEngine.Input.GetKeyDown(CK.Attack)) && Attack_Delay <= 0f && Lock_Timer <= 0f)
                {
                    if (Jump_Num == 2)
                    {
                        Jump2_End();
                    }
                    BackDashDelay = 0.3f;
                    if (AttackBelow_Timer <= 0f && State == Player_Control.AniState.Jump && inputY < 0f)
                    {
                        onAttack = true;
                        Attack_Timer = 0f;
                        Attack_Delay = 0.3f;
                        AttackBelow_Timer = 0.3f;
                        Sound_Attack();
                        if (inputX == 0f)
                        {
                            Ani.Set_Attack_Down2();
                            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Effect_Attack[GM.Weapon_Num], new global::UnityEngine.Vector3(base.transform.position.x - 4f * (float)facingRight, base.transform.position.y + 2.8f, base.transform.position.z), global::UnityEngine.Quaternion.Euler(0f, 0f, -90f)) as global::UnityEngine.GameObject;
                            gameObject.transform.parent = base.transform;
                            gameObject.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
                        }
                        else
                        {
                            Ani.Set_Attack_Down();
                            global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Effect_Attack[GM.Weapon_Num], new global::UnityEngine.Vector3(base.transform.position.x - 1f * (float)facingRight, base.transform.position.y, base.transform.position.z), global::UnityEngine.Quaternion.Euler(0f, 0f, -32f)) as global::UnityEngine.GameObject;
                            gameObject2.transform.parent = base.transform;
                            gameObject2.transform.localScale = new global::UnityEngine.Vector3(1f, 1f, 1f);
                        }
                    }
                    else if (State == Player_Control.AniState.Sit)
                    {
                        Attack_Num = 0;
                        Attack_Timer = 0f;
                        Attack_Delay = 0.3f;
                        onAttack = true;
                        Ani.Set_Attack();
                    }
                    else
                    {
                        if (Attack_Num == 0)
                        {
                            Attack_Num++;
                            Attack_Timer = 0.5f;
                            Attack_Delay = 0f;
                            Ani.Set_Attack();
                        }
                        else if (Attack_Num == 1)
                        {
                            Attack_Num++;
                            Attack_Timer = 0.5f;
                            Attack_Delay = 0f;
                            Ani.Set_Attack_2();
                        }
                        else if (Attack_Num == 2)
                        {
                            Attack_Num++;
                            Attack_Timer = 0.5f;
                            Attack_Delay = 0f;
                            Ani.Set_Attack_3();
                        }
                        else
                        {
                            Attack_Num = 0;
                            Attack_Timer = 0f;
                            Attack_Delay = 0.6f;
                            Ani.Set_Attack_4();
                        }
                        AttackBelow_Timer = 0.3f;
                        onAttack = true;
                    }
                }
                if (!GM.onEvent && Attack_Delay <= 0f && (global::UnityEngine.Input.GetButtonDown("_Y") || global::UnityEngine.Input.GetKeyDown(CK.Skill)) && GM.Check_Skill())
                {
                    GM.MP_Skill();
                    if (GM.Skill_Num != 2 && GM.Skill_Num != 4)
                    {
                        onAttack = true;
                        Attack_Delay = 0.2f;
                        Ani.Set_Throw();
                        Sound_Attack();
                    }
                    Fire_Magic();
                }
            }
        }
        switch (State)
        {
            case Player_Control.AniState.Damage:
                if (!(Damage_Timer <= 0f))
                {
                    break;
                }
                if (grounded_Now)
                {
                    if (grounded_Slide && grounded_Dash && (bool)global::UnityEngine.Physics2D.Linecast(posSitLock_1.position, posSitLock_1C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")) && (bool)global::UnityEngine.Physics2D.Linecast(posSitLock_2.position, posSitLock_2C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
                    {
                        onSitLock = true;
                        State = Player_Control.AniState.Sit;
                        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
                        Ani.Set_Sit();
                    }
                    else
                    {
                        onSitLock = false;
                        State = Player_Control.AniState.Idle;
                        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
                        Ani.Set_Idle();
                    }
                }
                else
                {
                    State = Player_Control.AniState.Jump;
                    base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y);
                    Ani.Set_Jump();
                }
                break;
            case Player_Control.AniState.BackDash:
                BackDash_Timer += global::UnityEngine.Time.deltaTime;
                if (BackDash_Timer > 0.2f)
                {
                    if (grounded_Now)
                    {
                        State = Player_Control.AniState.Idle;
                        Ani.Set_Jump_Short();
                        BackDash_Timer = 0f;
                    }
                }
                else if (grounded_Now && !grounded_Dash && !grounded_Dash2)
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 35f * facingRight * -1f);
                }
                else
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 15f * facingRight * -1f);
                }
                Make_BackDashLag();
                break;
            case Player_Control.AniState.Slide:
                Slide_Timer += global::UnityEngine.Time.deltaTime;
                if (Slide_Timer > 0.2f)
                {
                    if (grounded_Now)
                    {
                        State = Player_Control.AniState.Sit;
                        Ani.End_Slide();
                        Slide_Timer = 0f;
                    }
                }
                else if (!grounded_Slide)
                {
                    base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * 35f * facingRight * 1f);
                }
                if (!Col_Bot.enabled)
                {
                    Col_Bot.enabled = true;
                }
                if (Col_Top.enabled)
                {
                    Col_Top.enabled = false;
                }
                if (Col_Jump2.enabled)
                {
                    Col_Jump2.enabled = false;
                }
                break;
            case Player_Control.AniState.Spin:
                Spin_Timer -= global::UnityEngine.Time.deltaTime;
                GM.MP_Spin();
                if (GM.MP < 3 || (Spin_Timer <= 0f && global::UnityEngine.Input.GetAxis("L_Trigger") <= 0f && !global::UnityEngine.Input.GetKey(CK.Spin)))
                {
                    if (grounded_Now)
                    {
                        State = Player_Control.AniState.Idle;
                        Ani.Set_Spin_End_Idle();
                    }
                    else
                    {
                        State = Player_Control.AniState.Jump;
                        Ani.Set_Spin_End_Jump();
                    }
                    onAttack = false;
                }
                if (!onJumpDrop && Jump_Pos_Y > base.transform.position.y)
                {
                    onJumpDrop = true;
                    Drop_Pos_Y = Jump_Pos_Y;
                }
                Jump_Pos_Y = base.transform.position.y;
                break;
            case Player_Control.AniState.Idle:
                if (!GM.onEvent && GM.onBackDash && BackDashDelay <= 0f && (global::UnityEngine.Input.GetButtonDown("L_B") || global::UnityEngine.Input.GetKeyDown(CK.LB)))
                {
                    State = Player_Control.AniState.BackDash;
                    Ani.Set_BackDash();
                    BackDash_Timer = 0f;
                    BackDash_Speed = 0f;
                    onAttack = false;
                    BackDashDelay = 0.2f;
                    Sound_Slide();
                }
                else if (!GM.onEvent && !onAttack && inputX != 0f)
                {
                    State = Player_Control.AniState.Run;
                    Ani.Set_Run();
                }
                else if (!GM.onEvent && !onAttack && inputY < 0f)
                {
                    State = Player_Control.AniState.Sit;
                    Ani.Set_Sit();
                }
                if (Slide_Timer > 0f)
                {
                    Slide_Timer = 0f;
                    SlideJump_Speed = 0f;
                }
                break;
            case Player_Control.AniState.Run:
                if (GM.onEvent)
                {
                    State = Player_Control.AniState.Idle;
                    Ani.Set_Run_Stop();
                }
                else if (!onAttack && inputY < -0.6f)
                {
                    State = Player_Control.AniState.Sit;
                    Ani.Set_Sit();
                }
                else if (!onAttack && inputX == 0f)
                {
                    State = Player_Control.AniState.Idle;
                    Ani.Set_Run_Stop();
                }
                break;
            case Player_Control.AniState.Sit:
                if (!GM.onEvent && inputY < 0f)
                {
                    if (global::UnityEngine.Input.GetButtonDown("Jump") || global::UnityEngine.Input.GetKeyDown(CK.Jump))
                    {
                        State = Player_Control.AniState.Slide;
                        Ani.Set_Slide();
                        Slide_Timer = 0f;
                        SlideJump_Speed = 0f;
                        onAttack = false;
                        Sound_Slide();
                    }
                }
                else if (!onSitLock && !GM.onEvent && !onAttack && global::UnityEngine.Mathf.Abs(inputX) > 0.6f && inputY > -0.6f)
                {
                    State = Player_Control.AniState.Run;
                    Ani.Set_Run();
                }
                else if (!onSitLock && !GM.onEvent && !onAttack && inputY == 0f)
                {
                    State = Player_Control.AniState.Idle;
                    Ani.Set_SitUp();
                }
                if (!Col_Bot.enabled)
                {
                    Col_Bot.enabled = true;
                }
                if (Col_Top.enabled)
                {
                    Col_Top.enabled = false;
                }
                if (Col_Jump2.enabled)
                {
                    Col_Jump2.enabled = false;
                }
                break;
            case Player_Control.AniState.Jump:
                if (HighJump_Timer > 0f)
                {
                    Make_HJump_Lag();
                }
                if (BackDash_Timer > 0f)
                {
                    BackDash_Timer -= global::UnityEngine.Time.deltaTime;
                    BackDash_Speed = global::UnityEngine.Mathf.Lerp(BackDash_Speed, 0f, global::UnityEngine.Time.deltaTime * 2f);
                    if (!grounded_Dash && !grounded_Dash2)
                    {
                        base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * BackDash_Speed);
                    }
                }
                else if (Slide_Timer > 0f)
                {
                    Slide_Timer -= global::UnityEngine.Time.deltaTime;
                    SlideJump_Speed = global::UnityEngine.Mathf.Lerp(SlideJump_Speed, 0f, global::UnityEngine.Time.deltaTime * 2f);
                    if (!grounded_Slide)
                    {
                        base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * SlideJump_Speed);
                    }
                }
                if (Jump_Num >= 2)
                {
                    Jump2_Rotate();
                }
                if (!onJumpDrop && Jump_Pos_Y > base.transform.position.y)
                {
                    onJumpDrop = true;
                    Drop_Pos_Y = Jump_Pos_Y;
                }
                Jump_Pos_Y = base.transform.position.y;
                break;
        }
        if (GM.onScrew && GM.MP > 10 && !onAttack && State == Player_Control.AniState.Jump && onRolling && (global::UnityEngine.Input.GetButton("Jump") || global::UnityEngine.Input.GetKey(CK.Jump)))
        {
            On_ScrewAttack();
        }
        else if (Screw_Opacity > 0f)
        {
            Off_ScrewAttack();
        }
        if ((State != Player_Control.AniState.Jump || !onRolling) && (State == Player_Control.AniState.Idle || State == Player_Control.AniState.Run || State == Player_Control.AniState.Jump || State == Player_Control.AniState.Spin || State == Player_Control.AniState.Damage))
        {
            Col_Top.enabled = true;
            if (Col_Bot.enabled)
            {
                Col_Bot.enabled = false;
            }
            if (Col_Jump2.enabled)
            {
                Col_Jump2.enabled = false;
            }
        }
        if (ErrorDrop_Timer > 0f)
        {
            ErrorDrop_Timer -= global::UnityEngine.Time.deltaTime;
        }
        Raycasting();
        prevX = inputX;
        prevY = inputY;
        Speed_X = inputX;
        Speed_Y = inputY;
        if (inputX != 0f && (State == Player_Control.AniState.Run || State == Player_Control.AniState.Jump))
        {
            if (global::UnityEngine.Input.GetAxis("L_Y") != 0f && global::UnityEngine.Input.GetAxis("L_Y") > -0.8f)
            {
                Accel += global::UnityEngine.Time.deltaTime;
            }
            else if (inputY >= 0f)
            {
                Accel += global::UnityEngine.Time.deltaTime;
            }
        }
        else
        {
            Accel = 0f;
        }
        if (State != Player_Control.AniState.Spin && global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().enabled)
        {
            global::UnityEngine.GameObject.Find("Col_Spin").GetComponent<global::UnityEngine.BoxCollider2D>().enabled = false;
        }
    }

    private void Raycasting()
    {
        grounded_Slide = global::UnityEngine.Physics2D.Linecast(groundedStart.position, checkFront.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        if (grounded_Slide && global::UnityEngine.Physics2D.Linecast(groundedStart.position, checkFront.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")).collider.name == "Push_Box")
        {
            grounded_Slide = false;
        }
        grounded_Dash = global::UnityEngine.Physics2D.Linecast(groundedStart.position, checkBack.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        grounded_Dash2 = global::UnityEngine.Physics2D.Linecast(groundedStart.position, checkBack2.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        grounded_Now = global::UnityEngine.Physics2D.Linecast(groundedStart.position, groundedEnd.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        grounded_Front = global::UnityEngine.Physics2D.Linecast(frontStart.position, frontEnd.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        grounded_Rear = global::UnityEngine.Physics2D.Linecast(rearStart.position, rearEnd.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground"));
        if (grounded_Now)
        {
            if (!grounded_Front || !grounded_Rear)
            {
                Ani.Set_Edge(true);
            }
            else
            {
                Ani.Set_Edge(false);
            }
            if (onJumpDrop && (State == Player_Control.AniState.Jump || State == Player_Control.AniState.Spin || State == Player_Control.AniState.Damage) && !grounded_Last && base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y <= 0f)
            {
                Jump_To_Ground();
                ErrorJump_Timer = 0f;
            }
            else if (State == Player_Control.AniState.Jump)
            {
                if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y == 0f || global::UnityEngine.Mathf.Abs(base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y) < 0.1f)
                {
                    ErrorJump_Timer += global::UnityEngine.Time.deltaTime;
                    if (ErrorJump_Timer > 0.05f)
                    {
                        Jump_To_Ground();
                    }
                }
                else
                {
                    ErrorJump_Timer = 0f;
                }
            }
            else
            {
                ErrorJump_Timer = 0f;
                if ((bool)global::UnityEngine.Physics2D.Linecast(posSitLock_1.position, posSitLock_1C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")) && (bool)global::UnityEngine.Physics2D.Linecast(posSitLock_2.position, posSitLock_2C.position, 1 << global::UnityEngine.LayerMask.NameToLayer("Ground")))
                {
                    onSitLock = true;
                }
                else
                {
                    onSitLock = false;
                }
            }
        }
        else
        {
            onSitLock = false;
            if (grounded_Last)
            {
                if (State != Player_Control.AniState.Jump && State != Player_Control.AniState.Spin)
                {
                    Check_Drop();
                }
                ErrorJump_Timer = 0f;
            }
            else if (State == Player_Control.AniState.Jump)
            {
                if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y == 0f && Screw_Opacity <= 0f)
                {
                    ErrorJump_Timer += global::UnityEngine.Time.deltaTime;
                    if (ErrorJump_Timer > 0.05f)
                    {
                        Jump_To_Ground();
                    }
                }
                else
                {
                    ErrorJump_Timer = 0f;
                }
            }
            else if (State != Player_Control.AniState.Spin && Jump_Num == 0 && base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y < 0f)
            {
                Check_Drop();
            }
            else
            {
                ErrorJump_Timer = 0f;
            }
        }
        grounded_Last = grounded_Now;
    }

    private void Check_Drop()
    {
        if (onAttack)
        {
            Ani.Set_DropAtk();
        }
        else if (State == Player_Control.AniState.Slide)
        {
            Ani.Set_Jump_Slide();
            SlideJump_Speed = 20 * facingRight;
            base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * SlideJump_Speed);
            Slide_Timer *= 5f;
        }
        else if (State == Player_Control.AniState.BackDash)
        {
            Ani.Set_Jump();
            BackDash_Speed = 20 * -facingRight;
            base.transform.Translate(global::UnityEngine.Vector3.right * global::UnityEngine.Time.deltaTime * BackDash_Speed);
            BackDash_Timer *= 5f;
        }
        else
        {
            Ani.Set_Jump();
        }
        State = Player_Control.AniState.Jump;
        Jump_Num = 1;
        ErrorDrop_Timer = 0.12f;
        DropJump_Delay = 0.08f;
    }

    private void Jump_To_Ground()
    {
        onHighJump = false;
        FootStep();
        if (Jump_Num == 2)
        {
            Jump2_End();
        }
        if (Screw_Opacity > 0f)
        {
            Screw_Speed = 1f;
            Screw_Opacity = 0f;
            Off_ScrewAttack();
        }
        if (!GM.onEvent && inputX != 0f && !onAttack)
        {
            State = Player_Control.AniState.Run;
            Ani.Set_Run();
        }
        else
        {
            State = Player_Control.AniState.Idle;
            if (Jump_Num == 2 || Drop_Pos_Y - base.transform.position.y > 10f)
            {
                Ani.Set_Jump_End();
            }
            else
            {
                Ani.Set_Jump_Short();
            }
        }
        Jump_Num = 0;
        onJumpDrop = false;
        grounded_Last = grounded_Now;
    }

    private void Flip()
    {
        Ani_rolling.transform.localPosition = new global::UnityEngine.Vector3(0f, 2.7f, 0f);
        if (!onAttack && State == Player_Control.AniState.Idle)
        {
            Ani.Set_Turn();
        }
        else if (!onAttack && State == Player_Control.AniState.Sit)
        {
            Ani.Set_Turn_Sit();
        }
        facingRight *= -1;
        base.transform.localScale = new global::UnityEngine.Vector3(1 * facingRight, 1f, 1f);
        GM.Velcocity.x = 0f;
    }

    private void Make_Lag()
    {
        Lag_Timer += global::UnityEngine.Time.deltaTime;
        if (!(Lag_Timer > 0.045f))
        {
            return;
        }
        if (Jump_Num < 2 || player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
        {
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Effect_Lag, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            if (GM.Weapon_Num == 5)
            {
                gameObject.SendMessage("Set_Color_5");
            }
            gameObject.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
            gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
            if (onDash_Keyboard && Dash_Timer < 0.4f)
            {
                gameObject.GetComponent<Effect_Opacity>().Set_Opacity(Dash_Timer);
            }
            Lag_Timer = 0f;
            return;
        }
        global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x - 0.19f * (float)facingRight, base.transform.position.y + 2.7f, 0f);
        global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Effect_Lag, position, Ani_rolling.transform.localRotation) as global::UnityEngine.GameObject;
        if (GM.Weapon_Num == 5)
        {
            gameObject2.SendMessage("Set_Color_5");
        }
        gameObject2.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
        gameObject2.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
        global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Effect_Lag, position, Effect_rolling.transform.localRotation) as global::UnityEngine.GameObject;
        if (GM.Weapon_Num == 5)
        {
            gameObject3.SendMessage("Set_Color_5");
        }
        gameObject3.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
        gameObject3.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Effect_Rolling;
        if (onDash_Keyboard && Dash_Timer < 0.4f)
        {
            gameObject2.GetComponent<Effect_Opacity>().Set_Opacity(Dash_Timer);
            gameObject3.GetComponent<Effect_Opacity>().Set_Opacity(Dash_Timer);
        }
        Lag_Timer = 0.03f;
    }

    private void Make_BackDashLag()
    {
        Lag_Timer += global::UnityEngine.Time.deltaTime;
        if (Lag_Timer > 0.03f)
        {
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Effect_BackDash, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            if (GM.Weapon_Num == 5)
            {
                gameObject.SendMessage("Set_Color_5");
            }
            gameObject.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
            gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
            Lag_Timer = 0f;
        }
    }

    private void Make_HJump_Lag()
    {
        Lag_Timer += global::UnityEngine.Time.deltaTime;
        if (!(Lag_Timer > 0.03f))
        {
            return;
        }
        float opacity = ((!(HighJump_Timer > 1f)) ? HighJump_Timer : 1f);
        if (player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().enabled)
        {
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Effect_BackDash, base.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            if (GM.Weapon_Num == 5)
            {
                gameObject.SendMessage("Set_Color_5");
            }
            gameObject.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
            gameObject.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
            gameObject.GetComponent<Effect_BackDash>().Set_Opacity(opacity);
        }
        else
        {
            global::UnityEngine.Vector3 position = new global::UnityEngine.Vector3(base.transform.position.x - 0.19f * (float)facingRight, base.transform.position.y + 2.7f, 0f);
            global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Effect_BackDash, position, global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            if (GM.Weapon_Num == 5)
            {
                gameObject2.SendMessage("Set_Color_5");
            }
            gameObject2.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
            gameObject2.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sprite;
            gameObject2.GetComponent<Effect_BackDash>().Set_Opacity(opacity);
            global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Effect_Lag, position, Effect_rolling.transform.localRotation) as global::UnityEngine.GameObject;
            if (GM.Weapon_Num == 5)
            {
                gameObject3.SendMessage("Set_Color_5");
            }
            gameObject3.transform.localScale = new global::UnityEngine.Vector3(base.transform.localScale.x, 1f, 1f);
            gameObject3.GetComponent<global::UnityEngine.SpriteRenderer>().sprite = Spr_Effect_Rolling;
            gameObject3.GetComponent<Effect_Opacity>().Set_Opacity(opacity);
        }
        Lag_Timer = 0f;
    }

    private void High_Jump()
    {
        onHighJump = true;
        Sound_Slide();
        Jump2_End();
        Jump_Num = 1;
        HighJump_Timer = 1.2f;
        if (ErrorDrop_Timer > 0f)
        {
            Jump_Num = 0;
        }
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
        base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 50f, global::UnityEngine.ForceMode2D.Impulse);
        onJumpDrop = false;
        Jump_Pos_Y = base.transform.position.y;
        State = Player_Control.AniState.Jump;
        Ani.Set_HighJump();
    }

    private void Jump()
    {
        Sound_Jump();
        if (ErrorDrop_Timer > 0f)
        {
            Jump_Num = 0;
        }
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
        if (Jump_Num < 1)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 25.4f, global::UnityEngine.ForceMode2D.Impulse);
        }
        else
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 30f, global::UnityEngine.ForceMode2D.Impulse);
        }
        onJumpDrop = false;
        Jump_Pos_Y = base.transform.position.y;
        State = Player_Control.AniState.Jump;
        Ani.Set_Jump();
        Jump_Num++;
        if (Jump_Num == 2)
        {
            Jump2_Start();
        }
    }

    private void Jump2_Rotate()
    {
        global::UnityEngine.Vector3 to = new global::UnityEngine.Vector3(-0.19f, 2.7f, 0f);
        Ani_rolling.transform.localPosition = global::UnityEngine.Vector3.Lerp(Ani_rolling.transform.localPosition, to, 0.02f);
        global::UnityEngine.GameObject.Find("Col_Rolling").transform.localPosition = Ani_rolling.transform.localPosition;
        Ani_rolling.transform.Rotate(0f, 0f, -2200f * global::UnityEngine.Time.deltaTime * Screw_Speed);
        Effect_rolling.transform.Rotate(0f, 0f, -500f * global::UnityEngine.Time.deltaTime * Screw_Speed);
        if (onRolling)
        {
            Screw_Delay += global::UnityEngine.Time.deltaTime;
            if (Screw_Delay > 0.2f - 0.1f * Screw_Opacity)
            {
                Screw_Delay = 0f;
                Sound_Jump();
            }
        }
    }

    private void Jump2_Start()
    {
        onRolling = true;
        Sound_Jump();
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        Ani_rolling.transform.localPosition = new global::UnityEngine.Vector3(0f, 2.7f, 0f);
        global::UnityEngine.GameObject.Find("Col_Rolling").transform.localPosition = new global::UnityEngine.Vector3(0f, 2.7f, 0f);
        global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().enabled = true;
        Col_Jump2.enabled = true;
        Col_Top.enabled = false;
        Col_Bot.enabled = false;
    }

    private void Jump2_End()
    {
        onRolling = false;
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = true;
        Ani_rolling.transform.localPosition = new global::UnityEngine.Vector3(0f, 2.7f, 0f);
        global::UnityEngine.GameObject.Find("Col_Rolling").transform.localPosition = new global::UnityEngine.Vector3(0f, 2.7f, 0f);
        global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().enabled = false;
        Col_Top.enabled = true;
        Col_Bot.enabled = false;
        Col_Jump2.enabled = false;
        Screw_Speed = 1f;
        Screw_Opacity = 0f;
        Screw_Delay = 0f;
        Off_ScrewAttack();
        if (GM.Weapon_Num < 3)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2f;
        }
        else if (GM.Weapon_Num == 3)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.04f;
        }
        else if (GM.Weapon_Num == 4)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f;
        }
        else if (GM.Weapon_Num == 5)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f;
        }
    }

    private void On_ScrewAttack()
    {
        Screw_Timer += global::UnityEngine.Time.deltaTime;
        if (Screw_Timer > 0.1f)
        {
            onScrewAttack = true;
            Screw_Speed = global::UnityEngine.Mathf.Lerp(Screw_Speed, 2f, global::UnityEngine.Time.deltaTime * 3f);
            Screw_Opacity = global::UnityEngine.Mathf.Lerp(Screw_Opacity, 1f, global::UnityEngine.Time.deltaTime * 3f);
            if (GM.Weapon_Num < 3)
            {
                global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2f + 0.2f * Screw_Opacity;
            }
            else if (GM.Weapon_Num == 3)
            {
                global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.04f + 0.2f * Screw_Opacity;
            }
            else if (GM.Weapon_Num == 4)
            {
                global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f + 0.2f * Screw_Opacity;
            }
            else if (GM.Weapon_Num == 5)
            {
                global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f + 0.4f * Screw_Opacity;
            }
        }
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Screw_Opacity);
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Screw_Opacity);
        if (Screw_Opacity > 0f)
        {
            GM.MP_Screw(Screw_Opacity);
        }
        if (GM.MP < 0)
        {
            GM.MP = 0;
        }
    }

    private void Off_ScrewAttack()
    {
        onScrewAttack = false;
        Screw_Timer = 0f;
        Screw_Speed = global::UnityEngine.Mathf.Lerp(Screw_Speed, 1f, global::UnityEngine.Time.deltaTime * 6f);
        Screw_Opacity = global::UnityEngine.Mathf.Lerp(Screw_Opacity, 0f, global::UnityEngine.Time.deltaTime * 6f);
        if (GM.Weapon_Num < 3)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2f + 0.2f * Screw_Opacity;
        }
        else if (GM.Weapon_Num == 3)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.04f + 0.2f * Screw_Opacity;
        }
        else if (GM.Weapon_Num == 4)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f + 0.2f * Screw_Opacity;
        }
        else if (GM.Weapon_Num == 5)
        {
            global::UnityEngine.GameObject.Find("Col_Rolling").GetComponent<global::UnityEngine.CircleCollider2D>().radius = 2.5f + 0.4f * Screw_Opacity;
        }
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Screw_Opacity);
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(1f, 1f, 1f, Screw_Opacity);
    }

    private void Fire_Magic()
    {
        if (GM.Skill_Num == 5)
        {
            float x = base.transform.position.x + 1.7f * (float)facingRight + (float)global::UnityEngine.Random.Range(-5, 5) * 0.01f;
            float num = base.transform.position.y + 4.2f + (float)global::UnityEngine.Random.Range(-5, 5) * 0.01f;
            if (State == Player_Control.AniState.Sit)
            {
                num -= 2.52f;
            }
            global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Magic_5, new global::UnityEngine.Vector3(x, num, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            gameObject.transform.localScale = new global::UnityEngine.Vector3((float)facingRight * 1f, 1f, 1f);
            return;
        }
        if (GM.Skill_Num == 4)
        {
            if (GM.Shield_Object != null)
            {
                GM.Shield_Object.SendMessage("Set_Broken");
            }
            GM.Shield_Object = global::UnityEngine.Object.Instantiate(Magic_4, new global::UnityEngine.Vector3(base.transform.position.x, base.transform.position.y + 2.7f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            GM.Shield_Object.transform.localScale = new global::UnityEngine.Vector3((float)facingRight * 0.85f, 0.85f, 1f);
            return;
        }
        if (GM.Skill_Num == 2)
        {
            global::UnityEngine.GameObject.Find("Menu").SendMessage("Sound_Select");
            float x2 = base.transform.position.x + (float)global::UnityEngine.Random.Range(-5, 5) * 0.01f;
            float num2 = base.transform.position.y + 3.2f + (float)global::UnityEngine.Random.Range(-5, 5) * 0.01f;
            if (State == Player_Control.AniState.Sit)
            {
                num2 -= 2.2f;
            }
            global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Magic_2, new global::UnityEngine.Vector3(x2, num2, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
            gameObject2.transform.localScale = new global::UnityEngine.Vector3(facingRight, 1f, 1f);
            return;
        }
        if (GM.Skill_Num == 3)
        {
            float x3 = base.transform.position.x + 1.6f * (float)facingRight + (float)global::UnityEngine.Random.Range(-5, 5) * 0.02f;
            float num3 = base.transform.position.y + 4.6f + (float)global::UnityEngine.Random.Range(-5, 5) * 0.02f;
            if (State == Player_Control.AniState.Sit)
            {
                num3 -= 2.52f;
            }
            global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Magic_3, new global::UnityEngine.Vector3(x3, num3, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 160f)) as global::UnityEngine.GameObject;
            gameObject3.transform.localScale = new global::UnityEngine.Vector3(facingRight, 1f, 1f);
            if (Jump_Num > 0)
            {
                if (base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y > 0f)
                {
                    gameObject3.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 12f, global::UnityEngine.ForceMode2D.Impulse);
                }
                else
                {
                    gameObject3.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 2f, global::UnityEngine.ForceMode2D.Impulse);
                }
            }
            else
            {
                gameObject3.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 2f, global::UnityEngine.ForceMode2D.Impulse);
            }
            gameObject3.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 12f * facingRight * (1f + GM.Velcocity.x), global::UnityEngine.ForceMode2D.Impulse);
            return;
        }
        if (Jump_Num == 2)
        {
            for (int i = 0; i < 12; i++)
            {
                global::UnityEngine.GameObject gameObject4 = global::UnityEngine.Object.Instantiate(Magic_1, Ani_rolling.transform.position, global::UnityEngine.Quaternion.Euler(0f, 0f, (float)i * 30f + global::UnityEngine.Random.Range(-5f, 5f))) as global::UnityEngine.GameObject;
                gameObject4.transform.Translate(global::UnityEngine.Vector3.right * 3f);
            }
            global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
            global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Magic_1(base.transform.position);
            return;
        }
        float x4 = base.transform.position.x + 2.8f * (float)facingRight;
        float num4 = base.transform.position.y + 4.2f + (float)global::UnityEngine.Random.Range(-5, 5) * 0.02f;
        if (State == Player_Control.AniState.Sit)
        {
            num4 -= 2.52f;
        }
        else if (Jump_Num > 0)
        {
            num4 += base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y * 0.02f;
        }
        global::UnityEngine.GameObject gameObject5 = global::UnityEngine.Object.Instantiate(Magic_1, new global::UnityEngine.Vector3(x4, num4 + 0.2f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 7 * facingRight)) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject6 = global::UnityEngine.Object.Instantiate(Magic_1, new global::UnityEngine.Vector3(x4, num4 + 0.1f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 3 * facingRight)) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject7 = global::UnityEngine.Object.Instantiate(Magic_1, new global::UnityEngine.Vector3(x4, num4, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, 0f)) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject8 = global::UnityEngine.Object.Instantiate(Magic_1, new global::UnityEngine.Vector3(x4, num4 - 0.1f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, -3 * facingRight)) as global::UnityEngine.GameObject;
        global::UnityEngine.GameObject gameObject9 = global::UnityEngine.Object.Instantiate(Magic_1, new global::UnityEngine.Vector3(x4, num4 - 0.2f, 0f), global::UnityEngine.Quaternion.Euler(0f, 0f, -7 * facingRight)) as global::UnityEngine.GameObject;
        gameObject7.transform.localScale = new global::UnityEngine.Vector3((float)facingRight * 1f, 1f, 1f);
        global::UnityEngine.Transform obj = gameObject5.transform;
        global::UnityEngine.Vector3 localScale = gameObject7.transform.localScale;
        gameObject8.transform.localScale = localScale;
        localScale = localScale;
        gameObject9.transform.localScale = localScale;
        localScale = localScale;
        gameObject6.transform.localScale = localScale;
        obj.localScale = localScale;
        gameObject7.transform.Translate(global::UnityEngine.Vector3.right * 1f);
        global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Boss_4_Fire(base.transform.position);
        global::UnityEngine.GameObject.Find("Sound_List").GetComponent<Sound_Control>().Magic_1(base.transform.position);
    }

    public void Set_Damage(float force)
    {
        if (Jump_Num > 0 && base.GetComponent<UnityEngine.Rigidbody2D>().velocity.y < 0f)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
        }
        if (Damage_Timer < 0.2f)
        {
            Voice_Damage();
        }
        onAttack = false;
        if (Jump_Num == 2)
        {
            Jump2_End();
        }
        Damage_Timer = 0.32f;
        Flicker_Timer = 2.5f;
        onFlicker = true;
        Flicker_Delay = 1f;
        State = Player_Control.AniState.Damage;
        Ani.Set_Damage();
        base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * force, global::UnityEngine.ForceMode2D.Impulse);
    }

    private void Damage_Flicker()
    {
        Flicker_Timer -= global::UnityEngine.Time.deltaTime;
        if (Flicker_Delay > 0.1f)
        {
            Flicker_Delay = 0f;
            if (player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color.a == 1f)
            {
                player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 0.6f);
            }
            else
            {
                player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 1f);
            }
        }
        else
        {
            Flicker_Delay += global::UnityEngine.Time.deltaTime;
        }
    }

    private void Damage_Flicker_2()
    {
        Flicker_Timer -= global::UnityEngine.Time.deltaTime;
        global::UnityEngine.Color color = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color;
        color = global::UnityEngine.Color.Lerp(color, new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 1f), global::UnityEngine.Time.deltaTime * 3f);
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = color;
    }

    private void End_Flicker()
    {
        onFlicker = false;
        Flicker_Timer = 0f;
        Flicker_Delay = 0f;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 1f);
    }

    private void Set_Flicker()
    {
        Flicker_Timer = 3f;
        onFlicker = true;
        Flicker_Delay = 1f;
    }

    public void Down(float force)
    {
        Voice_Death();
        onAttack = false;
        if (Jump_Num == 2)
        {
            Jump2_End();
        }
        Down_Timer = 0f;
        Num_Down_Impact = 2;
        DownUp_Timer = 0f;
        State = Player_Control.AniState.Down;
        Ani.Set_Down();
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 1f);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color;
        if (Screw_Opacity > 0f)
        {
            Screw_Opacity = 0f;
            Off_ScrewAttack();
        }
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
        base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * force * 0.35f, global::UnityEngine.ForceMode2D.Impulse);
        base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 20f, global::UnityEngine.ForceMode2D.Impulse);
    }

    private void H_Down()
    {
        Down_Timer = 0f;
        Num_Down_Impact = 1;
        DownUp_Timer = 0f;
        State = Player_Control.AniState.Down;
        Off_Effect();
    }

    private void Off_Effect()
    {
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().enabled = false;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color = new global::UnityEngine.Color(color_Player.r, color_Player.g, color_Player.b, 1f);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().color = player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().color;
        if (Screw_Opacity > 0f)
        {
            Screw_Opacity = 0f;
            Off_ScrewAttack();
        }
    }

    private void Check_SpeedUp_Pad()
    {
        if (State != Player_Control.AniState.BackDash)
        {
            Make_Lag();
        }
        if ((global::UnityEngine.Input.GetButton("R_B") || global::UnityEngine.Input.GetKey(CK.RB)) && GM.MP > 1)
        {
            GM.MP_SpeedUp();
            return;
        }
        onDash_Pad = false;
        Move_Speed = 1f;
        Move_FrRate = 0.03f;
        Ani.Set_Run_Speed(Move_FrRate);
    }

    private void Reset_Attack()
    {
        Attack_Num = 0;
        Attack_Timer = 0f;
        Attack_Delay = 0f;
    }

    public void UpperPunch()
    {
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
        base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 25f, global::UnityEngine.ForceMode2D.Impulse);
    }

    public void Atk3_Jump()
    {
        if (State == Player_Control.AniState.Jump && !grounded_Now)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.up * 15f, global::UnityEngine.ForceMode2D.Impulse);
        }
        if (grounded_Front)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 6f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
        }
    }

    public void Atk4_Force()
    {
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = new global::UnityEngine.Vector2(0f, 0f);
        if (State == Player_Control.AniState.Jump && !grounded_Now && inputX == 0f)
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 6f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
        }
        else
        {
            base.GetComponent<UnityEngine.Rigidbody2D>().AddForce(global::UnityEngine.Vector3.right * 10f * facingRight, global::UnityEngine.ForceMode2D.Impulse);
        }
    }

    public void Set_Weapon()
    {
        Ani.Set_Weapon();
    }

    public void OnOff_Cloth()
    {
        Ani.OnOff_Cloth();
    }

    public void GetUp()
    {
        State = Player_Control.AniState.Idle;
        Jump_Num = 0;
        DownUp_Timer = 0f;
        Ani.Set_Idle();
        Ani.Set_GetUp();
        Col_Top.enabled = true;
        Col_Bot.enabled = false;
        Col_Jump2.enabled = false;
    }

    public void Lock_GameLoad()
    {
        velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
        base.GetComponent<UnityEngine.Rigidbody2D>().Sleep();
        base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale = 0f;
    }

    public void Lock_GatePass()
    {
        velocity = base.GetComponent<UnityEngine.Rigidbody2D>().velocity;
        base.GetComponent<UnityEngine.Rigidbody2D>().Sleep();
        base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale = 0f;
        Col_Top.enabled = false;
        Col_Bot.enabled = false;
        Col_Jump2.enabled = false;

        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1200;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1201;
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1510;
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1199;
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1540;
        global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1550;
        global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(20);
        global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 1501;
        BackDash_Timer = 2f;
    }


    public void UnLock_GatePass()
    {
        if (State != Player_Control.AniState.Slide && State != Player_Control.AniState.Sit)
        {
            Col_Top.enabled = true;
        }
        else
        {
            Col_Bot.enabled = true;
        }
        if (Jump_Num == 2)
        {
            Col_Jump2.enabled = true;
        }
        base.GetComponent<UnityEngine.Rigidbody2D>().gravityScale = 5f;
        base.GetComponent<UnityEngine.Rigidbody2D>().WakeUp();
        base.GetComponent<UnityEngine.Rigidbody2D>().velocity = velocity;
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(10);
        player_ani.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 200;
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(10);
        Ani_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 201;
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(17);
        Effect_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 510;
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(10);
        Glow_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 199;
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(17);
        Border_rolling.GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 540;
        global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(17);
        global::UnityEngine.GameObject.Find("Effect_Attack").GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 550;
        global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().sortingLayerID = AxiSortingOrder.GetHashIDByUserID(17);
        global::UnityEngine.GameObject.Find("Effect_Spin").GetComponent<global::UnityEngine.SpriteRenderer>().sortingOrder = 501;
    }

    public void Sound_Attack()
    {
        PlayerSound.SendMessage("Sound_Attack");
    }

    public void FootStep()
    {
        PlayerSound.SendMessage("Sound_FootStep");
    }

    public void Sound_Down()
    {
        PlayerSound.SendMessage("Sound_Down");
    }

    public void Sound_Spin()
    {
        PlayerSound.SendMessage("Sound_Spin");
    }

    private void Sound_Jump()
    {
        PlayerSound.SendMessage("Sound_Jump");
    }

    private void Sound_Slide()
    {
        PlayerSound.SendMessage("Sound_Slide");
    }

    private void Voice_Damage()
    {
        int num = global::UnityEngine.Random.Range(1, 5);
        if (num > 2)
        {
            H_Sound.Sound_Moan(14, 1);
        }
        else if (num == 1)
        {
            H_Sound.Sound_Moan(12, 1);
        }
        else
        {
            H_Sound.Sound_Moan(13, 1);
        }
        Moan_Num = num;
    }

    private void Voice_Death()
    {
        global::UnityEngine.GameObject.Find("Sound_List_H").GetComponent<H_SoundControl>().Sound_Moan(11, 1);
    }
}
