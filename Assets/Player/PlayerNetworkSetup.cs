using UnityEngine;
using System.Collections;
using UnityEngine.Networking;
using System.Diagnostics;

/// <summary>
/// Player network setup.
/// Sets the player's camera.
/// Sets the player's identity (Unity Name) and parents it to players group
/// Populates the global_manager's players field
/// </summary>


/*
public class PlayerNetworkSetup : NetworkBehaviour {


	// Global variables
	[SerializeField] Transform player_manager;
	private GameObject global_manager;
	private GlobalSettings global_settings;
	private GlobalPlayers global_players;
	private Camera scene_camera;
	private PlayerInfo player_info;

	// Local Variables
	private Transform local_transform;
	[SerializeField] NetworkInstanceId player_net_id;
	[SerializeField] Camera player_camera;
	[SerializeField] AudioListener audio_listener;

	// synced server variables
	[SyncVar] private string player_unique_identity;
	[SyncVar] private Color player_color;


	// Awake runs first and runs even if there if component is disabled
	void Awake() {
		local_transform = player_manager; // self
		global_manager = GameObject.Find("global_manager"); // for some reason, an external transform cannot get added to a prefab, we have to find it our selves
		global_settings = global_manager.GetComponent<GlobalSettings> ();
		global_players = global_manager.GetComponent<GlobalPlayers> ();
		scene_camera = global_settings.scene_camera;
	}

	public override void OnStartClient() {
		// gets the netId from the NetworkIdentity component -- this is a unique id of the player
		player_net_id = GetComponent<NetworkIdentity>().netId;
		local_transform.name = "player_" + player_net_id.ToString();
		local_transform.SetParent(global_settings.player_group);
		CmdTellServerMyIdentity(local_transform.name);
		global_players.assignNextAvailableSpot(local_transform);
	}

	// Overriden function. Gets called when the local player first gets iniziated
	public override void OnStartLocalPlayer() {
		scene_camera.gameObject.SetActive(false); // Disable the main scene camera if it isn't disabled already
		player_camera.enabled = true; // enable local player camera only. Note that the camera componet must be turned off in the editor for this to work correctly
		audio_listener.enabled = true; // enable local camera's audio listener only. Note that the audio listener component must be turned off in the editor for this to work correctly	}
	}

	public override void OnNetworkDestroy() {
		if (!isLocalPlayer) {
			print ("destroyed: " + local_transform);
		}
	}

	void Update() {
		if (isLocalPlayer) {
			//print ("local: " + local_transform.name);	
		} else {
			//print("not local" + player_unique_identity);
		}
		//orbit.GetComponent<OrbitDescription>().orbit_color = player_color; // update the color of Orbit's metal parts
		//beacon.GetComponent<OrbitDescription> ().orbit_color = player_color;
		//TransmitBeaconPosition(); // runs only on client
		//SetBeaconPosition(); // runs only on client
	}

	[Command] // a command to the server which will sync this variable from this client to the server
	void CmdTellServerMyIdentity(string player_name) {
		player_unique_identity = player_name;
		//player_info.network_id = int.Parse(player_net_id.ToString()); // not working, not sure why
		//transform.parent = global_settings.player_group;
		//int index = (int)player_net_id.ToString();
		//global_settings.players[player_net_id.Value] = transform;
	}
}
*/