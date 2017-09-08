using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerraformRenderer{

	static int sideWidth = 128;

	public static void RenderAll(Dictionary<Vector2, float> terraforms){
		Vector3[] vertices = new Vector3[sideWidth*sideWidth];
		
		for(int x = 0; x < sideWidth; x++){
			for(int y = 0; y < sideWidth; y++){
				float height = TerraformController.defaultHeight;
				if(terraforms.ContainsKey(new Vector2(x,y))){
					height = terraforms[new Vector2(x,y)];
				}

				vertices[y + x * sideWidth] = new Vector3(x, height, y);
			}
		}
		
		int[] indices = new int[(sideWidth - 1) * (sideWidth - 1) * 6];
		
		for(int x = 0; x < sideWidth - 1; x++){
			for(int y = 0; y < sideWidth - 1; y++){
				int location = x * sideWidth + y;
				int index = x * (sideWidth - 1) + y;
				indices[index * 6] = location;
				indices[index * 6 + 1] = location + 1;
				indices[index * 6 + 2] = location + sideWidth + 1;
				
				indices[index * 6 + 3] = location;
				indices[index * 6 + 4] = location + sideWidth + 1;
				indices[index * 6 + 5] = location + sideWidth;
			}
		}

		Vector2[] uvs = new Vector2[sideWidth*sideWidth];

		for(int x = 0; x < sideWidth; x++){
			for(int y = 0; y < sideWidth; y++){
				uvs[y + x * sideWidth] = new Vector2(1 /(float)sideWidth * x, 1 / (float)sideWidth * y);
			}
		}
			
		Mesh mesh = new Mesh();
		mesh.vertices = vertices;
		mesh.triangles = indices;
		mesh.uv = uvs;
		mesh.RecalculateNormals();
		GameObject tfc = GameObject.Find ("TerrainChunk") as GameObject;
		MeshCollider meshcollider = tfc.GetComponent<MeshCollider>();
		meshcollider.sharedMesh = mesh;
		tfc.GetComponent<MeshFilter>().mesh = mesh;
	}
}
