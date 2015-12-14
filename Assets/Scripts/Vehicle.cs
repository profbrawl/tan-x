using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour {

	private int specialAmmo = 0;
	public int health = 100;
	public string playerPrefix;

	// Temp speeds
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
//		if(Input.GetKey(KeyCode.UpArrow))
//			transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
//
//		if(Input.GetKey(KeyCode.DownArrow))
//			transform.Translate(-Vector3.left * moveSpeed * Time.deltaTime);
//
//		if(Input.GetKey(KeyCode.LeftArrow))
//			transform.Rotate(Vector3.up, -turnSpeed * Time.deltaTime);
//
//		if(Input.GetKey(KeyCode.RightArrow))
//			transform.Rotate(Vector3.up, turnSpeed * Time.deltaTime);
//
//		if (Input.GetMouseButtonUp (0)) {
//		Debug.Log("player prefix: " + playerPrefix);
		if (Input.GetButton(playerPrefix + "Fire1")) {
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

		float translation = Input.GetAxis(playerPrefix + "Vertical") * moveSpeed;
		translation *= Time.deltaTime;
		transform.Translate(new Vector3(-1 * translation, 0, 0));

		float rotation = Input.GetAxis(playerPrefix + "Horizontal") * turnSpeed;
		rotation *= Time.deltaTime;
		transform.Rotate(new Vector3(0, rotation, 0));
	}

	public int getHealth() {
		return health;
	}
}
