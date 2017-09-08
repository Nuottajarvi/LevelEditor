using UnityEngine;
using System.Collections;

public class LevelCreatorCamera : MonoBehaviour
{
	
	public float cameraSensitivity = 90;
	private float moveSpeed = 30;
	
	private float minimumZoom = 5f;
	private float maximumZoom = 100f;	
	private float zoomSpeed = 1000f;

	private Vector3 forwardVector = new Vector3(0,0,1);

	void Update ()
	{		
		transform.position += forwardVector * moveSpeed * Input.GetAxis("Vertical") * Time.deltaTime;
		transform.position += transform.right * moveSpeed * Input.GetAxis("Horizontal") * Time.deltaTime;

		transform.position -= Vector3.up * Input.GetAxis ("Mouse ScrollWheel") * Time.deltaTime * zoomSpeed;

		float y = transform.position.y;
		y = Mathf.Clamp(y, minimumZoom, maximumZoom);
		y = transform.position.y - y;
		transform.position -= new Vector3(0,y,0);

		if(Input.GetKeyDown(KeyCode.Q) || Input.GetKeyDown(KeyCode.E)){
			Ray cameraRay = new Ray(transform.position, transform.forward);
			RaycastHit floorHit;
			float camRayLength = 100f;
			
			if (Physics.Raycast (cameraRay, out floorHit, camRayLength, LayerMask.GetMask("Floor"))) {

				float degree = -90;
				if(Input.GetKeyDown(KeyCode.Q)) degree = 90;
				transform.RotateAround(floorHit.point, Vector3.up, degree);
				forwardVector = Quaternion.AngleAxis(degree, Vector3.up) * forwardVector;
			}
		}
	}

	Vector3 getRealForward(Vector3 f){


		return Vector3.one;
	}
}