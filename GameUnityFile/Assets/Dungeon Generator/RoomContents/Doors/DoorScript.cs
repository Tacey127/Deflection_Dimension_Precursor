using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public Sprite[] doors;
	public Sprite[] lockedDoors;
	int DoorDirection; 
	Renderer rend;
	GameObject player;
	public bool doorLocked = false;
	public bool transition = false;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player(Clone)");
		rend = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (!doorLocked) {
			if (transition){
				setDoor(DoorDirection);
				transition = false;
			}
			if (hitPlayer ())
				player.GetComponent<PlayerControllerNew> ().doorDetection [DoorDirection] = true;
		}
		if (doorLocked)
		if (transition) {
			lockDoors();
			transition = false;
		}

	}
	//right down left up
	void lockDoors()
	{
		if (DoorDirection == 0) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = lockedDoors [2];
		}
		
		if (DoorDirection == 1) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = lockedDoors [0];
		}
		
		if (DoorDirection == 2) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = lockedDoors [1];
		}
		
		if (DoorDirection == 3) {
			gameObject.GetComponent<SpriteRenderer> ().sprite = lockedDoors [3];
		}
	}

	public void setDoor(int doorSide)
	{
		if (doorSide == 0) {
			DoorDirection = 0;
			gameObject.GetComponent<SpriteRenderer> ().sprite = doors [2];
		}

		if (doorSide == 1) {
			DoorDirection = 1;
			gameObject.GetComponent<SpriteRenderer> ().sprite = doors [3];
		}

		if (doorSide == 2) {
			DoorDirection = 2;
			gameObject.GetComponent<SpriteRenderer> ().sprite = doors [1];
		}

			if (doorSide == 3) {
			DoorDirection = 3;
			gameObject.GetComponent<SpriteRenderer> ().sprite = doors [0];
		}
	}

	public void removeDoor(){
		Destroy (gameObject);
	}

	public Bounds ObjectBounds()
	{
		return rend.bounds;
	}
	
	bool hitPlayer()
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0.1f, 0.1f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}
}
