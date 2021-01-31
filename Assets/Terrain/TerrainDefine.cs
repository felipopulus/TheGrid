using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class TerrainDefine : MonoBehaviour {
	
	public Transform global_manager;
	public TerrainNodeList terrain_nodes_list;
	public int terrain_size;

	private List<Transform> terrain_transforms_list = new List<Transform>();

	// Use this for initialization
	void Start () {
		GlobalSettings global_settings = global_manager.GetComponent<GlobalSettings>();
		Transform terrain_folder = global_settings.terrain;
		float beacon_max_distance = global_settings.beacon_max_distance;
		
		// Finds and creates a TerrainNodeList
		terrain_size = 0;
		foreach (Transform cube in GetAllChildren(terrain_folder)) {
			terrain_transforms_list.Add(cube);
			terrain_size++;
		}
		//Debug.Log(terrain_transforms_list);
		terrain_nodes_list = new TerrainNodeList(terrain_transforms_list, beacon_max_distance);
		//Debug.Log (terrain_nodes_list);

		// set the global 
		//global_settings.terrain_nodes_list = terrain_nodes_list;
	}

	// finds all the terrain nodes recursevely excluding any folder transforms
	private List<Transform>childs_of_game_object = new List<Transform>();
	private List<Transform> GetAllChildren(Transform transform_search) {
		foreach (Transform each_transform in transform_search.transform) {
			//Debug.Log (each_transform.name);
			GetAllChildren (each_transform);
			if (each_transform.childCount == 0) {
				childs_of_game_object.Add (each_transform);
			}
		}        
		return childs_of_game_object;
	}
}
