using UnityEngine;
using System.Collections;

public class CENA : MonoBehaviour {

	GameObject player;
	bool doneCena = false;
	Animator anim;

	[SerializeField] AudioClip speech;
	[SerializeField] GameObject cenaObj;
	[SerializeField] GameObject fader;
	float timer;
	float timer2;
	float timer3;

	// Use this for initialization
	void Start () {
		player = GameObject.Find("_Player");
		anim = cenaObj.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		if(player.transform.position.z > 12f && !doneCena){
			DoCena ();
		}

		if(timer > 0){
			timer -= Time.deltaTime;
			if(timer <= 0){
				anim.SetTrigger("SPIN");
				//Camera.main.GetComponent<CameraShake>().shakeAmount = 0.1f;
				//Camera.main.GetComponent<CameraShake>().shake = 25f;
			}
		}

		if(timer2 > 0){
			timer2 -= Time.deltaTime;
			if(timer2 <= 0){
				fader.GetComponent<SceneFade>().EndScene();
				Camera.main.GetComponent<ControllerMisc>().DoEndings(2);
			}
		}

		if(timer3 > 0){
			timer3 -= Time.deltaTime;
			if(timer3 <= 0){
				Application.LoadLevel(0);
			}
		}
	}

	void DoCena(){
		doneCena = true;
		player.GetComponent<Walk>().walkable = false;
		player.GetComponent<Rigidbody>().velocity = Vector3.zero;
		GetComponent<AudioSource>().PlayOneShot(speech);
		GameObject.Find("INTERCOM").GetComponent<AudioSource>().Stop();
		timer = 71.6f;
		timer2 = 89f;
		timer3 = 93f;
	}
}
