Shader "Hidden/TileEd/DummyShader"
{
    Properties
    {
		_MainTex("Diffuse", 2D) = "white" {}
		_Tint("Tint", Color) = (1, 1, 1, 1)
    }
    SubShader
    {
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" "IgnoreProjector" = "True" "ForceNoShadowCasting" = "True"}
		LOD 100

		Lighting Off
		Cull Off
		ZWrite Off
		ZTest LEqual
		Offset -1,-1
		ColorMask RGBA
		Blend SrcAlpha OneMinusSrcAlpha

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;
			fixed4 _Tint;


            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
				fixed4 col = _Tint;
				col.a = min(tex2D(_MainTex, i.uv).a, _Tint.a);
                return col;
            }
            ENDCG
        }
    }
}
