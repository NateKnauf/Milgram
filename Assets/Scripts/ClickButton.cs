using UnityEngine;
using System.Collections;

public class ClickButton : MonoBehaviour {

	[HideInInspector] public GameObject buttonActive;
	[SerializeField] AudioClip click;

	[SerializeField] GameObject red;
	[SerializeField] GameObject green;
	public bool canClick;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if(canClick){
			if(Input.GetMouseButtonDown(0)){
				RaycastHit hit;
				Debug.DrawRay(transform.position, transform.forward);
				if(Physics.Raycast(transform.position, transform.forward, out hit, 3f)){
					if (hit.collider.tag != null) {
						if (hit.collider.tag == "Button") {
							buttonActive = hit.collider.gameObject;
							Camera.main.GetComponent<AudioSource> ().PlayOneShot (click);
							buttonActive.GetComponent<Button> ().clicked = true;
						}
					}
				}
			}
		}

		if(Input.GetMouseButtonUp(0)){
			red.GetComponent<Button>().clicked = false;
			green.GetComponent<Button>().clicked = false;
		}
	}
}
