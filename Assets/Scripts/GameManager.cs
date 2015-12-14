using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {
	
    private int playerOneScore = 0;
    private int playerTwoScore = 0;
	public Text playerOneText;
	public Text playerTwoText;
	public GameObject gameOverText;
    public bool gamePaused = false;
	private const int WIN_SCORE = 10; 

    public void AddToScore(int player) {
        if (player == 1) {
            playerOneText.text = "Player 1 Kills: " + ++playerOneScore;
        } else {
            playerTwoText.text = "Player 2 Kills: " + ++playerTwoScore;
        }
        CheckGameOver();
    }

	public void CheckGameOver() {
        Text winnerText = gameOverText.GetComponent("Text") as Text;

        if (playerOneScore == WIN_SCORE) {
            winnerText.text = "Player 1 Wins!";
            gameOverText.SetActive(true);
            PauseGame();  
        } else if(playerTwoScore == WIN_SCORE) {
            winnerText.text = "Player 2 Wins!";
            gameOverText.SetActive(true);
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
				return (GameObject)Resources.Load("Player_TypeA");
			case 1:
				return (GameObject)Resources.Load("Player_TypeB");
			default:
				return null;
		}
	}

}
