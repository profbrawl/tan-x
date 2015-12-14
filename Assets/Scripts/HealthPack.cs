using UnityEngine;
using System.Collections;

public class HealthPack : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
		if (vehicle != null) {
			vehicle.health += 100;
		}
		Destroy (gameObject);
	}
}
