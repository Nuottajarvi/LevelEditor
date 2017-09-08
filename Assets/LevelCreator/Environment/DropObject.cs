using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropObject : MonoBehaviour {

	private ChunkMap chunkMap;
	
	//Sliders
	public int x;
	public int y;
	public int z;

	public int rotation;

	GameObject guidance;

	EventSystem eventSys;

	private List<List<Vector3>> objectsPutThisClick;
	
	void Start () {
		eventSys = EventSystem.current;
		chunkMap = GameObject.Find("EnviromentController").GetComponent<ChunkMap>();
		objectsPutThisClick = new List<List<Vector3>>();
		rotation = 0;
		guidance = GameObject.Find ("EnviromentGuider");
	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.Z)){
			if(objectsPutThisClick.Count > 0){
				for(int i = 0; i < objectsPutThisClick[0].Count; i++){
					Vector3 v = objectsPutThisClick[0][i];
					chunkMap.blockGrid[(int)v.x, (int)v.y, (int)v.z] = new Block(0,0,0);
				}
				objectsPutThisClick.RemoveAt(0);
				ChunkRenderer.renderAll();
			}
			return;
		}

		if(Input.GetKeyDown(KeyCode.R)){

			if(rotation == 3){
				rotation = 0;
			}else{
				rotation++;
			}
		}

		if (eventSys.IsPointerOverGameObject()){
			return;
		}

		ShowGuider();

		if(Input.GetMouseButtonDown(0)){
			if(objectsPutThisClick.Count >= 10)
			objectsPutThisClick.RemoveAt(9);

			objectsPutThisClick.Insert(0, new List<Vector3>());
		}

		if(Input.GetMouseButtonUp(0)){
			ChunkRenderer.renderAll();
		}

		if (Input.GetMouseButton(0)){
			Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit floorHit;
			float camRayLength = 100f;
			if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor"))) {
				Vector3 a = floorHit.point;
				Vector3 orig = getLocationOfOriginalBlock(a);

				//Bugfix for dragging from UI to game.
				if(objectsPutThisClick == null || objectsPutThisClick.Count == 0){
					return;
				}

				//If clicked smoothedge
				if(blockIsInsideSmoothBounds(orig)){
					if(chunkMap.blockGrid[(int)orig.x, (int)orig.y, (int)orig.z].shape == ObjectController.SMOOTHBORDER){
						a += Vector3.down;
						orig += Vector3.down;
					}
				}

				if(!objectsPutThisClick[0].Contains(new Vector3(orig.x, orig.y, orig.z))){

					a = getLocationOfNewBlock(a);

					ObjectController oc = ObjectController.getInstance();
					
					for(int i = 0; i < x; i++){
						for(int j = 0; j < y; j++){
							for(int k = 0; k < z; k++){

								Vector3 b = new Vector3();
								
								b.x = (int)(a.x) + i;
								b.y = a.y + k;
								b.z = (int)(a.z) + j;

								if(oc.shape != ObjectController.SMOOTH){
									if(blockIsInsideBounds(b)){
										chunkMap.blockGrid[(int)b.x, (int)b.y, (int)b.z] = new Block(oc.shape, oc.texture + 1, rotation);
										objectsPutThisClick[0].Add(new Vector3((int)b.x, (int)b.y, (int)b.z));
									}
								}else{
									if(blockIsInsideSmoothBounds(b)){
										chunkMap.blockGrid[(int)b.x, (int)b.y, (int)b.z] = new Block(oc.shape, oc.texture + 1, rotation);
										objectsPutThisClick[0].Add(new Vector3((int)b.x, (int)b.y, (int)b.z));
									}
								}
							}
						}
					}
					if(objectsPutThisClick.Count != 0 && objectsPutThisClick[0].Count != 0)
						ChunkRenderer.renderTemporarily(objectsPutThisClick[0]);
				}

			}
		}

		if(Input.GetMouseButtonDown(1)){
			Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit floorHit;
			float camRayLength = 200f;

			if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor"))) {
				Vector3 a = floorHit.point;

				a = getLocationOfOriginalBlock(a);

				for(int i = 0; i < x; i++){
					for(int j = 0; j < y; j++){
						for(int k = 0; k < z; k++){		

							Vector3 b = new Vector3();

							b.x = (int)(a.x) + i;
							b.y = a.y - k;
							b.z = (int)(a.z) + j;
							
							if(blockIsInsideBounds(b)){
								chunkMap.blockGrid[(int)b.x, (int)b.y, (int)b.z] = new Block(0,0,0);
							}
						}
					}
				}
				ChunkRenderer.renderAll();
			}
		}
	}

	void ShowGuider(){

		if(Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)){
			guidance.GetComponent<MeshRenderer>().enabled = false;
		}

		if(Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)){
			guidance.GetComponent<MeshRenderer>().enabled = true;
		}


		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		float camRayLength = 200f;
		if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor"))) {
			Vector3 a = floorHit.point;
			Vector3 orig = getLocationOfOriginalBlock(a);
			
			//If clicked smoothedge
			if(blockIsInsideSmoothBounds(orig)){
				if(chunkMap.blockGrid[(int)orig.x, (int)orig.y, (int)orig.z].shape == ObjectController.SMOOTHBORDER){
					a += Vector3.down;
					orig += Vector3.down;
				}
			}

			a = getLocationOfNewBlock(a);
							
			Vector3 b = new Vector3();
			
			b.x = a.x + x/2f;
			b.y = a.y + z/2f;
			b.z = a.z + y/2f;

			if(blockIsInsideBounds(b)){
				guidance.transform.position = b;
				Vector3 scaling = new Vector3(x+0.02f,z+0.02f,y+0.02f);
				guidance.transform.localScale = scaling;
			}
					
	
		}
	}

	bool blockIsInsideBounds(Vector3 b){
		return (b.x >= 0 && b.x < ChunkMap.SizeX &&
		        b.y >= 0 && b.y < ChunkMap.SizeY &&
		        b.z >= 0 && b.z < ChunkMap.SizeZ);
	}

	bool blockIsInsideSmoothBounds(Vector3 b){
		return (b.x > 0 && b.x < ChunkMap.SizeX - 1 &&
		        b.y >= 0 && b.y < ChunkMap.SizeY &&
		        b.z > 0 && b.z < ChunkMap.SizeZ - 1);
	}

	Vector3 getLocationOfNewBlock(Vector3 a){
		a -= new Vector3(0.5f, 0.5f, 0.5f);

		Vector3 cameraPos = Camera.main.transform.position - a;

		cameraPos.x /= Math.Abs(cameraPos.x);
		cameraPos.y /= Math.Abs(cameraPos.y);
		cameraPos.z /= Math.Abs(cameraPos.z);
		a += cameraPos / 1000;

		a.x = Mathf.RoundToInt(a.x);
		a.y = Mathf.RoundToInt(a.y);
		a.z = Mathf.RoundToInt(a.z);
		return a;
	}

	Vector3 getLocationOfOriginalBlock(Vector3 a){
		a -= new Vector3(0.5f, 0.5f, 0.5f);
		
		Vector3 cameraPos = Camera.main.transform.position - a;
		
		cameraPos.x /= Math.Abs(cameraPos.x);
		cameraPos.y /= Math.Abs(cameraPos.y);
		cameraPos.z /= Math.Abs(cameraPos.z);
		a -= cameraPos / 1000;
		
		a.x = Mathf.RoundToInt(a.x);
		a.y = Mathf.RoundToInt(a.y);
		a.z = Mathf.RoundToInt(a.z);
		return a;
	}
}
