Shader "Custom/NoirShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_bwBlend ("bwBlend", Float) = 0
	}
	SubShader
	{
		Pass {
			ZTest Always Cull Off ZWrite Off Fog { Mode off }

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"
			#pragma fragmentoption ARB_precision_hint_fastest

			uniform sampler2D _MainTex;
			uniform float _bwBlend = 1;

			half4 frag(v2f_img i) : COLOR {
				half4 c = tex2D(_MainTex, i.uv);
				
				float lum = c.r*.3 + c.g*.59 + c.b*.11;
				float3 bw = float3( lum, lum, lum ); 
				
				half4 result = c;
				result.rgb = lerp(c.rgb, bw, _bwBlend);
				return result;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
