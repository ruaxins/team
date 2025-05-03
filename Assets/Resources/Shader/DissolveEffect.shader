Shader "UI/DissolveEffect"  
{  
    Properties  
    {  
        _MainTex ("Sprite Texture", 2D) = "white" {} // Source Image  
        _Color ("Tint", Color) = (1,1,1,1) // Image 的 Color        
        _NoiseTex ("Noise Texture", 2D) = "white" {} // 溶解用的噪声图  
        _Cutoff ("Dissolve Amount", Range(0,1)) = 0.0 // 溶解控制  
        [HDR]_EdgeColor ("Edge Color", Color) = (1,0.5,0,1) // 溶解边缘颜色  
        _EdgeWidth ("Edge Width", Range(0,0.2)) = 0.05 // 边缘宽度  
    }  
    
    SubShader  
    {  
        Tags  
        {  
            "Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"  
        }  
        Blend SrcAlpha OneMinusSrcAlpha  
        Cull Off        Lighting Off        ZWrite Off  
        Pass        
        {  
            CGPROGRAM  
            #pragma vertex vert  
            #pragma fragment frag  
            #include "UnityCG.cginc"  
            
            sampler2D _MainTex;  
            sampler2D _NoiseTex;  
            float4 _MainTex_ST;  
            float4 _NoiseTex_ST;  
            float4 _Color;  
            float _Cutoff;  
            float4 _EdgeColor;  
            float _EdgeWidth;  
            
            struct appdata_t  
            {  
                float4 vertex : POSITION;  
                float2 texcoord : TEXCOORD0;  
            };  
            struct v2f  
            {  
                float2 uv : TEXCOORD0;  
                float2 uvNoise : TEXCOORD1;  
                float4 vertex : SV_POSITION;  
            };  
            v2f vert(appdata_t v)  
            {                v2f o;  
                o.vertex = UnityObjectToClipPos(v.vertex);  
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);  
                o.uvNoise = TRANSFORM_TEX(v.texcoord, _NoiseTex);  
                return o;  
            }  
            fixed4 frag(v2f i) : SV_Target  
            {  
                float2 uv = i.uv;  
                
                fixed4 texColor = tex2D(_MainTex, uv) * _Color;  
                float noise = tex2D(_NoiseTex, i.uvNoise).r +_EdgeWidth; // 这里加上 _EdgeWidth 是为了避免噪声值为 0 时，还会触发燃烧效果  
                clip(noise-_Cutoff); // 这里 clip 相当于控制是否溶解  
                
                float edge = smoothstep(_Cutoff, _Cutoff+ _EdgeWidth , noise);  
                texColor.rgb = lerp(_EdgeColor.rgb, texColor.rgb, edge);  
                texColor.a *= edge; // 让边缘透明度渐隐（可选）  
                return texColor;  
            }            
            ENDCG  
        }  
    }
}