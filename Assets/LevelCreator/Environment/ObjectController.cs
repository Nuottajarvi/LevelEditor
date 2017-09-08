using UnityEngine;
using System.Collections;

public class ObjectController : MonoBehaviour{

	private static ObjectController objectController;

	public const int BRICKS = 0;
	public const int GRASS = 1;
	public const int SAND = 2;
	public const int ROCK = 3;
	public const int STONE = 4;
	public const int SANDSTONE = 5;
	public const int SNOW = 6;
	public const int STEEL = 7;
	public const int WATER = 8;
	public const int WOOD = 9;
	public const int LAVA = 10;
	public const int ICE = 11;

	public const int BLOCK = 0;
	public const int STAIRS = 1;
	public const int SLOPE = 2;
	public const int SMOOTH = 3;
	public const int SLAB = 4;
	public const int LOWSLOPE = 5;
	public const int SMOOTHBORDER = 6;
	
	public int shape;
	public int texture;

	void Start(){
		objectController = this;
	}

	public void switchTexture(int texture){
		this.texture = texture;
	}

	public void switchPiece(int shape){
		this.shape = shape;
	}

	public static ObjectController getInstance(){
		return objectController;
	}
}
