using UnityEngine;
using System.Collections;

public class Destroyer : MonoBehaviour {

	float t;
	[SerializeField] float timer;

	void Start(){
		t = timer;
	}

	// Update is called once per frame
	void Update () {
		if(t > 0){
			t -= Time.deltaTime;
			if(t <= 0){
				Destroy (gameObject);
			}
		}
	}
}
