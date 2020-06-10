using UnityEngine;
using System.Collections;

public class WallScript : MonoBehaviour {

	Renderer rend;
	public Sprite[] walls;
	GameObject player;
	GameObject gameController;
	int wallDirection;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player(Clone)");
		gameController = GameObject.Find ("GameController");
		rend = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void FixedUpdate()
	{
		if (hitPlayer ())
			player.GetComponent<PlayerControllerNew> ().collisionDirection[wallDirection] = true;


		for (int i = 0; 20 > i; i++) {
			if (gameController.GetComponent<GameController> ().eAttributes [i] != null) {
				if (hitenemy(i))
					gameController.GetComponent<GameController> ().eAttributes [i].collisionDirection[wallDirection] = true;
				}
			}

		for (int i = 0; 100 > i; i++) {
		if (gameController.GetComponent<GameController> ().bAttributes [i] != null) {
				if (hitbullet(i))
				{
				
									gameController.GetComponent<GameController> ().bAttributes [i].destroyBullet();
				}
			}
		}

		for (int i = 0; 50 > i; i++) {
			if (gameController.GetComponent<GameController> ().pAttributes [i] != null) {
				if (hitPickup(i))
				{
					gameController.GetComponent<GameController> ().pAttributes [i].collisionDirection[wallDirection] = true;
				}
			}
		}

	}
	


	public void setWall(int direction, int width, int height)
	{
	
		if (direction == 0) { 
			wallDirection = 0;
			if (height == 12)
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [3];
			else
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [7];
		}

		if (direction == 1) {
			wallDirection = 1;
			if (width == 12)
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [1];
			else
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [5];
		}

		if (direction == 2) {
			wallDirection = 2;
			if (height == 12)
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [2];
			else
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [6];
		}

		if (direction == 3) {
			wallDirection = 3;
			if (width == 12)
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [0];
			else
				gameObject.GetComponent<SpriteRenderer> ().sprite = walls [4];
		}


	}

	public Bounds ObjectBounds()
	{
		return rend.bounds;
	}

	bool hitbullet(int i)
	{
		Bounds checkbounds;
		//checkbounds = ObjectBounds ();
		checkbounds = gameController.GetComponent<GameController> ().bAttributes [i].ObjectBounds ();
		checkbounds.Expand (new Vector3 (0.01f, 0.01f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	bool hitenemy(int i)
	{
		Bounds checkbounds;
		checkbounds = gameController.GetComponent<GameController> ().eAttributes [i].EnemyBounds ();
		checkbounds.Expand (new Vector3 (0.01f, 0.01f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

 	bool hitPlayer()
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0.01f, 0.01f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	bool hitPickup(int i)
	{
		Bounds checkbounds;
		checkbounds = gameController.GetComponent<GameController>().pAttributes[i].ObjectBounds();
		checkbounds.Expand (new Vector3 (0.01f, 0.01f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	public void removeWalls(){
		Destroy (gameObject);
	}
}
