using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScene3_Manager : GameManager {
	
	public GameObject[] players = new GameObject[2];

	private GameObject player1;
	private GameObject player2;

	public static GameManager instance = null;
	
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		SetupPlayer1 ();
		SetupPlayer2 ();
	}    
	
	void Update () {        
		Debug.Log (player1.transform.position);
		Debug.Log (player1.transform.rotation);
        if(!gamePaused) {
			player1.GetComponent<Vehicle>().getHealth();
			if (player1.GetComponent<Vehicle>().getHealth() <= 0) {
				AddToScore(2);
				Destroy(player1);
				SetupPlayer1();
			} else if (player2.GetComponent<Vehicle>().getHealth() <= 0) {
				AddToScore(1);
				Destroy(player2);
				SetupPlayer2();
			}
        }
		CheckGameOver ();
	}

	void SetupPlayer1() {
		//player1 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerOneCharacter % 8));
		player1 = Instantiate(getVehicle (4));
		player1.transform.position = new Vector3 (-19.68f, -1.32f, 15.84f);
		player1.transform.rotation = new Quaternion (0.0f, 0.3f, 0.0f, 1.0f);
	}
	
	void SetupPlayer2() {
		//player2 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 8));
		player2 = Instantiate(getVehicle (5));
		player2.transform.position = new Vector3 (20.85f, 0f, -16.625f);
		player2.transform.rotation = new Quaternion (0.0f, 0.3f, 0.0f, 1.0f);
	}
}
