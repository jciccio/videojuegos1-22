Shader "CI0162/SineWaveShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
        _Modifier ("Modifier", Float) = 0
        _Distance ("Amplitude", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" "ÏgnoreProjecto" = "False" "Queue"="Geometry" }
        
        ZWrite On 
        Blend Off 
        Cull Off 
        Lighting On
        Fog {Mode Off}

        Pass{
    
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag
            #pragma glsl

            uniform fixed4 _Color;
            uniform fixed _Modifier;
            uniform fixed _Distance;

            struct appdata{
                half4 vertex : POSITION;
            };

            struct v2f{
                half4 pos : SV_POSITION;
                half4 other : COLOR;
            };

            v2f vert(appdata v){
                v2f o;
                fixed xValue = v.vertex.x * 3.14159 * 10;
                fixed zValue = v.vertex.z * 3.14159 * 10;
                fixed distance = sqrt(xValue* xValue + zValue*zValue) + _Modifier;
                v.vertex.y = sin(distance) * _Distance;

                o.pos = UnityObjectToClipPos(v.vertex);
                o.other = v.vertex;
                return o;
            }

            float4 frag(v2f IN): SV_TARGET{
                fixed4 newColor;

                newColor.r = _Color.x *  (IN.other.y * 10 + 0.5);
                newColor.g = _Color.y*  (IN.other.y* 10 + 0.5);
                newColor.b = _Color.z*  (IN.other.y* 10 + 0.5);

                //newColor.rgb = _Color.xyz * IN.other.xyz + 0.5;

                return newColor;
            }
            ENDCG
        }

    }
    FallBack "Diffuse"
}
