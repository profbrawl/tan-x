﻿using UnityEngine;
using System.Collections;

public class Tank : MonoBehaviour {

<<<<<<< HEAD
	// Temp speeds
	public float moveSpeed = 10f;
	public float turnSpeed = 100f;
	
	void Update () {

		// Temp controls for testing
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.forward * moveSpeed * Time.deltaTime);
=======
	private int specialGunCount = 3;

	// Temp speeds
	public float moveSpeed = 4f;
	public float turnSpeed = 200f;
	public int shootForce = 400;
	
	void Update () {
		
		// Temp controls for testing
		if(Input.GetKey(KeyCode.UpArrow))
			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.DownArrow))
			transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
>>>>>>> bc454263923d6640cd52d60c5809893d9857902a
		
		if(Input.GetKey(KeyCode.LeftArrow))
			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
		
		if(Input.GetKey(KeyCode.RightArrow))
			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
<<<<<<< HEAD
=======

		if (Input.GetMouseButtonUp (0)) {
			GameObject laser;
			if (specialGunCount != 0) {
				laser = (GameObject)Instantiate (Resources.Load ("SpecialLaserBullet"));
				specialGunCount--;
			} else {
				laser = (GameObject)Instantiate (Resources.Load ("LaserBullet"));
			}
			laser.transform.position = transform.position;
			laser.GetComponent<Rigidbody>().AddForce(-transform.right * shootForce);
		}
>>>>>>> bc454263923d6640cd52d60c5809893d9857902a
	}
}
