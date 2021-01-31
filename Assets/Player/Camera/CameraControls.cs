using UnityEngine;
using System.Collections;

public class CameraControls : MonoBehaviour {

	// public variables 
	public Transform player_manager; // an empty transform that contains vital information about the scene
	
	// scene info
	public PlayerInfo player_info; // player_info contains static information about the scene
	public PlayerInput player_input; // platform-independnt information about the user's control inputs. Ex: Click, Jump, Zoom, etc...

	// camera transform
	public Camera cam; // the scene camera
	public Transform cam_controler; // the parent of the camera which controls the rotation and position of the camera
	public CameraPerspectiveEditor cam_editor; // script that controls the camera's dolly zoom
	public Vector3 cam_position_goal; // positional goal that cam_controler wants to reach
	public float cam_y_roataion_start; // the starting rotation angle before any animation takes place
	public float cam_y_rotation_goal; // rotational goal that cam_controler wants to reach
	public int cam_y_rotation_goal_counter; // used as a clock to animate the rotation of the cam_controler

	// camera zoom
	public float cam_dolly_zoom_goal; // Dolly zoom goal that cam wants to reach
	public float cam_zoom_focal_distance_goal; // Focal distance goal that cam wants to reach
	public float cam_zoom_focal_distance_storage; // Stores the focal distance for when it's needed again.

	// player
	private Transform player; // the transform that holds infomration about the player



	// TODO: remove?
	private Vector3 prespective_unit_position;
	private Vector2 initial_mouse_position; 
	private Vector3 initial_rotation;
	private float rotaion_goal;
	private float stored;

	// Use this for initialization
	void Start () {
		// player_info contains information about the scene
		player_info = player_manager.GetComponent<PlayerInfo>();
		player_input = player_manager.GetComponent<PlayerInput>();

		// Get camera info from player_info
		cam = player_info.cam;
		cam_controler = player_info.cam_controler;
		cam_editor = player_info.cam_editor;

		// player
		player = player_info.player;

		// camera variables
		cam_dolly_zoom_goal = 1;
		//cam_zoom_focal_distance_goal = cam_editor.dollyZoomFocalDistance;
		cam_position_goal = transform.position;
		//cam_y_rotation_goal = 315;
		prespective_unit_position = Vector3.zero;
		initial_mouse_position = Vector2.zero;
		stored = 0;
			
		//cam_editor.dollyZoomFocalTarget = cam_follow.transform
	}
	
	// Update is called once per frame
	void Update () {
		// Camera Position -- follows the player
		cam_controler.position = Vector3.Lerp(cam_controler.position, player.position, Time.deltaTime * 10) ;

		// Zoom
		cam_editor.dollyZoomFocalDistance = player_input.zoom;
		//cam_controler.localScale = new Vector3(player_input.zoom, player_input.zoom, player_input.zoom);
		cam.transform.localPosition = new Vector3(0, 0, -player_input.zoom);

		// If a PrespNode is clicked/dragged and it is active 
		// (beacon must be on it and player must be adjacent to it),
		// then rotate cam_controler by degree_interval set in the PrespNode script
		PrespNodeCamRoation();


	}

	void PrespNodeCamRoation() {
		// Stores the initial y rotation value of the cam_controler when a prespective node is clicked (down)
		// We use this instead of player_input.node_click_down_stored.roation.eulerAngles.y sometimes returns
		// the angle+360 and it causes pops in animation
		if (player_input.node_click_down != null && player_input.node_click_down.CompareTag("PrespNode")) {
			cam_y_roataion_start = cam_controler.rotation.eulerAngles.y;

		}
		
		// Sets y rotational goal when a prespective node is clicked (up) as defined by the PrespNode script
		// If the PrespNode is a a cube, it will most likely rotate by 45 degrees
		if (player_input.node_click_up != null && player_input.node_click_up.CompareTag("PrespNode")) {
			// PrespNode holds information prpperties about a prespective node
			PrespNode presp_node = player_input.node_click_up.GetComponent<PrespNode>();
			if (presp_node.is_active) {
				cam_y_rotation_goal = presp_node.GetNewRotationGoal("right");
			}
		}
		
		Vector3 final_rotation; // will hold the final roation of the cam_controler
		if (player_input.is_dragging && player_input.node_click_down_stored != null && player_input.node_click_down_stored.CompareTag("PrespNode")) {
			PrespNode presp_node = player_input.node_click_down_stored.GetComponent<PrespNode>();
			final_rotation = presp_node.GetDragRotation(player_input.drag.x + cam_y_roataion_start);
			cam_y_rotation_goal = presp_node.GetNewRotationGoal("none");
		} 
		else {
			float y_rotation = Mathf.LerpAngle(cam_controler.rotation.eulerAngles.y, cam_y_rotation_goal, Time.deltaTime * 10);
			final_rotation = new Vector3 (cam_controler.rotation.eulerAngles.x, y_rotation, cam_controler.rotation.eulerAngles.z);
		}
		cam_controler.rotation = Quaternion.Euler(final_rotation);
	}
}
