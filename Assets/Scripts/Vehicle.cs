using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

	private int specialAmmo = 0;
	public int health = 100;
	public float moveSpeed = 4f;
	public float turnSpeed = 200f;
	public int shootForce = 400;

	public int SpecialAmmo {
		get { return specialAmmo; }
		set { specialAmmo += value; }
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.layer == 8) {
			health -= 5;
			Destroy (col.gameObject);
		}
	}

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

		if (Input.GetMouseButtonUp (0)) {
			GameObject laser;
			if (specialAmmo != 0) {
				laser = (GameObject)Instantiate (Resources.Load ("SpecialLaserBullet"));
				specialAmmo--;
			} else {
				laser = (GameObject)Instantiate (Resources.Load ("LaserBullet"));
			}
			laser.transform.position = transform.position + new Vector3(-0.4f, 0, 0);
			laser.GetComponent<Rigidbody>().AddForce(-transform.forward * shootForce);
		}
	}

	public int getHealth() {
		return health;
	}
}
