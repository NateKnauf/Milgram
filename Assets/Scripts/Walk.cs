using UnityEngine;
using UnityEngine.VR;
using System.Collections;
using VRStandardAssets;

[RequireComponent (typeof (Rigidbody))]
[RequireComponent (typeof (CapsuleCollider))]

public class Walk : MonoBehaviour {
	
	public float speed = 10.0f;
	public float gravity = 10.0f;
	public float maxVelocityChange = 10.0f;
	public bool canJump = false;
	public float jumpHeight = 2.0f;
	private bool grounded = false;

	public GameObject fader;
	public GameObject cam;

	public bool walkable = true;
	
	float timer;
	
	void Awake () {
		GetComponent<Rigidbody>().freezeRotation = true;
		GetComponent<Rigidbody>().useGravity = false;
	}

	void Update(){
		if(Input.GetMouseButtonDown(0)){
			RaycastHit hit;
			Debug.DrawRay(transform.position, transform.forward);
			if(Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 3f)){
				Debug.Log(hit.collider.name);
				if (hit.collider.tag != null) {
					if (hit.collider.tag == "Chair") {
						GetComponent<Sit> ().SitDown ();
					} else if (hit.collider.tag == "Door" && GetComponent<Sit> ().canLeave) {
						fader.GetComponent<SceneFade> ().EndScene ();
						timer = 3.5f;
						Camera.main.GetComponent<ControllerMisc> ().DoEndings (0);
					} else if (hit.collider.tag == "Secret Wall") {
						hit.collider.GetComponent<SecretWall> ().InsertZeldaNoiseHere ();
					}
				}
			}
		}

		if(timer > 0){
			timer -= Time.deltaTime;
			if(timer <= 0){
				Application.LoadLevel(0);
			}
		}
	}

	void FixedUpdate () {
		if (grounded && walkable) {
			// Calculate how fast we should be moving
			Vector3 targetVelocity = Quaternion.Euler(0, cam.transform.localEulerAngles.y, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
			targetVelocity = transform.TransformDirection(targetVelocity);
			targetVelocity *= speed;
			
			// Apply a force that attempts to reach our target velocity
			Vector3 velocity = GetComponent<Rigidbody>().velocity;
			Vector3 velocityChange = (targetVelocity - velocity);
			velocityChange.x = Mathf.Clamp(velocityChange.x, -maxVelocityChange, maxVelocityChange);
			velocityChange.z = Mathf.Clamp(velocityChange.z, -maxVelocityChange, maxVelocityChange);
			velocityChange.y = 0;
			GetComponent<Rigidbody>().AddForce(velocityChange, ForceMode.VelocityChange);
			
			// Jump
			if (canJump && Input.GetButton("Jump")) {
				GetComponent<Rigidbody>().velocity = new Vector3(velocity.x, CalculateJumpVerticalSpeed(), velocity.z);
			}
		}

		if (Input.GetKeyDown(KeyCode.E)) {
			transform.rotation = transform.rotation * Quaternion.Euler (new Vector3 (0, 30, 0));
		}
		if (Input.GetKeyDown(KeyCode.Q)) {
			transform.rotation = transform.rotation * Quaternion.Euler (new Vector3 (0, -30, 0));
		}

		if (Input.GetKeyDown (KeyCode.R)) {
			InputTracking.Recenter ();
		}
		
		// We apply gravity manually for more tuning control
		GetComponent<Rigidbody>().AddForce(new Vector3 (0, -gravity * GetComponent<Rigidbody>().mass, 0));
		
		grounded = false;
	}
	
	void OnCollisionStay () {
		grounded = true;    
	}
	
	float CalculateJumpVerticalSpeed () {
		// From the jump height and gravity we deduce the upwards speed 
		// for the character to reach at the apex.
		return Mathf.Sqrt(2 * jumpHeight * gravity);
	}
}
