using UnityEngine;
using System.Collections;


public class MouseLook : MonoBehaviour {

	public float sensitivityX = 15F;
	public float sensitivityY = 15F;

	public float minimumX = -360F;
	public float maximumX = 360F;

	public float minimumY = -60F;
	public float maximumY = 60F;

	public bool x = false;
	public bool restrictX = false;

	float rotationY = 0F;
	float rotationX = 0f;

	void Update ()
	{
		if(x){
			if(restrictX){
				rotationX += Input.GetAxis("Mouse X") * sensitivityX;
				rotationX = Mathf.Clamp (rotationX, minimumX, maximumX);
			}else
				rotationX += Input.GetAxis("Mouse X") * sensitivityX;
		}else{	
			rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
			rotationY = Mathf.Clamp (rotationY, minimumY, maximumY);
		}
			
		transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
	}
}