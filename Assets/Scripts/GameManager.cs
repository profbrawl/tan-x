using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public int numberOfMaps = 1;
	public GameObject[] players = new GameObject[1];
	public GameObject[] maps = new GameObject[1];
    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public Text playerOneText;
    public Text playerTwoText;
    public GameObject gameOverText;
    public bool gamePaused = false;
    private const int WIN_SCORE = 300;

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


        if(!gamePaused)
        {
            AddToScore(1);
            //AddToScore(2);
        }        
	}

	void SetupTanks() {
		Instantiate (players [0]).transform.forward = new Vector3 (0,0,1);
		//Instantiate (players[1], new Vector3(-0.54f, 0f, 0.56f), Quaternion.identity);
	}

    /// <summary>
    /// Adds a point to the given player's score
    /// </summary>
    /// <param name="player"> 1 for player one, 2 for player two</param>
    void AddToScore(int player)
    {
        if (player == 1)
        {
            playerOneText.text = "Player 1 Kills: " + ++playerOneScore;
        }
        else
        {
            playerTwoText.text = "Player 2 Kills: " + ++playerTwoScore;
        }
        CheckGameOver();
    }

    void CheckGameOver()
    {
        Text winnerText = gameOverText.GetComponent("Text") as Text;
        if (playerOneScore == WIN_SCORE)
        {
            winnerText.text = "Player 1 Wins!";
            gameOverText.SetActive(true);
            PauseGame();  
        }else if(playerTwoScore == WIN_SCORE)
        {
            winnerText.text = "Player 2 Wins!";
            gameOverText.SetActive(true);
            PauseGame();
        }
    }

    void PauseGame()
    {
        Time.timeScale = 0;
        gamePaused = true;
    }

    void UnPauseGame()
    {
        Time.timeScale = 1;
        gamePaused = false;
    }

    void RestartGame()
    {
        Application.LoadLevel(Application.loadedLevel);
        UnPauseGame();
    }

    void LoadMainMenu()
    {
        Application.LoadLevel("Home Screen");
        Time.timeScale = 1;
    }

    public void Restart_Click()
    {
        RestartGame();
    }

    public void MainMenu_Click()
    {
        LoadMainMenu();
    }

}
