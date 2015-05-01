using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	// Use this for initialization

    public GameObject Zombie;
    public int health;
	void Start () {
        health = 100;
	}
	
	// Update is called once per frame
	void Update () {
        if (health == 0)
        {
            Zombie.gameObject.SetActive(false);
        }
	}
    void OnTriggerStay(Collider obj)
    {
        if(obj.tag == "Player")
        {
            health -= 20;
        }
    }
}
