using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Smooth : ChunkRenderer{

	static Smooth instance;

	Mesh side;
	Mesh innerCorner;
	Mesh outerCorner;

	Smooth(){
		GameObject outerCornerObject = Resources.Load("outercorner") as GameObject;
		MeshFilter mesh = outerCornerObject.GetComponent<MeshFilter>();
		outerCorner = mesh.sharedMesh;

		GameObject innerCornerObject = Resources.Load ("innercorner") as GameObject;
		mesh = innerCornerObject.GetComponent<MeshFilter>();
		innerCorner = mesh.sharedMesh;

		GameObject sideObject = Resources.Load ("side") as GameObject;
		mesh = sideObject.GetComponent<MeshFilter>();
		side = mesh.sharedMesh;
	}

	public static void makeCube(int i, int x, int y, int z,
	                            List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                            Block[] adjacentBlocks, Block[,,] blockGrid){

		if(instance == null){
			instance = new Smooth();
		}

		Block[] adjacentBlocksNoRot = getAdjacentBlocks(blockGrid, x, y, z, 0);
		Block left = adjacentBlocksNoRot[(int)Directions.Left];
		Block front = adjacentBlocksNoRot[(int)Directions.Front];
		Block right = adjacentBlocksNoRot[(int)Directions.Right];
		Block back = adjacentBlocksNoRot[(int)Directions.Back];
		Block middle = adjacentBlocksNoRot[(int)Directions.Middle];

		Block NE = blockGrid[x+1,y,z+1];
		Block SE = blockGrid[x+1,y,z-1];
		Block SW = blockGrid[x-1,y,z-1];
		Block NW = blockGrid[x-1,y,z+1];

		//Make middlecube
		Cube.makeCube(i,x,y,z,vertices,indices,UVs,adjacentBlocks);

		//SIDES AND CORNER INS
		if(left.texture != middle.texture){
			if(NW.texture == middle.texture){

			}else if(SW.texture == middle.texture){
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 0, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.innerCorner.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.innerCorner.subMeshCount; n++){
					foreach(int indice in instance.innerCorner.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.innerCorner.uv){
					UVs.Add(uv);
				}
				blockGrid[x-1,y,z] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
			
			}else{
				//STRAIGHT
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				foreach(Vector3 vertice in instance.side.vertices){
					vertices.Add(position + vertice);
				}

				for(int n = 0; n < instance.side.subMeshCount; n++){
					foreach(int indice in instance.side.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.side.uv){
					UVs.Add(uv);
				}
				blockGrid[x-1,y,z] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
			}

		}

		if(front.texture != middle.texture){

			if(NW.texture == middle.texture){
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 90, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.innerCorner.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.innerCorner.subMeshCount; n++){
					foreach(int indice in instance.innerCorner.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.innerCorner.uv){
					UVs.Add(uv);
				}
				blockGrid[x,y,z+1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
				
			}else if(NE.texture == middle.texture){
				
			}else{
				//STRAIGHT
			
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);

				Quaternion rot = Quaternion.Euler (0, 90, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);

				foreach(Vector3 vertice in instance.side.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.side.subMeshCount; n++){
					foreach(int indice in instance.side.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.side.uv){
					UVs.Add(uv);
				}
				blockGrid[x,y,z+1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
			}
		}

		if(right.texture != middle.texture){

			if(NE.texture == middle.texture){
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 180, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.innerCorner.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.innerCorner.subMeshCount; n++){
					foreach(int indice in instance.innerCorner.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.innerCorner.uv){
					UVs.Add(uv);
				}
				blockGrid[x+1,y,z] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
				
			}else if(SE.texture == middle.texture){
				
			}else{
				//STRAIGHT
			
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 180, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.side.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.side.subMeshCount; n++){
					foreach(int indice in instance.side.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.side.uv){
					UVs.Add(uv);
				}
				blockGrid[x+1,y,z] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
			}
		}

		if(back.texture != middle.texture){

			if(SE.texture == middle.texture){
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 270, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.innerCorner.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.innerCorner.subMeshCount; n++){
					foreach(int indice in instance.innerCorner.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.innerCorner.uv){
					UVs.Add(uv);
				}
				blockGrid[x,y,z-1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
				
			}else if(SW.texture == middle.texture){
				
			}else{
				//STRAIGHT
				int vertexIndex = vertices.Count;
				Vector3 position = new Vector3(x,y,z);
				
				Quaternion rot = Quaternion.Euler (0, 270, 0);
				Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
				
				foreach(Vector3 vertice in instance.side.vertices){
					vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
				}
				
				for(int n = 0; n < instance.side.subMeshCount; n++){
					foreach(int indice in instance.side.GetIndices (n)){
						indices.Add (indice + vertexIndex);
					}
				}
				foreach(Vector2 uv in instance.side.uv){
					UVs.Add(uv);
				}
				blockGrid[x,y,z-1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
			}
		}

		//CORNER OUTS
		if(NW.texture != middle.texture && front.texture != middle.texture && left.texture != middle.texture){
			int vertexIndex = vertices.Count;
			Vector3 position = new Vector3(x,y,z);

			foreach(Vector3 vertice in instance.outerCorner.vertices){
				vertices.Add(position + vertice);
			}
			
			for(int n = 0; n < instance.outerCorner.subMeshCount; n++){
				foreach(int indice in instance.outerCorner.GetIndices (n)){
					indices.Add (indice + vertexIndex);
				}
			}
			foreach(Vector2 uv in instance.outerCorner.uv){
				UVs.Add(uv);
			}
			blockGrid[x-1,y,z+1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
		}

		if(NE.texture != middle.texture && front.texture != middle.texture && right.texture != middle.texture){
			int vertexIndex = vertices.Count;
			Vector3 position = new Vector3(x,y,z);
			
			Quaternion rot = Quaternion.Euler (0, 90, 0);
			Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
			
			foreach(Vector3 vertice in instance.outerCorner.vertices){
				vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
			}
			
			for(int n = 0; n < instance.outerCorner.subMeshCount; n++){
				foreach(int indice in instance.outerCorner.GetIndices (n)){
					indices.Add (indice + vertexIndex);
				}
			}
			foreach(Vector2 uv in instance.outerCorner.uv){
				UVs.Add(uv);
			}
			blockGrid[x+1,y,z+1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
		}

		if(SE.texture != middle.texture && back.texture != middle.texture && right.texture != middle.texture){
			int vertexIndex = vertices.Count;
			Vector3 position = new Vector3(x,y,z);
			
			Quaternion rot = Quaternion.Euler (0, 180, 0);
			Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
			
			foreach(Vector3 vertice in instance.outerCorner.vertices){
				vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
			}
			
			for(int n = 0; n < instance.outerCorner.subMeshCount; n++){
				foreach(int indice in instance.outerCorner.GetIndices (n)){
					indices.Add (indice + vertexIndex);
				}
			}
			foreach(Vector2 uv in instance.outerCorner.uv){
				UVs.Add(uv);
			}
			blockGrid[x+1,y,z-1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
		}

		if(SW.texture != middle.texture && back.texture != middle.texture && left.texture != middle.texture){
			int vertexIndex = vertices.Count;
			Vector3 position = new Vector3(x,y,z);
			
			Quaternion rot = Quaternion.Euler (0, 270, 0);
			Vector3 middlePoint = new Vector3(x + 0.5f, y + 0.5f, z + 0.5f);
			
			foreach(Vector3 vertice in instance.outerCorner.vertices){
				vertices.Add(rot * ((position + vertice) - middlePoint) + middlePoint);
			}
			
			for(int n = 0; n < instance.outerCorner.subMeshCount; n++){
				foreach(int indice in instance.outerCorner.GetIndices (n)){
					indices.Add (indice + vertexIndex);
				}
			}
			foreach(Vector2 uv in instance.outerCorner.uv){
				UVs.Add(uv);
			}
			blockGrid[x-1,y,z-1] = new Block(ObjectController.SMOOTHBORDER, 0, 0);
		}
	}
}
