Shader "Unlit/CooldownShader"
{
    Properties
    {
        _Cooldown ("Cooldown", Range(0,1)) = .5
        _ColorReady ("ColorReady", Color) = (0.0,1.0,0.0, 1.0)
        _ColorCooldown ("ColorCooldown", Color) = (1.0,0.0,0.0, 1.0)
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            float _Cooldown;
            float4 _ColorReady;
            float4 _ColorCooldown;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = lerp(_ColorReady, _ColorCooldown, step(_Cooldown, i.uv.x));
                // apply fog
                UNITY_APPLY_FOG(i.fogCoord, col);
                return col;
            }
            ENDCG
        }
    }
}
