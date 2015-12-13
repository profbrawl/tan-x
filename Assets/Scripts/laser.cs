using UnityEngine;
using System.Collections;

public class laser : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

	void OnCollisionEnter(Collision col){
		if (col.gameObject.name == "Tank") {
			Destroy(gameObject);
			Destroy(col.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
