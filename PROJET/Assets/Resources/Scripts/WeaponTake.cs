

using UnityEngine;
using System.Collections;

public class WeaponTake : MonoBehaviour {

	public WaponOnPlayer WeaponOn;


	void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Player")
		{

			Destroy(gameObject);			
			WeaponOn.tag = "on";
		}
	}
}
