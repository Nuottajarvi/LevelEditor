using System;
using UnityEngine;
using System.Collections.Generic;

public class Cube : ChunkRenderer{
	public static void makeCube(int i, int x, int y, int z, List<Vector3> vertices, List<int> indices, List<Vector2> UVs, Block[] adjacentBlocks){
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Top)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x, y+1, z+1));
			vertices.Add(new Vector3(x+1, y+1, z+1));
			vertices.Add(new Vector3(x+1, y+1, z));
			
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
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Bottom)){
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
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Right)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x+1, y+1, z));
			vertices.Add(new Vector3(x+1, y, z));
			vertices.Add(new Vector3(x+1, y, z+1));
			vertices.Add(new Vector3(x+1, y+1, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,1));
			UVs.Add (new Vector2(0,1));
			
		}
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Left)){
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
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Front)){
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
		if(showsAsVisible(adjacentBlocks, (int)ChunkRenderer.Directions.Back)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y+1, z));
			vertices.Add(new Vector3(x+1, y+1, z));
			vertices.Add(new Vector3(x+1, y, z));
			
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
		
	}

	public static void makeSlab(int i, int x, int y, int z, List<Vector3> vertices, List<int> indices, List<Vector2> UVs, Block[] adjacentBlocks){
		if(showsAsVisible(adjacentBlocks, (int)Directions.Top) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Front) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Back) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Right) ||
		   showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+0.5f, z));
			vertices.Add(new Vector3(x, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z));
			
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
			
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(0.5f,0));
			UVs.Add (new Vector2(0.5f,1));
			UVs.Add (new Vector2(0,1));
			
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Left)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y+0.5f, z));
			vertices.Add(new Vector3(x, y, z));
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+0.5f, z+1));
			
			indices.Add (vertexIndex);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex+2);
			
			indices.Add (vertexIndex+2);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex);
			
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(0.5f,0));
			UVs.Add (new Vector2(0.5f,1));
			UVs.Add (new Vector2(0,1));
			
		}
		if(showsAsVisible(adjacentBlocks, (int)Directions.Front)){
			int vertexIndex = vertices.Count;
			vertices.Add(new Vector3(x, y, z+1));
			vertices.Add(new Vector3(x, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y+0.5f, z+1));
			vertices.Add(new Vector3(x+1, y, z+1));
			
			indices.Add (vertexIndex+2);
			indices.Add (vertexIndex+1);
			indices.Add (vertexIndex);
			
			indices.Add (vertexIndex);
			indices.Add(vertexIndex+3);
			indices.Add (vertexIndex+2);
			
			UVs.Add (new Vector2(0,0.5f));
			UVs.Add (new Vector2(0,0));
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,0.5f));
			
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
			
			UVs.Add (new Vector2(1,0));
			UVs.Add (new Vector2(1,0.5f));
			UVs.Add (new Vector2(0,0.5f));
			UVs.Add (new Vector2(0,0));
		}
		
	}

}

