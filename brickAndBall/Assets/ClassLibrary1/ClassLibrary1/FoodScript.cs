using UnityEngine;
using System.Collections;

public class FoodScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnCollisionEnter(Collision col)
    {


       Destroy(gameObject);
        //
        GameObject paddle = GameObject.Find("paddle");
        if (paddle)
        {
            paddleScript paddleS = paddle.GetComponent<paddleScript>();
            if (paddleS)
            {
                paddleS.EatFood();
            }
        }

    }
}
