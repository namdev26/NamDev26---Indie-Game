Shader "Custom/URP_VertexColorOnly"
{
    SubShader
    {
        Tags {"RenderType"="Opaque" "Queue"="Geometry"}

        Pass
        {
            Name "ForwardLit"
            Tags {"LightMode"="UniversalForward"}

            HLSLPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #pragma target 3.0
            
            #include "Packages/com.unity.render-pipelines.universal/ShaderLibrary/Core.hlsl"

            struct Attributes
            {
                float4 positionOS : POSITION;
                float4 color : COLOR;   // <-- nhận màu từ mesh
            };

            struct Varyings
            {
                float4 positionHCS : SV_POSITION;
                float4 color : COLOR;
            };

            Varyings vert (Attributes IN)
            {
                Varyings OUT;
                OUT.positionHCS = TransformObjectToHClip(IN.positionOS.xyz);
                OUT.color = IN.color;    // <-- truyền màu sang pixel shader
                return OUT;
            }

            float4 frag (Varyings IN) : SV_Target
            {
                return IN.color;         // <-- tô đúng màu vertex
            }

            ENDHLSL
        }
    }
}
