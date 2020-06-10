using UnityEngine;
using System.Collections;

public class D_Gen : MonoBehaviour {

	public int level;
	public GameObject Instructions;
	public GameObject SpawnParticle;
	GameObject instruction;

	public GameObject[] rooms = new GameObject[3];
	private D_Room[] rAttributes = new D_Room[3]; 
	private Bounds[] rBounds = new Bounds[3];

	private GameObject[] createdRooms = new GameObject[12]; // this is the room game object
	public D_Room[] cAttributes = new D_Room[12];	//this is to store the script and info about each placed room
	private Bounds[] cBounds = new Bounds[12];


	public void Activate () 	{

		GetRoomAttributes ();

		if (level == 0) {
			MakeStartingRoom ();
			SpawnRooms ();
			if (createdRooms [2] == null)
				Application.LoadLevel ("ControllerBase");
			//		SetDungeonAttributes (); //		for when there are modifiers in the dungeon //if 
			IndicateFinalRoom ();
		}

		if (level == 1) {
			PlaceRoomNoCheck(1, Vector3.zero, true);
			IndicateFinalRoom ();
		}

	}

	void PlaceRoomNoCheck(int roomtype, Vector3 location, bool isBossRoom){
		createdRooms[0] = (GameObject)Instantiate (rooms[roomtype], Vector3.zero, Quaternion.identity); // Make sure Maximum Range is equal to the variation of rooms available
		cAttributes [0] = createdRooms [0].GetComponent<D_Room> ();
		cAttributes [0].startingRoom = true;
		cAttributes [0].isBossRoom = isBossRoom;
		cBounds[0] = cAttributes [0].ObjectBounds();
	}

	private void GetRoomAttributes()	{

		for (int i = 0; i < rooms.Length; i++) 
		{
			rAttributes [i] = rooms [i].GetComponent<D_Room> ();
			rBounds[i] = rAttributes[i].ObjectBounds();
		}
	}

	 void MakeStartingRoom ()	{
		int r = Random.Range (0, 3);
		createdRooms[0] = (GameObject)Instantiate (rooms[r], Vector3.zero, Quaternion.identity); // Make sure Maximum Range is equal to the variation of rooms available
		cAttributes [0] = createdRooms [0].GetComponent<D_Room> ();
		cAttributes [0].startingRoom = true;
		cBounds[0] = cAttributes [0].ObjectBounds();
		instruction = (GameObject)Instantiate(Instructions, gameObject.transform.position, Quaternion.identity);
	}


	private	void SpawnRooms()	{
		int j = 0;
		int q = -1;

		while(j < 8 && q != j)
		{
			q = j;
			int r = Random.Range(0, 3);		// Make sure Maximum Range is equal to the variation of rooms available

			for (int i = 0; i < 4; i++) {
				if ((Random.value > 0.5) && (cAttributes[q].ePoint[i] == false)) {
					switch (i) {
					case 0:
						if (cAttributes [q].ePoint [i] == false) {
							if (placementCheck (j, q, r, i)) {
								j++;
								createdRooms [j] = (GameObject)Instantiate (rooms [r], SpawnLocation (q, r, i), Quaternion.identity);
								cAttributes [j] = createdRooms [j].GetComponent<D_Room> ();
								cBounds [j] = cAttributes [j].ObjectBounds ();
								cAttributes [j].ePoint [2] = true;
								cAttributes[q].ePoint [i] = true;
							}
						}
						break;

					case 1:
						if (cAttributes [q].ePoint [i] == false) {
							if (placementCheck (j, q, r, i)) {
								j++;
								createdRooms [j] = (GameObject)Instantiate (rooms [r], SpawnLocation (q, r, i), Quaternion.identity);
								cAttributes [j] = createdRooms [j].GetComponent<D_Room> ();
								cBounds [j] = cAttributes [j].ObjectBounds ();
								cAttributes [j].ePoint [3] = true;
								cAttributes[q].ePoint [i] = true;
							}
						}
						break;

					case 2:
						if (cAttributes [q].ePoint [i] == false) {
							if (placementCheck (j, q, r, i)) {
								j++;
								createdRooms [j] = (GameObject)Instantiate (rooms [r], SpawnLocation (q, r, i), Quaternion.identity);
								cAttributes [j] = createdRooms [j].GetComponent<D_Room> ();
								cBounds [j] = cAttributes [j].ObjectBounds ();
								cAttributes [j].ePoint [0] = true;
								cAttributes[q].ePoint [i] = true;
							}
						}
							

						break;
					case 3:
						if (cAttributes [q].ePoint [i] == false) {
							if (placementCheck (j, q, r, i)) {
								j++;
								createdRooms [j] = (GameObject)Instantiate (rooms [r], SpawnLocation (q, r, i), Quaternion.identity);
								cAttributes [j] = createdRooms [j].GetComponent<D_Room> ();
								cBounds [j] = cAttributes [j].ObjectBounds ();
								cAttributes [j].ePoint [1] = true;
								cAttributes[q].ePoint [i] = true;
							}
						}
						break;
					}
				}
				//cAttributes[q].ePoint [i] = true;
			}
		}
	}

