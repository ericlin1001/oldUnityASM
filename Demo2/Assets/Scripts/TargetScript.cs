using UnityEngine;
using System.Collections;

public class TargetScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
 
    void OnTriggerEnter(Collider other) {
        Destroy(gameObject);
        Debug.Log("you hit target");
        TextReader tr=gameObject.transform.parent.gameObject.GetComponent<TextReader>();
        if (tr != null)
        {
            tr.HitTarget();
        }
        
        //Destroy(other.gameObject);
       
    }
}
