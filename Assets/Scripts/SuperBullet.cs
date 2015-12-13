using UnityEngine;
using System.Collections;

public class SuperBullet : Weapon {

	void OnCollisionEnter(Collision collision) {
		Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
		if (vehicle != null) {
			vehicle.health -= 10;
		}
		Destroy (gameObject);
	}
}
