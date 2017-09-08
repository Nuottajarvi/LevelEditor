using System;
using UnityEngine;
using System.Collections.Generic;

public class Slope : ChunkRenderer{

	public static int makeSlope(int i, int x, int y, int z,
	                              List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                              Block[] adjacentBlocks, Block[,,] blockGrid, float minY, float maxY){
		
		Block[] adjacentBlocksNoRot = getAdjacentBlocks(blockGrid, x, y, z, 0);
		
		Block left = adjacentBlocksNoRot[(int)Directions.Left];
		Block front = adjacentBlocksNoRot[(int)Directions.Front];
		Block right = adjacentBlocksNoRot[(int)Directions.Right];
		Block back = adjacentBlocksNoRot[(int)Directions.Back];
		
		if(left.shape == ObjectController.SLOPE && front.shape == ObjectController.SLOPE ||
		   left.shape == ObjectController.LOWSLOPE && front.shape == ObjectController.LOWSLOPE){
			if(left.rotation == 1 && front.rotation == 0){
				makeSlopeOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 0;
			}else if(left.rotation == 3 && front.rotation == 2){
				makeSlopeInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 2;
			}
			
		}else if(front.shape == ObjectController.SLOPE && right.shape == ObjectController.SLOPE ||
		         front.shape == ObjectController.LOWSLOPE && right.shape == ObjectController.LOWSLOPE){
			if(front.rotation == 2 && right.rotation == 1){
				makeSlopeOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 1;
			}else if(front.rotation == 0 && right.rotation == 3){
				makeSlopeInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 3;
			}
			
		}else if(right.shape == ObjectController.SLOPE && back.shape == ObjectController.SLOPE ||
		         right.shape == ObjectController.LOWSLOPE && back.shape == ObjectController.LOWSLOPE){
			if(right.rotation == 3 && back.rotation == 2){
				makeSlopeOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 2;
			}else if(right.rotation == 1 && back.rotation == 0){
				makeSlopeInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 0;
			}
			
		}else if(back.shape == ObjectController.SLOPE && left.shape == ObjectController.SLOPE ||
		         back.shape == ObjectController.LOWSLOPE && left.shape == ObjectController.LOWSLOPE){
			if(back.rotation == 0 && left.rotation == 3){
				makeSlopeOuterCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 3;
			}else if(back.rotation == 2 && left.rotation == 1){
				makeSlopeInnerCorner(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
				return 1;
			}
			
		}
		makeSlopeStraight(i,x,y,z,vertices,indices,UVs,adjacentBlocks, minY, maxY);
		return adjacentBlocks[(int)Directions.Middle].rotation;
		
	}
	
	static void makeSlopeStraight(int i, int x, int y, int z, 
	                              List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                              Block[] adjacentBlocks, float minY, float maxY){
		int rotation = adjacentBlocks[(int)Directions.Middle].rotation;

		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Front) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			
			int vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x, y+maxY, z));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);

			if(rotation == 0){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,1));
			}else if(rotation == 3){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
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
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+maxY, z));
			vertices.Add(new Vector3(x, y+minY, z));
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			
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
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);

			if(rotation == 0){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,1));
			}else if(rotation == 1){
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 3){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,1));
			}

			
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Back)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+minY, z));
			vertices.Add(new Vector3(x, y+maxY, z));
			vertices.Add(new Vector3(x+1, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);

			if(rotation == 0){
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 1){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(1,1));
				UVs.Add (new Vector2(0,0));
			}else if(rotation == 2){
				UVs.Add (new Vector2(0,1));
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,1));

			}else if(rotation == 3){
				UVs.Add (new Vector2(0,0));
				UVs.Add (new Vector2(1,0));
				UVs.Add (new Vector2(0,1));
			}
		}
		
	}
	
	static void makeSlopeOuterCorner(int i, int x, int y, int z, 
	                                 List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                                 Block[] adjacentBlocks, float minY, float maxY){
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Front) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			
			int vertexIndex = vertices.Count;
			
			//RIGHT SIDE
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(1,0));
			//BACKSIDE
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x, y+minY, z));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(0,0));
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
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x, y+minY, z));
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(1,1));
			
		}
		
	}
	
	static void makeSlopeInnerCorner(int i, int x, int y, int z,
	                                 List<Vector3> vertices, List<int> indices, List<Vector2> UVs,
	                                 Block[] adjacentBlocks, float minY, float maxY){
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right)){
			
			int vertexIndex = vertices.Count;

			
			//RIGHT SIDE
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x+1, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(1,0));
			

			
			//BACKSIDE
			vertexIndex = vertices.Count;
			
			vertices.Add(new Vector3(x, y+maxY, z));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector3(1, 1));
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
		
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z));
			vertices.Add(new Vector3(x, y+minY, z));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(0,1));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(1,0));
			
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+minY, z+1));
			vertices.Add(new Vector3(x, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+maxY, z+1));
			vertices.Add(new Vector3(x+1, y+minY, z+1));
			
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
	}

}

