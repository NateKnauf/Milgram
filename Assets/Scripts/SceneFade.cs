using UnityEngine;
using System.Collections;

public class SceneFade : MonoBehaviour
{
	public float fadeSpeed = 1.5f; 

	public bool sceneStarting = true; 
	bool sceneEnding = false;
	
	
	void Awake (){
		GetComponent<GUITexture>().pixelInset = new Rect(0f, 0f, Screen.width*4, Screen.height*4);
	}
	
	
	void Update (){
		if(sceneStarting)
			StartScene();

		if(sceneEnding)
			EndScene();
	}
	
	
	void FadeToClear (){
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.clear, fadeSpeed * Time.deltaTime);
	}
	
	
	void FadeToBlack (){
		GetComponent<GUITexture>().color = Color.Lerp(GetComponent<GUITexture>().color, Color.black, fadeSpeed * Time.deltaTime);
	}
	
	
	void StartScene (){
		FadeToClear();

		if(GetComponent<GUITexture>().color.a <= 0.01f){
			GetComponent<GUITexture>().color = Color.clear;
			GetComponent<GUITexture>().enabled = false;
			sceneStarting = false;
		}
	}
	
	
	public void EndScene (){
		GetComponent<GUITexture>().enabled = true;
		sceneEnding = true;

		FadeToBlack();

		if(GetComponent<GUITexture>().color.a > 0.95f){
			GetComponent<GUITexture>().color = Color.black;

			sceneEnding = false;
		}
	}
}