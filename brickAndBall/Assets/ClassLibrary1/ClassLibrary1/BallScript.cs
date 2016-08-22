using UnityEngine;
using System.Collections;

public class BallScript : MonoBehaviour {
	// Use this for initialization
    public int hitPower = 1;
    public int ballSpeed = 10;
	void Start () {
      
	}
	
	// Update is called once per frame
	void Update () {
	
	}
     void Die(){
         Destroy(gameObject);
    }
    void OnTriggerEnter(Collider other)
    {
       if(other==GameObject.Find("deathField").collider){
            Die();
        }
       
    }
   
}
