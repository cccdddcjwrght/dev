// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "FairyGUI/Image"
{
    Properties
    {
        _MainTex ("Base (RGB), Alpha (A)", 2D) = "black" {}

        _StencilComp ("Stencil Comparison", Float) = 8
        _Stencil ("Stencil ID", Float) = 0
        _StencilOp ("Stencil Operation", Float) = 0
        _StencilWriteMask ("Stencil Write Mask", Float) = 255
        _StencilReadMask ("Stencil Read Mask", Float) = 255

        _ColorMask ("Color Mask", Float) = 15

        _BlendSrcFactor ("Blend SrcFactor", Float) = 5
        _BlendDstFactor ("Blend DstFactor", Float) = 10
        
        _ClipBox ("clipbox",Vector)  = (-2,-2, 0, 0)
        _ClipSoftness("clipsoft ness", Vector) = (0,0, 0, 0)
    }
    
    SubShader
    {
        LOD 100

        Tags
        {
            "Queue" = "Transparent"
            "IgnoreProjector" = "True"
            "RenderType" = "Transparent"
        }
        
        Stencil
        {
            Ref [_Stencil]
            Comp [_StencilComp]
            Pass [_StencilOp] 
            ReadMask [_StencilReadMask]
            WriteMask [_StencilWriteMask]
        }

        Cull Off
        Lighting Off
        ZWrite Off
        Fog { Mode Off }
        Blend [_BlendSrcFactor] [_BlendDstFactor], One One
        ColorMask [_ColorMask]

        Pass
        {
            Tags{"LightMode"="UniversalForward"}

            HLSLPROGRAM
                #pragma multi_compile NOT_COMBINED COMBINED
                #pragma multi_compile NOT_GRAYED GRAYED COLOR_FILTER
                #pragma multi_compile NOT_CLIPPED CLIPPED SOFT_CLIPPED ALPHA_MASK
                #pragma vertex vert
                #pragma fragment frag

                #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
    
                struct appdata_t
                {
                    float4 vertex : POSITION;
                    half4 color : COLOR;
                    float4 texcoord : TEXCOORD0;
                };
    
                struct v2f
                {
                    float4 vertex : SV_POSITION;
                    half4 color : COLOR;
                    float4 texcoord : TEXCOORD0;

                    #ifdef CLIPPED
                    float2 clipPos : TEXCOORD1;
                    #endif

                    #ifdef SOFT_CLIPPED
                    float2 clipPos : TEXCOORD1;
                    #endif
                };
    
                // sampler2D _MainTex;
                TEXTURE2D(_MainTex);
                SAMPLER(sampler_MainTex);
                half4 _MainTex_ST;

                #ifdef COMBINED
                sampler2D _AlphaTex;
                #endif
/*
                CBUFFER_START(UnityPerMaterial)
                #ifdef CLIPPED
                float4 _ClipBox = float4(-2, -2, 0, 0);
                #endif

                #ifdef SOFT_CLIPPED
                float4 _ClipBox = float4(-2, -2, 0, 0);
                float4 _ClipSoftness = float4(0, 0, 0, 0);
                #endif
                CBUFFER_END
*/

                CBUFFER_START(UnityPerMaterial)
                float4 _ClipBox = float4(-2, -2, 0, 0);
                float4 _ClipSoftness = float4(0, 0, 0, 0);
                CBUFFER_END
                
                #ifdef COLOR_FILTER
                float4x4 _ColorMatrix;
                float4 _ColorOffset;
                float _ColorOption = 0;
                #endif

                inline half3 GammaToLinearSpace (half3 sRGB)
                {
                    return sRGB * (sRGB * (sRGB * 0.305306011h + 0.682171111h) + 0.012522878h);
                }
            
                v2f vert (appdata_t v)
                {
                    v2f o;
                    o.vertex = TransformObjectToHClip(v.vertex.xyz);
                    o.texcoord = v.texcoord;
                    #if !defined(UNITY_COLORSPACE_GAMMA) && (UNITY_VERSION >= 550)
                    o.color.rgb = GammaToLinearSpace(v.color.rgb);
                    o.color.a = v.color.a;
                    #else
                    o.color = v.color;
                    #endif

                    #ifdef CLIPPED
                    o.clipPos = mul(unity_ObjectToWorld, v.vertex).xy * _ClipBox.zw + _ClipBox.xy;
                    #endif

                    #ifdef SOFT_CLIPPED
                    o.clipPos = mul(unity_ObjectToWorld, v.vertex).xy * _ClipBox.zw + _ClipBox.xy;
                    #endif

                    return o;
                }
                
                half4 frag (v2f i) : SV_Target
                {
                    half4 col = SAMPLE_TEXTURE2D(_MainTex, sampler_MainTex, i.texcoord.xy / i.texcoord.w) * i.color;

                    #ifdef COMBINED
                    col.a *= tex2D(_AlphaTex, i.texcoord.xy / i.texcoord.w).g;
                    #endif

                    #ifdef GRAYED
                    half grey = dot(col.rgb, half3(0.299, 0.587, 0.114));
                    col.rgb = half3(grey, grey, grey);
                    #endif

                    #ifdef SOFT_CLIPPED
                    float2 factor = float2(0,0);
                    if(i.clipPos.x<0)
                        factor.x = (1.0-abs(i.clipPos.x)) * _ClipSoftness.x;
                    else
                        factor.x = (1.0-i.clipPos.x) * _ClipSoftness.z;
                    if(i.clipPos.y<0)
                        factor.y = (1.0-abs(i.clipPos.y)) * _ClipSoftness.w;
                    else
                        factor.y = (1.0-i.clipPos.y) * _ClipSoftness.y;
                    col.a *= clamp(min(factor.x, factor.y), 0.0, 1.0);
                    #endif

                    #ifdef CLIPPED
                    float2 factor = abs(i.clipPos);
                    col.a *= step(max(factor.x, factor.y), 1);
                    #endif

                    #ifdef COLOR_FILTER
                    if (_ColorOption == 0)
                    {
                        half4 col2 = col;
                        col2.r = dot(col, _ColorMatrix[0]) + _ColorOffset.x;
                        col2.g = dot(col, _ColorMatrix[1]) + _ColorOffset.y;
                        col2.b = dot(col, _ColorMatrix[2]) + _ColorOffset.z;
                        col2.a = dot(col, _ColorMatrix[3]) + _ColorOffset.w;
                        col = col2;
                    }
                    else //premultiply alpha
                        col.rgb *= col.a;
                    #endif

                    #ifdef ALPHA_MASK
                    clip(col.a - 0.001);
                    #endif

                    return col;
                }
            ENDHLSL
        }
    }
}
