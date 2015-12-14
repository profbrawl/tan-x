using UnityEngine;
using System.Collections;

public class SpecialAmmo : MonoBehaviour {

	void OnCollisionEnter(Collision collision) {
		Vehicle vehicle = collision.gameObject.GetComponent<Vehicle>();
		if (vehicle != null) {
			vehicle.SpecialAmmo = 3;
		}
		Destroy (gameObject);
	}
}
