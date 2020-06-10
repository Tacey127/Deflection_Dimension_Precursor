using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour {

	public GameObject SkelParticle;
	public GameObject GobParticle;

	public GameObject[] KillSound;

	public float moveSpeed = 2f;			//default movementspeed
	public float runSpeed = 2f;

	 float maxDistance = 4f;
 	float minDistance = 2f;

	public float bulletSpeed= 3f;

	public int lives = 1;

	public Sprite[] sprites;

	public GameObject target;

	public GameObject bullet;

	Vector3 movement;

	Vector3 lastPosition;
	Renderer enemyRenderer;

	public int decision = 0;

	GameObject gameController;

	Vector3 changePosition;

	bool attacking = false;

	public GameObject pickup;
	public int enemyType = 0;

	Vector3 previousPosition;
	public bool[] collisionDirection;

	Animator anim;
	bool horizontalOrVertical = false;
	bool positive = false;
	// Use this for initialization
	void Start()
	{
		anim = GetComponent<Animator>();
		target = GameObject.Find ("Player(Clone)");
		gameController = GameObject.Find ("GameController");

		enemyRenderer = GetComponent<Renderer> ();

		enemyType = Random.Range (0, 2);
		if (enemyType == 0) {
			StartCoroutine (AnimateSketeton (.15f));
		} else {
			StartCoroutine (AnimateGoblin (.15f));
		}


		StartCoroutine(CreateDecision(1));
	}

	void FixedUpdate()
	{
		movement = transform.position - lastPosition;
		changePosition = Vector3.zero;//reset enemy translation direction


		runAiEngagementRoutines ();

		checkCollision ();

		changePosition.z = 0;//prevent translation on z co-ordinates
		transform.position += changePosition *Time.deltaTime * moveSpeed;
		lastPosition = transform.position;
	}

	void MoveToTarget()
	{
			Vector3 dir = target.transform.position - transform.position;
			dir.Normalize();
			movement = dir;
			changePosition = dir;
		//	movement = -transform.position - target.transform.position;
		//changePosition = Vector3.MoveTowards (transform.position, target.transform.position, Time.deltaTime * moveSpeed);
	}

	void MoveFromTarget()
	{
		Vector3 dir = transform.position-target.transform.position;
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

	void checkCollision()
	{
		if (collisionDirection [0] == true)
			if (changePosition.x > 0)
				changePosition.x = 0;
		
		if (collisionDirection [1] == true)
			if (changePosition.y < 0)
				changePosition.y = 0;
		
		if (collisionDirection [2] == true)
			if (changePosition.x < 0)
				changePosition.x = 0;
		
		if (collisionDirection [3] == true)
			if (changePosition.y > 0)
				changePosition.y = 0;
		
		collisionDirection [0] = false;
		collisionDirection [1] = false;
		collisionDirection [2] = false;
		collisionDirection [3] = false;
	}

	void runAiEngagementRoutines()
	{
		if (distanceToTarget () > maxDistance) {// if far away
			switch (decision) {
			case 0:
				Attack();
				break;
			case 1:
				MoveToTarget ();
				break;
			case 2:
				MoveToTarget();
				break;
			default:
				break;
			}
		}
		
		if ((distanceToTarget() < maxDistance) && (distanceToTarget() > minDistance)) //if in middle
		{
			switch (decision) {
			case 0:
				MoveFromTarget();
				break;
			case 1:
				Attack();
				break;
			case 2:
				MoveToTarget ();
				break;
			default:
				break;
			}
			//	Attack();
		}
		
		if ((distanceToTarget () < minDistance) && (distanceToTarget()< maxDistance)){// if close up
			switch (decision) {
			case 0:
				MoveFromTarget();
				break;
			case 1:
				Attack();
				break;
			case 2:
				Attack ();
				break;
			default:
				break;
			}
		}
		
		if (lives < 1)
			Die ();
	}

	public void Die(){
		if (Random.value < 0.1)
			gameController.GetComponent<GameController> ().spawnPickup(this.transform.position, 0);
		if (enemyType == 0) {
			Instantiate (SkelParticle, gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
		} else {
			Instantiate (GobParticle, gameObject.transform.position + new Vector3(0, 0, -1), Quaternion.identity);
		}
		Destroy(this.gameObject);

	}

	public void changeHealth(int amount)
	{
		lives += amount;
		if (lives < 0)
			lives = 0;
		if (lives == 0) {
			
			Instantiate (KillSound [0]);
		} 
		else
			Instantiate (KillSound [1]);
	
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
		gameController.GetComponent<GameController> ().spawnBullet (this.transform.position, target.transform.position, bulletSpeed, enemyType, false);
		yield return new WaitForSeconds(waitTime);
		attacking = false;
	}


	float distanceToTarget()
	{
		return Vector3.Distance (this.transform.position, target.transform.position);
	}

	IEnumerator AnimateGoblin(float waitTime)
	{
		while (true) {
			if ((movement.y > movement.x) && (movement.y > -movement.x)) {
				anim.Play ("GWalkingBack");
				horizontalOrVertical = false;
				positive = true;
			} else if ((-movement.y > movement.x) && (-movement.y > -movement.x)) {
				anim.Play ("GWalkingFront");
				horizontalOrVertical = false;
				positive = false;
			} else
			if ((movement.x > movement.y) && (movement.x > -movement.y)) {
				anim.Play ("GWalkingRight");
				horizontalOrVertical = true;
				positive = true;
			} else if ((-movement.x > movement.y) && (-movement.x > -movement.y)) {
				anim.Play ("GWalkingLeft");
				horizontalOrVertical = true;
				positive = false;
			}
			
			if ((movement.y == 0) && (movement.x == 0)) {
				if (horizontalOrVertical && positive)
					anim.Play ("GIdleRight");
				if (horizontalOrVertical && !positive)
					anim.Play ("GIdleLeft");
				if (!horizontalOrVertical && positive)
					anim.Play ("GIdleBack");
				if (!horizontalOrVertical && !positive)
					anim.Play ("GIdleFront");
				
			}


			yield return new WaitForSeconds (waitTime);
		}
	}
	
	
	IEnumerator AnimateSketeton(float waitTime)
	{
		while (true) {
			if ((movement.y > movement.x) && (movement.y > -movement.x)) {
				anim.Play ("SWalkingBack");
				horizontalOrVertical = false;
				positive = true;
			} else if ((-movement.y > movement.x) && (-movement.y > -movement.x)) {
				anim.Play ("SWalkingFront");
				horizontalOrVertical = false;
				positive = false;
			} else
			if ((movement.x > movement.y) && (movement.x > -movement.y)) {
				anim.Play ("SWalkingRight");
				horizontalOrVertical = true;
				positive = true;
			} else if ((-movement.x > movement.y) && (-movement.x > -movement.y)) {
				anim.Play ("SWalkingLeft");
				horizontalOrVertical = true;
				positive = false;
			}
			
			if ((movement.y == 0) && (movement.x == 0)) {
				if (horizontalOrVertical && positive)
					anim.Play ("SIdleRight");
				if (horizontalOrVertical && !positive)
					anim.Play ("SIdleLeft");
				if (!horizontalOrVertical && positive)
					anim.Play ("SIdleBack");
				if (!horizontalOrVertical && !positive)
					anim.Play ("SIdleFront");
				
			}
			yield return new WaitForSeconds (waitTime);
			}
	}
}




