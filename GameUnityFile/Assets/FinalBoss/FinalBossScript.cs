using UnityEngine;
using System.Collections;

public class FinalBossScript : MonoBehaviour {
	
	public float moveSpeed = 2f;			//default movementspeed
	public float runSpeed = 2f;
	
	float maxDistance = 4f;
	float minDistance = 1f;

	public GameObject[] BossSounds;

	public float bulletSpeed= 3f;
	
	int lives = 10;
	
	public GameObject target;
	
	public GameObject bullet;
	
	Vector3 movement;

	Renderer enemyRenderer;
	
	public int decision = 0;
	
	GameObject gameController;
	
	Vector3 changePosition;
	Vector3 lastPosition;
	bool attacking = false;
	
	Vector3 previousPosition;

	public bool[] collisionDirection;
	
	Animator anim;

	bool changingEncounter = true;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		target = GameObject.Find ("Player(Clone)");
		gameController = GameObject.Find ("GameController");
		
		enemyRenderer = GetComponent<Renderer> ();

		StartCoroutine (descend());

	}

	IEnumerator descend(){
		anim.Play ("AcsendAnim");
		changePosition = new Vector3 (0, -1, 0);
		yield return new WaitForSeconds (4f);
		StartCoroutine (AnimateSpider (.15f));
		StartCoroutine(CreateDecision(1));
		changingEncounter = false;
	}

	void FixedUpdate()
	{
		if (!changingEncounter) {
			movement = transform.position - lastPosition;
			changePosition = Vector3.zero;//reset enemy translation direction
		
			runAiEngagementRoutines ();

			checkCollision ();
		
			changePosition.z = 0;//prevent translation on z co-ordinates
		}
			transform.position += changePosition * Time.deltaTime * moveSpeed;
			lastPosition = transform.position;
		
	}

	void checkCollision(){
		if (gameObject.transform.position.x < -3.34f && changePosition.x <0)
			changePosition = Vector3.zero;

		   if( gameObject.transform.position.x > 3.3f && changePosition.x > 0)
			changePosition = Vector3.zero;
	}

	void MoveInDirection(Vector3 dir){
		dir.Normalize();
		movement = dir;
		changePosition = dir;
	}
	
	IEnumerator CreateDecision(float waitTime)
	{
		while(true){ //this makes the loop itself
			//do stuff
			yield return new WaitForSeconds(waitTime);
			//if you want to stop the loop, use: break;
			decision = Random.Range (0, 3);
		}
	}    

	void runAiEngagementRoutines()
	{

			switch (decision) {
			case 0:
				Attack();
				break;
			case 1:
			MoveInDirection(Vector3.right);
				break;
			case 2:
			MoveInDirection (Vector3.left);
				break;
			default:
				break;
			}
		
		
		
		if (lives < 1)
			Die ();
	}
	
	void Die(){
		if (Random.value < 0.1)
			gameController.GetComponent<GameController> ().spawnPickup(this.transform.position, 0);
		Destroy(this.gameObject);
	}
	
	public void changeHealth(int amount)
	{
		lives += amount;
		if (lives < 0)
			lives = 0;
		if (lives == 0) {
			
			Instantiate (BossSounds[0]);
		} 
		else
			Instantiate (BossSounds[1]);
	}
	
	public Bounds EnemyBounds()
	{
		Renderer rend = gameObject.GetComponent<Renderer> ();
		return rend.bounds;
	}
	
	void Attack()
	{
		if (!attacking) {
			attacking = true;
			StartCoroutine(AttackPlayer(1f));
		}
	}
	
	IEnumerator AttackPlayer(float waitTime)
	{
		target = GameObject.Find ("Player(Clone)");
		int attackType = Random.Range (0, 2);
		if (attackType == 0) { 
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 2, true);
			yield return new WaitForSeconds (waitTime/4);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 2, true);
			yield return new WaitForSeconds (waitTime/4);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 2, true);
			yield return new WaitForSeconds (waitTime/4);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 2, true);
			yield return new WaitForSeconds (waitTime/4);
			attacking = false;
		}
		if (attackType == 1) { 
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(5,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(-5,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(9,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(-9,0,0), bulletSpeed, 3, true);
			yield return new WaitForSeconds (waitTime);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position, bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(6,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(-6,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(10,0,0), bulletSpeed, 3, true);
			gameController.GetComponent<GameController> ().spawnBullet (this.transform.position +new Vector3(0,-5.5f,0), target.transform.position+new Vector3(-10,0,0), bulletSpeed, 3, true);
			yield return new WaitForSeconds (waitTime);
			attacking = false;
		}
	}
	



	float distanceToTarget()
	{
		return Vector3.Distance (this.transform.position, target.transform.position);
	}
	
	IEnumerator AnimateSpider(float waitTime)
	{
		while (true) {
			if ((movement != Vector3.zero)) {
				anim.Play ("WalkAnim");
			}
			if ((movement == Vector3.zero)) {
				anim.Play ("IdleAnim");
			}
			if(attacking){
				anim.Play("AttackAnim");
			}
			yield return new WaitForSeconds (waitTime);
		}
	}

}