Shader "Custom/TileShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _LightTex ("_LightTex", 2D) = "white" {}
	}

	SubShader {
        Pass {
			LOD 200

			CGPROGRAM
			#pragma vertex vert_img
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			bool _shadows;

			half4 frag (v2f_img i) : COLOR {
                half4 c = tex2D(_MainTex, i.uv); // get the texture color
                float lum = c.r*.3 + c.g*.59 + c.b*.11; 
				float3 bw = float3( lum, lum, lum ); 
				
				float4 result = c;
				result.rgb = lerp(c.rgb, bw, 1);
				result.r = result.r * .25;
				result.g = result.g * .41;
				result.b = result.b * .87;

                return result;
			}
			ENDCG
		}
	}
	Fallback off
}
