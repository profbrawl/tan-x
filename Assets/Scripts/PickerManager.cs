using UnityEngine;
using System.Collections;

public class PickerManager : MonoBehaviour {

	public GameObject sphere;
	public GameObject cylinder;
	public GameObject capsule;
	public GameObject cube;
	public GameObject sphere1;
	public GameObject cylinder1;
	public GameObject capsule1;
	public GameObject cube1;
	public GameObject playerOnePos;
	public GameObject playerTwoPos;

	private GameObject[,] mCharacters = new GameObject[4, 2];
	private int mP1CurrentSelectedHorizontal = -1;
	private int mP1CurrentSelectedVertical = 0;
	private int mP2CurrentSelectedHorizontal = -1;
	private int mP2CurrentSelectedVertical = 0;
	private bool mIsP1HAxisInUse = false;
	private bool mIsP1VAxisInUse = false;
	private bool mIsP2HAxisInUse = false;
	private bool mIsP2VAxisInUse = false;
	private string mPlayerOnePrefix = "";
	private string mPlayerTwoPrefix = "";
	bool mP1InitalSelection = true;
	bool mP2InitalSelection = true;
	private int numberOfMaps = 3;

	private static PickerManager instance = null;

	public string PlayerOne
	{
		get
		{
			return this.mPlayerOnePrefix;
		}
	}

	public string PlayerTwo
	{
		get
		{
			return this.mPlayerTwoPrefix;
		}
	}

	// width * row + col
	public int PlayerOneCharacter
	{
		get
		{
			return 2 * mP1CurrentSelectedVertical + mP1CurrentSelectedHorizontal;
		}
	}

	// width * row + col
	public int PlayerTwoCharacter
	{
		get
		{
			return 2 * mP2CurrentSelectedVertical + mP2CurrentSelectedHorizontal;
		}
	}

	public static PickerManager getInstance() {
		return instance;
	}

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

		StartCoroutine(WaitForSelector());

