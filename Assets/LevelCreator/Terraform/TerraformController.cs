using UnityEngine;
using System.Collections;

public class TerraformController : MonoBehaviour{
	private static TerraformController terraformController;
	
	public const int UP = 0;
	public const int DOWN = 1;
	public const int AUTO = 2;
	public const int SMOOTH = 3;
	public const int RESET = 4;
	
	public int tool;

	public static readonly int defaultHeight = 15;
	
	void Start(){
		terraformController = this;
	}

	public static TerraformController getInstance(){
		return terraformController;
	}

	public void switchTool(int tool){
		this.tool = tool;
	}
}
