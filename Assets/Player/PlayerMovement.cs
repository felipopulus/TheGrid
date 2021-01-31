using UnityEngine;
using System.Collections;


// PlayerInput requires the GameObject to have a PlayerInfo and PlayerInput components
[RequireComponent (typeof (PlayerInfo))]
[RequireComponent (typeof (PlayerInput))]
public class PlayerMovement : MonoBehaviour {

	public float move_speed;
	public float jump_thrust;
	public float gravity;
	public bool jump_now;
	public Vector3 player_move;

	private PlayerInfo player_info;
	private PlayerInput player_input;
	private Rigidbody player_rigidbody;


	// Use this for initialization
	void Start () {
		player_info = GetComponent<PlayerInfo>();
		player_input = GetComponent<PlayerInput>();
		player_rigidbody = player_info.player.GetComponent<Rigidbody>();
		//speed = 1;
	}

	// ------------------------------------------------------------ //
	// ------------------------- UPDATE --------------------------- //
	// ------------------------------------------------------------ //
	void Update() {
		// mantain our own jump_now variable to avoid sycrony issue between update and fixed update
		// Will be set to false on FixedUpdate
		if (player_input.jump_now) { jump_now = true; }

	}


	// ------------------------------------------------------------ //
	// ----------------------- FIXED UPDATE ----------------------- //
	// ------------------------------------------------------------ //
	void FixedUpdate () {
		// move
		player_move = player_input.move * move_speed;
		player_rigidbody.AddForce(player_move, ForceMode.VelocityChange);

		// jump
		if (jump_now) {
			player_rigidbody.AddForce(Vector3.up*jump_thrust, ForceMode.VelocityChange);
			jump_now = false;
		}
	}

	void GetLeftNode() {
		//terrain_nodes_list
	}
}
