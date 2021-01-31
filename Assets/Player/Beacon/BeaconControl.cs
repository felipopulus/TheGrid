using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BeaconControl : MonoBehaviour {
	
	// public variables 
	public Transform global_manager;
	public GlobalSettings global_settings;
	public Transform player_manager; // an empty transform that contains vital information about the scene
	
	// scene info
	private PlayerInfo player_info; // player_info contains static information about the scene
	private PlayerInput player_input; // platform-independnt information about the user's control inputs. Ex: Click, Jump, Zoom, etc...

	//public variables
	public TerrainDefine terrain_define;
	public Color beacon_color;
	public Material default_node_material;
	public Material light_node_material;
	public Material mat_white;
	public Material mat_red;
	public Material mat_green;
	public Material mat_green_closest;
	public Material mat_path;
	public LayerMask layer_mask;
	public float max_distance;
	
	private TerrainNode[] path_to_follow;
	private int current_path_index = 0;
	private float jump_distance = 1.0f;

	// player
	private Transform player; // the transform that holds infomration about the player
	
	// privare variables
	private TerrainNodeList terrain_nodes_list;
	private Pathfinder pathfinder;

	// Use this for initialization
	void Start () {
		// global_settings contains information about the scene
		global_settings = global_manager.GetComponent<GlobalSettings>();
		terrain_define = global_settings.terrain_define;

		// player_info contains information about the player
		player_info = player_manager.GetComponent<PlayerInfo>();
		player_input = player_manager.GetComponent<PlayerInput>();
		
		// player
		player = player_info.player;

		// terrain
		terrain_nodes_list = terrain_define.terrain_nodes_list;
		pathfinder = global_settings.pathfinder;
		//Debug.Log(terrain_nodes_list);
		path_to_follow = new TerrainNode[1] {terrain_nodes_list.FindClosestTerrainNodeToPoint(new Vector3(0.0f, 0.0f, 0.0f))};
	}

	// Update is called once per frame
	void Update () {

		Transform node_clicked = player_input.node_click_hold;
		if (node_clicked != null) {
			// TODO: check for layer mask: layer_mask
			//UpdateCubes();
			current_path_index = 0;

			//Debug.Log(node_clicked);
			path_to_follow = pathfinder.FindPath(transform.position, node_clicked);
			//Debug.Log(path_to_follow[1].transform.name);
			//pathfinder.ShowPath();
		}
		MoveAlongPath();
		default_node_material.SetVector("_BeaconPosition1", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));
		light_node_material.SetVector("_BeaconPosition1", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));

		//node_material.SetColor("_" beacon_color
	}

	void UpdateCubes(RaycastHit hit) {
		// Change Materials
		foreach (TerrainNode terrain_node in terrain_nodes_list) {
			terrain_node.transform.gameObject.GetComponent<Renderer>().material = mat_white;
		}
		TerrainNode terrain_node_hit = terrain_nodes_list.FindTerrainNodeFromTransform(hit.collider.transform);
		terrain_node_hit.transform.gameObject.GetComponent<Renderer>().material = mat_red;
		if (terrain_node_hit.GetAdjecentNodesArray().Length == 0) {
			Debug.Log("This node is unreachable");
		} 
		else {
			foreach (AdjacentNode adjacent_node in terrain_node_hit.GetAdjecentNodesArray()) {
				adjacent_node.transform.gameObject.GetComponent<Renderer>().material = mat_green;
			}
		}
	}
	
	void MoveAlongPath() {
		if (path_to_follow.Length > 0) {
			Vector3 path_node_position = path_to_follow [current_path_index].transform.position;
			float beacon_speed = Vector3.Distance (transform.position, path_to_follow [path_to_follow.Length - 1].transform.position);
			if (current_path_index != path_to_follow.Length - 1) {
				jump_distance = Vector3.Distance (path_node_position, path_to_follow [current_path_index + 1].transform.position);
			}
			beacon_speed = Time.deltaTime * 3 * (beacon_speed + 10) / Mathf.Pow (jump_distance, 3); 
			transform.position = Vector3.Lerp (transform.position, path_node_position, beacon_speed);
			if (Vector3.Distance (transform.position, path_node_position) < 0.1f && current_path_index != path_to_follow.Length - 1) {
				current_path_index++;
			}
		}
	}
}