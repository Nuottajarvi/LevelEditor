using UnityEngine;
using System.Collections;
using System;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DropTerraform : MonoBehaviour {

	public Dictionary<Vector2, float> terraforms;
	
	//Sliders
	public int diameter = 5;
	public float strength = 0.1f;
	public int seed;

	EventSystem eventSys;

	public Texture circle;
	public Texture square;

	GameObject guidance;

	private List<List<KeyValuePair<Vector2, float>>> terraformPutThisClick;
	
	void Start () {
		eventSys = EventSystem.current;
		terraforms = new Dictionary<Vector2, float>();
		terraformPutThisClick = new List<List<KeyValuePair<Vector2, float>>>();
		TerraformRenderer.RenderAll(terraforms);
		seed = new System.Random().Next(9000) + 1000;
		guidance = GameObject.Find ("TerraformGuider");
	}

	void Update () {

		if(Input.GetKeyDown(KeyCode.Z)){
			if(terraformPutThisClick.Count > 0){
				for(int i = 0; i < terraformPutThisClick[0].Count; i++){

				}
				terraformPutThisClick.RemoveAt(0);
				TerraformRenderer.RenderAll(terraforms);
			}
			return;
		}

		if (eventSys.IsPointerOverGameObject()){
			return;
		}

		if(Input.GetMouseButtonDown(0)){
			if(terraformPutThisClick.Count >= 10)
			terraformPutThisClick.RemoveAt(9);

			terraformPutThisClick.Insert(0, new List<KeyValuePair<Vector2, float>>());
		}


		Ray cameraRay = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit floorHit;
		float camRayLength = 300f;
		if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor"))) {
			Vector3 a = floorHit.point;

			guidance.transform.position = a + Vector3.up * 100;
			Projector projector = guidance.GetComponent<Projector>();
			projector.orthographicSize = (float)diameter / 2;
			TerraformController tfc = TerraformController.getInstance();

			if(tfc.tool == TerraformController.SMOOTH || tfc.tool == TerraformController.AUTO || tfc.tool == TerraformController.RESET){
				projector.material.SetTexture("_ShadowTex", square);
			}else{
				projector.material.SetTexture("_ShadowTex", circle);
			}

			//Bugfix for dragging from UI to game.
			if(terraformPutThisClick == null || terraformPutThisClick.Count == 0){
				return;
			}
		
			if (Input.GetMouseButton(0)){

				for(int i = 0; i < diameter; i++){
					for(int j = 0; j < diameter; j++){

						Vector2 b = new Vector2();
						
						b.x = (int)(a.x) - Mathf.Round(diameter/2f) + i;
						b.y = (int)(a.z) - Mathf.Round(diameter/2f) + j;
						if(blockIsInsideBounds(b)){
							Vector2 a2 = new Vector2(a.x, a.z);
							float height = CalculateNewHeight(new Vector2((int)b.x, (int)b.y), diameter, Vector2.Distance(a2, b), strength);
							height = Mathf.Clamp(height, 1, ChunkMap.SizeY);

							KeyValuePair<Vector2, float> kvp = new KeyValuePair<Vector2, float>(new Vector2((int)b.x, (int)b.y), height);


							if(terraforms.ContainsKey(kvp.Key)){
								terraforms[kvp.Key] = kvp.Value;
							}else{
								terraforms.Add (kvp.Key, kvp.Value);
							}
							terraformPutThisClick[0].Add(kvp);
						}
					}
				}

				TerraformRenderer.RenderAll(terraforms);
			}
		}
	}

	bool blockIsInsideBounds(Vector2 b){
		return (b.x >= 0 && b.x <= ChunkMap.SizeX &&
		        b.y >= 0 && b.y <= ChunkMap.SizeZ);
	}

	float CalculateNewHeight(Vector2 b, int radius, float distance, float strength){

		TerraformController tfc = TerraformController.getInstance();

		switch(tfc.tool){
			case TerraformController.DOWN:
				strength = -strength;
				break;
			case TerraformController.AUTO:
				if(terraforms.ContainsKey(b))
					return terraforms[b] + (Mathf.PerlinNoise(b.x * 0.1f + seed, b.y * 0.1f + seed) - 0.5f) * strength;
				break;
			case TerraformController.SMOOTH:
				if(terraforms.ContainsKey(b)){
					return terraforms[b] - (terraforms[b] - CalculateSmoothing(b)) * Mathf.Clamp(strength,0,2);
				}else{
					terraforms.Add (b, TerraformController.defaultHeight);
				}
				break;
			case TerraformController.RESET:
				return TerraformController.defaultHeight;
		}

		if(terraforms.ContainsKey(b)){
			return (terraforms[b] + strength * Mathf.Clamp((radius / 2 - distance), 0,100) /radius);
		}
		return TerraformController.defaultHeight;
	}

	float CalculateSmoothing(Vector2 b){
		Vector2 right = new Vector2(b.x + 1, b.y);
		Vector2 left = new Vector2(b.x - 1, b.y);
		Vector2 up = new Vector2(b.x, b.y + 1);
		Vector2 down = new Vector2(b.x, b.y - 1);

		float total = 0;
		int count = 0;

		
		if(terraforms.ContainsKey(right)){
			total+=terraforms[right];
			count++;
		}else if(blockIsInsideBounds(right)){
			terraforms.Add(right, TerraformController.defaultHeight);
			total+=terraforms[right];
			count++;
		}

		if(terraforms.ContainsKey(left)){
			total+=terraforms[left];
			count++;
		}else if(blockIsInsideBounds(left)){
			terraforms.Add(left, TerraformController.defaultHeight);
			total+=terraforms[left];
			count++;
		}

		if(terraforms.ContainsKey(up)){
			total+=terraforms[up];
			count++;
		}else if(blockIsInsideBounds(up)){
			terraforms.Add(up, TerraformController.defaultHeight);
			total+=terraforms[up];
			count++;
		}

		if(terraforms.ContainsKey(down)){
			total+=terraforms[down];
			count++;
		}else if(blockIsInsideBounds(down)){
			terraforms.Add(down, TerraformController.defaultHeight);
			total+=terraforms[down];
			count++;
		}

		if(count == 0) return TerraformController.defaultHeight;
		return total / count;
	}
}
