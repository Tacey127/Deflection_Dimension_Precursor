using UnityEngine;

using System.Collections;

public class PlayerControllerNew : MonoBehaviour {

	public GameObject mirror;

	GameObject cMirror;

	public GameObject PauseMenuObject;

	Vector3 movement;

	public GameObject[] PlayerSounds;

	float speedMultiplier= 1f;
	float moveSpeed = 3f;
	public float runSpeed = 2f;
	public float maxSpeed = 100f;

	public float mirrorDistance;

	public bool[] collisionDirection;
	public bool[] doorDetection;

	int mirrorSize = 0;
	int maxLives = 6;
	int lives = 6;

	public Sprite[] sprites;
	GameObject healthUI;
	Health hScript;
    
    Animator anim;
	Renderer playerRenderer;

	bool Dead = false;
	SpriteRenderer playerSprite;
    bool horizontalOrVertical = false;
    bool positive = false;

	// Use this for initialization
	void Start()
	{
        anim = GetComponent<Animator>();
		playerSprite = GetComponent<SpriteRenderer> ();
		playerRenderer = GetComponent<Renderer> ();
		healthUI = GameObject.Find ("Health");
		hScript = healthUI.GetComponent<Health>();
		cMirror = spawnMirror ();
		hScript.currentHealth = lives;
		hScript.maxHealth = maxLives;

        anim.Play("IdleFront"); //anim.SetBool(Animator.StringToHash("IdleBack"), true);
		StartCoroutine(PickAnimation (.15f));
	}

	// Update is called once per frame
	void Update () 
	{

	}

	void FixedUpdate()
	{
		
		if (!Dead) {
			movement.y = Input.GetAxis ("Vertical");// * moveSpeed;
			movement.x = Input.GetAxis ("Horizontal");// * moveSpeed;
			movement = Vector3.Normalize (movement) * moveSpeed;
		}
        if (Input.GetKey (KeyCode.LeftShift)) {
            movement.x *= runSpeed;
            movement.y *= runSpeed;
        }

		if (Input.GetKey (KeyCode.Escape)) {
			if (GameObject.Find ("PauseMenuCanvas") == null)
				Instantiate (PauseMenuObject);
			else {
				GameObject.Find ("ButtonResume").GetComponent<ButtonResume> ().Resume();
			}
		}

	checkCollisions ();
		
		transform.position += movement * Time.deltaTime;
	}

	public Bounds PlayerBounds()
	{
		Renderer rend = gameObject.GetComponent<Renderer> ();
		Bounds checkBounds = rend.bounds;
		checkBounds.Expand(new Vector3 (-.5f,-0.9f, 0));
		checkBounds.center += new Vector3 (0, -.5f, 0);
		return checkBounds;
	}

	public Bounds PlayerHeadBounds()
	{
		Renderer rend = gameObject.GetComponent<Renderer> ();
		Bounds checkBounds = rend.bounds;
		checkBounds.Expand(new Vector3 (-.5f,-1.8f, 0));
		checkBounds.center += new Vector3 (0, .5f, 0);
		return checkBounds;
	}


	GameObject spawnMirror()
	{
		GameObject nMirror = (GameObject)Instantiate(mirror, new Vector3 (mirrorDistance, 0, -1f), Quaternion.identity);
		nMirror.GetComponent<Mirror>().center = this.transform;
		return nMirror;
	}

	public void changeSpeed(float speedChange){
		moveSpeed = moveSpeed * ((speedChange / 100) + 1);
	}

	void checkCollisions(){
		// 0 = right 1 = down 2 = left 3 = up
		if (collisionDirection [0] == true)
			if (movement.x > 0)
				movement.x = 0;
		
		if (collisionDirection [1] == true)
			if (movement.y < 0)
				movement.y = 0;
		
		if (collisionDirection [2] == true)
			if (movement.x < 0)
				movement.x = 0;
		
		if (collisionDirection [3] == true)
			if (movement.y > 0)
				movement.y = 0;
		
		collisionDirection [0] = false;
		collisionDirection [1] = false;
		collisionDirection [2] = false;
		collisionDirection [3] = false;
		
		if (doorDetection [0] == true)
			movement.x = 205;
		
		if (doorDetection [1] == true)
			movement.y = -200;
		
		if (doorDetection [2] == true)
			movement.x = -205;
		
		if (doorDetection [3] == true)
			movement.y = 200;
		
		doorDetection [0] = false;
		doorDetection [1] = false;
		doorDetection [2] = false;
		doorDetection [3] = false;

	}


	public void ChangeHealth(int amount)
	{
		int compare=lives;

		lives += amount;
		if (lives > maxLives)
			lives = maxLives;
		hScript.currentHealth = lives;
		if (lives <= 0) {
			lives = 0;
			killPlayer();
		}
		if (compare > lives && !Dead)
			Instantiate (PlayerSounds [1]);
	}

	public void changeLife(int amount)
	{
		maxLives += amount;
		if (lives >= maxLives)
			lives = maxLives;
		hScript.maxHealth = maxLives;
		hScript.currentHealth = lives;
		if (lives <= 0) {
			lives = 0;
			killPlayer ();
		}
	}

	public void changeMirror(int amount)
	{
		mirrorSize += amount;
		if (mirrorSize < 0)
			mirrorSize = 0;
		if (mirrorSize > 3)
			mirrorSize = 3;
		cMirror.GetComponent<Mirror> ().changeMirrorState (mirrorSize);
		
	}

	public void changeStaminaRegen(int amount)
	{
		cMirror.GetComponent<Mirror> ().changeMirrorRegen (amount);
	}

	public bool isFullHP()
	{
		return(lives == maxLives);
	}

	void killPlayer(){
		if (!Dead) 
		    Instantiate (PlayerSounds [0]);
		Dead = true;
		movement = Vector3.zero;
		StartCoroutine (EndGame());
	}

	IEnumerator EndGame()
	{
		anim.Play ("DeadAnim");
		yield return new WaitForSeconds (2);
		Application.LoadLevel ("GameOver");

	}

	IEnumerator PickAnimation(float waitTime)
	{
		while (!Dead) {
			if (movement.y > 0) {
				anim.Play ("WalkBack");
				horizontalOrVertical = false;
				positive = true;
			} else if (movement.y < 0) {
				anim.Play ("WalkFront");
				horizontalOrVertical = false;
				positive = false;
			} else
			if (movement.x > 0) {
				anim.Play ("WalkRight");
				horizontalOrVertical = true;
				positive = true;
			} else if (movement.x < 0) {
				anim.Play ("WalkLeft");
				horizontalOrVertical = true;
				positive = false;
			}
		
			if ((movement.y == 0) && (movement.x == 0)) {
				if (horizontalOrVertical && positive)
					anim.Play ("IdleRight");
				if (horizontalOrVertical && !positive)
					anim.Play ("IdleLeft");
				if (!horizontalOrVertical && positive)
					anim.Play ("IdleBack");
				if (!horizontalOrVertical && !positive)
					anim.Play ("IdleFront");
			
			}
			yield return new WaitForSeconds (waitTime);
		}
	}

}