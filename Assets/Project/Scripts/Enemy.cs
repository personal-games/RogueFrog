using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float speed = 1f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);

        if (transform.position.x > (Screen.width / 100f) /2f + 1f) {
            transform.position = new Vector3(-transform.position.x + 1f, transform.position.y, transform.position.z);
        } else if (transform.position.x < -(Screen.width / 100f) / 2f - 1f) {
            transform.position = new Vector3(-transform.position.x - 1f, transform.position.y, transform.position.z);   
        }

	}
}
