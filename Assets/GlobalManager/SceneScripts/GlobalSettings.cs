using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class GlobalSettings: NetworkBehaviour {
	public Transform player_group;
	//[SyncVars] public Transform[] players = new Transform[8];
	public Transform terrain;
	public TerrainDefine terrain_define;
	public Pathfinder pathfinder;
	public Camera scene_camera;
	public string node_shader;
	public float beacon_max_distance;


	
	public void Awake() {
		node_shader = "TheGrid/NodeShaderDesktop";
		terrain_define = terrain.GetComponent<TerrainDefine>();
		pathfinder = terrain.GetComponent<Pathfinder>();
		//beacon_max_distance = 1.0f;
		/*
		if (apple_ios || google_android) {
			node_shader = "TheGrid/NodeShaderMobil";
		} else if (windows_pc || apple_mac) {
			node_shader = "TheGrid/NodeShaderDesktop";
		}
		*/
	}
}
