using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeritMoveScript : MonoBehaviour {
    public float speed;
    Rigidbody rgb;
	// Use this for initialization
	void Start () {
        rgb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        rgb.velocity = new Vector3(-speed, 0, 0);
        if (transform.position.x < -4)
        {
            Destroy(gameObject);
        }
	}
}
