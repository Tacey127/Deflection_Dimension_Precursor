using UnityEngine;
using System.Collections;

public class FollowPlayer : MonoBehaviour {

	GameObject player;
	public float speed;
	public GameObject Cursor;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (player == null)
		player = GameObject.Find ("Player(Clone)");

		//this.transform.position = (Input.mousePosition + player.transform.position) / 2;

			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			// create a plane at 0,0,0 whose normal points to +Y:
			Plane hPlane = new Plane(Vector3.forward, Vector3.zero);
			// Plane.Raycast stores the distance from ray.origin to the hit point in this variable:
			float distance = 0; 
			// if the ray hits the plane...
		Vector3 mouse = Vector3.zero;
			if (hPlane.Raycast (ray, out distance)) {
			// get the hit point:
			mouse = ray.GetPoint (distance);
		}

		

		Camera.main.ScreenToWorldPoint (Input.mousePosition);
		transform.Translate(Vector3.Scale(((player.transform.position + mouse) / 2) - transform.position, new Vector3(speed, speed, 0)));
		//transform.Translate(Vector3.Scale(Vector3.Lerp(player.transform.position, Cursor.transform.position, 0.5f) - transform.position, new Vector3(0.1f, 0.1f, 0)));
//		gameObject.transform.position = player.transform.position + new Vector3(0,0,-9);
	}
}
