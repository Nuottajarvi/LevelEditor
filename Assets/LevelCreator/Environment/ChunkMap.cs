using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ChunkMap : MonoBehaviour{
	
	public const int SizeX = 128;
	public const int SizeY = 48;
	public const int SizeZ = 128;
	
	public Block[,,] blockGrid;
	public GameObject[] chunkbases = new GameObject[12];
	public List<GameObject>[] chunks = new List<GameObject>[12]; //one chunklist per material

	public GameObject tempChunk;

	void Awake(){

		blockGrid = new Block[SizeX, SizeY, SizeZ];

		for(int i = 0; i < chunks.Length; i++){
			chunks[i] = new List<GameObject>();
		}
	}
}