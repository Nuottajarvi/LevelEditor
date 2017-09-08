using UnityEngine;
using System.Collections;

public class DetailsController : MonoBehaviour {

	public GameObject[] prefabs = new GameObject[10];

	public bool positionLocked;
	public bool rotationLocked;

	public int currentPrefab;

	public void changePrefab(int prefab){
		currentPrefab = prefab;
	}

	public GameObject getPrefab(){
		return prefabs[currentPrefab];
	}

	public GameObject getPrefab(int i){
		return prefabs[i];
	}

	public void lockPosition(bool state){
		positionLocked = state;
	}

	public void lockRotation(bool state){
		rotationLocked = state;
	}
	
}
