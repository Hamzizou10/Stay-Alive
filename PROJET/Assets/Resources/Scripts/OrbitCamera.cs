
using UnityEngine;
using System.Collections;


public class OrbitCamera : MonoBehaviour
{
	public Texture2D crosshairTexture;
	public float crosshairScale = 0.47f;

	public Transform _target;
	public Transform targetShoot;
	public Transform targetOrbit;
	
	public float _distance = 3.0f;
	

	public float _xSpeed = 3f;
	public float _ySpeed = 3f;
	public float _MinY = 90f;
	public float _MaxY = 90f;
	
	private float _x = 0.0f;
	private float _y = 0.0f;
	
	private Vector3 _distanceVector;


	void Start ()
	{
		_distanceVector = new Vector3(0.0f,0.0f,-_distance);
		
		Vector2 angles = this.transform.localEulerAngles;
		_x = angles.x;
		_y = angles.y;
		
		this.Rotate(_x, _y);
		
	}
	void Update (){

		bool ShCam = Input.GetButton ("Fire2");

		if (ShCam) {
			_target = targetShoot;
			GetComponent<Camera>().fieldOfView = 20;
		
		} else {
			_target = targetOrbit;
			GetComponent<Camera>().fieldOfView = 60;
		}
	}
	
	
	void LateUpdate()
	{
		if ( _target )
		{
			this.RotateControls();
		}
	}

	void OnGUI()
	{
		if(Time.timeScale != 0 && _target == targetShoot)
		{
			if(crosshairTexture!=null )
				GUI.DrawTexture(new Rect((Screen.width-crosshairTexture.width*crosshairScale)/2 ,(Screen.height-crosshairTexture.height*crosshairScale)/2, crosshairTexture.width*crosshairScale, crosshairTexture.height*crosshairScale),crosshairTexture);
		}
	}

	void RotateControls()
	{
		
		_x += Input.GetAxis("Mouse X") * _xSpeed;
		_y += -Input.GetAxis("Mouse Y")* _ySpeed;
		
		this.Rotate(_x,_y);

		if (_y > _MaxY) {
			_y = _MaxY;		
		}
		if (_y < _MinY) {
			_y = _MinY;		
		}
				
		
		
	}
	

	void Rotate( float x, float y )
	{
		Quaternion rotation = Quaternion.Euler(y,x,0.0f);

		Vector3 position = rotation * _distanceVector + _target.position;
		
	
		transform.rotation = rotation;
		transform.position = position;
	}
	

	
} 

