using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class ChunkRenderer{
	
	private static ChunkMap chunkMap;
	private static Block[,,] tempGrid;

	public enum Directions {Top, Bottom, Right, Back, Left, Front, Middle};

	public static void renderAll(){

		if(chunkMap == null){
			chunkMap = GameObject.Find("EnviromentController").GetComponent<ChunkMap>();
		}

		GameObject.Destroy(chunkMap.tempChunk);
		chunkMap.tempChunk = null;
		tempGrid = null;

		List<GameObject>[] chunks = chunkMap.chunks;
		Block[,,] blockGrid = chunkMap.blockGrid;

		List<Mesh>[] meshes = getMeshes(chunks, blockGrid);

		for(int i = 0; i < chunks.Length; i++){
			for(int j = 0; j < meshes[i].Count; j++){

				if(chunks[i].Count < j + 1){
					GameObject newChunk = (GameObject)GameObject.Instantiate(chunkMap.chunkbases[i], Vector3.zero, Quaternion.identity);
					newChunk.transform.SetParent(GameObject.Find ("ChunkParent").transform);
					chunks[i].Add(newChunk);
				}

				MeshFilter meshfilter = chunks[i][j].GetComponent<MeshFilter>();
				meshfilter.mesh = meshes[i][j];
				MeshCollider meshcollider = chunks[i][j].GetComponent<MeshCollider>();
				meshcollider.sharedMesh = meshes[i][j];
				meshes[i][j].RecalculateNormals();

			}
		}
	}

	public static void renderTemporarily(List<Vector3> blockCoords){

		//TODO: 65k vertice block

		if(chunkMap == null){
			chunkMap = GameObject.Find("EnviromentController").GetComponent<ChunkMap>();
		}

		Block block = chunkMap.blockGrid[(int)blockCoords[0].x, (int)blockCoords[0].y, (int)blockCoords[0].z];
				
		List<GameObject>[] chunks = new List<GameObject>[1];
		chunks[0] = new List<GameObject>();

		if(chunkMap.tempChunk == null){
			chunkMap.tempChunk = (GameObject)GameObject.Instantiate(chunkMap.chunkbases[block.texture - 1], Vector3.zero, Quaternion.identity);
			chunkMap.tempChunk.transform.SetParent(GameObject.Find ("ChunkParent").transform);
		}

		chunks[0].Add(chunkMap.tempChunk);
	
		if(block.shape == ObjectController.SMOOTH){
			tempGrid = chunkMap.blockGrid;
		}else if(tempGrid == null || tempGrid == chunkMap.blockGrid){
			tempGrid = new Block[ChunkMap.SizeX, ChunkMap.SizeY, ChunkMap.SizeZ];
		}

		foreach(Vector3 blockCoord in blockCoords){
			tempGrid[(int)blockCoord.x, (int)blockCoord.y, (int)blockCoord.z] = new Block(block.shape, block.texture, block.rotation);
		}

		List<Mesh>[] meshes = getMeshes(chunks, tempGrid);
		
		for(int i = 0; i < chunks.Length; i++){
			for(int j = 0; j < meshes[i].Count; j++){
				
				MeshFilter meshfilter = chunkMap.tempChunk.GetComponent<MeshFilter>();
				meshfilter.mesh = meshes[i][j];
				MeshCollider meshcollider = chunkMap.tempChunk.GetComponent<MeshCollider>();
				meshcollider.sharedMesh = meshes[i][j];
				meshes[i][j].RecalculateNormals();
				
			}
		}


	}

	public static List<Mesh>[] getMeshes(List<GameObject>[] chunks, Block[,,] blockGrid){
		//Array of each texture
		//List of each chunk for texture
		//List of each vertex/index/UV
		List<List<Vector3>>[] vertices = new List<List<Vector3>>[chunks.Length];
		List<List<int>>[] indices = new List<List<int>>[chunks.Length];
		List<List<Vector2>>[] UVs = new List<List<Vector2>>[chunks.Length];

		for(int x = 0; x < ChunkMap.SizeX; x++){
			for(int y = 0; y < ChunkMap.SizeY; y++){
				for(int z = 0; z < ChunkMap.SizeZ; z++){
					Block block = blockGrid[x,y,z];

					if(block.texture != 0){

						int i = block.texture - 1; //choose the Chunk
						if(chunks.Length == 1) i = 0;

						if(vertices[i] == null){
							vertices[i] = new List<List<Vector3>>();
							indices[i] = new List<List<int>>();
							UVs[i] = new List<List<Vector2>>();
						}

						int verticeCountInStart = 0;
						if(vertices[i].Count != 0){
							verticeCountInStart = vertices[i].Last().Count();
						}

						//Create new list for chunk
						if(vertices[i].Count == 0 || verticeCountInStart > 50000){
							vertices[i].Add(new List<Vector3>());
							indices[i].Add (new List<int>());
							UVs[i].Add (new List<Vector2>());
						}

						Block[] adjacentBlocks = getAdjacentBlocks(blockGrid, x, y, z, block.rotation);

						List<Vector3> verts = vertices[i].Last();
						List<int> inds = indices[i].Last();
						List<Vector2> uvs = UVs[i].Last();

						int rotation = block.rotation;

						switch(block.shape){
							case 0:Cube.makeCube(i, x, y, z, verts, inds, uvs, adjacentBlocks);break;
							case 1:rotation = Stairs.makeStairs(i, x, y, z, verts, inds, uvs, adjacentBlocks, blockGrid);break;
							case 2:rotation = Slope.makeSlope(i, x, y, z, verts, inds, uvs, adjacentBlocks, blockGrid, 0, 1);break;
							case 3:Smooth.makeCube(i, x, y, z, verts, inds, uvs, adjacentBlocks, blockGrid);break;
							case 4:Cube.makeSlab(i, x, y, z, verts, inds, uvs, adjacentBlocks);break;
							case 5:rotation = Slope.makeSlope(i, x, y, z, verts, inds, uvs, adjacentBlocks, blockGrid, 0, 0.5f);break;
						}

						//ROTATE
						for(int n = verticeCountInStart; n < verts.Count; n++){
							Quaternion rot = Quaternion.Euler (0, 90 * rotation, 0);
							Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
						
							verts[n] = rot * (verts[n] - middlePoint) + middlePoint;
						}
						
					}
				}
			}
		}
		List<Mesh>[] meshes = new List<Mesh>[chunks.Length];
		for(int i = 0; i < chunks.Length; i++){
			meshes[i] = new List<Mesh>();

			if(vertices[i] != null){
				for(int j = 0; j < vertices[i].Count; j++){
					meshes[i].Add (new Mesh());
					meshes[i][j].vertices = vertices[i][j].ToArray();
					meshes[i][j].triangles = indices[i][j].ToArray();
					meshes[i][j].uv = UVs[i][j].ToArray();
				}
			}else{
				meshes[i].Add (new Mesh());
			}

		}
		return meshes;
	}

	protected static Block[] getAdjacentBlocks(Block[,,] blockGrid, int x, int y, int z, int rotation){
		Block middle = blockGrid[x,y,z];
		Block top;
		Block bottom;
		Block[] sides = new Block[4];
		
		if(y != ChunkMap.SizeY - 1)
			top = blockGrid[x, y+1, z];
		else
			top = new Block(0,0,0);
		
		if(y != 0){
			
			bottom = blockGrid[x, y-1, z];
		}
		
		else{
			bottom = new Block(0,0,0);
		}

		
		if(x != ChunkMap.SizeX - 1)
			sides[(4 - rotation) % 4] = blockGrid[x+1, y, z];
		else
			sides[(4 - rotation) % 4] = new Block(0,0,0);
		
		if(z != 0)
			sides[(5 - rotation) % 4] = blockGrid[x, y, z-1];
		else
			sides[(5 - rotation) % 4] = new Block(0,0,0);
		
		if(x != 0)
			sides[(6 - rotation) % 4] = blockGrid[x-1, y, z];
		else
			sides[(6 - rotation) % 4] = new Block(0,0,0);
		
		if(z != ChunkMap.SizeZ - 1)
			sides[(7 - rotation) % 4] = blockGrid[x, y, z+1];
		else
			sides[(7 - rotation) % 4] = new Block(0,0,0);

		return new Block[]{top, bottom, sides[0], sides[1], sides[2], sides[3], middle};
	}

	protected static bool showsAsVisible(Block[] adjacentBlocks, int direction){
		//TODO: Optimise
		if(adjacentBlocks[direction].shape != 0){
			return true;
		}

		//If both are water
		if(adjacentBlocks[(int)Directions.Middle].texture == 9 && adjacentBlocks[(int)direction].texture == 9){
			return false;
		}
		
		int texture = adjacentBlocks[direction].texture;
		//Nothing, Water or Ice
		if( texture == 0 || texture == 9 || texture == 12){
			return true;
		}else{
			return false;
		}
	}
}
