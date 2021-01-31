using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//
public class TerrainNode : IHeapItem<TerrainNode>
{
	public Transform transform; // The node
	public Vector3 position; // Position of the node
	public float g_cost; // distance from this node to adjecent node. Note that g_cost will be populated by Pathfinder.cs
	public float h_cost; // distance from adjacent node to target node. Note that h_cost will be populated by Pathfinder.cs
	public TerrainNode parent;
	//private AdjacentNode[] adjacent_nodes = new AdjacentNode[8]; // Holds information about closests adjacent nodes (sorted:closest-farthest). This array can only hold a maximum of 8 items
	private List<AdjacentNode> adjacent_node_list = new List<AdjacentNode>();
	private int heap_index;

	// Constructor
	public TerrainNode (Transform _node) {
		transform = _node;
		position = _node.transform.position;
	}
	public void AddAdjecentNode(AdjacentNode adjacent_node) {
		adjacent_node_list.Add(adjacent_node);
	}

	// returns adjacent_node_list. Use this when editing (cleaner code)
	public List<AdjacentNode> GetAdjecentNodesList() {
		return adjacent_node_list;
	}

	// returns adjacent_node_list. Use this when done editing (faster iterations)

	public AdjacentNode[] GetAdjecentNodesArray() {
		return adjacent_node_list.ToArray();
	}

	// Given a list of TerrainNodes, this populates/updates the TerrainNodes's adjacent_nodes list
	// max_distance is the max distance the beacon is willing to travel per turn
	public static void SortAdjacentNodes(List<TerrainNode> terrain_node_list, float max_distance) {
		for (int i=0; i<terrain_node_list.Count; i++) {
			for (int j=i; j<terrain_node_list.Count; j++) {
				float distance = Vector3.Distance(terrain_node_list[i].position, terrain_node_list[j].position);
				if (distance <= max_distance && distance > 0.0f) {
					terrain_node_list[i].AddAdjecentNode(new AdjacentNode(terrain_node_list[j], distance));
					terrain_node_list[j].AddAdjecentNode(new AdjacentNode(terrain_node_list[i], distance));
				}
			}
			AdjacentNode.SortAdjacentNodes(terrain_node_list[i].GetAdjecentNodesArray());
		}
	}

	// for this game f_cost = g_cost^2 + h_cost because we want to discourage beacon air jumps. Jumps require more energy (distance^2)
	public float f_cost { 
		get {
			return Mathf.Pow(g_cost, 2) + h_cost;
		}
	}

	public int HeapIndex {
		get {
			return heap_index;
		}
		set {
			heap_index = value;
		}
	}

	public int CompareTo(TerrainNode node_to_compare) {
		int compare = f_cost.CompareTo(node_to_compare.f_cost);
		if (compare == 0) {
			compare = h_cost.CompareTo(node_to_compare.h_cost);
		}
		return -compare;
	}

	/*
	// Return clean AdjacentNode array. Use this when done editing. It's faster.
	public AdjacentNode[] GetAdjecentNodesArray() {
		List<AdjacentNode> temp_adjecent_nodes_list = new List<AdjacentNode>();
		for (int i=0; i<8; i++) {
			if (adjacent_node_list[i] != null) {
				temp_adjecent_nodes_list.Add(adjacent_node_list[i]);
			}
		}
		return temp_adjecent_nodes_list.ToArray();
	}


	// Compares two TerrainNodes and updates their adjacent_nodes list.
	// Note that only the first TerrainNode argument in this function gets updated
	// if you want to update the second argument, call this function with arguments in reverse order
	public static void OldCompareAndUpdateAdjacentNodes(TerrainNode terrain_node_1, TerrainNode terrain_node_2, float distance) {
		for (int i=0; i<8; i++) {
			if (terrain_node_1.adjacent_nodes [i] == null) {
				terrain_node_1.adjacent_nodes [i] = new AdjacentNode (terrain_node_2.node, distance);
				break;
			} 
			else {
				float adjecent_distance = Vector3.Distance (terrain_node_1.adjacent_nodes[i].position, terrain_node_1.position);
				if (distance < adjecent_distance) {
					for (int j=7; j>=i; j--) {
						if (j != 0) {
							if (terrain_node_1.adjacent_nodes[j-1] != null) {
								terrain_node_1.adjacent_nodes[j] = terrain_node_1.adjacent_nodes [j-1];
							}
						}
					}
					terrain_node_1.adjacent_nodes[i] = new AdjacentNode (terrain_node_2.node, distance);
					break;
				}
			}
		}
	}*/
}
