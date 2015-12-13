using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public int numberOfMaps = 1;
	public GameObject[] players = new GameObject[1];
	public GameObject[] maps = new GameObject[1];

	public static GameManager instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup (0); // temp until we get more maps. Will randomize.
	}
	
	public void Setup(int mapID) {
		Instantiate(maps[mapID]);
		SetupTanks();
	}
	
	void Update () {
		// Map effects :D
	}

	void SetupTanks() {
		Instantiate (players [0]).transform.forward = new Vector3 (0,0,1);
		//Instantiate (players[1], new Vector3(-0.54f, 0f, 0.56f), Quaternion.identity);
	}
}
