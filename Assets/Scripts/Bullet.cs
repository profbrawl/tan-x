using UnityEngine;
using System.Collections;

public class Bullet : Weapon {

	void OnCollisionEnter(Collision collision) {
		Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
		if (vehicle != null) {
			vehicle.health -= 5;
		}
		Destroy (gameObject);
	}
}
