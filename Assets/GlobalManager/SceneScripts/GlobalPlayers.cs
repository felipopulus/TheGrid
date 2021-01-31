using UnityEngine;
using System.Collections;
using System.Diagnostics;

public class GlobalPlayers : MonoBehaviour {

	public int num_of_players;
	public Transform player1;
	public Color player1_color;
	public Transform player2;
	public Color player2_color;
	public Transform player3;
	public Color player3_color;
	public Transform player4;
	public Color player4_color;
	
	// Update is called once per frame
	void Update () {
		print(num_of_players);
	}

	public void assignNextAvailableSpot(Transform player) {
		if (player1 == null) {
			player1 = player;
		} else if (player2 == null) {
			player2 = player;
		} else if (player3 == null) {
			player3 = player;
		} else if (player4 == null) {
			player4 = player;
		} else {
			print("a maximum of 4 players allowed");
		}
	}
}
