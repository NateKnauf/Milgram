using UnityEngine;
using System.Collections;

public class SecretWall : MonoBehaviour {

	[SerializeField] GameObject device;
	Animator anim;

	// Use this for initialization
	void Start () {
		anim = device.GetComponent<Animator>();
	}

	public void InsertZeldaNoiseHere(){
		Debug.Log ("Hit");
		anim.SetTrigger("Reveal");
	}
}
