using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour {

	public GameObject DungeonSpawner;
	private D_Gen DSpawner;

	public GameObject Player;
	private GameObject cPlayer;
	private PlayerControllerNew nPlayer;

	public GameObject boss;

	public GameObject enemy;
	public GameObject[] enemies = new GameObject[20];
	public EnemyAI[] eAttributes = new EnemyAI[20];

	public GameObject bullet;
	public GameObject[] bullets = new GameObject[100];
	public Bullet[] bAttributes = new Bullet[100];

	public GameObject pickup;
	public GameObject[] pickups = new GameObject[50];
	public Pickup[] pAttributes;

	private Bounds bPlayer;

	public int gameType;

	void Start () 	{
		gameObject.GetComponent<AudioSource> ().Play ();
		DSpawner = DungeonSpawner.GetComponent<D_Gen> ();
			SpawnLevel();
			cPlayer = DSpawner.SpawnPlayer (Player);
			nPlayer = cPlayer.GetComponent<PlayerControllerNew> ();
			bPlayer = nPlayer.PlayerBounds ();
	}

	public void SpawnLevel(){
		DSpawner.level = gameType;
		DSpawner.Activate ();
	}

	public void removeDungeon(){
		gameObject.GetComponent<AudioSource>().Pause();
		//kill all enemies
		for (int i = 0; 20 > i; i++) {
			if (eAttributes [i] != null) {
				eAttributes[i].Die();
				eAttributes[i] = null;
			}
		}
		//remove all rooms
		DSpawner.RemoveAllRooms ();

		//remove all pickups
		for (int i = 0; 50 > i; i++) {
			if (pickups [i] != null) {
				Destroy(pickups[i]);
				pickups[i] = null;
			}
		}
	}

	public void spawnBullet(Vector3 origin, Vector3 direction, float moveSpeed, int bulletType, bool isBoss)
	{
		bool check = true;
		for(int i = 0;(99>i)&& check; i++)
		{
			if (bullets[i] == null)
			{
				bullets[i] = (GameObject)Instantiate(bullet, origin, Quaternion.identity);
				bAttributes[i] = bullets[i].GetComponent<Bullet>();
				Vector3 dir = direction-origin;
				dir.Normalize();
				bAttributes[i].setDirection(dir);
				bAttributes[i].setbullet(bulletType);
				bAttributes[i].bossBullet = isBoss;
				//bullets[i].transform.LookAt (cPlayer.transform.position, new Vector3 (0, 0, 1));
				bAttributes[i].changeRotation(dir);
				check = false;
			}
		}
	}

	public void spawnBoss(Vector3 origin)
	{
		
		bool check = true;
		for(int i = 0;(19>i) && check ; i++)
		{
			if (enemies[i] == null)
			{
				check = false;
				enemies[i] = (GameObject)Instantiate(boss, origin, Quaternion.identity);
				//eAttributes[i] = enemies[i].GetComponent<EnemyAI>();
				//eAttributes[i].bulletSpeed = bulletSpeed;
				//eAttributes[i].moveSpeed = characterMoveSpeed;
				
			}
		}
	}

	public void spawnEnemy(Vector3 origin, float bulletSpeed, float characterMoveSpeed, int enemyType)
	{

		bool check = true;
		for(int i = 0;(19>i) && check ; i++)
		{
			if (enemies[i] == null)
			{
				check = false;
				enemies[i] = (GameObject)Instantiate(enemy, origin, Quaternion.identity);
				eAttributes[i] = enemies[i].GetComponent<EnemyAI>();
				eAttributes[i].bulletSpeed = bulletSpeed;
				eAttributes[i].moveSpeed = characterMoveSpeed;

			}
		}
	}

	public void spawnPickup(Vector3 origin, int type)
	{
		
		bool check = true;
		for(int i = 0;(49>i) && check ; i++)
		{
			if (pickups[i] == null)
			{
				check = false;
				pickups[i] = (GameObject)Instantiate(pickup, origin, Quaternion.identity);
				pAttributes[i] = pickups[i].GetComponent<Pickup>();
				pAttributes[i].setPickup(type);

				
			}
		}
	}

	public void resetPlayerPosition(){
		cPlayer.transform.position = new Vector3 (0, 0, -1.5f);
		GameObject.Find ("Main Camera").transform.position = new Vector3 (0, 1, -10);
	}

	void FixedUpdate()
	{
		
	}

}
