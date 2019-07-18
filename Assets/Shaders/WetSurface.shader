Shader "Custom/WetSurface" {
	Properties {
		_MainTex("Texture", 2D) = "white" {}
	_BumpMap("WetMap", 2D) = "wet" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
	}
	SubShader {
			Tags{ "RenderType" = "Opaque" }
			CGPROGRAM
#pragma surface surf Lambert
			struct Input {
			float2 uv_MainTex;
			float2 uv_wetmap;
		};
		sampler2D _MainTex;
		sampler2D _wetmap;
		void surf(Input IN, inout SurfaceOutput o) {
			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			o.Normal = UnpackNormal(tex2D(_wetmap, IN.uv_wetmap));
		}
		ENDCG
		}
			Fallback "Diffuse"
}