﻿using UnityEngine;
using System.Collections;

public class traps : MonoBehaviour
{
	public GameObject laser_Emitter;
	public GameObject laser;


	void Start ()
	{
		InvokeRepeating("shootLaser", 5, 5.0F);
	}


	void shootLaser(){
		GameObject Temporary_laser_Handler;
		Temporary_laser_Handler = Instantiate(laser,laser_Emitter.transform.position,laser_Emitter.transform.rotation) as GameObject;
		Rigidbody Temporary_RigidBody;
		Temporary_RigidBody = Temporary_laser_Handler.GetComponent<Rigidbody>();
		Temporary_RigidBody.AddForce(transform.forward *200f);
		Destroy(Temporary_laser_Handler, 1.5f);
	}
}