using UnityEngine;
using System.Collections;

public class PickerManager : MonoBehaviour {

	bool initalSelection = true;
	public GameObject sphere;
	public GameObject cylinder;
	public GameObject capsule;
	public GameObject cube;

	private GameObject[] mCharacters = new GameObject[4];
	private int mCurrentSelected = -1;
	private bool mIsAxisInUse = false;
	private bool mIsSelectedInUse = false;

	// Use this for initialization
	void Start () {
		mCharacters[0] = sphere;
		mCharacters[1] = cylinder;
		mCharacters[2] = capsule;
		mCharacters[3] = cube;
	}
	
	// Update is called once per frame
	void Update () {
	
		float clicked = Input.GetAxisRaw("Horizontal");
		//Debug.Log("Input axis value: " + clicked);
		if(clicked == 0) {
			mIsAxisInUse = false;
		} else if(clicked < 0) { // We moved our controller joystick left
			if (!initalSelection) {
				if (mIsAxisInUse == false) {
					mIsAxisInUse = true;
					animateBackward(mCurrentSelected);
					int nextMovePosition = (((mCurrentSelected - 1) % 4) + 4) % 4;
					animateForward(nextMovePosition);
					mCurrentSelected = nextMovePosition;
				}
			} else {
				if (mIsAxisInUse == false) {
					mIsAxisInUse = true;
					mCurrentSelected = 3;
					mCharacters[3].GetComponent<Animation>().Blend("Cube_SelectedForward");
					initalSelection = false;
				}
			}
		} else { // We moved our controller joystick right
			if (!initalSelection) {
				if (mIsAxisInUse == false) {
					mIsAxisInUse = true;
					animateBackward(mCurrentSelected);
					int nextMovePosition = (mCurrentSelected + 1) % 4;
					animateForward(nextMovePosition);
					mCurrentSelected = nextMovePosition;
				}
			} else { // Bring left most character forward
				if (mIsAxisInUse == false) {
					mIsAxisInUse = true;
					mCurrentSelected = 0;
					mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
					initalSelection = false;
				}
			}
		}

		if (Input.GetKey(KeyCode.KeypadEnter) || Input.GetButtonDown("Submit")) {
			Debug.Log("Submit button");
			Application.LoadLevel("GameScene");
		}
			
	}

	private void animateForward(int position) {
		switch(position) {
			case 0:
				mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedForward");
				break;
			case 1:
				mCharacters[1].GetComponent<Animation>().Blend("Cylinder_SelectedForward");
				break;
			case 2:
				mCharacters[2].GetComponent<Animation>().Blend("Capsule_SelectedForward");
				break;
			case 3:
				mCharacters[3].GetComponent<Animation>().Blend("Cube_SelectedForward");
				break;
			default:
				break;
		}
	}

	private void animateBackward(int position) {
		switch(position) {
			case 0:
				mCharacters[0].GetComponent<Animation>().Blend("Sphere_SelectedBackward");
				break;
			case 1:
				mCharacters[1].GetComponent<Animation>().Blend("Cylinder_SelectedBackward");
				break;
			case 2:
				mCharacters[2].GetComponent<Animation>().Blend("Capsule_SelectedBackward");
				break;
			case 3:
				mCharacters[3].GetComponent<Animation>().Blend("Cube_SelectedBackward");
				break;
			default:
				break;
		}
	}

	private void animateSelected(int position) {
		if (position != -1) { // No item has been selected yet.
			mCharacters[position].GetComponent<Animation>().Blend("SelectedEnter");
		}
	}
}
