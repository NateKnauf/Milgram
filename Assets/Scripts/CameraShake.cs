using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{

	public float shake = 0f;

	public float shakeAmount = 0.25f;
	public float decreaseFactor = 1.0f;
	
	Vector3 originalPos;
	
	void OnEnable()
	{
		originalPos = transform.localPosition;
	}
	
	void Update()
	{
		if (shake > 0)
		{
			transform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;
			
			shake -= Time.deltaTime * decreaseFactor;
		}
		else
		{
			shake = 0f;
			transform.localPosition = originalPos;
		}
	}
}