using UnityEngine;
using System.Collections;

public class ZombieMovement : MonoBehaviour {

    public GameObject Player;
    public GameObject ZombiePos;
    public float speed;
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        ZombiePos.transform.LookAt(Player.transform.position);
        ZombiePos.transform.position += transform.TransformDirection(Vector3.forward)*speed;
	}
}
