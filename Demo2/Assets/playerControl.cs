using UnityEngine;
using System.Collections;

public class playerControl : MonoBehaviour {
    public float moveForce = 100f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        rigidbody.AddForce(Input.GetAxis("Horizontal") * moveForce, 0, 0);
        rigidbody.AddForce(0, 0, Input.GetAxis("Vertical") * moveForce);
	}
    
}
