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

	void Start() {
        
		
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
        float distanceBetweenPlayers = Vector3.Distance(player1.transform.position, player2.transform.position);
        Vector3 midpoint = (player1.transform.position + player2.transform.position) / 2;
        //((GameObject)GetCamera()).transform.Translate(new Vector3(0, 0 , ) * Time.deltaTime);
        ((GameObject)GetCamera()).GetComponent<Camera>().transform.LookAt(midpoint);
    }

	void SetupPlayer1() {
        //player1 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerOneCharacter % 8));
        int selectedVehicle = -1;
        if (PickerManager.getInstance() != null) 
            selectedVehicle = PickerManager.getInstance().PlayerOneCharacter % 5;
        if (selectedVehicle < 0)
        {
            player1 = Instantiate(getVehicle(3));
            //player1.transform.position = new Vector3(-29.84f, 0f, -16.83f);
            //player1.transform.rotation = new Quaternion(0.0f, 0.3f, 0.0f, 1.0f);
        } else
        {
            Debug.Log("character: " + PickerManager.getInstance().PlayerOneCharacter);
            player1 = Instantiate(getVehicle(selectedVehicle));
        }

        string playerOne = "";
        if (PickerManager.getInstance() != null)
            playerOne = PickerManager.getInstance().PlayerOne;
        if (playerOne.Length != 0)
        {
            Debug.Log("player 1: " + PickerManager.getInstance().PlayerOne);
            player1.GetComponent<Vehicle>().playerPrefix = playerOne;
        }
        else
        {
            player1.GetComponent<Vehicle>().playerPrefix = "j1";
        }

    }

    void SetupPlayer2() {
        //player2 = Instantiate(instance.getVehicle(PickerManager.getInstance().PlayerTwoCharacter % 8));
        
        int selectedVehicle = -1;
        if (PickerManager.getInstance() != null)
            selectedVehicle = PickerManager.getInstance().PlayerTwoCharacter % 5;
        if (selectedVehicle < 0)
        {
            player2 = Instantiate(getVehicle(4));
            //player2.transform.position = new Vector3(20.8f, 0f, 16.84f);
            //player2.transform.rotation = new Quaternion(0.0f, 0.3f, 0.0f, 1.0f);
        }
        else
        {
            Debug.Log("character: " + PickerManager.getInstance().PlayerTwoCharacter);
            player2 = Instantiate(getVehicle(selectedVehicle));
        }

        string playerTwo = "";
        if (PickerManager.getInstance() != null)
            playerTwo = PickerManager.getInstance().PlayerTwo;
        if (playerTwo.Length != 0)
        {
            Debug.Log("player 2: " + PickerManager.getInstance().PlayerTwo);
            player2.GetComponent<Vehicle>().playerPrefix = PickerManager.getInstance().PlayerTwo;
        }
        else
        {
            player2.GetComponent<Vehicle>().playerPrefix = "j2";
        }
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
