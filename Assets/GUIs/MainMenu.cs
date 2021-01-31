using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {

	public GUISkin my_skin;
	public string my_text;

	void OnGUI() {
		GUI.skin = my_skin;
		GUILayout.BeginArea (new Rect (50, 50, 400, Screen.width / 2));
		GUILayout.BeginHorizontal();
		GUILayout.Label ("This is a Label");
		GUILayout.EndHorizontal ();
		GUILayout.BeginHorizontal();
		if(GUILayout.Button("button")) {
			my_text = "pushed";
		}
		GUILayout.EndHorizontal ();
		GUILayout.EndArea();


	}
}
