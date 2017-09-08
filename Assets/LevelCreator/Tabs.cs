using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tabs : MonoBehaviour {

	List<GameObject>[] panels;
	GameObject[] controllers;
	GameObject[] guiders;

	int currentTab;

	void Start(){
		panels = new List<GameObject>[6];
		panels[0] = new List<GameObject>();
		panels[0].Add (GameObject.Find ("EnviromentPanel"));
		panels[0].Add (GameObject.Find ("EnviromentSliderPanel"));

		panels[1] = new List<GameObject>();
		panels[1].Add (GameObject.Find ("TerraformPanel"));
		panels[1].Add (GameObject.Find ("TerraformSliderPanel"));

		panels[2] = new List<GameObject>();
		panels[2].Add (GameObject.Find ("DetailsPanel"));
		panels[2].Add (GameObject.Find ("LockRotPosPanel"));

		controllers = new GameObject[6];
		controllers[0] = GameObject.Find ("EnviromentController");
		controllers[1] = GameObject.Find ("TerraformController");
		controllers[2] = GameObject.Find ("DetailsController");

		guiders = new GameObject[6];
		guiders[0] = GameObject.Find ("EnviromentGuider");
		guiders[1] = GameObject.Find ("TerraformGuider");

		changeTab(0);
	}

	public void changeTab(int tab){

		guiders[2] = controllers[2].GetComponent<DropPrefab>().activeObject;
		currentTab = tab;

		for(int i = 0; i < 3/*panels.Length*/; i++){
			if(tab == i){
				foreach(GameObject panel in panels[i]){
					panel.SetActive(true);
				}
				controllers[i].SetActive(true);
				if(guiders[i] != null)
					guiders[i].SetActive(true);
			}else{
				foreach(GameObject panel in panels[i]){
					panel.SetActive(false);
				}
				controllers[i].SetActive(false);
				if(guiders[i] != null)
					guiders[i].SetActive(false);
			}
		}
	}

	public void activateAll(){
		for(int i = 0; i < 3/*panels.Length*/; i++){
			foreach(GameObject panel in panels[i]){
				panel.SetActive(true);
			}
			controllers[i].SetActive(true);
			if(guiders[i] != null)
				guiders[i].SetActive(true);
		}
	}

	public void returnState(){
		changeTab(currentTab);
	}
}
