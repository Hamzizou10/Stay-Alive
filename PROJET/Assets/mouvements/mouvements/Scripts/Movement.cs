using UnityEngine;
using System.Collections;
using System.Timers;
using System.Collections.Generic;

public class Movement : MonoBehaviour {

	// Use this for initialization
    public float speed = 0.2f;
    public float speedRun = 0.4f;
    public float speedrot = 3f;
    public CharacterController controller;
    public Vector3 moveDirection;
    public GameObject Character;
    static Timer s_myTimer = new Timer();
    private bool walk;
    private bool run;
    public float gravity;


	void Start () 
    {
	}

    
	// Update is called once per frame
    void Update()
    {
        
        
        s_myTimer.Elapsed += s_myTimer_Elapsed;
        if (s_myTimer.Enabled == false) 
        {
            transform.Rotate(0, Input.GetAxis("Horizontal") * speedrot, 0);
            if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.UpArrow))
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical") * speedRun);
                run = true;
            }
            else
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical") * speed);
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    walk = true;
                }

            }
            if (Input.GetKey(KeyCode.Space))
            {
                Character.GetComponent<Animation>().CrossFade("saut");
                s_myTimer.Interval = 1200;
                s_myTimer.Start();
               
            }

            else if (walk == true)
            {
                
                Character.GetComponent<Animation>().CrossFade("Marche");
            }
            else if (run==true)
            {
                Character.GetComponent<Animation>().CrossFade("run");
            }
            else 
            {
                Character.GetComponent<Animation>().CrossFade("Arret");
            }
            walk = false;
            run = false;

            moveDirection = transform.TransformDirection(moveDirection);
            moveDirection.y -= gravity;
            controller.Move(moveDirection);
        }


    }

    void s_myTimer_Elapsed(object sender, ElapsedEventArgs e)
    {
        s_myTimer.Enabled = false;
    }

}
