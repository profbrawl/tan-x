using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameScene1_Manager : GameManager {

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
	}

	void Start() {
		Debug.Log("player 1: " + PickerManager.getInstance().PlayerOne);
		player1.GetComponent<Vehicle>().playerPrefix = PickerManager.getInstance().PlayerOne;
		Debug.Log("player 2: " + PickerManager.getInstance().PlayerTwo);
		player2.GetComponent<Vehicle>().playerPrefix = PickerManager.getInstance().PlayerTwo;
	}

	void Update () {
		Debug.Log (player1.transform.rotation);
        if(!gamePaused) {
//			player1.GetComponent<Vehicle>().getHealth();
			if (player1.GetComponent<Vehicle>().getHealth() <= 0) {
				//AddToScore(2);
				Destroy(player1);
				SetupPlayer1();
			} else if (player2.GetComponent<Vehicle>().getHealth() <= 0) {
				//AddToScore(1);
				Destroy(player2);
				SetupPlayer2();
			}
        }
		CheckGameOver ();
	}

	void SetupPlayer1() {
		//player1 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerOneCharacter % 8));
//		player1 = Instantiate(getVehicle (4));
//		player1.transform.position = new Vector3 (-29.84f, 0f, -16.83f);
//		player1.transform.rotation = new Quaternion (0.0f, 0.3f, 0.0f, 1.0f);
		Debug.Log("character: " + PickerManager.getInstance().PlayerOneCharacter);
		player1 = Instantiate(getVehicle(PickerManager.getInstance().PlayerOneCharacter % 5));
	}

	void SetupPlayer2() {
		//player2 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 8));
//		player2 = Instantiate (getVehicle (5));
//		player2.transform.position = new Vector3 (20.8f, 0f, 16.84f);
//		player2.transform.rotation = new Quaternion (0.0f, 0.3f, 0.0f, 1.0f);
		Debug.Log("character: " + PickerManager.getInstance().PlayerTwoCharacter);
		player2 = Instantiate(getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 5));
	}

    void SetupPowerUpLocations()
    {
        this.powerUpSpots = new List<PowerUpLocation>();
        this.powerUpSpots.Add(new PowerUpLocation(-2f, 0.8f, 13f));
        this.powerUpSpots.Add(new PowerUpLocation(-2f, 0.8f, -13f));
        this.powerUpSpots.Add(new PowerUpLocation(8f, 0.8f, -4f));
        this.powerUpSpots.Add(new PowerUpLocation(8f, 0.8f, 5f));
    }

}
