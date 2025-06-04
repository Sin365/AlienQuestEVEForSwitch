using System.Collections.Generic;

public class AxiPlayerPrefsForSwitch : AxiPlayerPrefsFileBase
{
    public AxiPlayerPrefsForSwitch():base(NSLoadData, NSSaveData)
    {
	}

    public static Dictionary<string, AxiPlayerPrefsKeyValye> NSLoadData()
    {
#if UNITY_EDITOR
        return new Dictionary<string, AxiPlayerPrefsKeyValye>();
#else

#endif
    }

    public static void NSSaveData(Dictionary<string, AxiPlayerPrefsKeyValye> data)
    {
#if UNITY_EDITOR
#else

#endif
    }

}