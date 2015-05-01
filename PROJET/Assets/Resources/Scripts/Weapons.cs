

using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Animator))]


public class Weapons : MonoBehaviour {

	public GUISkin CustomSkin;
	public AudioClip WeaponEmpty;

	public enum BaseState
	{
		Base,
		Combat,
		Climb,
		Swim,
		Pracute,
		JetPack

	}

	public BaseState baseState = BaseState.Base;

	public AnimControllers animCtrl;
	[System.Serializable]
	public class AnimControllers
	{
		public RuntimeAnimatorController Base;
		
	}

	private bool GuiIsActive = false;
	private Animator m_Animator;

	[HideInInspector] public Transform WeapObj;
	[HideInInspector]public Transform leftHandle;
	[HideInInspector]public Transform rightHandle;
	[HideInInspector]public GameObject MuzzleFlash;
	[HideInInspector]public AudioClip WeaponSound;

	[HideInInspector]public string WAnimation = "None";
	[HideInInspector]public string WAnimOld = "None";
	[HideInInspector]public string WIk = "Bazooka.Bazooka";
	[HideInInspector]public string PreFireAnim;
	[HideInInspector]public string FireAnim;

	[HideInInspector]public Rigidbody RigidBodyPrefab;
	[HideInInspector]public Transform RigidSpawnTarget;

	[HideInInspector]public int PowerThrow ;

	private RaycastHit hit;
	private Ray ray;
	private bool Reloading;
	private float nextFire = 0.0f;
	private float LastReload = 0.0f;

	[HideInInspector]public int RaysPerShoot;
	[HideInInspector]public int Bullets;
	[HideInInspector]public int Magazine;

	[HideInInspector]public float FireRate;
	[HideInInspector]public float FireAnimatorSpeed;
	[HideInInspector]public float Power = 0.0f;
	[HideInInspector]public float Imprecision;
	[HideInInspector]public float ReloadTime = 1.0f;

	public float HitParticlesLifeTime = 10.1f;
	public GameObject[] _hitParticles;

	

	void Start () 
	{
		m_Animator = GetComponent<Animator>();        
	}