		mCharacters[0, 0] = sphere;
		mCharacters[1, 0]= cylinder;
		mCharacters[2, 0] = capsule;
		mCharacters[3, 0] = cube;
		mCharacters[0, 1] = cube1;
		mCharacters[1, 1] = capsule1;
		mCharacters[2, 1] = sphere1;
		mCharacters[3, 1]= cylinder1;

//		StartCoroutine(playerController(mPlayerOnePrefix));
//		StartCoroutine(playerController(mPlayerTwoPrefix));
	}

	IEnumerator WaitForSelector() {
		while (true) {
			if (Input.GetButton("kFire1")) {
				Debug.Log("First controller is keyboard");
				mPlayerOnePrefix = "k";
				mPlayerTwoPrefix = "j";
				Destroy(GameObject.Find("PlayerSelectionDesc"));
				StartCoroutine(WaitForStartGame());
				break;
			} else if (Input.GetButton("jFire1")) {
				Debug.Log("First controller is joystick");
				mPlayerOnePrefix = "j";
				mPlayerTwoPrefix = "k";
				Destroy(GameObject.Find("PlayerSelectionDesc"));
				StartCoroutine(WaitForStartGame());
				break;
			} else {
				yield return null;
			}
		}
	}

	IEnumerator WaitForStartGame() {
		while (true) {
			if (mPlayerOnePrefix.Length != 0) {
				if (Input.GetButton(mPlayerOnePrefix + "Submit")) {
					Application.LoadLevel("GameScene_" + Random.Range(1, numberOfMaps + 3));
					break;
				} else {
					yield return null;
				}
			} else {
				yield return null;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
		if(mPlayerOnePrefix.Length != 0 && mPlayerTwoPrefix.Length != 0) {
			player1Controller();
			player2Controller();
		}
	}

	private void player1Controller() {
//		GameObject playerObject;
//		bool playerVAxisInUse;
//		bool playerHAxisInUse;
//		bool playerInitalSelection;
//		int playerCurrentSelectedVertical;
//		int playerCurrentSelectedHorizontal;
//
//		if (mPlayerOnePrefix == prefix) {
//			Debug.Log("player ONE is making a move.");
//			playerObject = playerOnePos;
//			playerVAxisInUse = mIsP1VAxisInUse;
//			playerHAxisInUse = mIsP1HAxisInUse;
//			playerInitalSelection = mP1InitalSelection;
//			playerCurrentSelectedVertical = mP1CurrentSelectedVertical;
//			playerCurrentSelectedHorizontal = mP1CurrentSelectedHorizontal;
//		} else {
//			Debug.Log("player TWO is making a move.");
//			playerObject = playerTwoPos;
//			playerVAxisInUse = mIsP2VAxisInUse;
//			playerHAxisInUse = mIsP2HAxisInUse;
//			playerInitalSelection = mP2InitalSelection;
//			playerCurrentSelectedVertical = mP2CurrentSelectedVertical;
//			playerCurrentSelectedHorizontal = mP2CurrentSelectedHorizontal;
//		}
//
//		Debug.Log(
//			"Player " + prefix + ":\n" +
//			"\t playerVAxisInUse: " + playerVAxisInUse + ":\n" +
//			"\t playerHAxisInUse: " + playerHAxisInUse + ":\n" +
//			"\t playerInitalSelection: " + playerInitalSelection + ":\n" +
//			"\t playerCurrentSelectedVertical: " + playerCurrentSelectedVertical + ":\n" +
//			"\t playerCurrentSelectedHorizontal: " + playerCurrentSelectedHorizontal
//		);

		float verticalAxis = Input.GetAxisRaw(mPlayerOnePrefix + "Vertical");
		if (verticalAxis == 0) {
			mIsP1VAxisInUse = false;
		} else if (verticalAxis < 0) { // We move our controller joystick down
			if (mP1InitalSelection) {
				if (mIsP1VAxisInUse == false) { 
					mIsP1VAxisInUse = true;
					mP1CurrentSelectedVertical = 0;
					animateForward(playerOnePos, 0, mP1CurrentSelectedVertical);
					mP1InitalSelection = false;
				}
			} else {
				if (mIsP1VAxisInUse == false && mP1CurrentSelectedVertical != 0) {
					mIsP1VAxisInUse = true;
					animateBackward(mP1CurrentSelectedVertical, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					mP1CurrentSelectedVertical = 0;
					animateForward(playerOnePos, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
				}
			}
		} else { // We move our controller joystick up
			if (mP1InitalSelection) {
				if (mIsP1VAxisInUse == false) { 
					mIsP1VAxisInUse = true;
					mP1CurrentSelectedVertical = 1;
					animateForward(playerOnePos, 0, mP1CurrentSelectedVertical);
					mP1InitalSelection = false;
				}
			} else {
				if (mIsP1VAxisInUse == false && mP1CurrentSelectedVertical != 1) {
					mIsP1VAxisInUse = true;
					animateBackward(mP1CurrentSelectedVertical, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					mP1CurrentSelectedVertical = 1;
					animateForward(playerOnePos, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
				}
			}
		}

		float horizontalAxis = Input.GetAxisRaw(mPlayerOnePrefix + "Horizontal");
		if (horizontalAxis == 0) {
			mIsP1HAxisInUse = false;
		} else if(horizontalAxis < 0) { // We moved our controller joystick left
			if (!mP1InitalSelection) {
				if (mIsP1HAxisInUse == false) {
					mIsP1HAxisInUse = true;
					animateBackward(mP1CurrentSelectedVertical, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					int nextMovePosition = (((mP1CurrentSelectedHorizontal - 1) % 4) + 4) % 4;
					animateForward(playerOnePos, nextMovePosition, mP1CurrentSelectedVertical);
					mP1CurrentSelectedHorizontal = nextMovePosition;
				}
			} else {
				if (mIsP1HAxisInUse == false) {
					mIsP1HAxisInUse = true;
					mP1CurrentSelectedHorizontal = 3;
					animateForward(playerOnePos, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					mP1InitalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!mP1InitalSelection) {
				if (mIsP1HAxisInUse == false) {
					mIsP1HAxisInUse = true;
					animateBackward(mP1CurrentSelectedVertical, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					int nextMovePosition = (mP1CurrentSelectedHorizontal + 1) % 4;
					animateForward(playerOnePos, nextMovePosition, mP1CurrentSelectedVertical);
					mP1CurrentSelectedHorizontal = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (mIsP1HAxisInUse == false) {
					mIsP1HAxisInUse = true;
					mP1CurrentSelectedHorizontal = 0;
					animateForward(playerOnePos, mP1CurrentSelectedHorizontal, mP1CurrentSelectedVertical);
					//mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					mP1InitalSelection = false;
				}
			}
		}
	}

	private void player2Controller() {
//		GameObject playerObject;
//		bool playerVAxisInUse;
//		bool playerHAxisInUse;
//		bool playerInitalSelection;
//		int playerCurrentSelectedVertical;
//		int playerCurrentSelectedHorizontal;
//
//		if (mPlayerOnePrefix == prefix) {
//			Debug.Log("player ONE is making a move.");
//			playerObject = playerOnePos;
//			playerVAxisInUse = mIsP1VAxisInUse;
//			playerHAxisInUse = mIsP1HAxisInUse;
//			playerInitalSelection = mP1InitalSelection;
//			playerCurrentSelectedVertical = mP1CurrentSelectedVertical;
//			playerCurrentSelectedHorizontal = mP1CurrentSelectedHorizontal;
//		} else {
//			Debug.Log("player TWO is making a move.");
//			playerObject = playerTwoPos;
//			playerVAxisInUse = mIsP2VAxisInUse;
//			playerHAxisInUse = mIsP2HAxisInUse;
//			playerInitalSelection = mP2InitalSelection;
//			playerCurrentSelectedVertical = mP2CurrentSelectedVertical;
//			playerCurrentSelectedHorizontal = mP2CurrentSelectedHorizontal;
//		}
//
//		Debug.Log(
//			"Player " + prefix + ":\n" +
//			"\t playerVAxisInUse: " + playerVAxisInUse + ":\n" +
//			"\t playerHAxisInUse: " + playerHAxisInUse + ":\n" +
//			"\t playerInitalSelection: " + playerInitalSelection + ":\n" +
//			"\t playerCurrentSelectedVertical: " + playerCurrentSelectedVertical + ":\n" +
//			"\t playerCurrentSelectedHorizontal: " + playerCurrentSelectedHorizontal
//		);

		float verticalAxis = Input.GetAxisRaw(mPlayerTwoPrefix + "Vertical");
		if (verticalAxis == 0) {
			mIsP2VAxisInUse = false;
		} else if (verticalAxis < 0) { // We move our controller joystick down
			if (mP2InitalSelection) {
				if (mIsP2VAxisInUse == false) { 
					mIsP2VAxisInUse = true;
					mP2CurrentSelectedVertical = 0;
					animateForward(playerTwoPos, 0, mP2CurrentSelectedVertical);
					mP2InitalSelection = false;
				}
			} else {
				if (mIsP2VAxisInUse == false && mP2CurrentSelectedVertical != 0) {
					mIsP2VAxisInUse = true;
					animateBackward(mP2CurrentSelectedVertical, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					mP2CurrentSelectedVertical = 0;
					animateForward(playerTwoPos, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
				}
			}
		} else { // We move our controller joystick up
			if (mP2InitalSelection) {
				if (mIsP2VAxisInUse == false) { 
					mIsP2VAxisInUse = true;
					mP2CurrentSelectedVertical = 1;
					animateForward(playerTwoPos, 0, mP2CurrentSelectedVertical);
					mP2InitalSelection = false;
				}
			} else {
				if (mIsP2VAxisInUse == false && mP2CurrentSelectedVertical != 1) {
					mIsP2VAxisInUse = true;
					animateBackward(mP2CurrentSelectedVertical, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					mP2CurrentSelectedVertical = 1;
					animateForward(playerTwoPos, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
				}
			}
		}

		float horizontalAxis = Input.GetAxisRaw(mPlayerTwoPrefix + "Horizontal");
		if (horizontalAxis == 0) {
			mIsP2HAxisInUse = false;
		} else if(horizontalAxis < 0) { // We moved our controller joystick left
			if (!mP2InitalSelection) {
				if (mIsP2HAxisInUse == false) {
					mIsP2HAxisInUse = true;
					animateBackward(mP2CurrentSelectedVertical, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					int nextMovePosition = (((mP2CurrentSelectedHorizontal - 1) % 4) + 4) % 4;
					animateForward(playerTwoPos, nextMovePosition, mP2CurrentSelectedVertical);
					mP2CurrentSelectedHorizontal = nextMovePosition;
				}
			} else {
				if (mIsP2HAxisInUse == false) {
					mIsP2HAxisInUse = true;
					mP2CurrentSelectedHorizontal = 3;
					animateForward(playerTwoPos, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					mP2InitalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!mP2InitalSelection) {
				if (mIsP2HAxisInUse == false) {
					mIsP2HAxisInUse = true;
					animateBackward(mP2CurrentSelectedVertical, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					int nextMovePosition = (mP2CurrentSelectedHorizontal + 1) % 4;
					animateForward(playerTwoPos, nextMovePosition, mP2CurrentSelectedVertical);
					mP2CurrentSelectedHorizontal = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (mIsP2HAxisInUse == false) {
					mIsP2HAxisInUse = true;
					mP2CurrentSelectedHorizontal = 0;
					animateForward(playerTwoPos, mP2CurrentSelectedHorizontal, mP2CurrentSelectedVertical);
					//mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					mP2InitalSelection = false;
				}
			}
		}
	}

	private void animateForward(GameObject player, int xPos, int yPos) {
		StartCoroutine(MoveObject(
			mCharacters[xPos, yPos].transform, 
			mCharacters[xPos, yPos].transform.position, 
			player.transform.position, 0.2f));
	}

	private void animateBackward(int selectedVerticalPos, int xPos, int yPos) {
		//TODO: mP1CurrentSelectedVertical doesn't look right, but this is what I'm using for now.
		float vecYPosition = selectedVerticalPos == 0 ? 0f : 3f;
		switch(xPos) {
			case 0:
				StartCoroutine(MoveObject(getTransform(xPos, yPos), getTransform(xPos, yPos).position,
					new Vector3(-4.75f, vecYPosition, 5), 0.2f));
				break;
			case 1:
				StartCoroutine(MoveObject(getTransform(xPos, yPos), getTransform(xPos, yPos).position,
					new Vector3(-1.80f, vecYPosition, 5), 0.2f));
				break;
			case 2:
				StartCoroutine(MoveObject(getTransform(xPos, yPos), getTransform(xPos, yPos).position,
					new Vector3(1.42f, vecYPosition, 5), 0.2f));
				break;
			case 3:
				StartCoroutine(MoveObject(getTransform(xPos, yPos), getTransform(xPos, yPos).position,
					new Vector3(4.53f, vecYPosition, 5), 0.2f));
				break;
		}
	}

	private IEnumerator MoveObject(Transform thisTransform, Vector3 startPos, Vector3 endPos, float time) {
		float i = 0.0f;
		float rate = 1.0f/time;
		while (i < 1.0) {
			i += Time.deltaTime * rate;
			thisTransform.position = Vector3.Lerp(startPos, endPos, i);
			yield return null;
		}
	}

	private Transform getTransform(int x, int y) {
		return mCharacters[x, y].transform;
	}
}
