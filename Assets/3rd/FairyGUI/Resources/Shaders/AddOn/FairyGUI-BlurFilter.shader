// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FairyGUI/BlurFilter" {
    Properties {
    _MainTex ("Base (RGB)", 2D) = "white" {}
}

SubShader {
    Pass {
        ZTest Always 
        ZWrite Off
        Tags{"LightMode"="UniversalForward"}

        HLSLPROGRAM
        #pragma vertex vert
        #pragma fragment frag

        #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

        struct appdata_t {
            float4 vertex : POSITION;
            float2 texcoord : TEXCOORD0;
        };

        struct v2f {
            float4 vertex : SV_POSITION;
            half2 texcoord : TEXCOORD0;
            half2 taps[4] : TEXCOORD1; 
        };

        sampler2D _MainTex;
        half4 _MainTex_TexelSize;
        half4 _BlurOffsets;
        
        v2f vert (appdata_t v)
        {
            v2f o;
            o.vertex = TransformObjectToHClip(v.vertex.xyz);
            o.texcoord = v.texcoord - _BlurOffsets.xy * _MainTex_TexelSize.xy;
            o.taps[0] = o.texcoord + _MainTex_TexelSize.xy * _BlurOffsets.xy;
            o.taps[1] = o.texcoord - _MainTex_TexelSize.xy * _BlurOffsets.xy;
            o.taps[2] = o.texcoord + _MainTex_TexelSize.xy * _BlurOffsets.xy * half2(1,-1);
            o.taps[3] = o.texcoord - _MainTex_TexelSize.xy * _BlurOffsets.xy * half2(1,-1);
            return o;
        }
        
        half4 frag (v2f i) : SV_Target
        {
            half4 color = tex2D(_MainTex, i.taps[0]);
            color += tex2D(_MainTex, i.taps[1]);
            color += tex2D(_MainTex, i.taps[2]);
            color += tex2D(_MainTex, i.taps[3]); 
            return color * 0.25;
        }
    ENDHLSL
    }
}
Fallback off
}
