using UnityEngine;
using System.Collections;

public class Collision : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerStay(Collider coll)
    {
        if (coll.tag == "Player" && Input.GetKey(KeyCode.Return))
        {
            Application.LoadLevel(3);
        }
        
    }
}
