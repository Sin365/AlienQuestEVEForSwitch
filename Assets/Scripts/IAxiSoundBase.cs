public abstract class AxiSoundBase : UnityEngine.MonoBehaviour
{
    public string resourceName { get; set; }
    public long Seed { get; set; }
    public abstract void Init();
    public abstract void ReleaseToPool();
}
