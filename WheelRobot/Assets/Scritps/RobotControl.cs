using UnityEngine;
using System.Collections;

public class RobotControl : MonoBehaviour {

	// Use this for initialization
    GameObject leftWheel;
    GameObject rightWheel;
	void Start () {
        leftWheel = gameObject.transform.FindChild("LeftWheel").gameObject;
        rightWheel = gameObject.transform.FindChild("RightWheel").gameObject;
        //
        rigidbody.AddRelativeTorque(new Vector3(0, 1f, 0));
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 t = new Vector3(0, 0,10f );
        leftWheel.rigidbody.AddTorque(t);
        rightWheel.rigidbody.AddTorque(t);
        //leftWheel.rigidbody.AddTorque(new Vector3(0, 100f, 0));
	}
}
