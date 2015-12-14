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
	private bool mIsHAxisInUse = false;
	private bool mIsVAxisInUse = false;
	private string mController = "j";

	private static PickerManager instance = null;

	// Use this for initialization
	void Start () {
		if (instance == null) {
			instance = this;
		} else if (instance != this) {
			Destroy(gameObject);
		}

//		StartCoroutine(WaitForSelector());

		mCharacters[0, 0] = sphere;
		mCharacters[1, 0]= cylinder;
		mCharacters[2, 0] = capsule;
		mCharacters[3, 0] = cube;
		mCharacters[0, 1] = cube1;
		mCharacters[1, 1] = capsule1;
		mCharacters[2, 1] = sphere1;
		mCharacters[3, 1]= cylinder1;

//		if (Input.GetButton("Jump"))
//			mController = "k";

//		else if (Input.GetButton("jSubmit"))
//			mController = "j";
	}

	IEnumerator WaitForSelector() {
		while (true) {
			if (Input.GetButton("Jump")) {
				Debug.Log("Main controller is keyboard");
				mController = "k";
				break;
			} else if (Input.GetButton("jSubmit")) {
				Debug.Log("Main controller is joystick");
				mController = "j";
				break;
			} else {
				yield return null;
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
		float verticalAxis = Input.GetAxisRaw(mController + "Vertical");
		if (verticalAxis == 0) {
			mIsVAxisInUse = false;
		} else if (verticalAxis < 0) { // We move our controller joystick down
			if (initalSelection) {
				if (mIsVAxisInUse == false) { 
					mIsVAxisInUse = true;
					mCurrentSelectedVertical = 0;
					animateForward(0, mCurrentSelectedVertical);
					initalSelection = false;
				}
			} else {
				if (mIsVAxisInUse == false && mCurrentSelectedVertical != 0) {
					mIsVAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					mCurrentSelectedVertical = 0;
					animateForward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
				}
			}
		} else { // We move our controller joystick up
			if (initalSelection) {
				if (mIsVAxisInUse == false) { 
					mIsVAxisInUse = true;
					mCurrentSelectedVertical = 1;
					animateForward(0, mCurrentSelectedVertical);
					initalSelection = false;
				}
			} else {
				if (mIsVAxisInUse == false && mCurrentSelectedVertical != 1) {
					mIsVAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					mCurrentSelectedVertical = 1;
					animateForward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
				}
			}
		}

		float horizontalAxis = Input.GetAxisRaw(mController + "Horizontal");
		if (horizontalAxis == 0) {
			mIsHAxisInUse = false;
		} else if(horizontalAxis < 0) { // We moved our controller joystick left
			if (!initalSelection) {
				if (mIsHAxisInUse == false) {
					mIsHAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					int nextMovePosition = (((mCurrentSelectedHorizontal - 1) % 4) + 4) % 4;
					animateForward(nextMovePosition, mCurrentSelectedVertical);
					mCurrentSelectedHorizontal = nextMovePosition;
				}
			} else {
				if (mIsHAxisInUse == false) {
					mIsHAxisInUse = true;
					mCurrentSelectedHorizontal = 3;
					animateForward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					initalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!initalSelection) {
				if (mIsHAxisInUse == false) {
					mIsHAxisInUse = true;
					animateBackward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					int nextMovePosition = (mCurrentSelectedHorizontal + 1) % 4;
					animateForward(nextMovePosition, mCurrentSelectedVertical);
					mCurrentSelectedHorizontal = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (mIsHAxisInUse == false) {
					mIsHAxisInUse = true;
					mCurrentSelectedHorizontal = 0;
					animateForward(mCurrentSelectedHorizontal, mCurrentSelectedVertical);
					//mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					initalSelection = false;
				}
			}
		}

		if (Input.GetButtonDown(mController + "Submit")) {
			Debug.Log("Submit button hit");
			//TODO Pass the selection to the GameScene
			Application.LoadLevel("GameScene");
		}
			
	}

	private void animateForward(int xPos, int yPos) {
		StartCoroutine(MoveObject(
			mCharacters[xPos, yPos].transform, 
			mCharacters[xPos, yPos].transform.position, 
			playerOnePos.transform.position, 0.2f));
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