	void Update()
	{
		var fwd = transform.TransformDirection(Vector3.forward);

		bool Shoot = Input.GetButton ("Fire1");
		bool Aim = Input.GetButton ("Fire2");
		bool Reload = Input.GetKey (KeyCode.R);
		bool ThrowRelease = Input.GetButtonUp ("Fire1");

		if (FireAnim != "") {
						m_Animator.SetBool (FireAnim, false);
				}

		if (Reload && Magazine > 0 && Bullets == 0 && !Aim) {
						Reloading = true;
						m_Animator.SetBool ("Reload", true);
						var Put = WeapObj.GetComponent<WaponOnPlayer> ().bullets;
						Bullets = Put;
						Magazine = Magazine - 1;
			            LastReload = Time.time + ReloadTime;
				} else {
			if (Time.time > LastReload){
			Reloading = false;
			m_Animator.SetBool ("Reload", false);	
			}
		}


		if (Shoot && Aim && WAnimation == "Equip" && Time.time > nextFire && Bullets > 0 && !Reloading) {

			            m_Animator.SetBool (FireAnim, true);
			            m_Animator.speed = FireAnimatorSpeed;

						MuzzleFlash.SetActive (true);
			            GetComponent<AudioSource>().PlayOneShot(WeaponSound);
			            Bullets = Bullets -1;

						nextFire = Time.time + FireRate;

			for(int i =0; i<RaysPerShoot; i++){

				Vector2 screenCenterPoint = new Vector2 (Screen.width / 2 + Random.Range(Imprecision, - Imprecision), Screen.height / 2 + Random.Range(Imprecision, - Imprecision));
				
				ray = Camera.main.ScreenPointToRay (screenCenterPoint);

				if (Physics.Raycast (ray, out hit, Camera.main.farClipPlane)) {

					Quaternion rot = Quaternion.FromToRotation (Vector3.forward, hit.normal);
		

					if (hit.collider.tag == "Dirt") {
																			
						var DirtHole = Instantiate (_hitParticles [0], hit.point, rot) as GameObject;
						DirtHole.transform.parent = hit.transform;
						Destroy(DirtHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Metal") {
						
						var MetalHole = Instantiate (_hitParticles [1], hit.point, rot) as GameObject;
						MetalHole.transform.parent = hit.transform;
						Destroy(MetalHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Wood") {
						
						var WoodHole = Instantiate (_hitParticles [2], hit.point, rot) as GameObject;
						WoodHole.transform.parent = hit.transform;
						Destroy(WoodHole, HitParticlesLifeTime);

					}
					if (hit.collider.tag == "Sand") {
						
						var GlassHole = Instantiate (_hitParticles [3], hit.point, rot) as GameObject;
						GlassHole.transform.parent = hit.transform;
						Destroy(GlassHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Water") {
						
						var WaterHole = Instantiate (_hitParticles [4], hit.point, rot) as GameObject;
						WaterHole.transform.parent = hit.transform;
						Destroy(WaterHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Blood") {
						
						var BloodHole = Instantiate (_hitParticles [5], hit.point, rot) as GameObject;
						BloodHole.transform.parent = hit.transform;
						Destroy(BloodHole, HitParticlesLifeTime);
					}
					if (hit.collider.tag == "Ground") {
						
						var GroundHole = Instantiate (_hitParticles [6], hit.point, rot) as GameObject;
						GroundHole.transform.parent = hit.transform;
						Destroy(GroundHole, HitParticlesLifeTime);
					}
					
					if (hit.rigidbody)
						
						hit.rigidbody.AddForceAtPosition (fwd * Power, hit.point);  
						}
			}	

				
		} else {


			if (MuzzleFlash && Time.time > nextFire) {

				MuzzleFlash.SetActive (false);
				m_Animator.speed = 0.91f;


						}
				}


		if (Shoot && WAnimation == "Throw") {
			m_Animator.SetBool ("PreThrow", true);	
			PowerThrow = PowerThrow + 20; 
			if (PowerThrow > 800)
				PowerThrow = 800;
		}

		if (ThrowRelease && WAnimation == "Throw" && Time.time > nextFire && Bullets > 0) {
			m_Animator.SetBool ("Throw", true);
			m_Animator.SetBool ("PreThrow", false);	
			Rigidbody GranadeInstance;
			GranadeInstance = Instantiate(RigidBodyPrefab, RigidSpawnTarget.position, RigidSpawnTarget.rotation) as Rigidbody;
			GranadeInstance.AddForce(RigidSpawnTarget.forward * PowerThrow);
			PowerThrow = 0;
			nextFire = Time.time + FireRate;
			Bullets = Bullets -1;

		}
		else
		{
			
			m_Animator.SetBool("Throw" , false);                
		}
		

		
		if(Shoot && Aim && WAnimation == "Bazooka" && Time.time > nextFire && Bullets > 0)
		{
			nextFire = Time.time + FireRate;
			Bullets = Bullets -1;

			Rigidbody rocketInstance;
			rocketInstance = Instantiate(RigidBodyPrefab, RigidSpawnTarget.position, RigidSpawnTarget.rotation) as Rigidbody;
			rocketInstance.AddForce(RigidSpawnTarget.forward * 5000);

		}
		if ( Input.GetButtonDown("Fire1") && Bullets == 0 && WAnimation == "Equip"){
			GetComponent<AudioSource>().PlayOneShot(WeaponEmpty);
		}
	}



	void LateUpdate () 
	{
				if (WeapObj) {
						if (WeapObj.tag == "on") {
								if (Input.GetButton ("Fire2") && WAnimation != "Bazooka" && !Reloading) {
				
										m_Animator.SetBool (PreFireAnim, true);

				
								} else {
										if (WAnimation != "Bazooka")
												m_Animator.SetBool (PreFireAnim, false);
										m_Animator.SetBool (WAnimOld, false);
										m_Animator.SetBool (WAnimation, true);
										
								}
						}


						if (WeapObj.tag == "off") {
								leftHandle = null;
								rightHandle = null;
								m_Animator.SetBool (WAnimation, false);	
						}

						m_Animator.SetLayerWeight (2, 1);

		if (WeapObj != null) {
		   
				GuiIsActive = true;		
						
			} 
			else {
				GuiIsActive = false;
			}

			}
		}


	void OnGUI(){

		GUI.skin = CustomSkin;

		if (GuiIsActive)
		GUI.Box (new Rect (Screen.width - 100,0,100,50), Bullets + " / " + Magazine);

	}
	
	void OnAnimatorIK(int layerIndex)
	{
		if(!enabled) return;
		
		if (layerIndex == 2) 
		{
			float ikWeight =  m_Animator.GetCurrentAnimatorStateInfo(2).IsName(WIk) ? 1 : 0;
			
			if (leftHandle != null)
			{
				m_Animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandle.transform.position);
				m_Animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandle.transform.rotation);
				m_Animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, ikWeight);
				m_Animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, ikWeight);
			}
			
			if (rightHandle != null)
			{
				m_Animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandle.transform.position);
				m_Animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandle.transform.rotation);
				m_Animator.SetIKPositionWeight(AvatarIKGoal.RightHand, ikWeight);
				m_Animator.SetIKRotationWeight(AvatarIKGoal.RightHand, ikWeight);
			}
		}
	}
}
