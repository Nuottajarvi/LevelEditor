using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System;

public class EnviromentSaveAndLoadManager{

	private static EnviromentSaveAndLoadManager instance;

	private string key = "envblocks";
	
	private EnviromentSaveAndLoadManager(){
		
	}
	
	public static EnviromentSaveAndLoadManager getInstance(){
		if(instance == null){
			instance = new EnviromentSaveAndLoadManager();
		}	
		return instance;
	}

	public void loadMap(){

		ChunkMap chunkMap = GameObject.Find("EnviromentController").GetComponent<ChunkMap>();

		for(int i = 0; i < ChunkMap.SizeX; i++){
			for(int j = 0; j < ChunkMap.SizeY; j++){
				for(int k = 0; k < ChunkMap.SizeZ; k++){
					chunkMap.blockGrid[i,j,k] = new Block(0,0,0);			
				}	
			}
		}

		string text = SaveAndLoadManager.getInstance().loadText(key);
		string[] lines = text.Split('\n');
		int count = 0;

		List<Block> blocks = new List<Block>();
		Regex blockSegment = new Regex(@"\d+\/\d+\/\d+\/\d+");
		Regex emptySegment = new Regex(@"-\/-\/-\/\d+");

		foreach(string line in lines){
			//Has Block
			if(blockSegment.IsMatch(line)){
				int shape = int.Parse(line.Substring(0,line.IndexOf('/')));
				string rest = line.Substring(line.IndexOf('/') + 1);
				int texture = int.Parse(rest.Substring(0, rest.IndexOf('/')));
				rest = rest.Substring(rest.IndexOf('/') + 1);
				int rotation = int.Parse(rest.Substring(0, rest.IndexOf('/')));
				rest = rest.Substring(rest.IndexOf('/') + 1);
				int amount = int.Parse(rest);

				for(int i = 0; i < amount; i++){
					Block block = new Block(shape, texture, rotation);

					int z = count % ChunkMap.SizeZ;
					int x = count / ChunkMap.SizeZ % ChunkMap.SizeX;
					int y = count / (ChunkMap.SizeZ * ChunkMap.SizeX);

					chunkMap.blockGrid[x,y,z] = block;

					count++;
				}
			//Is Empty
			}else if(emptySegment.IsMatch(line)){
				string rest = line.Substring(line.LastIndexOf('/') + 1);
				int amount = int.Parse(rest);

				count+=amount;
			}
		}

		ChunkRenderer.renderAll();
	}

	//Packs and saves the map
	public void saveMap(Block[,,] blocks){

		string text = "";
		Block lastBlock = new Block(0,0,0);
		int amount = 1;

		for(int j = 0; j < ChunkMap.SizeY; j++){
			for(int i = 0; i < ChunkMap.SizeX; i++){
				for(int k = 0; k < ChunkMap.SizeZ; k++){
					Block block = blocks[i,j,k];

					if(isEmpty(block)){
						if(isEmpty(lastBlock)){
							amount++;
						}else{
							if(isEmpty(lastBlock)){
								text += "-/-/-/" + amount + "\n";
							}else{
								text += "" + lastBlock.shape + "/" + lastBlock.texture + "/" + lastBlock.rotation + "/" + amount + "\n";
							}
							amount = 1;
						}
					}else{
						if(isEmpty(lastBlock)){
							text += "-/-/-/" + amount + "\n";
							amount = 1;
						}else if(block.shape == lastBlock.shape && block.texture == lastBlock.texture && block.rotation == lastBlock.rotation){
							amount++;
						}else{
							text += "" + lastBlock.shape + "/" + lastBlock.texture + "/" + lastBlock.rotation + "/" + amount + "\n";
							amount = 1;
						}
					}

					//remove first
					if(i == 0 && j == 0 && k == 0){
						if(isEmpty(block))
							amount = 1;
						else
							text = "";
					}

					lastBlock = block;
				}
			}
		}

		//last block
		if(isEmpty(lastBlock)){
			text += "-/-/-/" + amount;
		}else{
			text += "" + lastBlock.shape + "/" + lastBlock.texture + "/" + lastBlock.rotation + "/" + amount;
		}

		SaveAndLoadManager.getInstance().saveText(key, text);
	}

	bool isEmpty(Block block){
		return block.texture == 0;
	}
}
