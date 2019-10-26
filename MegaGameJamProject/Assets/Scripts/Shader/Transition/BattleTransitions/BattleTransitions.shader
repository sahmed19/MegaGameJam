// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Hidden/BattleTransitions"
{
	Properties
	{
		_MainRenderTex("RenderTexture", 2D) = "white" {}
		_MainRenderTex2("OtherRenderTexture", 2D) = "white" {}
		_TransitionTex("Transition Texture", 2D) = "white" {}
		_Color("TearColor", Color) = (0,0,0,1)
		_Cutoff("Cutoff", Range(0, 1)) = 0
	}

		SubShader
		{
			// No culling or depth
			Cull Off ZWrite Off ZTest Always

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
					float2 uv1 : TEXCOORD1;
					float2 uv2 : TEXCOORD2;
					float4 vertex : SV_POSITION;
				};

				float4 _MainRenderTex_TexelSize;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					o.uv1 = v.uv;
					o.uv2 = v.uv;

					#if UNITY_UV_STARTS_AT_TOP
					if (_MainRenderTex_TexelSize.y < 0)
						o.uv2.y = 1 - o.uv2.y;
					#endif

					return o;
				}

				sampler2D _TransitionTex;

				sampler2D _MainRenderTex;
				sampler2D _MainRenderTex2;
				float _Cutoff;
				fixed4 _Color;

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 transit = tex2D(_TransitionTex, i.uv1);

					fixed4 col = tex2D(_MainRenderTex, i.uv);
					fixed4 col2 = tex2D(_MainRenderTex2, i.uv1);

					if (transit.b < _Cutoff) {
						if(_Cutoff - transit.b > .03f) {
						return col2;
						} else {
							return _Color;
						}
					}

					return col;
				}					
				ENDCG
			}
		}
}
