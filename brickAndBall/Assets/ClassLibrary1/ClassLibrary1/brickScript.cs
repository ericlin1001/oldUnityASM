using UnityEngine;
using System.Collections;

public class brickScript : MonoBehaviour {

	// Use this for initialization
    public int hitPoint = 1;
    public int score = 1;
	void Start () {

	}
    void OnCollisionEnter(Collision col)
    {
       

        foreach (ContactPoint contact in col.contacts)
        {
            //
            if (contact.thisCollider == this.collider)
            {
                GetComponent<AudioSource>().Play();
                hitPoint -= contact.otherCollider.GetComponent<BallScript>().hitPower;

                if (hitPoint <= 0)
                {
                    Instantiate(GameObject.Find("paddle").GetComponent<paddleScript>().foodPre,transform.position,Quaternion.identity);
                    Die();
                }

            }
        }
    }
    void Die()
    {
        
        Destroy(gameObject);
        //
        GameObject paddle = GameObject.Find("paddle");
        
        if (paddle)
        {
            paddleScript paddleS = paddle.GetComponent<paddleScript>();
            if (paddleS)
            {
                paddleS.PlayBrcikBrokeSound();
                paddleS.addScore(score);
               
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
