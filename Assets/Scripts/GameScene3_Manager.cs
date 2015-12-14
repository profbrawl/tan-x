using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

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
        SetupPowerUpLocations();
        InvokeRepeating("SpawnPowerup", 5f, 10f);
	}

	void Start() {
		Debug.Log("player 1: " + PickerManager.getInstance().PlayerOne);
		player1.GetComponent<Vehicle>().playerPrefix = PickerManager.getInstance().PlayerOne;
		Debug.Log("player 2: " + PickerManager.getInstance().PlayerTwo);
		player2.GetComponent<Vehicle>().playerPrefix = PickerManager.getInstance().PlayerTwo;
	}

	void Update () {
		Debug.Log (player1.transform.position);
		Debug.Log (player1.transform.rotation);
        if(!gamePaused) {
//			player1.GetComponent<Vehicle>().getHealth();
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
		Debug.Log("character: " + PickerManager.getInstance().PlayerOneCharacter);
		player1 = Instantiate(getVehicle(PickerManager.getInstance().PlayerOneCharacter % 5));
	}

	void SetupPlayer2() {
		Debug.Log("character: " + PickerManager.getInstance().PlayerTwoCharacter);
		player2 = Instantiate(getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 5));
	}

    void SetupPowerUpLocations()
    {
        this.powerUpSpots = new List<PowerUpLocation>();
        this.powerUpSpots.Add(new PowerUpLocation(-8.5f, 0.8f, 8.5f));
        this.powerUpSpots.Add(new PowerUpLocation(-8.5f, 0.8f, -8.5f));
        this.powerUpSpots.Add(new PowerUpLocation(8.5f, 0.8f, -8.5f));
        this.powerUpSpots.Add(new PowerUpLocation(8.5f, 0.8f, 8.5f));
    }
}
