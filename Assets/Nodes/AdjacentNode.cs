using UnityEngine;
using System.Collections;

public class AdjacentNode {

	public TerrainNode terrain_node;
	public Transform transform;
	public Vector3 position;
	public float distance;

	/*
	public AdjacentNode (TerrainNode _adjacent_node) {
		adjacent_node = _adjacent_node;
		position = adjacent_node.node.transform.position;
		distance = 0.0f;
	}*/
	public AdjacentNode (TerrainNode _terrain_node, float _distance) {
		terrain_node= _terrain_node;
		position = terrain_node.position;
		transform = terrain_node.transform;
		distance = _distance;
	}
	public static void SortAdjacentNodes(AdjacentNode[] adjacent_nodes) {
		//TODO: implement IComparable to sort adjacent nodes nodes by g_cost.
		// Not super important at the moment
	}
}
