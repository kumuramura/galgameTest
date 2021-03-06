Shader "Custom/changePicture"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
		_CutTex("Cut Tex",2D)="white"{}
 
		// 切换速度
		_Speed("speed",Range(-1,1))=0.2
 
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100
 
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
    
                float4 vertex : SV_POSITION;
            };
 
            sampler2D _MainTex;
            float4 _MainTex_ST;
			sampler2D _CutTex;
			float _Speed;
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
  
                return o;
            }
 
            fixed4 frag (v2f i) : SV_Target
            {
                // sample the texture
                fixed4 col = tex2D(_MainTex,i.uv);
                
 
				// 过场切换的位置
				float curPos = _Time.y*_Speed;
				// 当小于 x,渲染_CutTex（开始都是小于 x的，开始渲染_CutTex), 
				// 随着事件变化,切换为 _MainTex
				if(curPos < i.uv.x){
					col = tex2D(_CutTex,i.uv);
				}
				
 
                return col;
            }
            ENDCG
        }
    }

}
