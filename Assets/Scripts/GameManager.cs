using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameManager : MonoBehaviour {

	// Public variables
	public int numberOfTanks = 1;
	public GameObject[] players = new GameObject[2];
    public int playerOneScore = 0;
    public int playerTwoScore = 0;
    public Text playerOneText;
    public Text playerTwoText;
    public Text intro;    

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
		SetupTanks();
	}
	
	void Update () {        
        //AddToScore(1);        
        //AddToScore(2);
	}

	void SetupTanks() {
		Instantiate (players [0]).transform.forward = new Vector3 (0,0,1);
		Instantiate (players [1]).transform.forward = new Vector3 (0,0,1);
	}
	
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
