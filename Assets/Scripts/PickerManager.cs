using UnityEngine;
using System.Collections;

public class PickerManager : MonoBehaviour {

	bool initalSelection = true;
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
	private int mCurrentSelectedHorizontal = -1;
	private int mCurrentSelectedVertical = 0;
	private bool mIsP1HAxisInUse = false;
	private bool mIsP1VAxisInUse = false;
	private bool mIsP2HAxisInUse = false;
	private bool mIsP2VAxisInUse = false;
	private string mPlayerOnePrefix = "";
	private string mPlayerTwoPrefix = "";

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
					Debug.Log("Submit button hit");
					//TODO Pass the selection to the GameScene
					Application.LoadLevel("GameScene_1");
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
		if (mPlayerOnePrefix == prefix) {
			Debug.Log("player ONE is making a move.");
			playerObject = playerOnePos;
			playerVAxisInUse = mIsP1VAxisInUse;
			playerHAxisInUse = mIsP1HAxisInUse;
		} else {
			Debug.Log("player TWO is making a move.");
			playerObject = playerTwoPos;
			playerVAxisInUse = mIsP2VAxisInUse;
			playerHAxisInUse = mIsP2HAxisInUse;
		}

		float verticalAxis = Input.GetAxisRaw(prefix + "Vertical");
		if (verticalAxis == 0) {
			playerVAxisInUse = false;
		} else if (verticalAxis < 0) { // We move our controller joystick down
			if (initalSelection) {
				if (playerVAxisInUse == false) { 
					playerVAxisInUse = true;
					mCurrentSelectedVertical = 0;
					animateForward(playerObject, 0, mCurrentSelectedVertical);
					initalSelection = false;
				}
			} else {
				if (playerVAxisInUse == false && mCurrentSelectedVertical != 0) {
					playerVAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					mCurrentSelectedVertical = 0;
					animateForward(playerObject, mCurrentSelectedHorizontal, mCurrentSelectedVertical);
				}
			}
		} else { // We move our controller joystick up
			if (initalSelection) {
				if (playerVAxisInUse == false) { 
					playerVAxisInUse = true;
					mCurrentSelectedVertical = 1;
					animateForward(playerObject, 0, mCurrentSelectedVertical);
					initalSelection = false;
				}
			} else {
				if (playerVAxisInUse == false && mCurrentSelectedVertical != 1) {
					playerVAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					mCurrentSelectedVertical = 1;
					animateForward(playerObject, mCurrentSelectedHorizontal, mCurrentSelectedVertical);
				}
			}
		}

		float horizontalAxis = Input.GetAxisRaw(prefix + "Horizontal");
		if (horizontalAxis == 0) {
			playerHAxisInUse = false;
		} else if(horizontalAxis < 0) { // We moved our controller joystick left
			if (!initalSelection) {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					int nextMovePosition = (((mCurrentSelectedHorizontal - 1) % 4) + 4) % 4;
					animateForward(playerObject, nextMovePosition, mCurrentSelectedVertical);
					mCurrentSelectedHorizontal = nextMovePosition;
				}
			} else {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					mCurrentSelectedHorizontal = 3;
					animateForward(playerObject, mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					initalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!initalSelection) {
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					int nextMovePosition = (mCurrentSelectedHorizontal + 1) % 4;
					animateForward(playerObject, nextMovePosition, mCurrentSelectedVertical);
					mCurrentSelectedHorizontal = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (playerHAxisInUse == false) {
					playerHAxisInUse = true;
					mCurrentSelectedHorizontal = 0;
					animateForward(playerObject, mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					//mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					initalSelection = false;
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

	private void animateBackward(int xPos, int yPos) {
		float vecYPosition = mCurrentSelectedVertical == 0 ? 0f : 3f;
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
