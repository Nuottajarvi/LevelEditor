using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class DetailsSaveAndLoadManager {

	private static DetailsSaveAndLoadManager instance;
	
	private string key = "details";
	
	private DetailsSaveAndLoadManager(){
		
	}
	
	public static DetailsSaveAndLoadManager getInstance(){
		if(instance == null){
			instance = new DetailsSaveAndLoadManager();
		}	
		return instance;
	}
	
	public void loadDetails(){

		DropPrefab dropPrefab = GameObject.Find("DetailsController").GetComponent<DropPrefab>();

		string text = SaveAndLoadManager.getInstance().loadText(key);
		string[] lines = text.Split('\n');

		Regex prefabSegment = new Regex(@"\d\/\d+.\d\d,\d+.\d\d,\d+.\d\d\/\-?\d+.\d\d,-?\d+.\d\d,-?\d+.\d\d,-?\d+.\d\d");

		foreach(string line in lines){
			//Has Block
			if(prefabSegment.IsMatch(line)){

				int prefab = int.Parse(line.Substring(0,line.IndexOf('/')));
				string rest = line.Substring(line.IndexOf('/') + 1);

				Vector3 position;
				position.x = float.Parse(rest.Substring(0, rest.IndexOf(',')));
				rest = rest.Substring(rest.IndexOf(',') + 1);
				position.y = float.Parse(rest.Substring(0, rest.IndexOf(',')));
				rest = rest.Substring(rest.IndexOf(',') + 1);
				position.z = float.Parse(rest.Substring(0, rest.IndexOf('/')));
				rest = rest.Substring(rest.IndexOf('/') + 1);

				Quaternion rotation;
				rotation.x = float.Parse(rest.Substring(0, rest.IndexOf(',')));
				rest = rest.Substring(rest.IndexOf(',') + 1);
				rotation.y = float.Parse(rest.Substring(0, rest.IndexOf(',')));
				rest = rest.Substring(rest.IndexOf(',') + 1);
				rotation.z = float.Parse(rest.Substring(0, rest.IndexOf(',')));
				rest = rest.Substring(rest.IndexOf(',') + 1);
				rotation.w = float.Parse(rest);

				dropPrefab.Instantiate(prefab, position, rotation);

			}
		}
	}
	
	//Packs and saves the map
	public void saveDetails(Dictionary<GameObject, GameObjectNotation> details){
		
		string text = "";

		foreach(GameObjectNotation gon in details.Values){
			text+=gon.prefab + "/" + gon.position.x.ToString("0.00") + "," + gon.position.y.ToString("0.00") + "," + gon.position.z.ToString("0.00") + "/"
				+ gon.rotation.x.ToString("0.00") + "," + gon.rotation.y.ToString("0.00") + "," + gon.rotation.z.ToString("0.00") + "," + gon.rotation.w.ToString("0.00") + "\n";
		 }
		
		SaveAndLoadManager.getInstance().saveText(key, text);
	}
}
