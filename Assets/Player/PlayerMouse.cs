/// <summary>
/// Scene mouse.
/// Reads raw mouse input and sets the corresponding attributes
/// </summary>

using UnityEngine;
using System.Collections;

public class PlayerMouse : MonoBehaviour {

	public Vector2 mouse_position;
	public Vector2 mouse_position_down;
	public Vector2 mouse_drag;
	public Transform node_click_down;
	public Transform node_click_hold;
	public Transform node_click_up;
	public Transform node_click_down_stored;
	public float mouse_wheel;
	public float left_click_drag;
	public int mouse_state; // 0 = not pressed; 1 = down; 2 = up; 3 = hold; 4 = drag;

	private Transform node_hover_over;
	private CameraPerspectiveEditor cam_editor;
	
	void Start () {
		cam_editor = GetComponent<PlayerInfo>().cam_editor;
		mouse_wheel = 0;
		mouse_state = 0;
	}

	// Update is called once per frame
	void Update () {
		// Mosue Position
		mouse_position = Input.mousePosition;

		// Finds transfom object hit by mouse
		Ray cam_ray = cam_editor.ScreenPointToRay(mouse_position);
		RaycastHit hit;
		if (Physics.Raycast(cam_ray.origin, cam_ray.direction, out hit, Mathf.Infinity)) {
			node_hover_over = hit.collider.transform;
		}
		else {
			node_hover_over = null;
		}

		// Update class variables in regards to a click
		UpdateMouseNodeClick();
		
		// Updates class variable: mouse_wheel	
		UpdateMouseWheel();

		// Updates class variable: left_click_drag 
		UpdateLeftClickDrag();
	}

	// Update class variables: node_click_down, node_click_hold, node_click_up
	private void UpdateMouseNodeClick() {
		if (Input.GetMouseButtonDown(0)) {
			mouse_state = 1;
			mouse_position_down = mouse_position;
			node_click_down = node_hover_over;
			node_click_down_stored = node_hover_over;
			node_click_hold = node_hover_over;
		} else if (Input.GetMouseButtonUp(0)) {
			if (mouse_state != 4) {
				node_click_up = node_hover_over;
			}
			mouse_state = 2;
			node_click_hold = null;
			node_click_down_stored = null;
		} else if (Input.GetMouseButton(0)) {
				node_click_hold = node_hover_over;
				node_click_down = null;
			if (node_click_down_stored == node_hover_over && mouse_state != 4) {
				mouse_state = 3;
			}
			else {
				if (mouse_state == 3) {
					mouse_position_down = mouse_position;
				}
				mouse_state = 4;
				mouse_drag = ((mouse_position - mouse_position_down) * 500) / Screen.width;
				//rotation_goal = mouse_delta.x + initial_rotation.y
			}
		}
		else {
			mouse_state = 0;
			node_click_up = null;
		}
	}

	// Updates class variable: mouse_wheel
	private void UpdateMouseWheel() {
		mouse_wheel = Input.GetAxis ("Mouse ScrollWheel");
	}

	private void UpdateLeftClickDrag(){
		left_click_drag = 0.0f;
	}

}
