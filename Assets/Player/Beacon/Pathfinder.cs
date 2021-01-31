using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

public class Pathfinder : MonoBehaviour {
	public Transform global_manager;
	public GlobalSettings global_settings;
	public TerrainDefine terrain_define;
	public Material mat_start;
	public Material mat_target;
	public Material mat_path;
	public List<TerrainNode> path;
	private TerrainNodeList terrain_nodes_list;
	private int terrain_size;

	void Start() {
		global_settings = global_manager.GetComponent<GlobalSettings>();
		terrain_define = global_settings.terrain_define;
		terrain_nodes_list = terrain_define.terrain_nodes_list;
		//TerrainCreator terrain_creator = GameObject.Find ("terrain").GetComponent<TerrainCreator>();
		//terrain_nodes_list = terrain_creator.terrain_nodes_list;
		terrain_size = terrain_define.terrain_size;
	}

	public void ShowPath() {
		foreach (TerrainNode node in path) {
			print(node.transform.name);
			node.transform.gameObject.GetComponent<Renderer>().material = mat_start;
		}
	}

	public TerrainNode[] FindPath(Vector3 start_position, Transform target_transform) {
		TerrainNode start_node = terrain_nodes_list.FindClosestTerrainNodeToPoint(start_position);
		TerrainNode target_node = terrain_nodes_list.FindTerrainNodeFromTransform(target_transform);
		//print (target_node.transform.name);
		AdjacentNode closest_node_to_target = new AdjacentNode(start_node, Vector3.Distance(start_node.position, target_node.position));
		Heap<TerrainNode> open_set = new Heap<TerrainNode>(terrain_size);
		HashSet<TerrainNode> closed_set = new HashSet<TerrainNode>();
		open_set.Add(start_node);

		//start_node.transform.gameObject.GetComponent<Renderer>().material = mat_start;
		//target_node.transform.gameObject.GetComponent<Renderer>().material = mat_target;

		// A* pathfinding algorithm
		while (open_set.Count > 0) {
			TerrainNode current_node = open_set.RemoveFirst();
			closed_set.Add(current_node);

			//current_node.transform.gameObject.GetComponent<Renderer>().material = mat_path;
			if (current_node == target_node) {
				return RetracePath(start_node, target_node);
			}
			foreach (AdjacentNode adj_neighbour in current_node.GetAdjecentNodesArray()) {
				TerrainNode neighbour = adj_neighbour.terrain_node;
				if (closed_set.Contains(neighbour)) {
					continue;
				}
				float new_movement_cost_to_new_neighbour = current_node.g_cost + adj_neighbour.distance;
				if (new_movement_cost_to_new_neighbour < neighbour.g_cost || !open_set.Contains(neighbour)) {
					neighbour.g_cost = new_movement_cost_to_new_neighbour;
					neighbour.h_cost = Vector3.Distance(neighbour.position, target_node.position);
					neighbour.parent = current_node;

					if (!open_set.Contains(neighbour)) {
						open_set.Add(neighbour);
					}
					else {
						open_set.UpdateItem(neighbour);
					}
					if (neighbour.h_cost < closest_node_to_target.distance) {
						closest_node_to_target = new AdjacentNode(neighbour, neighbour.h_cost);
					}
				}
			}
		}
		// no path found, return the closest node to target
		return FindPath(start_position, closest_node_to_target.transform);
	}

	private TerrainNode[] RetracePath(TerrainNode start_node, TerrainNode end_node) {
		path = new List<TerrainNode>();
		TerrainNode current_node = end_node;
		if (current_node == start_node) {
			return new TerrainNode[1] {end_node};
		}
		while (current_node != start_node) {
			path.Add(current_node);
			current_node = current_node.parent;
		}
		path.Reverse();
		return path.ToArray();
	}
}
