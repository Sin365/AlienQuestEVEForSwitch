
using System;
using System.Collections.Generic;
using System.Linq;

public abstract class AxiPlayerPrefsFileBase : IAxiPlayerPrefs
{
    Dictionary<string,AxiPlayerPrefsKeyValye> m_keyval = new Dictionary<string, AxiPlayerPrefsKeyValye>();
    Func<Dictionary<string, AxiPlayerPrefsKeyValye>> m_LoadFunc;
    Action<Dictionary<string, AxiPlayerPrefsKeyValye>> m_SaveFunc;

	[Serializable]
    public class AxiPlayerPrefsAllData
    {
        public int version;
        public List<AxiPlayerPrefsKeyValye> datalist;
    }

    [Serializable]
    public class AxiPlayerPrefsKeyValye
    {
        public string key;
        public int intval;
        public string strval;
        public float floatval;
    }

    public AxiPlayerPrefsFileBase(Func<Dictionary<string, AxiPlayerPrefsKeyValye>> load, Action<Dictionary<string, AxiPlayerPrefsKeyValye>> save)
    {
        m_LoadFunc = load;
        m_SaveFunc = save;
    }
    public static Dictionary<string, AxiPlayerPrefsKeyValye> JsonStrToData(string dataStr)
    {
        AxiPlayerPrefsAllData alldata = UnityEngine.JsonUtility.FromJson<AxiPlayerPrefsAllData>(dataStr);
        Dictionary<string, AxiPlayerPrefsKeyValye> data = new Dictionary<string, AxiPlayerPrefsKeyValye>();
        foreach (var item in alldata.datalist)
        {
            data.Add(item.key, item);
        }
        return data;
    }

	public static string DataToJsonStr(Dictionary<string, AxiPlayerPrefsKeyValye> data)
    {
        return UnityEngine.JsonUtility.ToJson(new AxiPlayerPrefsAllData() {version = 1, datalist = data.Values.ToList() });
    }

    AxiPlayerPrefsKeyValye GetByKey(string key,bool NonAutoCreate,out bool IsNew)
    {
        if (!m_keyval.ContainsKey(key))
        {
            IsNew = true;

            if (!NonAutoCreate)
                return null;

            m_keyval.Add(key, new AxiPlayerPrefsKeyValye() { key = key });
        }
        else
            IsNew = false;

        return m_keyval[key];
    }

    public void Load()
    {
        m_keyval = m_LoadFunc.Invoke();
    }

    public void Save()
    {
        m_SaveFunc.Invoke(m_keyval);
    }

    public float GetFloat(string key, float defaultValue)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key,true,out bool IsNew);
        if (IsNew)
            kv.floatval = defaultValue;
        return kv.floatval;
    }

    public int GetInt(string key, int defaultValue)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, true, out bool IsNew);
        if (IsNew)
            kv.intval = defaultValue;
        return kv.intval;
    }

    public string GetString(string key, string defaultValue)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, true, out bool IsNew);
        if (IsNew)
            kv.strval = defaultValue;
        return kv.strval;
    }

    public float GetFloat(string key)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, false, out bool _);
        if(kv != null) return kv.floatval;
        return default(float);
    }

    public int GetInt(string key)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, false, out bool _);
        if (kv != null) return kv.intval;
        return default(int);
    }

    public string GetString(string key)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, false, out bool _);
        if (kv != null) return kv.strval;
        return string.Empty;
    }

    
    public void SetInt(string key, int value)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, true, out bool _);
        kv.intval = value;
		m_SaveFunc.Invoke(m_keyval);
	}

    public void SetString(string key, string value)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, true, out bool _);
        kv.strval = value;
		m_SaveFunc.Invoke(m_keyval);
	}

    public void SetFloat(string key, float value)
    {
        AxiPlayerPrefsKeyValye kv = GetByKey(key, true, out bool _);
        kv.floatval = value;
		m_SaveFunc.Invoke(m_keyval);
	}
    public void DeleteAll()
    {
        m_keyval.Clear();
		m_SaveFunc.Invoke(m_keyval);
	}

}