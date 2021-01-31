using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PrespNode : MonoBehaviour {

	public Transform global_scripts;
	public bool is_active;
	public float degree_interval;

	private PlayerInfo scene_info;
	private Transform player;
	private Transform beacon;
	private Transform cam_controler;
	private float[] angle_array;

	// Use this for initialization
	void Start () {
		scene_info = global_scripts.GetComponent<PlayerInfo>();
		player = scene_info.player;
		beacon = scene_info.beacon;
		cam_controler = scene_info.cam_controler;
		List<float> angle_list = new List<float>();
		int num_of_angles = Mathf.RoundToInt(360.0f / degree_interval);
		for (int i=0; i < num_of_angles; i++) {
			angle_list.Add((float)i * degree_interval);
			Debug.Log(angle_list[i]);
		}
		angle_array = angle_list.ToArray();
	}

	public float GetNewRotationGoal(string direction="none") {
		float shortest_dist = 360.0f;
		int rotational_goal = 0;
		float tmp_degree_interval;
		if (direction == "right") {
			tmp_degree_interval = -degree_interval;
		} else if (direction == "left") {
			tmp_degree_interval = degree_interval;
		} else {
			tmp_degree_interval = 0.0f;
		}
		foreach (int angle in angle_array) {
			float magnitude = Mathf.Abs(Mathf.DeltaAngle(angle, cam_controler.rotation.eulerAngles.y + tmp_degree_interval));
			if (Mathf.Abs(magnitude) < shortest_dist) {
				shortest_dist = magnitude;
				rotational_goal = angle;
			}
		}
		return rotational_goal;
	}

	public Vector3 GetDragRotation(float rotation_goal){
		// Sin wave function that transforms a linear input into 
		// y = a * sin(-b * 0.0174532925 * x) + x; where a is the tangent of the sin wave and b is the number of angles
		float y_rotation = 7.14f * Mathf.Sin (-8.0f * Mathf.Deg2Rad * rotation_goal) + rotation_goal;
		return new Vector3 (cam_controler.rotation.eulerAngles.x, y_rotation, cam_controler.rotation.eulerAngles.z);
	}

	// Update is called once per frame
	void Update () {
		float dist_presp_player = Vector3.Distance (transform.position, player.position);
		float dist_presp_beacon = Vector3.Distance (transform.position, beacon.position);
		if (dist_presp_player <= 1.05f && dist_presp_beacon < 0.1f ) {
			is_active = true;

		} 
		else {
			is_active = false;
		}
	}
}
