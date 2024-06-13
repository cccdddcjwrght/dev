// Unity built-in shader source. Copyright (c) 2016 Unity Technologies. MIT license (see license.txt)

Shader "UI/Focus_Mask"
{
  Properties
  {
    [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
    _Color ("Tint", Color) = (1,1,1,1)

    _StencilComp ("Stencil Comparison", Float) = 8
    _Stencil ("Stencil ID", Float) = 0
    _StencilOp ("Stencil Operation", Float) = 0
    _StencilWriteMask ("Stencil Write Mask", Float) = 255
    _StencilReadMask ("Stencil Read Mask", Float) = 255

    _ColorMask ("Color Mask", Float) = 15

    [Toggle(UNITY_UI_ALPHACLIP)] _UseUIAlphaClip ("Use Alpha Clip", Float) = 0
 //-------------------add----------------------
 _Center("Center", vector) = (0, 0, 0, 0)
 _Radius("Radius", Range(0,1000)) = 1000 // sliders
 _TransitionRange("Transition Range", Range(0, 100)) = 10
 _Width("Width", Float) = 1
 _Height("Height", Float) = 1
 _Ellipse("Ellipse", Float) = 4
 _ReduceTime("ReduceTime", Float) = 1
 _TotalTime("TotalTime", Float) = 1
 _StartTime("StartTime", Float) = 0
 _MaxRadius("MaxRadius", Float) = 1000
 //[KeywordEnum(ROUND, ELLIPSE, DYNAMIC_ROUND)] _RoundMode("Mask mode", Float) = 0
 //-------------------add----------------------
  }

  SubShader
  {
    Tags
    {
      "Queue"="Transparent"
      "IgnoreProjector"="True"
      "RenderType"="Transparent"
      "PreviewType"="Plane"
      "CanUseSpriteAtlas"="True"
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
    ZTest [unity_GUIZTestMode]
    Blend SrcAlpha OneMinusSrcAlpha
    ColorMask [_ColorMask]

    Pass
    {
      Name "Default"
    CGPROGRAM
      #pragma vertex vert
      #pragma fragment frag
      #pragma target 2.0

      #include "UnityCG.cginc"
      #include "UnityUI.cginc"

      #pragma multi_compile __ UNITY_UI_CLIP_RECT
      #pragma multi_compile __ UNITY_UI_ALPHACLIP
  #pragma multi_compile _ROUNDMODE_ROUND _ROUNDMODE_ELLIPSE _ROUNDMODE_DYNAMIC_ROUND

      struct appdata_t
      {
        float4 vertex  : POSITION;
        float4 color  : COLOR;
        float2 texcoord : TEXCOORD0;
        UNITY_VERTEX_INPUT_INSTANCE_ID
      };

      struct v2f
      {
        float4 vertex  : SV_POSITION;
        fixed4 color  : COLOR;
        float2 texcoord : TEXCOORD0;
        float4 worldPosition : TEXCOORD1;
        UNITY_VERTEX_OUTPUT_STEREO
      };

      fixed4 _Color;
      fixed4 _TextureSampleAdd;
      float4 _ClipRect;
  //-------------------add----------------------
  half _Radius;
  float2 _Center;
  half _TransitionRange;
  half _Width;
  half _Height;
  half _Ellipse;
  float _ReduceTime;
  half _TotalTime;
  float _StartTime;
  half _MaxRadius;

  //-------------------add----------------------

      v2f vert(appdata_t v)
      {
        v2f OUT;
        UNITY_SETUP_INSTANCE_ID(v);
        UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(OUT);
        OUT.worldPosition = v.vertex;
        OUT.vertex = UnityObjectToClipPos(OUT.worldPosition);

        OUT.texcoord = v.texcoord;

        OUT.color = v.color * _Color;
        return OUT;
      }

      sampler2D _MainTex;

      fixed4 frag(v2f IN) : SV_Target
      {
        half4 color = (tex2D(_MainTex, IN.texcoord) + _TextureSampleAdd) * IN.color;

        #ifdef UNITY_UI_CLIP_RECT
        color.a *= UnityGet2DClipping(IN.worldPosition.xy, _ClipRect);
        #endif

        #ifdef UNITY_UI_ALPHACLIP
        clip (color.a - 0.001);
        #endif

  //-------------------add----------------------
//#ifdef _ROUNDMODE_ROUND
//  //����ƬԪ���������Ŀ������λ�õľ���
//  float dis = distance(IN.worldPosition.xy, _Center.xy);
//  //���˵�����С�ڣ��뾶-���ɷ�Χ����ƬԪ
//  clip(dis - (_Radius - _TransitionRange));
//  //�Ż�if�����жϣ��������С�ڰ뾶��ִ����һ��������if(dis < _Radius)
//  fixed tmp = step(dis, _Radius);
//  //������ɷ�Χ�ڵ�alphaֵ
//  color.a *= (1 - tmp) + tmp * (dis - (_Radius - _TransitionRange)) / _TransitionRange;
//#elif _ROUNDMODE_ELLIPSE
//  //����X�᷽�����
//  float disX = distance(IN.worldPosition.x, _Center.x);
//  //����Y�᷽�����
//  float disY = distance(IN.worldPosition.y, _Center.y);
//  //������Բ���̼���ƬԪ��alphaֵ��_EllipseΪ��Բϵ��
//  fixed factor = clamp(pow(abs(disX / _Width), _Ellipse) + pow(abs(disY / _Height), _Ellipse), 0.0, 1.0);
//  //�Ż�if�����ж�
//  fixed tmp = step(factor, 1.0f);
//  //��ֵ��Բ�����Բ�ڵ�alphaֵ
//  color.a *= (1 - tmp) + tmp * factor;
//#else
  //_StartTimeΪЧ����ʼʱ��㣬Unity�ж�Ӧ��ֵmaterial.SetFloat("_StartTime", Time.timeSinceLevelLoad);
  float processTime = _Time.y - _StartTime;
  //�ж�shaderִ��ʱ���Ƿ񳬹�_TotalTime
  //clip(_TotalTime - processTime);
  //�Ż�if�����ж�
  fixed tmp = step(processTime, _ReduceTime);
  //���㵱ǰʱ����Բ���οհ뾶
  float curRadius = (1 - tmp) * _Radius + tmp * (_MaxRadius - (_MaxRadius - _Radius) * processTime / _ReduceTime);
  float dis = distance(IN.worldPosition.xy, _Center.xy);
  //��������С�ڵ�ǰԲ���οհ뾶��ƬԪ
  clip(dis - curRadius);
//#endif
  //-------------------add----------------------
        return color;
      }
    ENDCG
    }
  }
}