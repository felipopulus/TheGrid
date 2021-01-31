using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// This class creats a list of TerrainNodes
// Does due diligence when creating a new TerrainNodeList by calling the updating the SortAdjacentNodes method
// TerrainNode.adjacent_nodes is a list of the 8 most adjacent nodes to a given node
// Keeps the TerrainNode.adjacent_Nodes updated when a new TerrainNode gets added or destroyed

public class TerrainNodeList : IEnumerable {

	public List<TerrainNode> terrain_node_list = new List<TerrainNode>();
	public float max_distance;

	// Constructor if empty
	public TerrainNodeList () {
	}

	// Constructor if given a list of transforms 
	public TerrainNodeList (List<Transform> transform_node_list, float _max_distance) {
		max_distance = _max_distance;
		foreach (Transform transform_node in transform_node_list) {
			terrain_node_list.Add(new TerrainNode(transform_node));
		}
		TerrainNode.SortAdjacentNodes(terrain_node_list, max_distance);
	}
	
	// Override from IEnumerable. Allows to iteration of terrain_node_list by using [] syntx
	public IEnumerator GetEnumerator() {
		foreach (TerrainNode terrain_node in terrain_node_list) {
			yield return terrain_node;
		}
	}

	public void AddTerrainNode(Transform node) {
		TerrainNode terrain_node = new TerrainNode(node);
		terrain_node_list.Add(terrain_node);
		// TODO: Instead of reanalyzing the entire list,
		// there could be a method in TerrainNode (Heap-like structure)
		// that would update only the nodes that need to be updated. 
		TerrainNode.SortAdjacentNodes(terrain_node_list, max_distance);
	}

	public void RemoveTerrainNode(Transform transform_node) {
		foreach (TerrainNode terrain_node in terrain_node_list) {
			if (transform_node == terrain_node.transform) {
				terrain_node_list.Remove(terrain_node);
				// TODO: Instead of reanalyzing the entire list,
				// there could be a method in TerrainNode (Heap-like structure)
				// that would update only the nodes that need to be updated. 
				TerrainNode.SortAdjacentNodes(terrain_node_list, max_distance); 
				break;
			}
		}
	}


	// given a Transform, this will return the TerrainNode equivalent
	public TerrainNode FindTerrainNodeFromTransform(Transform transform_node) {
		foreach (TerrainNode terrain_node in terrain_node_list) {
			if (transform_node == terrain_node.transform) {
				return terrain_node;
			}
		}
		return null;
	}

	// Finds the closest Terrrain Node in the list to a given point
	public TerrainNode FindClosestTerrainNodeToPoint(Vector3 point) {
		TerrainNode closest_terrain_node = terrain_node_list[0];
		float shortest_distance = Vector3.Distance(terrain_node_list[0].position, point);
		foreach (TerrainNode terrain_node in terrain_node_list) {
			float distance = Vector3.Distance (terrain_node.position, point);
			if (distance < shortest_distance) {
				closest_terrain_node = terrain_node;
				shortest_distance = distance;
			}
		}
		return closest_terrain_node;
	}
}
