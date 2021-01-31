using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//[ExecuteInEditMode] // turn on if you want to bake the terrain into scene. Turn off on_or_off
public class TerrainCreator : MonoBehaviour {

	public Transform global_manager;
	public Transform cube_prefab;

	private List<Transform> terrain_transforms_list = new List<Transform>();
	public TerrainNodeList terrain_nodes_list;
	public int terrain_size;
	public bool on_or_off = false;

	// Use this for initialization
	void Start() {
		//Debug.Log (global_manager);
		GlobalSettings global_settings = global_manager.GetComponent<GlobalSettings>();
		Transform terrain = global_settings.terrain;
		float beacon_max_distance = global_settings.beacon_max_distance;

		// on_or_off: on creates nodes, off: does not create nodes but still looks adds nodes to the TerrainNodeList  
		if (on_or_off) {
			terrain_size = 0;
			// when world starts, create a grid 20x20
			for (int x = -10; x <= 10; x++) {
				for (int z = -10; z <= 10; z++) {
					terrain_size++;
					Transform cube = (Transform)Instantiate (cube_prefab, new Vector3 (x, Random.Range (-0.1f, 0.1f), z), Quaternion.identity);
					cube.SetParent(terrain);
					cube.name = "cube_" + terrain_size.ToString ();
				}
			}
			// when world starts, create a grid 20x20
			for (int x = -10; x <= 10; x++) {
				for (int y = 1; y <= 10; y++) {
					terrain_size++;
					Transform cube = (Transform)Instantiate (cube_prefab, new Vector3 (x, y, Random.Range (10.9f, 11.1f)), Quaternion.identity);
					cube.SetParent (terrain);
					cube.name = "cube_" + terrain_size.ToString ();
				}
			}

			// when world starts, create a grid 20x20
			for (int y = 1; y <= 10; y++) {
				for (int z = -10; z <= 10; z++) {
					terrain_size++;
					Transform cube = (Transform)Instantiate (cube_prefab, new Vector3 (Random.Range (-10.9f, -11.1f), y, z), Quaternion.identity);
					cube.SetParent (terrain);
					cube.name = "cube_" + terrain_size.ToString ();
				}
			}
		}

		// Finds and creates a TerrainNodeList
		terrain_size = 0;
		foreach (Transform cube in terrain) {
			terrain_transforms_list.Add(cube);
			terrain_size++;
		}
		terrain_nodes_list = new TerrainNodeList(terrain_transforms_list, beacon_max_distance);
	}
}
