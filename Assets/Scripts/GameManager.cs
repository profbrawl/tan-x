using UnityEngine;
using System.Collections;
<<<<<<< HEAD
=======
using UnityEngine.UI;
>>>>>>> bc454263923d6640cd52d60c5809893d9857902a

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public int numberOfMaps = 1;
<<<<<<< HEAD
	public GameObject[] tanks = new GameObject[1];
	public GameObject[] maps = new GameObject[1];
=======
	public GameObject[] players = new GameObject[1];
	public GameObject[] maps = new GameObject[1];
    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public Text playerOneText;
    public Text playerTwoText;
    public Text intro;    
>>>>>>> bc454263923d6640cd52d60c5809893d9857902a

	public static GameManager instance = null;

	// Use this for initialization
	void Awake () {
		if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		Setup (0); // temp until we get more maps. Will randomize.
<<<<<<< HEAD
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
=======
	}    
	
	public void Setup(int mapID) {                
		Instantiate(maps[mapID]);        
		SetupTanks();
	}
	
	void Update () {        
        AddToScore(1);        
        AddToScore(2);
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
    }

    //IEnumerator PlayIntro()
    //{
    //    intro.text = "3...";
    //    yield return new WaitForSeconds(1);
    //    intro.text = "2...";
    //    yield return new WaitForSeconds(1);
    //    intro.text = "1...";
    //    yield return new WaitForSeconds(1);
    //    intro.text = "FIGHT!";        
    //}
>>>>>>> bc454263923d6640cd52d60c5809893d9857902a
}
