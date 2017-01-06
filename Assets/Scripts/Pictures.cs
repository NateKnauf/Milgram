using UnityEngine;
using System.Collections;

public class Pictures : MonoBehaviour {

	[SerializeField] Material[] pics;
	[SerializeField] float time;
	float t;

	// Use this for initialization
	void Start () {
		t = time;
	}
	
	// Update is called once per frame
	void Update () {
		if(t > 0){
			t -= Time.deltaTime;
			if(t <= 0){
				CyclePicture();
			}
		}
	}

	void CyclePicture(){
		gameObject.GetComponent<Renderer>().material = pics[Random.Range(0, pics.Length - 1)];
		t = time;
	}
}
