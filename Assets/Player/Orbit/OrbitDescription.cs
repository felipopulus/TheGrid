using UnityEngine;
using System.Collections;

public class OrbitDescription : MonoBehaviour {
	public Transform glass;
	public Transform right_side;
	public Transform left_side;
	public Transform right_metal_plate;
	public Transform left_metal_plate;
	public Color orbit_color;
	public Material glass_mat;
	public Material metal_mat;

	// Use this for initialization
	void Start () {
		glass_mat = glass.GetComponent<MeshRenderer>().material;
		metal_mat = right_metal_plate.GetComponent<MeshRenderer>().sharedMaterial;
	}

	void Update() {
		//TODO: optimize this so there there it doesn't have to update material every frame
		glass_mat.color = orbit_color;
		metal_mat.color = orbit_color;
	}
}
