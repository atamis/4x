Shader "Custom/WorldShader"
{
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
        //_Col ("_Col", Color) = (1, 1, 1, 1)
    }
 
    SubShader {
        Pass {
            ZTest Always Cull Off ZWrite Off Fog { Mode off }
 
            CGPROGRAM
 
            #pragma vertex vert_img
            #pragma fragment frag
            #pragma fragmentoption ARB_precision_hint_fastest

            #include "UnityCG.cginc"
            #pragma target 3.0

            uniform sampler2D _MainTex;
            //u _Col;

            half4 frag(v2f_img i): COLOR
            {
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
    FallBack "Diffuse"
}