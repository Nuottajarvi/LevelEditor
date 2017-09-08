using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;

public class DropPrefab : MonoBehaviour {

	public Dictionary<GameObject, GameObjectNotation> details;

	DetailsController detailsController;
	EventSystem eventSys;

	float rotationKickInTime = 0.2f;
	float rotationKickInTimeLeft;

	GameObject plantedObject;
	public GameObject activeObject;
	GameObject currentDetail;

	void Start(){
		details = new Dictionary<GameObject, GameObjectNotation>();
		detailsController = GetComponent<DetailsController>();
		eventSys = EventSystem.current;
	}

	void Update(){
		if (eventSys.IsPointerOverGameObject()){
			return;
		}

		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		float camRayLength = 100f;

		if(Input.GetMouseButtonUp(0)){
			details.Add(plantedObject, new GameObjectNotation(detailsController.currentPrefab, plantedObject.transform.position, plantedObject.transform.rotation));
			plantedObject = null;
		}

		if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor", "Enviromental"))) {

			if(plantedObject != null){

				Vector3 objectPosition = plantedObject.transform.position;
				objectPosition.y = 0;

				Vector3 floorPoint = floorHit.point;
				floorPoint.y = 0;

				if(rotationKickInTimeLeft <= 0){
					float angle = Vector3.Angle(objectPosition - floorPoint, Vector3.forward);
					float directioner = floorHit.point.x - plantedObject.transform.position.x;
					int direction = -(int)(directioner / Mathf.Abs(directioner));

					if(detailsController.rotationLocked){
						angle = ((int)(angle + 45) / 90) * 90;
					}

					plantedObject.transform.rotation = Quaternion.Euler (270,0,angle * direction + 180);
				}
				rotationKickInTimeLeft-=Time.deltaTime;
			}

			Vector3 a = floorHit.point;
			GameObject detail = detailsController.getPrefab();
			
			if(detailsController.positionLocked){
				a = new Vector3(0.5f + (int)a.x, (int)a.y, 0.5f + (int)a.z);
			}

			if(currentDetail != detail){
				GameObject.Destroy(activeObject);
				activeObject = null;
			}
			if(!Input.GetMouseButton(0)){
				if(activeObject == null){
					activeObject = (GameObject)GameObject.Instantiate(detail, a, Quaternion.Euler (270,0,180));
					currentDetail = detail;
				}else{
					activeObject.transform.position = a;
				}
			}

			if (Input.GetMouseButtonDown(0)){
				plantedObject = activeObject;
				plantedObject.layer = LayerMask.NameToLayer("Enviromental");
				activeObject = null;
				rotationKickInTimeLeft = rotationKickInTime;
			}
		}

		if (Input.GetMouseButtonDown(1)){
			RaycastHit objectHit;
			if (Physics.Raycast (cameraRay, out objectHit, camRayLength, LayerMask.GetMask("Enviromental"))){
				GameObject clickedObject = objectHit.transform.gameObject;
				details.Remove(clickedObject);
				GameObject.Destroy(clickedObject);
			}
		}
	}

	public void Instantiate(int prefabIndex, Vector3 position, Quaternion rotation){
		GameObject detail = (GameObject)GameObject.Instantiate(detailsController.getPrefab(prefabIndex), position, rotation);
		details.Add(detail, new GameObjectNotation(prefabIndex, position, rotation));
	}
}
