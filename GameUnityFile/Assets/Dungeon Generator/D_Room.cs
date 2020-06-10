using UnityEngine;
using System.Collections;

public class D_Room : MonoBehaviour 
{
	public float width = 1;
	public float height = 1;

	int minEnemiesPerWave = 2;

	public LayerMask playerMask;

	public bool[] ePoint = new bool[4];

	public bool overlapping;// for debug

	public bool playerIsInRoom = false;

	public bool startingRoom = false;

	public bool finalRoom = false;

	public bool isBossRoom = false;

	public GameObject TrapDoor;

	public GameObject Chest;

	GameObject reward;

	bool activated = false;
	bool inUse = false;
	bool roomCleared = false;

	public GameObject door;//for doors
	public GameObject[] cDoors; // for the spawned doors

	public GameObject wall;//for all the walls
	public GameObject[] cWalls; // for the spawned doors

	Renderer roomRender;
	GameObject gameController;

	int enemyWave = 0;


	void Start()
	{
		roomRender = GetComponent<Renderer>();
		gameController = GameObject.Find ("GameController");
		spawnWallsAndDoors ();
	}

	void FixedUpdate()
	{
		if (isBossRoom && !inUse){
			gameController.GetComponent<GameController>().spawnBoss(this.transform.position + new Vector3(0, height / 2 +10, -1.5f));
			inUse = true;
		}

		if (playerIsInRoomBounds () && !activated && !startingRoom) {
			activated = true;
			Activate ();
		}

	if (enemyWave < 2 && activated) {
		bool nextwave = true;
		for (int i = 0; (19>i); i++) {
			if (gameController.GetComponent<GameController> ().enemies [i] != null) {
				nextwave = false;
			}
			}
			if (nextwave) {
				spawnEnemies ();
				enemyWave += 1;
			}
	}

		if (activated && (enemyWave >= 2) && !roomCleared) {
			roomCleared = true;
			for (int i = 0; (19>i); i++) {
				if (gameController.GetComponent<GameController> ().enemies [i] != null) {
					roomCleared = false;
				}
			}
			if (roomCleared)
			{
				unlockDoors();
				if ((Random.value < 0.5f) && !finalRoom)
					SpawnChest();
		//		debugCleared ();
			}
			if (roomCleared && finalRoom)
				SpawnTrapDoor();
		}

	}

	public Bounds ObjectBounds()
	{
		Renderer rend = gameObject.GetComponent<Renderer> ();
		return rend.bounds;

	}


	public void Activate()
	{	
		lockDoors ();
		//debugActivate ();
		spawnEnemies ();
	}

	void unlockDoors()
	{
		for (int i = 0; i < 4; i++) {
			if(cDoors[i] != null)
			{
				cDoors[i].GetComponent<DoorScript>().doorLocked = false;
				cDoors[i].GetComponent<DoorScript>().transition = true;
			}
		}
	}

	void lockDoors()
	{
		for (int i = 0; i < 4; i++) {
			if(cDoors[i] != null){
				cDoors[i].GetComponent<DoorScript>().doorLocked = true;
				cDoors[i].GetComponent<DoorScript>().transition = true;
			}
		}
	}

	void debugActivate()
	{
		roomRender.material.color = new Color (1, 0, 0);
	}

	void debugCleared()
	{
		roomRender.material.color = new Color (0, 1, 0);
	}

	void spawnWallsAndDoors ()//might close doors
	{

		cWalls [0] = (GameObject)Instantiate (wall, transform.position + new Vector3(width/2-0.6f,0,-1), Quaternion.identity);	//right
		cWalls[0].GetComponent<WallScript>().setWall(0,(int)width, (int)height);

		if (ePoint [0] == true) {
			cDoors [0] = (GameObject)Instantiate (door, transform.position + new Vector3 (width / 2 - 0.7f, 0, -1.1f), Quaternion.identity);	//right
			cDoors [0].GetComponent<DoorScript> ().setDoor (0);
		}

		cWalls [1] = (GameObject)Instantiate (wall, transform.position - new Vector3(0, height/2-0.6f,1), Quaternion.identity);	//down
		cWalls[1].GetComponent<WallScript>().setWall(1,(int)width, (int)height);

		if (ePoint [1] == true) {
			cDoors [1] = (GameObject)Instantiate (door, transform.position - new Vector3 (0, height / 2 - 0.7f, 1.1f), Quaternion.identity);	//down
			cDoors [1].GetComponent<DoorScript> ().setDoor (1);
		}

		cWalls [2] = (GameObject)Instantiate (wall, transform.position - new Vector3(width/2-0.6f,0,1), Quaternion.identity);	//left
		cWalls[2].GetComponent<WallScript>().setWall(2,(int)width, (int)height);

		if (ePoint [2] == true) {
			cDoors [2] = (GameObject)Instantiate (door, transform.position - new Vector3 (width / 2 - 0.7f, 0, 1.1f), Quaternion.identity);	//left
			cDoors [2].GetComponent<DoorScript> ().setDoor (2);
		}


			cWalls [3] = (GameObject)Instantiate (wall, transform.position + new Vector3 (0, height / 2 - 0.6f, -1), Quaternion.identity);	//up
			cWalls [3].GetComponent<WallScript> ().setWall (3, (int)width, (int)height);

			if (ePoint [3] == true) {
				cDoors [3] = (GameObject)Instantiate (door, transform.position + new Vector3 (0, height / 2 - 0.7f, -1.5f), Quaternion.identity);	//up
				cDoors [3].GetComponent<DoorScript> ().setDoor (3);
			}
		if (isBossRoom) {
			cWalls[3].GetComponent<SpriteRenderer>().enabled = false;		
		}
	}

	void spawnEnemies()
	{
		int amount = Random.Range (2, 5);
		for (int i = 0; i < amount; i++) {

			int randx = Random.Range ((int)-width/2 +2, (int)width/2 -2);
			int randy = Random.Range ((int)-height/2 +2, (int)height/2 -2);
			int enemytype = Random.Range (0,1);
			gameController.GetComponent<GameController>().spawnEnemy(this.transform.position + new Vector3(randx,randy, 0f), 4f+enemytype, 2f, enemytype);
		}
		//most likely a set of template gameobjects that hold spawnlocation
	}

	bool playerIsInRoomBounds()
	{
		Debug.DrawRay ((Vector3)this.transform.position, new Vector3 (0, 0, -10));

		Bounds checkbounds;
		checkbounds = GameObject.Find ("Player(Clone)").GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	void SpawnChest(){
		reward = (GameObject)Instantiate (Chest, this.transform.position + new Vector3(0f,0f,-0.5f), Quaternion.identity);
	}


	void SpawnTrapDoor()
	{
		reward = (GameObject)Instantiate (TrapDoor, this.transform.position + new Vector3(0f,0f,-0.5f), Quaternion.identity);
	}


	public void killRoom(){
		//remove doors
		for (int i = 0; i < 4; i++) {
			if(cDoors[i] != null){
				cDoors[i].GetComponent<DoorScript>().removeDoor();
				cDoors[i] = null;
			}
		}
		//remove walls
		for (int i = 0; i < 4; i++) {
			if(cWalls[i] != null){
				cWalls[i].GetComponent<WallScript>().removeWalls();
				cWalls[i] = null;
			}
		}
		//remove room Contents
		if (reward != null) {
			Destroy(reward.gameObject);
			reward = null;
		}

		Destroy (gameObject);
	}




}