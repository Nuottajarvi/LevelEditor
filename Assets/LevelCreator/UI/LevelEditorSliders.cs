using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LevelEditorSliders : MonoBehaviour {

	DropObject dropObject;
	Slider xSlider;
	Slider ySlider;
	Slider zSlider;

	DropTerraform dropTerraform;

	Text xText;
	Text yText;
	Text zText;

	int[] values = new int[]{1,2,3,4,5,6,8,10,12,16,25,32,64};

	void Start () {

		if(gameObject.name == "EnviromentSliderPanel"){
			dropObject = GameObject.Find("EnviromentController").GetComponent<DropObject>();
			xSlider = GameObject.Find ("Width").GetComponent<Slider>();
			xText = xSlider.transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
			ySlider = GameObject.Find ("Depth").GetComponent<Slider>();
			yText = ySlider.transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
			zSlider = GameObject.Find ("Height").GetComponent<Slider>();
			zText = zSlider.transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
		}else{
			dropTerraform = GameObject.Find ("TerraformController").GetComponent<DropTerraform>();
			xSlider = GameObject.Find ("Diameter").GetComponent<Slider>();
			xText = xSlider.transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
			ySlider = GameObject.Find ("Strength").GetComponent<Slider>();
			yText = ySlider.transform.Find("Handle Slide Area/Handle/Text").GetComponent<Text>();
		}
	}

	void Update () {

		int x = values[(int)xSlider.value];
		int y = values[(int)ySlider.value];
		int z = 0;
		if(zSlider != null)
			 z = values[(int)zSlider.value];

		if(gameObject.name == "EnviromentSliderPanel"){
			dropObject.x = x;
			dropObject.y = y;
			dropObject.z = z;
		}else{
			dropTerraform.diameter = x;
			dropTerraform.strength = y / 20.0f;
		}

		xText.text = string.Format("{0:00}", x);		
		yText.text = string.Format("{0:00}", y);
		if(zSlider != null)
			zText.text = string.Format("{0:00}", z);
	}
}
