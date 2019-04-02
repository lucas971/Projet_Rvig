Shader "Custom/SnowTracks"
{
    Properties
    {
		_NormalMap("Normalmap", 2D) = "bump" {}
		_Tess("Tessellation", Range(1,32)) = 4 
		_SnowTex ("Albedo (RGB)", 2D) = "white" {}
		_SnowColor ("Color", Color) = (1,1,1,1)
		_GroundTex("Albedo (RGB)", 2D) = "white" {}
		_GroundColor("Color", Color) = (1,1,1,1)
		_Splat ("SplatMap",2D) = "black" {}
		_Displacement ("Displacement",Range(0,1.0)) = 0.3
        
       
        _Glossiness ("Smoothness", Range(0,1)) = 0.5
        _Metallic ("Metallic", Range(0,1)) = 0.0
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows vertex:disp tessellate:tessDistance

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 4.6

		#include "Tessellation.cginc"

			struct appdata {
			float4 vertex : POSITION;
			float4 tangent : TANGENT;
			float3 normal : NORMAL;
			float2 texcoord : TEXCOORD0;

		};

		float _Tess;

		float4 tessDistance(appdata v0, appdata v1, appdata v2) {
			float minDist = 10.0;
			float maxDist = 60.0;
			return UnityDistanceBasedTess(v0.vertex, v1.vertex, v2.vertex, minDist, maxDist, _Tess);
		}

		sampler2D _Splat;
		float _Displacement;

		void disp(inout appdata v)
		{
			float d = tex2Dlod(_Splat, float4(v.texcoord.xy,0,0)).r * _Displacement;
			v.vertex.xyz -= v.normal * d;
			v.vertex.xyz += v.normal * _Displacement; //Pour replacer la hitbox en bas, en déplaçant tout en haut
		}

        sampler2D _SnowTex;
		fixed4 _SnowColor;
		sampler2D _GroundTex;
		fixed4 _GroundColor;


        struct Input
        {
            float2 uv_GroundTex;
			float2 uv_SnowTex;
			float2 uv_Splat;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;
		sampler2D _NormalMap;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
			half amount = tex2Dlod(_Splat, float4(IN.uv_Splat, 0, 0)).r;
			fixed4 c = lerp(tex2D(_SnowTex, IN.uv_SnowTex) * _SnowColor, tex2D(_GroundTex, IN.uv_GroundTex) * _GroundColor, amount);
            //fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
			o.Normal = UnpackNormal(tex2D(_NormalMap, IN.uv_SnowTex));
        }
        ENDCG
    }
    FallBack "Diffuse"
}
