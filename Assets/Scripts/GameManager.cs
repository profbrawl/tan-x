using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public int numberOfMaps = 1;
	public GameObject[] tanks = new GameObject[1];
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
		for (int i = 0; i < numberOfTanks; ++i) {
			Instantiate (tanks [i]);
		}
	}
}
