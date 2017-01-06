using UnityEngine;
using System.Collections;

public class Sit : MonoBehaviour {

	public bool sitting;
	public bool canLeave = false;
	[SerializeField] Vector3 sittingPos;
	[SerializeField] Vector3 standingPos;
	[SerializeField] Vector3 sittingElectricPos;

	[SerializeField] GameObject particles;
	[SerializeField] Vector3 deadPos;

	public GameObject chair;
	public GameObject fader;
	float deadTimer;
	float endTimer;
	float reallyEndTimer;

	public bool canSit = true;
	
	// Update is called once per frame
	void Update () {

		if(deadTimer > 0){
			deadTimer -= Time.deltaTime;
			if(deadTimer <= 0){
				//Camera.main.transform.localRotation = Quaternion.Euler(deadPos);
				endTimer = 2f;
			}
		}

		if(endTimer > 0){
			endTimer -= Time.deltaTime;
			if(endTimer <= 0){
				fader.GetComponent<SceneFade>().EndScene();
				reallyEndTimer = 1.5f;
			}
		}

		if(reallyEndTimer > 0){
			reallyEndTimer -= Time.deltaTime;
			if(reallyEndTimer <= 0){
				Application.LoadLevel(0);
			}
		}

	}

	public void SitDown(){
		if(canSit){
			sitting = true;
			chair.GetComponent<BoxCollider> ().enabled = false;
			Camera.main.GetComponent<ClickButton>().enabled = true;
			GetComponent<Walk>().walkable = false;
			GetComponent<Rigidbody>().isKinematic = true;
			GetComponent<Rigidbody>().detectCollisions = false;
			transform.position = sittingPos;
			transform.rotation = Quaternion.identity;
		}
	}

	public void GetUp(){
		if(canSit){
			sitting = false;
			canLeave = true;
			Camera.main.GetComponent<ClickButton>().enabled = false;
			GetComponent<Rigidbody>().isKinematic = false;
			GetComponent<Rigidbody>().detectCollisions = true;
			GetComponent<Walk>().walkable = true;

			transform.position = standingPos;
		}
	}

	public void GoToElectricRoom(){
		sitting = true;
		Camera.main.GetComponent<ClickButton>().enabled = false;
		GetComponent<Walk>().enabled = false;
		GetComponent<Rigidbody>().isKinematic = true;
		GetComponent<Rigidbody>().detectCollisions = false;
		//GetComponent<MouseLook>().minimumX = -60;
		//GetComponent<MouseLook>().maximumX = 60;
		//GetComponent<MouseLook>().restrictX = true;
		transform.position = sittingElectricPos;
	}

	public void Electrocute(){
		particles.SetActive(true);
		GetComponent<MouseLook>().enabled = false;
		//Camera.main.GetComponent<MouseLook>().enabled = false;
		//Camera.main.GetComponent<CameraShake>().shake = 2.4f;
		deadTimer = 2.5f;
		Camera.main.GetComponent<ControllerMisc>().DoEndings(1);
	}
}
