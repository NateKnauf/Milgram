using UnityEngine;
using System.Collections;

public class StartGame : MonoBehaviour {

	[SerializeField] GameObject splash;
	[SerializeField] Material[] mats;

	void Start(){
		int endings = PlayerPrefs.GetInt("EndingsDiscovered");

		splash.GetComponent<Renderer>().material = mats[endings];
	}

	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)){
			Application.LoadLevel(1);
		}
	}
}
