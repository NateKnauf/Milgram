using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class Lines : MonoBehaviour {

	[SerializeField] AudioClip[] l;
	[SerializeField] AudioClip[] n;
	[SerializeField] AudioClip[] f;
	[SerializeField] AudioClip slam;
	[SerializeField] GameObject[] particles;

	AudioSource s;
	Sit st;
	ControllerMisc c;

	public int sequencer = 0;
	float smokeTimer;
	float fadeTimer;
	float teleportTimer;
	float fadeUpTimer;
	float lastLineTimer;
	float killTimer;

	// Use this for initialization
	void Start () {
		s = GetComponent<AudioSource>();
		st = FindObjectOfType<Sit>();
		c = FindObjectOfType<ControllerMisc>();

		s.PlayOneShot(l[0]);
		s.PlayOneShot(slam);
	}

	void Update(){
		if(sequencer == 12 && st.sitting){
			st.GetUp();
		}

		if(smokeTimer > 0){
			smokeTimer -= Time.deltaTime;
			if(smokeTimer <= 0){
				foreach(GameObject p in particles){
					p.SetActive(true);
				}

			}
		}

		if(fadeTimer > 0){
			fadeTimer -= Time.deltaTime;
			if(fadeTimer <= 0){
				st.fader.GetComponent<SceneFade>().EndScene();
			}
		}

		if(teleportTimer > 0){
			teleportTimer -= Time.deltaTime;
			if(teleportTimer <= 0){
				st.GoToElectricRoom();
			}
		}

		if(fadeUpTimer > 0){
			fadeUpTimer -= Time.deltaTime;
			if(fadeUpTimer <= 0){
				st.fader.GetComponent<SceneFade>().sceneStarting = true;
			}
		}

		if(lastLineTimer > 0){
			lastLineTimer -= Time.deltaTime;
			if(lastLineTimer <= 0){
				s.PlayOneShot(l[l.Length - 1]);
			}
		}

		if(killTimer > 0){
			killTimer -= Time.deltaTime;
			if(killTimer <= 0){
				st.Electrocute();
			}
		}

		if(sequencer == 0 && st.sitting){
			sequencer++;
			c.buttonPresses = 0;
			NextLine(sequencer, false, false);
		}else if(sequencer == 1 && c.buttonPresses >= 10){
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 2 && c.redPressed){
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 2 && c.greenPressed){
			NextLine(sequencer, true, false);
		}else if(sequencer == 3 && c.greenPressed){
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 3 && c.redPressed){
			NextLine(sequencer, true, false);
		}else if(sequencer == 3 && c.greenPressed){
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 4 && c.greenPressed){
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 4 && c.redPressed){ 
			NextLine(sequencer, true, false);
		}else if(sequencer == 5 && c.redPressed){//First scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 5 && c.greenPressed){//First scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 6 && c.redPressed){//Second scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 6 && c.greenPressed){//Second scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 7 && c.redPressed){//Third scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 7 && c.greenPressed){//Third scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 8 && c.redPressed){//Fourth scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 8 && c.greenPressed){//Fourth scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 9 && c.redPressed){//Fifth scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 9 && c.greenPressed){//Fifth scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 10 && c.redPressed){//Sixth scream
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 10 && c.greenPressed){//Sixth scream (failure)
			NextLine(sequencer, false, true);
		}else if(sequencer == 11 && c.redPressed){//Last
			sequencer++;
			NextLine(sequencer, false, false);
		}else if(sequencer == 11 && c.greenPressed){//Last (failure)
			NextLine(sequencer, false, true);
		}
	}

	void NextLine(int sequence, bool negative, bool failure){
		Debug.Log (sequencer);
		if(!negative && !failure){
			s.PlayOneShot(l[sequence]);
		}else if(negative && !failure){
			s.PlayOneShot(n[Random.Range(0, n.Length - 1)]);
		}else if(!negative && failure){
			if(c.failures == f.Length - 1){
				s.PlayOneShot(f[c.failures]);
				smokeTimer = 8.7f;
				fadeTimer = 14f;
				teleportTimer = 16f;
				fadeUpTimer = 20f;
				lastLineTimer = 28f;
				killTimer = 31f;
				Debug.Log("Knocking the player out and teleporting!");
			}else{
				s.PlayOneShot(f[c.failures]);
				c.failures++;
			}
		}
	}
}
