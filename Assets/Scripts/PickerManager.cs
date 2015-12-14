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
	private int numberOfMaps = 1;

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
			return 4 * mP1CurrentSelectedHorizontal + mP1CurrentSelectedVertical;
		}
	}

	// width * row + col
	public int PlayerTwoCharacter
	{
		get
		{
			return 4 * mP2CurrentSelectedHorizontal + mP2CurrentSelectedVertical;
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
			playerController(mPlayerOnePrefix);
			playerController(mPlayerTwoPrefix);
		}
	}

	private void playerController(string prefix) {
		GameObject playerObject;
		bool playerVAxisInUse;
		bool playerHAxisInUse;
		bool playerInitalSelection;
		int playerCurrentSelectedVertical;
		int playerCurrentSelectedHorizontal;

		if (mPlayerOnePrefix == prefix) {
			Debug.Log("player ONE is making a move.");
			playerObject = playerOnePos;
			playerVAxisInUse = mIsP1VAxisInUse;
			playerHAxisInUse = mIsP1HAxisInUse;
			playerInitalSelection = mP1InitalSelection;
			playerCurrentSelectedVertical = mP1CurrentSelectedVertical;
			playerCurrentSelectedHorizontal = mP1CurrentSelectedHorizontal;
		} else {
			Debug.Log("player TWO is making a move.");
			playerObject = playerTwoPos;
			playerVAxisInUse = mIsP2VAxisInUse;
			playerHAxisInUse = mIsP2HAxisInUse;
			playerInitalSelection = mP2InitalSelection;
			playerCurrentSelectedVertical = mP2CurrentSelectedVertical;
			playerCurrentSelectedHorizontal = mP2CurrentSelectedHorizontal;
		}

		float verticalAxis = Input.GetAxisRaw(prefix + "Vertical");
		if (verticalAxis == 0) {
			playerVAxisInUse = false;
		} else if (verticalAxis < 0) { // We move our controller joystick down
			if (playerInitalSelection) {
				if (playerVAxisInUse == false) { 
					playerVAxisInUse = true;
					playerCurrentSelectedVertical = 0;
					animateForward(playerObject, 0, playerCurrentSelectedVertical);
					playerInitalSelection = false;
				}
			} else {
				if (playerVAxisInUse == false && playerCurrentSelectedVertical != 0) {
					playerVAxisInUse = true;
					animateBackward(playerCurrentSelectedVertical, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					playerCurrentSelectedVertical = 0;
					animateForward(playerObject, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
				}
			}
		} else { // We move our controller joystick up
			if (playerInitalSelection) {
				if (playerVAxisInUse == false) { 
					playerVAxisInUse = true;
					playerCurrentSelectedVertical = 1;
					animateForward(playerObject, 0, playerCurrentSelectedVertical);
					playerInitalSelection = false;
				}
			} else {
				if (playerVAxisInUse == false && playerCurrentSelectedVertical != 1) {
					playerVAxisInUse = true;
					animateBackward(playerCurrentSelectedVertical, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					playerCurrentSelectedVertical = 1;
					animateForward(playerObject, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
				}
			}
		}

		float horizontalAxis = Input.GetAxisRaw(prefix + "Horizontal");
		if (horizontalAxis == 0) {
			playerHAxisInUse = false;
		} else if(horizontalAxis < 0) { // We moved our controller joystick left
			if (!playerInitalSelection) {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					animateBackward(playerCurrentSelectedVertical, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					int nextMovePosition = (((playerCurrentSelectedHorizontal - 1) % 4) + 4) % 4;
					animateForward(playerObject, nextMovePosition, playerCurrentSelectedVertical);
					playerCurrentSelectedHorizontal = nextMovePosition;
				}
			} else {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					playerCurrentSelectedHorizontal = 3;
					animateForward(playerObject, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					playerInitalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!playerInitalSelection) {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					animateBackward(playerCurrentSelectedVertical, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					int nextMovePosition = (playerCurrentSelectedHorizontal + 1) % 4;
					animateForward(playerObject, nextMovePosition, playerCurrentSelectedVertical);
					playerCurrentSelectedHorizontal = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					playerCurrentSelectedHorizontal = 0;
					animateForward(playerObject, playerCurrentSelectedHorizontal, playerCurrentSelectedVertical);
					//mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					playerInitalSelection = false;
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
