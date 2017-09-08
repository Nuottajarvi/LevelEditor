using System;
using UnityEngine;

public class GameObjectNotation{
	
	public int prefab;
	public Vector3 position;
	public Quaternion rotation;

	public GameObjectNotation (int prefab, Vector3 position, Quaternion rotation){
		this.prefab = prefab;
		this.position = position;
		this.rotation = rotation;
	}
}

