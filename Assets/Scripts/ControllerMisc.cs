using UnityEngine;
using System.Collections;

public class ControllerMisc : MonoBehaviour {

	[SerializeField] AudioSource s;
	[SerializeField] AudioSource owen;
	[SerializeField] AudioSource zapper;

	public int buttonPresses;
	
	public int failures;

	public bool redPressed;
	public bool greenPressed;

	[SerializeField] GameObject red;
	[SerializeField] GameObject green;

	ClickButton b;
	Sit st;

	// Use this for initialization
	void Start () {
		b = Camera.main.GetComponent<ClickButton>();
		st = FindObjectOfType<Sit>();
	}
	
	// Update is called once per frame
	void Update () {
		CheckButtonPresses();

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

		if(s.isPlaying){
			b.canClick = false;
		}else{
			b.canClick = true;
		}

		if(s.isPlaying){
			st.canSit = false;
		}else{
			st.canSit = true;
		}

		if(Input.GetKey(KeyCode.Escape)){
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
		}
	}

	void CheckButtonPresses(){
		if(b.buttonActive != null){
			if(b.buttonActive == red){
				Debug.Log("It's Red!");
				buttonPresses++;
				redPressed = true;
				greenPressed = false;
				b.buttonActive = null;
			}else{
				Debug.Log("It's Green!");
				buttonPresses++;
				redPressed = false;
				greenPressed = true;
				b.buttonActive = null;
			}
		}else{
			redPressed = false;
			greenPressed = false;
		}
	}

	public void DoEndings(int endingToGive){
		if(endingToGive == 0){
			PlayerPrefs.SetInt("GoodEnding", 1);
		}else if(endingToGive == 1){
			PlayerPrefs.SetInt("BadEnding", 1);
		}else{
			PlayerPrefs.SetInt("WeirdEnding", 1);
		}

		PlayerPrefs.SetInt("EndingsDiscovered", PlayerPrefs.GetInt("GoodEnding") + PlayerPrefs.GetInt("BadEnding") + PlayerPrefs.GetInt("WeirdEnding"));
	}
}
