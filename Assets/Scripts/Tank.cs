using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {
	
	// Temp speeds
	public float moveSpeed = 5f;
	public float turnSpeed = 200f;
	
	void Update () {
		
		// Temp controls for testing
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
	}
}
