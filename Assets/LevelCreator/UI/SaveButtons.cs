using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SaveButtons : MonoBehaviour {

	public void Save(){

		Tabs tabs = GameObject.Find ("Phases").GetComponent<Tabs>();
		tabs.activateAll();

		ChunkMap chunkMap = GameObject.Find ("EnviromentController").GetComponent<ChunkMap>();
		EnviromentSaveAndLoadManager.getInstance().saveMap(chunkMap.blockGrid);

		Dictionary<GameObject, GameObjectNotation> details = GameObject.Find ("DetailsController").GetComponent<DropPrefab>().details;
		DetailsSaveAndLoadManager.getInstance().saveDetails(details);

		Dictionary<Vector2, float> terraforms = GameObject.Find("TerraformController").GetComponent<DropTerraform>().terraforms;
		TerraformSaveAndLoadManager.getInstance().saveMap(terraforms);

		tabs.returnState();
	}

	public void Load(){
		Tabs tabs = GameObject.Find ("Phases").GetComponent<Tabs>();
		tabs.activateAll();

		EnviromentSaveAndLoadManager.getInstance().loadMap();
		DetailsSaveAndLoadManager.getInstance().loadDetails();
		TerraformSaveAndLoadManager.getInstance().loadMap();

		ChunkRenderer.renderAll();

		tabs.returnState();
	}
}
