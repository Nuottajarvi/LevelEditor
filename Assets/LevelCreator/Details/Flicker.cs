using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {

	Color currentColor;
	float currentIntensity;

	Material material;


	void Start () {
		currentColor = new Color(0f,0f,0f);
		material = GetComponent<Renderer>().material;
	}

	void Update () {

	
		float intensity = 2*Mathf.PerlinNoise(88 + Time.time, 89 + Time.deltaTime);
		GetComponent<Light>().intensity = intensity;
		currentColor = new Color(0.3f,0.3f,0f) * intensity;
		material.SetColor("_EmissionColor", currentColor);

	}
}
