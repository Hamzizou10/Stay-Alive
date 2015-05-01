using UnityEngine;
using System.Collections;

public class C2 : MonoBehaviour {

	 //Variables
    #region
    //Object
     public   GameObject personnage;
     public Vector3 decalage;
     public Vector3 decalage1;
    //Nombres 
     public float movementSpeed = 10;
     public float turningSpeed = 60;
    #endregion

    void Start () 
    {
        decalage = transform.position - personnage.transform.position;
        
	}
	
    //Fonction	
    void Update()
    {
        
    }

    void LateUpdate ()
    {

        Vector3 desiredPosition = personnage.transform.position - decalage;
        transform.position = desiredPosition;
        float desiredAngle = personnage.transform.eulerAngles.y;
        Quaternion rotation = Quaternion.Euler(0, desiredAngle, 0);
        transform.position = personnage.transform.position - (rotation * decalage);
        transform.LookAt(personnage.transform);
        
        
	}
	}

