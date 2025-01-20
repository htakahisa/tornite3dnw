Shader "Custom/TransparentDoubleSided"
{
    Properties
    {
        _Color("Color", Color) = (1,1,1,0.5)
    }
        SubShader
    {
        Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
        LOD 200

        Pass
        {
            Tags { "LightMode" = "ForwardBase" }

            Cull Off // Render both sides of the polygon
            ZWrite Off // Disable depth writing for transparency
            Blend SrcAlpha OneMinusSrcAlpha // Standard alpha blending

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
            };

            struct v2f
            {
                float4 pos : SV_POSITION;
            };

            float4 _Color;

            v2f vert(appdata v)
            {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                return o;
            }

            fixed4 frag(v2f i) : SV_Target
            {
                return _Color;
            }
            ENDCG
        }
    }
        FallBack "Diffuse"
}
