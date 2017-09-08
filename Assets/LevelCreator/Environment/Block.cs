using UnityEngine;
/*
public class Block{
	
	public int texture;
	public int shape;
	ObjectController objectController;

	public GameObject objectReference;
	public Vector3 position;
	public Vector3 rotation;
	
	public Block (int shape, int texture){
		this.objectController = ObjectController.getInstance();
		this.shape = shape;
		this.texture = texture;

	}

	public GameObject toGameObject(){
		GameObject blockToCreate = objectController.shapes[shape];
		Renderer renderer = blockToCreate.GetComponent<Renderer>();
		renderer.material = objectController.materials[texture];
		return blockToCreate;
	}

	public int getRotation(){
		Vector3 rot = objectReference.transform.rotation.eulerAngles;
		return (int)(rot.y / 90);
	}

	public void saveRotation(int rot){
		rotation = new Vector3(270, rot * 90, 0);
	}

	public void setRotation(){
		objectReference.transform.rotation = Quaternion.Euler(rotation);
	}
}*/

public struct Block{
	public readonly byte shape;
	public readonly byte texture;
	public readonly byte rotation;

	public Block(int shape, int texture, int rotation){
		this.shape = (byte)shape;
		this.texture = (byte)texture;
		this.rotation = (byte)rotation;
	}
}



