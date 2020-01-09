Shader "Unlit/TestShader"
{
	Properties
	{
		_LineColor("LineColor", Color) = (1, 1, 1, 1)
		_BackColor("BackColor", Color) = (1, 1, 1, 1)
		_MainTex("Base (RGB) Trans (A)", 2D) = "white" {}
	_Angle("Angle", float) = 0.0
		_LineSpace("LineSpace", float) = 0.0
		_LineWeight("LineWieght", float) = 0.2
		_Dis("Distance", Range(0, 0.5)) = 0.2
	}
		SubShader
	{

		Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent + 5" }
		LOD 100
		ZTest Always
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		Pass
	{
		CGPROGRAM
#pragma vertex vert
#pragma fragment frag
		// make fog work

		//#pragma multi_compile_fog

#include "UnityCG.cginc"
#define Pi 3.141592
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

	sampler2D _MainTex;
	float4 _MainTex_ST;
	float4 _LineColor;
	float4 _BackColor;
	half _LineSpace;
	half _Angle;
	half _LineWeight;
	half _Dis;

	v2f vert(appdata v)
	{
		v2f o;
		o.vertex = UnityObjectToClipPos(v.vertex);
		o.uv = TRANSFORM_TEX(v.uv, _MainTex);
		UNITY_TRANSFER_FOG(o,o.vertex);
		return o;
	}


	fixed4 frag(v2f i) : SV_Target
	{
		// sample the texture
		float4 col = _LineColor;
		col.a -= abs(0.5 - i.uv.y);
		col.a *= (1 - step(_Dis, i.uv.x)) + step(0.5 - _Dis, i.uv.x - 0.5);
		col.a *= step(_LineWeight, abs(sin(i.uv.y * _LineSpace - _Angle)));
		
		return col;
	}
		ENDCG
	}
	}
}
