// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/BlurShader" {
    Properties {
        _MainTex ("Texture", 2D) = "white" {}
        _intensity ("Intensity", Float) = 0.0075
    }
    SubShader {
        Tags { "Queue"="Transparent" "RenderType"="Opaque" }
        LOD 100

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Name "Horizontal Blur"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float _intensity;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _MainTex_ST;

            v2f vert(appdata_img v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                return o;
            }

            half4 frag(v2f i) : COLOR {
                half4 sum = half4(0, 0, 0, 0);
                float blur = _intensity;
                float2 tc = i.uv;

                sum += tex2D(_MainTex, float2(tc.x - 4.0 * blur, tc.y)) * 0.025;
                sum += tex2D(_MainTex, float2(tc.x - 3.0 * blur, tc.y)) * 0.05;
                sum += tex2D(_MainTex, float2(tc.x - 2.0 * blur, tc.y)) * 0.09;
                sum += tex2D(_MainTex, float2(tc.x - blur, tc.y)) * 0.12;

                sum += tex2D(_MainTex, float2(tc.x * blur, tc.y)) * 0.15;

                sum += tex2D(_MainTex, float2(tc.x + blur, tc.y)) * 0.12;
                sum += tex2D(_MainTex, float2(tc.x + 2.0 * blur, tc.y)) * 0.09;
                sum += tex2D(_MainTex, float2(tc.x + 3.0 * blur, tc.y)) * 0.05;
                sum += tex2D(_MainTex, float2(tc.x + 4.0 * blur, tc.y)) * 0.025;

                return half4(sum.rgb, 1.0);
            }
            ENDCG
        }

        GrabPass { }

        Pass {
            Blend SrcAlpha OneMinusSrcAlpha
            Name "Vertical Blur"

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            //#pragma fragmentoption ARB_precision_hint_fastest
            #include "UnityCG.cginc"

            sampler2D _GrabTexture : register(s0);
            float _intensity;

            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _GrabTexture_ST;

            v2f vert(appdata_img v) {
                v2f o;
                o.pos = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _GrabTexture);
                return o;
            }

            half4 frag(v2f i) : COLOR {
                half4 sum = half4(0, 0, 0, 0);
                float blur = _intensity;
                float2 tc = i.uv;

                sum += tex2D(_GrabTexture, float2(tc.x, tc.y - 4.0 * blur)) * 0.025;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y - 3.0 * blur)) * 0.05;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y - 2.0 * blur)) * 0.09;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y - blur)) * 0.12;

                sum += tex2D(_GrabTexture, float2(tc.x * blur, tc.y)) * 0.15;

                sum += tex2D(_GrabTexture, float2(tc.x, tc.y + blur)) * 0.12;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y + 2.0 * blur)) * 0.09;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y + 3.0 * blur)) * 0.05;
                sum += tex2D(_GrabTexture, float2(tc.x, tc.y + 4.0 * blur)) * 0.025;

                return half4(sum.rgb, 1.0);
            }
            ENDCG
        }
    }
    Fallback "Diffuse"
}
