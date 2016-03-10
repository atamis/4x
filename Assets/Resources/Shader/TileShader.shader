Shader "Custom/TileShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
        _LightTex ("_LightTex", 2D) = "white" {}
        _shadows ("Shadows", bool) = true
	}

	SubShader {
        Pass {
            Tags { "RenderType"="Transparent", "RenderQueue"="Transparent" }
			LOD 200

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			sampler2D _MainTex;
			bool _shadows;

			struct v2f {
				float4 pos : POSITION;
				float2 uv : TEXCOORD0;
			};

			v2f vert( appdata_img v )
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				o.uv = v.texcoord.xy;

				return o;
			}

			half4 frag (v2f i) : COLOR {
                half4 c = tex2D(_MainTex, i.uv);
                
                return c;
			}
			ENDCG
		}
	}
	Fallback off
}
