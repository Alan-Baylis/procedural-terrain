Shader "Custom/World" {
	Properties {
		_Color("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Standard fullforwardshadows
		#pragma target 3.0

		fixed4 _Color;

		struct Input {
			float3 worldPos;
		};

		float minHeight;
		float maxHeight;

		float inverseLerp(float a, float b, float value) {
			return saturate((value - a) / (b - a));
		}

		void surf (Input IN, inout SurfaceOutputStandard o) {
			float persent = inverseLerp(minHeight, maxHeight, IN.worldPos.y);

			float3 color1 = lerp(float3(0.5, 0.5, 0.5), float3(1, 1, 1), inverseLerp(0.4, 1, persent));
			float3 color2 = lerp(float3(0, 1, 0), color1, inverseLerp(0.3, 0.4, persent));
			float3 color3 = lerp(float3(1, 1, 0), color2, inverseLerp(0.15, 0.3, persent));
			float3 color4 = lerp(float3(0, 0, 1), color3, inverseLerp(0, 0.15, persent));
			// o.Albedo = color4;

			o.Albedo = _Color * persent;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
