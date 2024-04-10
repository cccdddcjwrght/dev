Shader "Effect/Effect_Base_GJ " {
    Properties {
		[HideInInspector]_Cutoff ("Alpha cutoff", Range(0,1)) = 0.5	
		[Enum(UnityEngine.Rendering.BlendMode)] SrcBlend("SrcBlend", Float) = 5//SrcAlpha
		[Enum(UnityEngine.Rendering.BlendMode)] DstBlend("DstBlend", Float) = 10//One
		[Enum(UnityEngine.Rendering.CullMode)] _Cull("Cull Mode", Float) = 0		
		[Enum(off,0,On,1)] _ZWrite ("ZWrite", float) = 0
		[Space(10)]
		[Toggle(IsCoustom)]IsCoustom("IsCoustom",int) = 0
		[Space(10)]
		[HDR] _BaseColor ("Base Color", Color) = (1,1,1,1)
		[Space(10)]
        _BaseMap ("Base Map", 2D) = "white" {}
		[Toggle(DeBlackBG)]DeBlackBG("DeBlackBG",int) = 0
        _BaseMapBrightness ("BaseMap Brightness", Float ) = 1
        _BaseMapPower ("BaseMap Power", Float ) = 1		
		_BaseMapPannerX ("BaseMapPannerX",Float) = 0 
		_BaseMapPannerY ("BaseMapPannerY",Float) = 0
		[Space(10)]
        _TurbulenceTex ("Turbulence Tex", 2D) = "white" {}
        _TurbulenceTexPannerX ("Turbulence Tex Panner X", Float ) = 0
        _TurbulenceTexPannerY ("Turbulence Tex Panner Y", Float ) = 0
		_TurbulenceStrength ("TurbulenceStrength", Float ) = 0
		[Space(10)]
		_MaskTex ("Mask Tex", 2D) = "white" {}
        _MaskTexPannerX ("Mask Tex Panner X", Float ) = 0
        _MaskTexPannerY ("Mask Tex Panner Y", Float ) = 0
		[Space(10)]
		_DissolveTex ("Dissolve Tex", 2D) = "white" {}
        _DissolvePannerX ("Dissolve Tex Panner X", Float ) = 0
        _DissolvePannerY ("Dissolve Tex Panner Y", Float ) = 0
		_DisSoftness ("DisSoftness",range (0.1,2)) = 0.1
		_AnDissolve ("AnDissolve",range (-2,2)  ) = -2
		[Space(10)]
		[Toggle(IsFresnel)]IsFresnel("IsFresnel",int) = 0
		[HDR] _FColor ("FColor",Color) = (1,1,1,1)
		_FScale ("FScale",Float ) = 1
		[Space(10)]
		[Toggle(IsDoubleFace)]IsDoubleFace("IsDoubleFace",int) = 0
		[HDR] _FaceInColor ("FaceInColor",Color) = (1,1,1,1)
		[HDR] _FaceOurColor ("FaceOurColor",Color) = (1,1,1,1)
		[Space(10)]
		_PointMove ("PointMove",vector ) =(0,0,0,0)     
		//[Enum(UnityEngine.Rendering.CompareFunction)]  _ZTest ("ZTest ",float) = 0	
    }
    SubShader {
        Tags {
            "IgnoreProjector"="True"
            "Queue"="Transparent"
            "RenderType"="Transparent"
        }
        Pass {
            Name "FORWARD"
            Tags {
                "LightMode"="UniversalForward"
            }
			Blend[SrcBlend][DstBlend]
            Cull [_Cull]
            ZWrite [_ZWrite]
            //ZTest [_ZTest]

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
			//#include "../../Shaders/ShaderLib/Fog.hlsl"
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"
			#pragma multi_compile _ EnableFog
			#pragma shader_feature __ DeBlackBG
			#pragma shader_feature __ IsFresnel
			#pragma shader_feature __ IsCoustom
			#pragma shader_feature __ IsDoubleFace


            CBUFFER_START(UnityPerMaterial)
				real4 _BaseColor;
				real4 _BaseMap_ST;
	            real _BaseMapBrightness;
				real _BaseMapPower;
				real _BaseMapPannerX;
				real _BaseMapPannerY;
				real4 _TurbulenceTex_ST;
            	real _TurbulenceTexPannerX;
            	real _TurbulenceTexPannerY;
				real _TurbulenceStrength;
				real4 _MaskTex_ST;
	            real _MaskTexPannerX;
	            real _MaskTexPannerY;
				real4 _DissolveTex_ST;
	            real _DissolvePannerX;
	            real _DissolvePannerY;
				real _DisSoftness;
				real _AnDissolve;
				real4 _PointMove;
				real _FScale;
				real4 _FColor;
				real4 _FaceInColor;
				real4 _FaceOurColor;
				
            CBUFFER_END
			
			TEXTURE2D(_BaseMap);
			SAMPLER(sampler_BaseMap);
			TEXTURE2D(_TurbulenceTex);
			SAMPLER(sampler_TurbulenceTex);
			TEXTURE2D(_MaskTex);
			SAMPLER(sampler_MaskTex);
			TEXTURE2D(_DissolveTex);
			SAMPLER(sampler_DissolveTex);

            struct VertexInput {
                real4 vertex : POSITION;
                real2 texcoord0 : TEXCOORD0;
                real4 texcoord1 : TEXCOORD1;
                real4 vertexColor : COLOR;
				real3 normal : NORMAL;
            };
            struct VertexOutput {
                real4 pos : SV_POSITION;
                real2 uv0 : TEXCOORD0;
                real4 uv1 : TEXCOORD1;
                real4 vertexColor : COLOR;
				real3 normal : TEXCOORD2;
				real3 posWS : TEXCOORD3;
				//FOG_INPUT(4)
            };


            VertexOutput vert (VertexInput v) {
				//Point move 
				real4 turCol = SAMPLE_TEXTURE2D_LOD(_TurbulenceTex, sampler_TurbulenceTex, v.texcoord0 + real2(_Time.y  * _TurbulenceTexPannerX, _Time.y * _TurbulenceTexPannerY), 0) - _PointMove.w;
				v.vertex.xyz += real3(turCol.x * _PointMove.x, turCol.y * _PointMove.y,turCol.y * turCol.x * _PointMove.z) ;

                VertexOutput o = (VertexOutput)0;			 
                o.uv0 = v.texcoord0;
                o.uv1 = v.texcoord1;
                o.vertexColor = v.vertexColor;
                o.pos = TransformObjectToHClip( v.vertex.xyz);
				o.posWS = TransformObjectToWorld(v.vertex.xyz);
				o.normal = v.normal;

				//VERT_FOG_TRANSFORM(o, o.posWS);
                return o;
            }


            real4 frag(VertexOutput i , real facing : VFACE) : COLOR {	
				//DoubleFace
				real3 FaceColor = (1,1,1) ;
				#if defined (IsDoubleFace)
				real DoubleFace = (facing >= 0 ?  1 : 0);
					 FaceColor = (DoubleFace * _FaceOurColor + (1 - DoubleFace) *_FaceInColor ).rgb;
				# endif 
				//Fresnel
				real3  N = normalize( TransformObjectToWorldNormal(i.normal));
				real3  V = normalize(GetCameraPositionWS() - i.posWS);
				real4  F = pow(( 1 - saturate( dot( N , V))),_FScale) * _FColor ;			
				//MASK
				real2 _MaksUV = (i.uv0+(real2(_MaskTexPannerX,_MaskTexPannerY)*_Time.g) );
                real4 _MaskTex_var = SAMPLE_TEXTURE2D(_MaskTex, sampler_MaskTex, TRANSFORM_TEX(_MaksUV, _MaskTex));
				real _MaskOut = _MaskTex_var.r * _MaskTex_var.a ;
				//Tuberlence
				real2 _TurbulenceUV = (i.uv0+(real2(_TurbulenceTexPannerX,_TurbulenceTexPannerY)*_Time.g) );
                real4 _TurbulenceTex_var = SAMPLE_TEXTURE2D(_TurbulenceTex, sampler_TurbulenceTex, TRANSFORM_TEX(_TurbulenceUV, _TurbulenceTex));
				//Base
				real2 _BaseUVCruve = real2(i.uv1.r , i.uv1.g ) * 0  ;
				#if defined(IsCoustom)
					 _BaseUVCruve = real2(i.uv1.r , i.uv1.g )   ;
				# endif        				      
				real2 _BaseUVSelfMove = real2(_BaseMapPannerX , _BaseMapPannerY) * _Time.g ;
				real2 _BaseUV = (_TurbulenceTex_var.r * _TurbulenceStrength + (i.uv0 + real2(_BaseUVCruve + _BaseUVSelfMove)));
				#if defined(IsCoustom)
					 _BaseUV = (_TurbulenceTex_var.r * i.uv1.b  + (i.uv0 + real2(_BaseUVCruve + _BaseUVSelfMove)));
				# endif
                real4 _BaseMap_var = SAMPLE_TEXTURE2D(_BaseMap, sampler_BaseMap, TRANSFORM_TEX(_BaseUV, _BaseMap));
				//Disslove
				real2 _DissolveUV =(_TurbulenceTex_var.r*i.uv1.b ) + (i.uv0+real2(_DissolvePannerX * _Time.g ,_DissolvePannerY * _Time.g));
				real4 _DissolveTex_var = SAMPLE_TEXTURE2D(_DissolveTex, sampler_DissolveTex, TRANSFORM_TEX(_DissolveUV, _DissolveTex));
				real _DissolveOut = pow (( saturate (_DissolveTex_var.r*_DissolveTex_var.a  - saturate(i.uv1.a)*2 -  _AnDissolve ) ), _DisSoftness ) ;	
				//Out
				real _AphaOut = 1;
				real3 _ColorOut = (1,1,1);
				#if defined(IsFresnel)
				   _ColorOut = _BaseColor.rgb * (pow((_BaseMapBrightness * _BaseMap_var.rgb), _BaseMapPower) * i.vertexColor.rgb)* FaceColor.rgb + F.rgb;
				#else
				   _ColorOut = _BaseColor.rgb * (pow((_BaseMapBrightness * _BaseMap_var.rgb), _BaseMapPower) * i.vertexColor.rgb) * FaceColor.rgb;
				# endif

				#if defined(DeBlackBG)				
                 _AphaOut = _BaseColor.a * _BaseMap_var.r * _BaseMap_var.a  * _MaskOut  * _DissolveOut * i.vertexColor.a;			
				#else
				 _AphaOut = _BaseColor.a  * _BaseMap_var.a  * _MaskOut  * _DissolveOut * i.vertexColor.a;	
				# endif

				real4 col = real4(_ColorOut,_AphaOut);

				//fog
                //MIX_FOG_ALPHA(col, i);
				
                return col;
            }
            ENDHLSL
        }
    }
}

