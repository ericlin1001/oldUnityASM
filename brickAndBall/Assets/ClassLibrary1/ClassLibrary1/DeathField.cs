using UnityEngine;
using System.Collections;

public class DeathField : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter (Collider other ) {
        Debug.Log("die");
        GameObject paddle = GameObject.Find("paddle");
        if (paddle)
        {
            paddleScript paddleS = paddle.GetComponent<paddleScript>();
            if (paddleS)
            {
                paddleS.Die();
            }
        }
    }
}
