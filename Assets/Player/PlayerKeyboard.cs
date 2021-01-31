using UnityEngine;
using System.Collections;

public class PlayerKeyboard : MonoBehaviour {

	public bool space_down;
	public int vertical_translation;
	public int sideways_translation;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		// space -- jumping
		space_down = false;
		if (Input.GetKeyDown (KeyCode.Space)) {
			space_down = true;
		}

		// axis -- movement
		sideways_translation = 0;
		vertical_translation = 0;
		if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.W)) { vertical_translation = 1; }
		if (Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKeyDown (KeyCode.S)) { vertical_translation = -1; }
		if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.A)) { sideways_translation = 1; }
		if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.D)) { sideways_translation = -1; }
	}
}
