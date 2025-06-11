using static UnityEditor.PlayerSettings;

public class Player_SoundList : global::UnityEngine.MonoBehaviour
{
    public global::UnityEngine.GameObject Attack_1;

    public global::UnityEngine.GameObject Attack_2;

    public global::UnityEngine.GameObject Attack_3;

    public global::UnityEngine.GameObject Jump;

    public global::UnityEngine.GameObject Slide;

    public global::UnityEngine.GameObject Spin;

    public global::UnityEngine.GameObject Down;

    public global::UnityEngine.GameObject FootStep;

    public global::UnityEngine.GameObject voiceDamage_1;

    public global::UnityEngine.GameObject voiceDamage_2;

    public global::UnityEngine.GameObject voiceDamage_3;

    public global::UnityEngine.GameObject voiceDamage_4;

    public global::UnityEngine.GameObject voiceDeath_1;

    private float Spin_Timer;

    private float Atk_Timer;

    private float Test_Timer;

    private void Update()
    {
        if (Spin_Timer > 0f)
        {
            Spin_Timer -= global::UnityEngine.Time.deltaTime;
        }
        if (Atk_Timer > 0f)
        {
            Atk_Timer -= global::UnityEngine.Time.deltaTime;
        }
    }

    private void Sound_Attack()
    {
        if (Atk_Timer <= 0f)
        {
            Atk_Timer = 0.05f;
            switch (global::UnityEngine.Random.Range(1, 4))
            {
                case 1:
                    {
                        //global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(Attack_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                        AxiSoundPool.AddSoundForPosRot(Attack_1, base.transform.position, base.transform.rotation);
                        break;
                    }
                case 2:
                    {
                        //global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(Attack_2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                        AxiSoundPool.AddSoundForPosRot(Attack_2, base.transform.position, base.transform.rotation);
                        break;
                    }
                default:
                    {
                        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Attack_3, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                        AxiSoundPool.AddSoundForPosRot(Attack_3, base.transform.position, base.transform.rotation);

                        break;
                    }
            }
        }
    }

    private void Sound_Jump()
    {
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Jump, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForPosRot(Jump, base.transform.position, base.transform.rotation);
    }

    private void Sound_Slide()
    {
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Slide, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForPosRot(Slide, base.transform.position, base.transform.rotation);

    }

    private void Sound_Spin()
    {
        if (Spin_Timer <= 0f)
        {
            Spin_Timer = 0.05f;
            //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Attack_3, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
            AxiSoundPool.AddSoundForPosRot(Attack_3, base.transform.position, base.transform.rotation);

        }
    }

    private void Sound_Down()
    {
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(Down, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForPosRot(Down, base.transform.position, base.transform.rotation);

    }

    private void Sound_FootStep()
    {
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(FootStep, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForPosRot(FootStep, base.transform.position, base.transform.rotation);
    }

    private void Voice_Damage()
    {
        switch (global::UnityEngine.Random.Range(1, 4))
        {
            case 1:
                {
                    //global::UnityEngine.GameObject gameObject3 = global::UnityEngine.Object.Instantiate(voiceDamage_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                    AxiSoundPool.AddSoundForPosRot(voiceDamage_1, base.transform.position, base.transform.rotation);
                    break;
                }
            case 2:
                {
                    //global::UnityEngine.GameObject gameObject2 = global::UnityEngine.Object.Instantiate(voiceDamage_2, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                    AxiSoundPool.AddSoundForPosRot(voiceDamage_2, base.transform.position, base.transform.rotation);
                    break;
                }
            default:
                {
                    //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(voiceDamage_4, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
                    AxiSoundPool.AddSoundForPosRot(voiceDamage_4, base.transform.position, base.transform.rotation);
                    break;
                }
        }
    }

    private void Voice_Death()
    {
        //global::UnityEngine.GameObject gameObject = global::UnityEngine.Object.Instantiate(voiceDeath_1, base.transform.position, base.transform.rotation) as global::UnityEngine.GameObject;
        AxiSoundPool.AddSoundForPosRot(voiceDeath_1, base.transform.position, base.transform.rotation);
    }
}
