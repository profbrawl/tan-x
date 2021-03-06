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
			GameObject laser1;
            GameObject laser2;
			if (specialAmmo != 0) {
				laser1 = (GameObject)Instantiate (Resources.Load ("SpecialLaserBullet"), transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                laser2 = (GameObject)Instantiate(Resources.Load("SpecialLaserBullet"), transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
                specialAmmo--;
			} else {
				laser1 = (GameObject)Instantiate (Resources.Load ("LaserBullet"), transform.position + new Vector3(-0.5f, 0, 0), Quaternion.identity);
                laser2 = (GameObject)Instantiate(Resources.Load("LaserBullet"), transform.position + new Vector3(0.5f, 0, 0), Quaternion.identity);
            }
			//laser.transform.position = transform.position + new Vector3(-0.4f, 0, 0);
			laser1.GetComponent<Rigidbody>().AddForce(-transform.forward * shootForce);
            laser2.GetComponent<Rigidbody>().AddForce(-transform.forward * shootForce);
            Destroy(laser1, 3);
            Destroy(laser2, 3);
        }

		float translation = Input.GetAxis(playerPrefix + "Vertical") * moveSpeed;
		translation *= Time.deltaTime;
		transform.Translate(new Vector3(-1 * translation, 0, 0));
        //transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        //transform.position = new Vector3(transform.position.x, 1f, transform.position.z);
        this.GetComponent<Rigidbody>().AddForce(new Vector3(0, -10, 0));

		float rotation = Input.GetAxis(playerPrefix + "Horizontal") * turnSpeed;
		rotation *= Time.deltaTime;
		transform.Rotate(new Vector3(0, rotation, 0));
	}

	public int getHealth() {
		return health;
	}
}
