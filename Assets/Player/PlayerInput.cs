using UnityEngine;
using System.Collections;

// PlayerInput requires the GameObject to have a PlayerKeyboard, PlayerMouse, and PlayerTouch components
[RequireComponent (typeof (PlayerKeyboard))]
[RequireComponent (typeof (PlayerMouse))]
[RequireComponent (typeof (PlayerTouch))]
public class PlayerInput : MonoBehaviour {

	public Transform node_click_down;
	public Transform node_click_hold;
	public Transform node_click_up;
	public Transform node_click_down_stored;
	public bool jump_now;
	public bool is_jumping;
	public float zoom;
	public bool is_dragging;
	public Vector2 drag;
	public Vector3 move;


	private PlayerInfo player_info;
	private PlayerMouse player_mouse;
	private PlayerKeyboard player_keyboard;

	// Use this for initialization
	void Start () {
		player_info = GetComponent<PlayerInfo>();
		player_mouse = GetComponent<PlayerMouse>();
		player_keyboard = GetComponent<PlayerKeyboard>();
		zoom = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
		// Touch Input Controlers
		if (player_info.apple_ios || player_info.google_android) {
			// Touch
		}
		// Mouse/Keyboard Input Controlers
		else if (player_info.windows_pc || player_info.apple_mac) { 
			node_click_down = player_mouse.node_click_down;
			node_click_hold = player_mouse.node_click_hold;
			node_click_up = player_mouse.node_click_up;
			node_click_down_stored = player_mouse.node_click_down_stored;
			zoom = Mathf.Clamp(zoom + player_mouse.mouse_wheel, 1.0F, 10.0F);
			is_dragging = player_mouse.mouse_state == 4;
			drag = player_mouse.mouse_drag;
			jump_now = player_keyboard.space_down;
			move = new Vector3(player_keyboard.vertical_translation, 0, player_keyboard.sideways_translation);
		}
	}
}
