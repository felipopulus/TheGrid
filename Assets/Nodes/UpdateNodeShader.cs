using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UpdateNodeShader : MonoBehaviour {	

	public Transform global_scripts;

	private PlayerInfo player_info;
	private Transform player;
	private Transform beacon;
	private Material[] node_shader_materials;


	// Use this for initialization
	void Start () {
		player_info = global_scripts.GetComponent<PlayerInfo>();
		player = player_info.player;
		beacon = player_info.beacon;

		List<Material> node_shader_materials_tmp = new List<Material> ();
		Object [] renderers = GameObject.FindObjectsOfType (typeof(Renderer));
		int i_max = renderers.Length;
		for (int i = 0; i < i_max; i++) {
			Material[] node_material_list = ((Renderer)renderers [i]).sharedMaterials;
			foreach (Material node_material in node_material_list) {
				if (node_material.shader.name == player_info.node_shader) {
					if (!node_shader_materials_tmp.Contains(node_material)) {
						node_shader_materials_tmp.Add(node_material);
					}
				}
			}
		}
		node_shader_materials = node_shader_materials_tmp.ToArray ();
	}
	
	// Update is called once per frame
	void Update () {
		foreach (Material node_material in node_shader_materials) {
			node_material.SetVector("_PlayerPosition", new Vector4(player.position.x, player.position.y, player.position.z, 1));
			node_material.SetVector("_BeaconPosition", new Vector4(beacon.position.x, beacon.position.y, beacon.position.z, 1));
		}
	}
}
