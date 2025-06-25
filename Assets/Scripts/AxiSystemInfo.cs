using System.Collections.Generic;
using UnityEngine;

public static class AxiSystemInfo
{
    static Dictionary<RenderTextureFormat, bool> dictSupportsRenderTextureFormat = new Dictionary<RenderTextureFormat, bool>();
    public static bool SupportsRenderTextureFormat(RenderTextureFormat format)
    {
        if (!dictSupportsRenderTextureFormat.ContainsKey(format))
            dictSupportsRenderTextureFormat[format] = UnityEngine.SystemInfo.SupportsRenderTextureFormat(format);

        return dictSupportsRenderTextureFormat[format];
    }
}
