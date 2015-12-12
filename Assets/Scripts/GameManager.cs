using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public int numberOfMaps = 1;
	public GameObject[] tanks = new GameObject[1];
	public GameObject[] maps = new GameObject[1];
    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public Text playerOneText;
    public Text playerTwoText;    

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
        AddToScore(1);
        AddToScore(2);
	}

	void SetupTanks() {
		for (int i = 0; i < numberOfTanks; ++i) {
			Instantiate (tanks [i]);
		}
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
}
