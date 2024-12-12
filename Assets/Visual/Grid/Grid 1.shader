Shader "Unlit/Grid"
{
    Properties
    {
        _MainTex("Texture", 2D) = "white" {}
        [HDR]_GridColour("Grid Colour", Color) = (.255,.0,.0,1)
        _GridSize("Grid Size", Range(0.0001, 0.01)) = 0.002
        _GridLineThickness("Grid Line Thickness", Range(0.000001, 0.0001)) = 0.00005
        _Alpha("Grid Transparency", Range(0, 1)) = 0.5
        _Intensity("Emission Intensity", Range(-50,50)) = 0
        _FlickerSpeed("Flicker Speed", Range(0.1, 5.0)) = 1.0
        _FlickerAmount("Flicker Amount", Range(0.0, 1.0)) = 0.5
    }

        SubShader
        {
            Tags { "RenderType" = "Opaque" }
            LOD 100

            Pass
            {
                CGPROGRAM
                #pragma vertex vert
                #pragma fragment frag
                // make fog work
                #pragma multi_compile_fog

                #include "UnityCG.cginc"

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
                float4 _GridColour;
                float _GridSize;
                float _GridLineThickness;
                float _Alpha;
                float _Intensity;
                float _FlickerSpeed;
                float _FlickerAmount;

                v2f vert(appdata v)
                {
                    v2f o;
                    o.vertex = UnityObjectToClipPos(v.vertex);
                    o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                    UNITY_TRANSFER_FOG(o,o.vertex);
                    return o;
                }

                float GridTest(float2 r) {
                    float result;

                    for (float i = 0.0; i <= 1; i += _GridSize) {
                        for (int j = 0; j < 2; j++) {
                            result += 1.0 - smoothstep(0.0, _GridLineThickness,abs(r[j] - i));
                        }
                    }

                    return result;
                }

                float Random(float2 st) {
                    return frac(sin(dot(st.xy,
                                         float2(12.9898,78.233)))
                                * 43758.5453123);
                }

                fixed4 frag(v2f i) : SV_Target
                {
                    float flicker = Random(i.uv + _Time.y * _FlickerSpeed);
                    flicker = pow(flicker, _FlickerAmount);
                    fixed4 gridColour = (_GridColour * GridTest(i.uv)) + tex2D(_MainTex, i.uv);
                    gridColour.rgb *= pow(2.0, _Intensity) * flicker;
                    gridColour.a = _Alpha;
                    return gridColour;
                }
                ENDCG
            }
        }
}
