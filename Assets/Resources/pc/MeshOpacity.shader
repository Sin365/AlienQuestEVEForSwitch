Shader "Custom/MeshOpacity"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _MainTex ("Main Texture (RGB)", 2D) = "white" {}
        _Opacity ("Opacity", Range(0,1)) = 1
    }
    SubShader
    {
        Tags { "Queue"="Transparent" "RenderType"="Transparent" }
        LOD 200

        CGPROGRAM
        #pragma surface surf Lambert alpha
        #pragma target 3.0

        sampler2D _MainTex;
        float4 _Color;
        float _Opacity;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
            fixed4 c = tex2D(_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            o.Alpha = c.a * _Opacity;
        }
        ENDCG
    }
    FallBack "Diffuse"
}