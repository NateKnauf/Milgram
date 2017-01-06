using UnityEngine;
using System.Collections;

public class Button : MonoBehaviour {

	public bool clicked;

	[SerializeField] Vector3 downPos;
	Vector3 originPos;

	// Use this for initialization
	void Start () {
		originPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if(clicked){
			transform.position = downPos;
		}else{
			transform.position = originPos;
		}
	}
}
