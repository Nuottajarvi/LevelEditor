using UnityEngine;
using System.Collections;
using System.Text.RegularExpressions;
using System.IO;

// segments are in form [#a591g9asd9]
public class SaveAndLoadManager{

	private static SaveAndLoadManager instance;

	private SaveAndLoadManager(){

	}

	public static SaveAndLoadManager getInstance(){
		if(instance == null){
			instance = new SaveAndLoadManager();
		}
		return instance;
	}

	public string loadText(string key){

		bool reading = false;
		Regex segment = new Regex(@"\[#\w*]"); //searches regex like "[#asd421891]"
		string text = "";

		try
		{
			using (StreamReader sr = new StreamReader("./Assets/LevelCreator/LevelFile.txt"))
			{
				string line = sr.ReadLine();
				while(line != null){

					if(reading){
						if(segment.IsMatch(line)){
							reading = false;
						}else{
							text += line + "\n";
						}
					}

					if(line.Contains("[#" + key + "]")){
						reading = true;
					}
					line = sr.ReadLine ();
				}
			}
		}
		catch (IOException e)
		{
			Debug.Log("File missing: ");
			Debug.Log(e.Message);
		}
		return text;
	}

	private string loadText(){

		string data = "";

		try
		{
			using (StreamReader sr = new StreamReader("./Assets/LevelCreator/LevelFile.txt"))
			{
				string line = sr.ReadToEnd();
				data += line;
			}
		}
		catch (IOException e){
			Debug.Log("File missing: ");
			Debug.Log(e.Message);
		}

		return data;
	}

	public void saveText(string key, string data){

		string[] lines = data.Split('\n');

		try{

			string filetext = loadText ();
			string[] filelines = filetext.Split('\n');

			Regex segment = new Regex(@"\[#\w*]");

			using(StreamWriter sw = new StreamWriter("./Assets/LevelCreator/LevelFile.txt")){

				bool writing = true;
				bool segmentFound = false;

				foreach(string fileline in filelines){

					if(segment.IsMatch(fileline)){
						writing = true;
					}

					if(fileline.Contains("[#" + key + "]")){
						sw.WriteLine ("[#" + key + "]");
						foreach(string line in lines){
							if(line != ""){
								sw.WriteLine(line);
							}
						}
						segmentFound = true;
						writing = false;
					}

					if(writing){
						if(fileline != ""){
							sw.WriteLine(fileline.TrimEnd('\r', '\n'));
						}
					}
				}

				//if there wasn't anything to append to
				if(!segmentFound){
					sw.WriteLine ("[#" + key + "]");
					foreach(string line in lines){
						if(line != ""){
							sw.WriteLine(line.TrimEnd('\r', '\n'));
						}
					}
				}
			}
		}catch (IOException e){
			Debug.Log("File missing: ");
			Debug.Log(e.Message);
		}
	}
}
