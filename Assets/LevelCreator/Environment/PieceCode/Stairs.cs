using System;
using UnityEngine;
using System.Collections.Generic;

public class Stairs : ChunkRenderer{
	public static int makeStairs(int i, int x, int y, int z, 
	                      List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                      Block[] adjacentBlocks, Block[,,] blockGrid){
		
		Block[] adjacentBlocksNoRot = getAdjacentBlocks(blockGrid, x, y, z, 0);
		
		Block left = adjacentBlocksNoRot[(int)Directions.Left];
		Block front = adjacentBlocksNoRot[(int)Directions.Front];
		Block right = adjacentBlocksNoRot[(int)Directions.Right];
		Block back = adjacentBlocksNoRot[(int)Directions.Back];
		
		if(left.shape == ObjectController.STAIRS && front.shape == ObjectController.STAIRS){
			if(left.rotation == 1 && front.rotation == 0){
				makeStairsOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,0);
				return 0;
			}else if(left.rotation == 3 && front.rotation == 2){
				makeStairsInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,2);
				return 2;
			}
			
		}else if(front.shape == ObjectController.STAIRS && right.shape == ObjectController.STAIRS){
			if(front.rotation == 2 && right.rotation == 1){
				makeStairsOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,1);
				return 1;
			}else if(front.rotation == 0 && right.rotation == 3){
				makeStairsInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,3);
				return 3;
			}
			
		}else if(right.shape == ObjectController.STAIRS && back.shape == ObjectController.STAIRS){
			if(right.rotation == 3 && back.rotation == 2){
				makeStairsOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,2);
				return 2;
			}else if(right.rotation == 1 && back.rotation == 0){
				makeStairsInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,0);
				return 0;
			}
			
		}else if(back.shape == ObjectController.STAIRS && left.shape == ObjectController.STAIRS){
			if(back.rotation == 0 && left.rotation == 3){
				makeStairsOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,3);
				return 3;
			}else if(back.rotation == 2 && left.rotation == 1){
				makeStairsInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks,1);
				return 1;
			}
			
		}
		makeStairsStraight(i,x,y,z,vertices,indices,UVs,adjacentBlocks);
		return adjacentBlocks[(int)Directions.Middle].rotation;
		
	}
	
	
	static void makeStairsStraight(int i, int x, int y, int z, List<Vector3> vertices, List<int> indices, List<Vector2> UVs, Block[] adjacentBlocks){
		
		int rotation = adjacentBlocks[(int)Directions.Middle].rotation;
		
		//TOP
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Front) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			
			int vertexIndex = vertices.Count;
			
			
			//Toplevel
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+1, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			if(rotation == 0){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.01f,1));
				UVs.Add (new Vector2(0.01f,0));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0.5f,1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0,0.5f));
			}
			
			//Low level
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1f, y+0.5f, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			if(rotation == 0){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 1));
				UVs.Add (new Vector2(0.01f, 1));
				UVs.Add (new Vector2(0.01f, 0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0, 0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(1, 1));
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
			}
			
			
			//Connector
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+1f, z));
			vertices.Add(new Vector3(x+0.5f, y+1f, z+1));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 1));
				UVs.Add (new Vector2(0.1f, 1));
				UVs.Add (new Vector2(0.1f, 0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0, 0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 1));
				UVs.Add (new Vector2(0.1f, 1));
				UVs.Add (new Vector2(0.1f, 0));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
			}

		}
		
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Back)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x+0.5f, y+1, z));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+3);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+3);
			indices.Add (vertexIndex+4);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+4);
			indices.Add (vertexIndex+5);
			
			if(rotation == 0){
				UVs.Add (new Vector2(0.01f,0));
				UVs.Add (new Vector2(0.01f,1));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.01f,1));
				UVs.Add (new Vector2(0.01f,0));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0,1));
			}
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex+3);
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex+4);
			indices.Add (vertexIndex+3);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex+5);
			indices.Add (vertexIndex+4);
			indices.Add (vertexIndex);
			
			if(rotation == 0){
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0,1));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0,1));
			}
			
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+1, z+1));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Bottom)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y, z));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y, z));
			
			
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+2);
			indices.Add (vertexIndex+3);
			
			if(rotation == 0){
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0,0.5f));
			}
		}
	}
	
	static void makeStairsOuterCorner(int i, int x, int y, int z, List<Vector3> vertices, List<int> indices, List<Vector2> UVs, Block[] adjacentBlocks, int rotation){

		//TOP
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Front) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			
			int vertexIndex = vertices.Count;
			
			//Toplevel
			vertices.Add(new Vector3(x, y+1, z+0.5f));
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+1, z+1));
			vertices.Add(new Vector3(x+0.5f, y+1, z+0.5f));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,0));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0,0));
			}
			
			//Low level
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			vertices.Add(new Vector3(x, y+0.5f, z));
			vertices.Add(new Vector3(x, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+3);
			indices.Add(vertexIndex+4);
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+4);
			indices.Add(vertexIndex+5);
			
			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0.5f, 1));
				UVs.Add (new Vector2(1, 1));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0, 1));
				UVs.Add (new Vector2(0.5f, 1));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(1, 0));
			}

			
			//Connector1
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+1f, z+1));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+1));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));
			}

			
			//Connector2
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+0, y+0.5f, z+0.5f));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
			}
		
			
			
		}
		
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Back)){
			int vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,0));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0.5f,0));
			}
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Bottom)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y, z));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			vertices.Add(new Vector3(x+1, y, z));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);

			if(rotation == 0 || rotation == 2){
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0.5f,0));
			}else if(rotation == 1 || rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(1,0));
			}
		}
	}
	
	static void makeStairsInnerCorner(int i, int x, int y, int z, List<Vector3> vertices, List<int> indices, List<Vector2> UVs, Block[] adjacentBlocks, int rotation){
		//TOP
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			
			int vertexIndex = vertices.Count;
			
			//Toplevel
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add (new Vector3(x+1, y+1, z+1));
			vertices.Add (new Vector3(x+1, y+1, z+0.5f));
			vertices.Add (new Vector3(x+0.5f, y+1, z+0.5f));
			vertices.Add (new Vector3(x+0.5f, y+1, z));
			vertices.Add (new Vector3(x, y+1, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex+3);
			indices.Add(vertexIndex+4);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+4);
			indices.Add (vertexIndex+5);

			if(rotation == 0){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(1,1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0.5f,1));
				UVs.Add (new Vector2(0.5f,0.5f));
				UVs.Add (new Vector2(0,0.5f));
				UVs.Add (new Vector2(0,0));
			}

			
			//Low level
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+1, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+3);
			indices.Add(vertexIndex);
			
			if(rotation == 0){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(1, 0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
				UVs.Add (new Vector2(1, 0));
			}else if(rotation == 3){
				UVs.Add (new Vector2(1, 0));
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(1, 0.5f));
			}

			
			//Connector1
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+1f, z+0));
			vertices.Add(new Vector3(x+0.5f, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
			}else if(rotation == 3){
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
			}

			
			//Connector2
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+0.5f, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+1, y+1f, z+0.5f));
			vertices.Add(new Vector3(x+1, y+0.5f, z+0.5f));
			vertices.Add(new Vector3(x+0.5f, y+0.5f, z+0.5f));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0){
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));	
			}else if(rotation == 1){
				UVs.Add (new Vector2(0.5f, 0));	
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));

			}else if(rotation == 2){
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));
				UVs.Add (new Vector2(0.5f, 0));	
			}else if(rotation == 3){
				UVs.Add (new Vector2(0.5f, 0));	
				UVs.Add (new Vector2(0.5f, 0.5f));
				UVs.Add (new Vector2(0, 0.5f));
				UVs.Add (new Vector2(0, 0));

			}
				
			
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+1, z+1));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add(new Vector3(x+1, y+1, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			
		}
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Bottom)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y, z));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
		}
	}
}

