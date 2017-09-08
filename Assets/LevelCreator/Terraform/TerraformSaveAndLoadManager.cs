using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class TerraformSaveAndLoadManager{

	private static TerraformSaveAndLoadManager instance;

	private string key = "terraform";
	
	private TerraformSaveAndLoadManager(){
		
	}
	
	public static TerraformSaveAndLoadManager getInstance(){
		if(instance == null){
			instance = new TerraformSaveAndLoadManager();
		}	
		return instance;
	}

	public void loadMap(){

		Dictionary<Vector2, float> terraforms = GameObject.Find("TerraformController").GetComponent<DropTerraform>().terraforms;
		terraforms.Clear();

		string text = SaveAndLoadManager.getInstance().loadText(key);
		string[] lines = text.Split('\n');

		Regex terraformSegment = new Regex(@"\d+,\d+\/\d+");

		foreach(string line in lines){
			//Has Block
			if(terraformSegment.IsMatch(line)){
				Vector2 position = Vector2.zero;

				position.x = int.Parse(line.Substring(0,line.IndexOf(',')));
				string rest = line.Substring(line.IndexOf(',') + 1);
				position.y = int.Parse(rest.Substring(0, rest.IndexOf('/')));
				rest = rest.Substring(rest.IndexOf('/') + 1);
				float height = float.Parse(rest);
				terraforms.Add(position, height);
			}
		}
		TerraformRenderer.RenderAll(terraforms);
	}
	
	public void saveMap(Dictionary<Vector2, float> terraforms){

		string text = "";

		foreach(KeyValuePair<Vector2, float> terraform in terraforms){

			text += terraform.Key.x + "," + terraform.Key.y + "/" + terraform.Value + "\n";
		}

		SaveAndLoadManager.getInstance().saveText(key, text);
	}
}
