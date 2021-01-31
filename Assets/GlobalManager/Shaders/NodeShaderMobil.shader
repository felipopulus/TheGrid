// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "TheGrid/NodeShaderMobil" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		_PlayerPosition ("Player Position", vector) = (0,0,0,0)
		_PlayerAoOffset ("Player AO Offset", float) = 0.0
		_BeaconPosition ("Beacon Position", vector) = (0,0,0,0)
		_BeaconColor ("Beacon Color", COLOR) = (1,0,0,1)
		_BeaconCtrl1 ("Beacon Light Control 1", float) = 0.0
		_BeaconCtrl2 ("Beacon Light Control 2", float) = 0.0
		_BeaconCtrl3 ("Beacon Light Control 3", float) = 0.0
		_BeaconBendCtrl1 ("Beacon Bend Control 1", float) = 0.0
		_BeaconBendCtrl2 ("Beacon Bend Control 2", float) = 0.0
		_BeaconBendCtrl3 ("Beacon Bend Control 3", float) = 0.0
		
		
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows vertex:vert

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			half4 v_pos_and_ao;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;
		half4 _PlayerPosition;
		half _PlayerAoOffset;
		half4 _BeaconPosition;
		fixed4 _BeaconColor;
		half _BeaconCtrl1;
		half _BeaconCtrl2;
		half _BeaconCtrl3;
		half _BeaconBendCtrl1;
		half _BeaconBendCtrl2;
		half _BeaconBendCtrl3;
		
		void vert (inout appdata_full v, out Input o) {
          	UNITY_INITIALIZE_OUTPUT(Input, o);
			o.v_pos_and_ao.xyz = mul(unity_ObjectToWorld, v.vertex).xyz;
          	o.v_pos_and_ao.a = clamp(_PlayerAoOffset / (distance(o.v_pos_and_ao.xyz, _PlayerPosition)), 0.0, 0.6);
      	}
		
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//IN.position;
			half3 texture_vector = _BeaconBendCtrl3 * normalize(IN.v_pos_and_ao.xyz - _BeaconPosition.xyz);
			half4 beacon_distance = distance(IN.v_pos_and_ao.xyz, _BeaconPosition.xyz);
			fixed4 player_ao = fixed4(IN.v_pos_and_ao.a, IN.v_pos_and_ao.a, IN.v_pos_and_ao.a, 1.0);
			half4 beacon_color = _BeaconColor * _BeaconCtrl1 / (beacon_distance + _BeaconCtrl2);
			half4 beacon_bend = _BeaconBendCtrl1 / pow(beacon_distance + _BeaconBendCtrl2, _BeaconBendCtrl3);
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex + beacon_bend) * _Color - player_ao + beacon_color;
			//fixed4 c = tex3D(_Volume, IN.v_volume + float3(_BeaconBendCtrl1, _BeaconBendCtrl2, _BeaconBendCtrl3));
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