	private bool placementCheck (int j, int q, int r, int i)
	{
		bool rValue = true;
		cAttributes [j].overlapping = false; // debug
		int f = q-j;
		int k=0;

		while ( k < j+f ) {
			if (checkSpawnLocation( q, r,  i).Intersects (cBounds [k])){
				cAttributes [k].overlapping = true;	// debug
				rValue = false;
			}
			k++;
		}
		return rValue;
	}

	//creates the bounds to check the location of the new room
	private Bounds checkSpawnLocation( int q, int r, int i)	{
		Bounds check;
		const float percentageAvoidCorners = 0.8f;
		check = new Bounds(SpawnLocation (q, r, i), new Vector3(rooms[r].transform.localScale.x * percentageAvoidCorners, rooms[r].transform.localScale.y * percentageAvoidCorners, 1f));
		return check;
	}

	private Vector3 SpawnLocation(int q, int r, int i)	{
		Vector3 location;
		switch (i) {
		case 0:
			location = new Vector3 ((rAttributes [r].width / 2) + (cAttributes [q].width / 2), 0, 0) + createdRooms [q].transform.position;		//right
			break;
		case 1:
			location = new Vector3 (0, -((rAttributes [r].height / 2) + (cAttributes [q].height / 2)), 0) + createdRooms [q].transform.position;//down
			break;

		case 2:
			location = new Vector3 (-((rAttributes [r].width / 2) + (cAttributes [q].width / 2)), 0, 0) + createdRooms [q].transform.position;	//left
			break;
		case 3:
			location = new Vector3 (0, (rAttributes [r].height / 2) + (cAttributes [q].height / 2), 0) + createdRooms [q].transform.position;	//up
			break;

			default:
			location = new Vector3 (0, 0, 0);
			break;
		}
		return location;
	}

		void SetDungeonAttributes ()
		{
	//		for when there are modifiers in the dungeon	//might not be the right place for this function
	//		cAttributes[0].applyRoomDecor();
		}
	

	public GameObject SpawnPlayer(GameObject player)
	{
		GameObject nPlayer = (GameObject)Instantiate(player, new Vector3 (0, 0, -1.5f) + createdRooms[0].transform.position, Quaternion.identity);
//		createdRooms[0] = (GameObject)Instantiate (rooms[r], Vector3.zero, Quaternion.identity)
		Instantiate (SpawnParticle, nPlayer.transform.position, Quaternion.identity);
		return nPlayer;
	}

	void IndicateFinalRoom()
	{
		int FinalExistingRoom=0;
		for (int i = 0; 12 > i; i++) {
			if ( createdRooms[i] != null) {
				FinalExistingRoom = i;
			}
		}
		cAttributes [FinalExistingRoom].finalRoom = true;
		
	}

	public void RemoveAllRooms(){
		for (int i = 0; 12 > i; i++) {
			if ( createdRooms[i] != null) {

				createdRooms[i].GetComponent<D_Room>().killRoom();


				createdRooms[i] = null;
			}
		}
		DestroyImmediate (instruction);
	}




}