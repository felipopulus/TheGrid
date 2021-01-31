// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'

Shader "TheGrid/NodeShaderDesktop" {
	Properties {
		
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness ("Smoothness", Range(0,1)) = 0.5
		_Metallic ("Metallic", Range(0,1)) = 0.0
		[Header(Beacon 1)]
		[Toggle] _Enable1 ("Enable", int) = 1
		_BeaconColor1 ("Beacon Color", COLOR) = (1,0,0,1)
		_PlayerAoOffset1 ("Player AO Offset", float) = 0.25
		_PlayerPosition1 ("Player Position", vector) = (0,0,0,0)
		_BeaconPosition1 ("Beacon Position", vector) = (0,0,0,0)
		_BeaconCtrl1 ("Beacon Light Control", vector) = (1,-0.45,2,0)
		_BeaconBlend1 ("Beacon Blend Control", vector) = (0.1,-0.45,2,0)
		
		[Header(Beacon 2)]
		[Toggle] _Enable2 ("Enable", int) = 1 
		_BeaconColor2 ("Beacon Color", COLOR) = (0,1,0,1)
		_PlayerAoOffset2 ("Player AO Offset", float) = 0.25
		_PlayerPosition2 ("Player Position", vector) = (0,0,0,0)
		_BeaconPosition2 ("Beacon Position", vector) = (0,0,0,0)
		_BeaconCtrl2 ("Beacon Light Control", vector) = (1,-0.45,2,0)
		_BeaconBlend2 ("Beacon Blend Control", vector) = (0.1,-0.45,2,0)
		
		[Header(Beacon 3)]
		[Toggle] _Enable3 ("Enable", int) = 1
		_BeaconColor3 ("Beacon Color", COLOR) = (0,1,0,1)
		_PlayerAoOffset3 ("Player AO Offset", float) = 0.25
		_PlayerPosition3 ("Player Position", vector) = (0,0,0,0)
		_BeaconPosition3 ("Beacon Position", vector) = (0,0,0,0)
		_BeaconCtrl3 ("Beacon Light Control", vector) = (1,-0.45,2,0)
		_BeaconBlend3 ("Beacon Blend Control", vector) = (0.1,-0.45,2,0)
		
		[Header(Beacon 4)]
		[Toggle] _Enable4 ("Enable", int) = 1
		_BeaconColor4 ("Beacon Color", COLOR) = (0,0,1,1)
		_PlayerAoOffset4 ("Player AO Offset", float) = 0.25
		_PlayerPosition4 ("Player Position", vector) = (0,0,0,0)
		_BeaconPosition4 ("Beacon Position", vector) = (0,0,0,0)
		_BeaconCtrl4 ("Beacon Light Control", vector) = (1,-0.45,2,0)
		_BeaconBlend4 ("Beacon Blend Control", vector) = (0.1,-0.45,2,0)
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
		
		bool _Enable1;
		half4 _PlayerPosition1;
		half _PlayerAoOffset1;
		half4 _BeaconPosition1;
		fixed4 _BeaconColor1;
		half3 _BeaconCtrl1;
		half3 _BeaconBlend1;
		
		bool _Enable2;
		half4 _PlayerPosition2;
		half _PlayerAoOffset2;
		half4 _BeaconPosition2;
		fixed4 _BeaconColor2;
		half3 _BeaconCtrl2;
		half3 _BeaconBlend2;
		
		bool _Enable3;
		half4 _PlayerPosition3;
		half _PlayerAoOffset3;
		half4 _BeaconPosition3;
		fixed4 _BeaconColor3;
		half3 _BeaconCtrl3;
		half3 _BeaconBlend3;
		
		bool _Enable4;
		half4 _PlayerPosition4;
		half _PlayerAoOffset4;
		half4 _BeaconPosition4;
		fixed4 _BeaconColor4;
		half3 _BeaconCtrl4;
		half3 _BeaconBlend4;
		
		void vert (inout appdata_full v, out Input o) {
          	UNITY_INITIALIZE_OUTPUT(Input, o);
			o.v_pos_and_ao.xyz = mul(unity_ObjectToWorld, v.vertex).xyz;
			o.v_pos_and_ao.a = 0.0;
			if (_Enable1) { o.v_pos_and_ao.a = o.v_pos_and_ao.a + clamp(_PlayerAoOffset1 / (distance(o.v_pos_and_ao.xyz, _PlayerPosition1)), 0.0, 1.0); }
          	if (_Enable2) { o.v_pos_and_ao.a = o.v_pos_and_ao.a + clamp(_PlayerAoOffset2 / (distance(o.v_pos_and_ao.xyz, _PlayerPosition2)), 0.0, 1.0); }
          	if (_Enable3) { o.v_pos_and_ao.a = o.v_pos_and_ao.a + clamp(_PlayerAoOffset3 / (distance(o.v_pos_and_ao.xyz, _PlayerPosition3)), 0.0, 1.0); }
          	if (_Enable4) { o.v_pos_and_ao.a = o.v_pos_and_ao.a + clamp(_PlayerAoOffset4 / (distance(o.v_pos_and_ao.xyz, _PlayerPosition4)), 0.0, 1.0); }
       }
		
		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			//IN.position;
			//half3 texture_vector = _BeaconBlend1[2] * normalize(IN.v_pos_and_ao.xyz - _BeaconPosition1.xyz);
			half3 texture_vector = normalize(IN.v_pos_and_ao.xyz - _BeaconPosition1.xyz);
			
			half4 beacon_distance1 = distance(IN.v_pos_and_ao.xyz, _BeaconPosition1.xyz);
			half4 beacon_distance2 = distance(IN.v_pos_and_ao.xyz, _BeaconPosition2.xyz);
			half4 beacon_distance3 = distance(IN.v_pos_and_ao.xyz, _BeaconPosition3.xyz);
			half4 beacon_distance4 = distance(IN.v_pos_and_ao.xyz, _BeaconPosition4.xyz);
			
			fixed4 player_ao = fixed4(IN.v_pos_and_ao.a, IN.v_pos_and_ao.a, IN.v_pos_and_ao.a, 1.0);
			
			half4 beacon_color = (0.0, 0.0, 0.0, 0.0);
			if (_Enable1) { beacon_color = beacon_color + _BeaconColor1 * _BeaconCtrl1[0] / (beacon_distance1 + _BeaconCtrl1[1]); }
			if (_Enable2) { beacon_color = beacon_color + _BeaconColor2 * _BeaconCtrl2[0] / (beacon_distance2 + _BeaconCtrl2[1]); }
			if (_Enable3) { beacon_color = beacon_color + _BeaconColor3 * _BeaconCtrl3[0] / (beacon_distance3 + _BeaconCtrl3[1]); }
			if (_Enable4) { beacon_color = beacon_color + _BeaconColor4 * _BeaconCtrl4[0] / (beacon_distance4 + _BeaconCtrl4[1]); }
			
			half4 beacon_blend = (0.0, 0.0, 0.0, 0.0);
			if (_Enable1) { beacon_blend = beacon_blend + _BeaconBlend1[0] / pow(beacon_distance1 + _BeaconBlend1[1], _BeaconBlend1[2]); }
			if (_Enable2) { beacon_blend = beacon_blend + _BeaconBlend2[0] / pow(beacon_distance2 + _BeaconBlend2[1], _BeaconBlend2[2]); }
			if (_Enable3) { beacon_blend = beacon_blend + _BeaconBlend3[0] / pow(beacon_distance3 + _BeaconBlend3[1], _BeaconBlend3[2]); }
			if (_Enable4) { beacon_blend = beacon_blend + _BeaconBlend4[0] / pow(beacon_distance4 + _BeaconBlend4[1], _BeaconBlend4[2]); }
			
			//fixed4 c = tex3D(_Volume, IN.v_volume + float3(_BeaconBlendCtrl1, _BeaconBlendCtrl2, _BeaconBlendCtrl3));
			fixed4 c = tex2D(_MainTex, IN.uv_MainTex + beacon_blend) * _Color - player_ao + beacon_color;
			
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
