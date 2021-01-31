using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Diagnostics;

public class NetworkBeaconControler : NetworkBehaviour {
	
	[SerializeField] Transform player_manager; // an empty transform that contains vital information about the scene
	//[SerializeField] Transform beacon;
	[SerializeField] Color beacon_color;
	[SerializeField] Material node_material;
	[SerializeField] float max_distance;

	[SerializeField] private PlayerNetworkSetup player_network_setup;
	[SerializeField] private PlayerInfo player_info;
	[SerializeField] private PlayerInput player_input;
	[SerializeField] private Transform player;
	[SerializeField] private Transform beacon;

	// sync variables
	[SyncVar] private Transform sync_node_clicked;
	[SyncVar] private Time sync_node_clicked_time;
	[SyncVar] private Vector3 sync_beacon_position;


	public override void OnStartClient() { // runs every time a new client is built
		player_network_setup = GetComponent<PlayerNetworkSetup>();
		player_info = player_manager.GetComponent<PlayerInfo>();
		player_input = player_manager.GetComponent<PlayerInput>();
		player = player_info.player;
		beacon = player_info.beacon;
	}
	public override void OnStartLocalPlayer() {
		//Material default_nodes_material = beacon.GetComponent<BeaconControl>().node_material;
		//string shader_property = "_Enable" + player_network_setup.player_net_id.ToString();
		//default_nodes_material.SetInt(shader_property, 1); // turn on beacon that corresponds to the player id
	}
	/*
	// updates the frames
	void Update() {
		TransmitBeacon(); // runs only on client
		SetBeaconPosition ();
	}

	void SetBeaconPosition() {
		if (!isLocalPlayer) {
			// disable the beacon controls from external players, pathfinding will be computed on their end, and only position is synced
			beacon.GetComponent<BeaconControl> ().enabled = false;
			beacon.position = sync_beacon_position;
		} else {
			// enable beacon controls for local player so that each local client computes their own pathfinding
			beacon.GetComponent<BeaconControl> ().enabled = true;
		}
	}
	[Client] // ClientCallback same as client but produces less clutter in the error log
	void TransmitBeacon() {
		if (isLocalPlayer) {
			CmdProvideBeaconPositionToServer(beacon.position);
		}
	}

	[Command] // a command to the server which will sync this variable from this client to the server
	void CmdProvideBeaconPositionToServer(Vector3 pos) {
		sync_beacon_position = pos;
	}
}*/
