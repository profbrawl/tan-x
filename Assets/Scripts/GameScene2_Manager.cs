using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameScene2_Manager : GameManager {
	
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
		Debug.Log("test");
		Debug.Log("character: " + PickerManager.getInstance().PlayerOneCharacter);
		player1 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerOneCharacter % 8));
	}

	void SetupPlayer2() {
		Debug.Log("character: " + PickerManager.getInstance().PlayerTwoCharacter);
		player2 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 8));
	}
}
