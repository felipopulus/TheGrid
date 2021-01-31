using UnityEngine;
using System.Collections;

public class OrbitMovement : MonoBehaviour {
	
	// public variables 
	public Transform global_scripts; // an empty transform that contains vital information about the scene
	
	// scene info
	private PlayerInfo player_info; // player_info contains static information about the scene
	private PlayerInput player_input; // platform-independnt information about the user's control inputs. Ex: Click, Jump, Zoom, etc...
	private Transform orbit; // Player Transform
	private Transform orbit_right; // Player's Right Track Transform (Used for animating character roation)
	private Transform orbit_left; // Player's Left Track Transform (Used for animating character roation)
	private Transform beacon; // Beacon Transform


	// Use this for initialization
	void Start () {
		global_scripts = GameObject.Find("global_scripts").transform;
		// player_info contains information about the scene
		player_info = global_scripts.GetComponent<PlayerInfo>();
		player_input = global_scripts.GetComponent<PlayerInput>();
		orbit = player_info.player;
		orbit_right = player_info.player_right;
		orbit_left = player_info.player_left;
		beacon = player_info.beacon;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 beacon_position = beacon.transform.position;
		//Vector3 beacon_to_robit_delata = new Vector3 (beacon_position - orbit.position);
		orbit.transform.LookAt(beacon_position);
		if (player_input.node_click_down) {
			orbit_right.transform.Rotate(new Vector3(100,0,0) * Time.deltaTime);
			orbit_left.transform.Rotate(new Vector3(100,0,0) * Time.deltaTime);
			}


		//orbit.position = new Vector3(beacon_position.x, beacon_position.y+1, beacon_position.z);
	
	}
}
