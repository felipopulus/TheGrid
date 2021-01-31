using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Material node_material;
	// Use this for initialization
	void Start () {
		//Material node_material = new Material(Shader.Find("NodeMaterial"));
	}
	
	// Update is called once per frame
	void Update () {
		node_material.SetVector("_PlayerPosition", new Vector4(transform.position.x, transform.position.y, transform.position.z, 1));
	}
}
