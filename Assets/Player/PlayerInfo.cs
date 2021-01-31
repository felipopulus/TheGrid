using UnityEngine;
using System.Collections;

public class PlayerInfo : MonoBehaviour {
	public Transform global_manager;
	public GlobalSettings global_settings;
	public TerrainDefine terrain_define;
	public TerrainNodeList terrain_nodes_list;
	public Pathfinder pathfinder;
	public bool apple_ios;
	public bool google_android;
	public bool windows_pc;
	public bool apple_mac;
	public Camera cam;
	public Transform cam_controler;
	public CameraPerspectiveEditor cam_editor;
	public Transform player;
	public Transform player_right;
	public Transform player_left;
	public Transform beacon;
	public bool camera_mode;
	public bool beacon_mode;
	public bool gui_mode;
	public bool zoom_mode;
	public int network_id;
	public string node_shader;

	public void Awake() {
		network_id = 0; // will be set by PlayerNetworkSetup
		global_settings = global_manager.GetComponent<GlobalSettings>();
		terrain_define = global_settings.terrain_define;
		terrain_nodes_list = terrain_define.terrain_nodes_list;
		pathfinder = global_settings.pathfinder;
		cam_editor = cam.GetComponent<CameraPerspectiveEditor>();
		//cam_controler = cam.transform.parent.transform;

		/*
		if (apple_ios || google_android) {
			node_shader = "TheGrid/NodeShaderMobil";
		} else if (windows_pc || apple_mac) {
			node_shader = "TheGrid/NodeShaderDesktop";
		}
		*/
	}
//	public void Update() {
//		
//	}
}
