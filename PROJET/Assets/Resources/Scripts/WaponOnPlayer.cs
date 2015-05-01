

using UnityEngine;
using System.Collections;

public class WaponOnPlayer : MonoBehaviour {

	public Transform thisWeapon;
	private Transform OtherWeapon;

	public AudioClip weapSound;
	public float fireRate=0.2f;
	public float power = 5.0f;
	public float imprecision = 20;
	public int raysPerShoot = 1;
	public int bullets = 5;
	public int magazine = 1;
	public float fireAnimatorSpeed = 1.0f;
	public Texture2D crossTexture;
	public GameObject muzzleFlash;
	public string Anim;
	public string Wik;
	public string preFireAnim;
	public string fireAnim;
	public Rigidbody rigidBodyPrefab;
	public Transform rigidSpawnTarget;
	public Transform lTarget;
	public Transform rTarget;


	void LateUpdate () {
	

		GameObject thePlayer = GameObject.Find("Player");
		Weapons playerScript = thePlayer.GetComponent<Weapons>();

		GameObject theCamera = GameObject.Find("PlayerCamera");
		OrbitCamera CamScript = theCamera.GetComponent<OrbitCamera>();

		if (playerScript.WeapObj != thisWeapon) {

				OtherWeapon = playerScript.WeapObj;


			if (gameObject.tag == "on") {

	if (playerScript.WeapObj != null)
				{
				OtherWeapon.tag = "off";
				}

				playerScript.WAnimOld = playerScript.WAnimation;

				CamScript.crosshairTexture = crossTexture;

				playerScript.RigidBodyPrefab = rigidBodyPrefab;
				playerScript.RigidSpawnTarget = rigidSpawnTarget;
				playerScript.Bullets = bullets;
				playerScript.Magazine = magazine;
				playerScript.PreFireAnim = preFireAnim;
				playerScript.FireAnim = fireAnim;
				playerScript.RaysPerShoot = raysPerShoot;
				playerScript.Imprecision = imprecision;
				playerScript.WeaponSound = weapSound;
				playerScript.FireRate = fireRate;
				playerScript.Power = power;
				playerScript.FireAnimatorSpeed = fireAnimatorSpeed;
				playerScript.MuzzleFlash = muzzleFlash;
				playerScript.WeapObj = thisWeapon;
				playerScript.leftHandle = lTarget;
				playerScript.rightHandle = rTarget;

				playerScript.WAnimation = Anim;
				playerScript.WIk = Wik;

				GetComponent<Renderer>().enabled = true;

			
			

								//gameObject.GetComponent<Weapons> ().Armed = true;
								//gameObject.GetComponent<Weapons> ().typology = WeapName;
								//gameObject.GetComponent<Weapons> ().ActiveWeapon = WeaponOnPlayer;
						}
				
		} 
		if (gameObject.tag == "off") {

						//gameObject.tag = "noActive";
						GetComponent<Renderer>().enabled = false;

				}
	
	}
}
