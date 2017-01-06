using UnityEngine;
using System.Collections;

public class FlickerLight : MonoBehaviour {

	[SerializeField] float amt;
	[SerializeField] float freq;
	float t;
	[SerializeField] float intens;
	float orig;
	bool on;

	// Use this for initialization
	void Start () {
		t = freq;
		orig = GetComponent<Light>().intensity;
	}
	
	// Update is called once per frame
	void Update () {
		if(t > 0){
			t -= Time.deltaTime;
			if(t <= 0){
				if(on){
					GetComponent<Light>().intensity = orig;
				}else{
					GetComponent<Light>().intensity = intens;
				}
				on = !on;
				t = freq;
			}
		}
	}
}
