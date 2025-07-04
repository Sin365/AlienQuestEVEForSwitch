using UnityEngine;

public static class AxiInputBridge
{
    public static Vector3 mousePosition;
    public static Vector2 mouseScrollDelta => Input.mouseScrollDelta;
    public static bool anyKeyDown => Input.anyKeyDown;

    static bool bOnLog = true;

    public static bool GetButtonDown(string key)
    {
        var ret = Input.GetButtonDown(key);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetButtonDown =>" + key + " | " + ret);
#endif
        return ret;
    }
    public static float GetAxis(string key)
    {
        var ret = Input.GetAxis(key);
#if UNITY_EDITOR
        if (ret != 0 && bOnLog)
            Debug.Log("[AxiInput]GetAxis =>" + key + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetKeyDown(KeyCode key)
    {
        var ret = Input.GetKeyDown(key);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetKeyDown =>" + key + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetKey(KeyCode key)
    {
        var ret = Input.GetKey(key);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetKey =>" + key + " | true");
#endif
        return ret;
    }

    internal static bool GetMouseButton(int button)
    {
        var ret = Input.GetMouseButton(button);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetMouseButton =>" + button + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetMouseButtonDown(int button)
    {
        var ret = Input.GetMouseButtonDown(button);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetMouseButtonDown =>" + button + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetMouseButtonUp(int button)
    {
        var ret = Input.GetMouseButtonUp(button);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetMouseButtonUp =>" + button + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetKeyUp(KeyCode key)
    {
        var ret = Input.GetKeyUp(key);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetKeyUp =>" + key + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetButtonUp(string buttonName)
    {
        var ret = Input.GetButtonUp(buttonName);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetButtonUp =>" + buttonName + " | " + ret);
#endif
        return ret;
    }

    internal static bool GetButton(string buttonName)
    {
        var ret = Input.GetButton(buttonName);
#if UNITY_EDITOR
        if (ret && bOnLog)
            Debug.Log("[AxiInput]GetButton =>" + buttonName + " | " + ret);
#endif
        return ret;
    }
}
