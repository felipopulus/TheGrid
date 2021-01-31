// Upgrade NOTE: replaced '_Object2World' with 'unity_ObjectToWorld'
// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "TheGrid/ProximityShader" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {} // Regular object texture 
		
		// Defines the light color for the player
		_PlayerPosition ("Player Position", vector) = (0,0,0,0) // The location of the player - will be set by script
		_PlayerColor ("Player Color", COLOR) = (1,0,0,1) // The tint that the player will cast on the surface
		_LightFallOff_player ("Light Falloff Player", float) = 0.5 // Player's light falloff
		_LightComponent_player ("Light Component Player", float) = -1.0 // Contorls the player's light-distance to player
		_LightA_player ("Light A Player", float) = -1.0 // Contorls the player's light-distance to player
		_LightB_player ("Light B Player", float) = -1.0 // Contorls the player's light-distance to player
		
		// Defines the light color for the player's jump
		_JumpPosition ("Jump Position", vector) = (0,0,0,0) // The location of the player - will be set by script
		_JumpColor ("Jump Color", COLOR) = (1,0,0,1)
		_JumpLightFallOff ("Jump Light Falloff", float) = 0.0
		_JumpLightComponent ("Jump Light Component", float) = -1.0
		
		// Defines the light for player's go-to position
		_BeaconPosition ("Beacon Position", vector) = (0,0,0,0) // The location of the player - will be set by script
		_BeaconColor ("Beacon Color", COLOR) = (1,0,0,1)
		_BeaconLightFallOff ("Beacon Light Falloff", float) = 0.05
		_BeaconLightComponent ("Beacon Light Component", float) = -1.15
		
		//_VisibleDistance ("Visibility Distance", float) = 10.0 // How close does the player have to be to make object visible
		//_OutlineWidth ("Outline Width", float) = 3.0 // Used to add an outline around visible area a la Mario Galaxy - http://www.youtube.com/watch?v=91raP59am9U
		//_OutlineColour ("Outline Colour", color) = (1.0,1.0,0.0,1.0) // Colour of the outline
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent"}
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			LOD 200

			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			// Access the shaderlab properties
			uniform sampler2D _MainTex;
			
			uniform float4 _PlayerPosition;
			uniform fixed4 _PlayerColor;
			uniform float _LightFallOff_player;
			uniform float _LightComponent_player;
			uniform float _LightA_player;
			uniform float _LightB_player;
			
			uniform float4 _JumpPosition;
			uniform fixed4 _JumpColor;
			uniform float _JumpLightFallOff;
			uniform float _JumpLightComponent;
			
			uniform float4 _BeaconPosition;
			uniform fixed4 _BeaconColor;
			uniform float _BeaconLightFallOff;
			uniform float _BeaconLightComponent;
			
			//uniform float _VisibleDistance;
			//uniform float _OutlineWidth;
			//uniform fixed4 _OutlineColour;

			// Input to vertex shader
			struct vertexInput {
				float4 vertex : POSITION;
				float2 texcoord : TEXCOORD0;
			};
			// Input to fragment shader
			struct vertexOutput {
				float4 pos : SV_POSITION;
				float4 position_in_world_space : TEXCOORD0;
				float2 tex : TEXCOORD1;
			};
	          
			// VERTEX SHADER
			vertexOutput vert(vertexInput input) 
			{
				vertexOutput output; 
				output.pos =  UnityObjectToClipPos(input.vertex);
				output.position_in_world_space = mul(unity_ObjectToWorld, input.vertex);
				output.tex = input.texcoord;
				return output;
			}

			// FRAGMENT SHADER
			float4 frag(vertexOutput input) : COLOR  {
				// Calculate distance to player position
				fixed4 position = input.position_in_world_space;
				
				float dist_player = distance(position, _PlayerPosition);
				float dist_reciprocal_playera = _LightFallOff_player / (dist_player + _LightComponent_player);
				float dist_reciprocal_player = dist_reciprocal_playera + _LightA_player / (dist_player - _LightB_player);
				fixed4 player_output = dist_reciprocal_player * _PlayerColor;
				
				float jump_dist = distance(position, _JumpPosition);
				float jump_dist_reciprocal =  _JumpLightFallOff / (jump_dist + _JumpLightComponent);
				fixed4 jump_output = jump_dist_reciprocal * _JumpColor;
				
				float beacon_dist = distance(position, _BeaconPosition);
				float beacon_dist_reciprocal =  _BeaconLightFallOff / (beacon_dist + _BeaconLightComponent);
				fixed4 beacon_output = beacon_dist_reciprocal * _BeaconColor;
				
				//float dist_reciprocal_final = dist_reciprocal_1 + dist_reciprocal_2;
				float dist_reciprocal_final = dist_reciprocal_player + jump_dist_reciprocal + beacon_dist_reciprocal;
				fixed4 final_output = player_output + jump_output + beacon_output;


				return tex2D(_MainTex, input.tex * dist_reciprocal_final) * final_output;
				//return tex2D(_MainTex, input.tex * dist_reciprocal) * dist_reciprocal;
				
				// Return appropriate colour
				//if (dist < _VisibleDistance) {
				//	return tex2D(_MainTex, float2(input.tex)); // Visible
					//return 1.0;
				//}
				//else if (dist < _VisibleDistance + _OutlineWidth) {
				//	return _OutlineColour; // Edge of visible range
					//return 0.5;
				//}
				//else {
				//	float4 tex = tex2D(_MainTex, float2(input.tex)); // Outside visible range
				//	tex.a = 0.1;
					//return 0.1;
				//}
			}
	 
	         ENDCG
	     }
     } 
     //FallBack "Diffuse"
 }