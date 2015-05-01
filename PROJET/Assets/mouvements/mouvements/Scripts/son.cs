using UnityEngine;
using System.Collections;

public class son : MonoBehaviour
{
    CharacterController cc;
    // Use this for initialization
    void Start()
    {

        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetComponent<AudioSource>().isPlaying == false && cc.velocity.magnitude > 0.5f && cc.velocity.magnitude <= 2f)
        {



            GetComponent<AudioSource>().volume = 1f;
            GetComponent<AudioSource>().pitch = 0.9f;
            GetComponent<AudioSource>().Play();
        }




        else if (GetComponent<AudioSource>().isPlaying == false &&  cc.velocity.magnitude > 2f)
        {

            GetComponent<AudioSource>().volume = 2f;
            GetComponent<AudioSource>().pitch = 1.9f;
            GetComponent<AudioSource>().Play();

        }
    }
}