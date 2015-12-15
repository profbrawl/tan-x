using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

    private int playerOneScore = 0;
    private int playerTwoScore = 0;
    protected List<PowerUpLocation> powerUpSpots;
	//public Text playerOneText;
	//public Text playerTwoText;
	//public GameObject gameOverText;
    public bool gamePaused = false;
	private const int WIN_SCORE = 10;
    private System.Random randomizer = new System.Random();
    protected GameObject camera;

    public void AddToScore(int player) {
        if (player == 1) {
            //playerOneText.text = "Player 1 Kills: " + ++playerOneScore;
        } else {
            //playerTwoText.text = "Player 2 Kills: " + ++playerTwoScore;
        }
        CheckGameOver();
    }

    public GameObject GetCamera()
    {
        return GameObject.Find("Main Camera");
    }

	public void CheckGameOver() {
        //Text winnerText = gameOverText.GetComponent("Text") as Text;

        if (playerOneScore == WIN_SCORE) {
      //      winnerText.text = "Player 1 Wins!";
        //    gameOverText.SetActive(true);
            PauseGame();
        } else if(playerTwoScore == WIN_SCORE) {
          //  winnerText.text = "Player 2 Wins!";
           // gameOverText.SetActive(true);
            PauseGame();
        }
    }

	public void PauseGame() {
        Time.timeScale = 0;
        gamePaused = true;
    }

	public void UnPauseGame() {
        Time.timeScale = 1;
        gamePaused = false;
    }

	public void RestartGame() {
        Application.LoadLevel(Application.loadedLevel);
        UnPauseGame();
    }

	public void LoadMainMenu() {
        Application.LoadLevel("Home Screen");
        UnPauseGame();
    }

    public void Restart_Click() {
        RestartGame();
    }

    public void MainMenu_Click() {
        LoadMainMenu();
    }

	public GameObject getVehicle(int value) {
		switch(value) {
			case 0:
				return (GameObject)Resources.Load("Vehicle1");
			case 1:
				return (GameObject)Resources.Load("Vehicle2");
			case 2:
				return (GameObject)Resources.Load("Vehicle3");
			case 3:
				return (GameObject)Resources.Load("Vehicle4");
			case 4:
				return (GameObject)Resources.Load("Vehicle5");
			default:
				return null;
		}
	}

    protected GameObject getPowerUp(int value)
    {
        switch(value)
        {
            case 1:
                return (GameObject)Resources.Load("SpecialAmmo");
            case 2:
                return (GameObject)Resources.Load("HealthPack");
            default:
                return null;
        }
    }

    protected void SpawnPowerup()
    {
        List<PowerUpLocation> availableLocations = new List<PowerUpLocation>();
        foreach(PowerUpLocation powerUp in this.powerUpSpots){
            if (powerUp.AttachedItem == null) { powerUp.Occupied = false; }
            if (!powerUp.Occupied) { availableLocations.Add(powerUp); }
        }
        if (availableLocations.Count == 0)
        {
            return;
        }else
        {
            PowerUpLocation locationToSpawn = availableLocations[randomizer.Next(0 , availableLocations.Count)];
            locationToSpawn.AttachedItem = Instantiate(getPowerUp(randomizer.Next(1,3)), new Vector3(locationToSpawn.X, locationToSpawn.Y, locationToSpawn.Z), new Quaternion(0.0f, 0.0f, 0.0f, 0.0f)) as GameObject;
            locationToSpawn.Occupied = true;
        }

    }

}
