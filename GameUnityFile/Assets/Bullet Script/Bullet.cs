using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	Vector3 movement;

	public GameObject BoneParticle;
	public GameObject ArrowParticle;
	public GameObject WebParticle;
	public GameObject BWebParticle;

	GameObject particleEffect;

	public GameObject[] AudioSources;

	int damage = 1;
	GameObject gameController;
	GameObject mirror;
	GameObject player;
	public bool freindly = false;
	Renderer rend;
	int minSpeed = 100;
	public int bulletType;
	public Sprite[] sprites;
	public float SpeedIncreace;
	Quaternion facing;
	bool isbone = false;
	bool broken = false;
	public bool bossBullet = false;
	Animator anim;
	GameObject boss;
	// Use this for initialization
	void Start () {
		gameController = GameObject.Find ("GameController");
		mirror = GameObject.Find ("Mirror(Clone)");
		player = GameObject.Find ("Player(Clone)");
		if (bossBullet)
			boss = GameObject.Find ("EnemyBoss(Clone)");
		rend = gameObject.GetComponent<Renderer> ();
		movement.z = 0;
		if (bulletType == 0) {
		Instantiate (AudioSources[0]);
		}
		if (bulletType == 1) {
			Instantiate (AudioSources[1]);
		}
		
		if (bulletType == 2) {
			Instantiate (AudioSources[2]);
			
		}
		if (bulletType == 3) {
			Instantiate (AudioSources[2]);
			
		}

	}

	public void setbullet( int bullet)
	{
		anim = GetComponent<Animator>();
		bulletType = bullet;
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [bulletType];
		if (bullet == 0) {
			anim.Play("Bone");
			isbone = true;
			SpeedIncreace = 6f;
		}
		if (bullet == 1) {
			anim.Play("Arrow");
			SpeedIncreace = 10f;
		}
		if (bullet == 2) {
			anim.Play("WebAnim");
			SpeedIncreace = 10f;
		}
		if (bullet == 3) {
			anim.Play("WebBAnim");
			SpeedIncreace = 12f;
		}

	}
	// Update is called once per frame
	void FixedUpdate () {
		if(!broken)
		checkCollision ();

		transform.position += movement / (minSpeed/SpeedIncreace);
		if (isbone)
			gameObject.transform.eulerAngles += new Vector3 (0, 0, 10f);
	}

	public Bounds ObjectBounds()
	{
		Bounds checkBounds = rend.bounds;
		checkBounds.Expand(new Vector3 (-.2f,-.2f, 0));
		return checkBounds;
	}



	void checkCollision()
	{
		if(boss != null)
		if (bossBullet && hitBoss()) {
			if (freindly){
				boss.GetComponent<FinalBossScript>().changeHealth(-damage);
			//bullet splash
			destroyBullet();
			}
		}

		if (hitPlayer()) {
			if (!freindly)
			{
			player.GetComponent<PlayerControllerNew>().ChangeHealth(-damage);
			//bullet splash
				destroyBullet();
			}
		}

		if (!freindly) {
			if (hitMirror ()) {
				if (mirror.GetComponent<Mirror> ().energy > 0){
					if (bulletType == 3){
						mirror.GetComponent<Mirror> ().energy -=5;
						movement = Vector3.zero;
						Instantiate (AudioSources[3]);
						destroyBullet();
					}
					else {
					mirror.GetComponent<Mirror> ().energy -=1;
					movement = (mirror.GetComponent<Mirror> ().direction ());
					movement.z = 0;
					changeRotation (mirror.GetComponent<Mirror> ().direction ());
						freindly = true;
						Instantiate (AudioSources[4]);
					}
				//mirror flash?
				}
			}
		}
		//splash

		for (int i = 0; 20 > i; i++) {
			if (gameController.GetComponent<GameController> ().eAttributes [i] != null) {
				if (ObjectBounds ().Intersects (gameController.GetComponent<GameController> ().eAttributes [i].EnemyBounds ()))
					if (freindly)
					{
						gameController.GetComponent<GameController> ().eAttributes [i].changeHealth (-damage);
						destroyBullet();
				}
				}
			}
					
	}


	public void destroyBullet()
	{
		broken = true;
		movement = Vector3.zero;
		StartCoroutine(breakApart(0.2f));
	}

	IEnumerator breakApart(float waitTime)
	{
		if (bulletType == 0) {
			anim.Play("BoneBreakAnim");
			Instantiate (BoneParticle,gameObject.transform.position,Quaternion.identity);
		}
		if (bulletType == 1) {
			anim.Play("ArrorBreakAnim");
			Instantiate (ArrowParticle,gameObject.transform.position,Quaternion.identity);
		}

		if (bulletType == 2) {
			anim.Play("WebBreak");
			Instantiate (WebParticle,gameObject.transform.position,Quaternion.identity);
		}
		if (bulletType == 3) {
			anim.Play("WebBBreak");
			Instantiate (BWebParticle,gameObject.transform.position,Quaternion.identity);
		}

		yield return new WaitForSeconds(waitTime);
		Destroy (gameObject);
	}
		    

	public void changeRotation(Vector3 q)
	{

		transform.eulerAngles = new Vector3 (0, 0, Mathf.Atan2 (q.y, q.x) * Mathf.Rad2Deg);

	}

	public void setDirection(Vector3 direction)
	{
		movement = direction;
	}

	bool hitPlayer()
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		Bounds headbounds = player.GetComponent<PlayerControllerNew>().PlayerHeadBounds();
		headbounds.Expand (new Vector3 (0, 0, 10));
		return ((ObjectBounds ().Intersects (checkbounds)) || (ObjectBounds ().Intersects (headbounds)));
	}

	bool hitBoss()
	{
		Bounds checkbounds;
		checkbounds = boss.GetComponent<FinalBossScript>().EnemyBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		return ((ObjectBounds ().Intersects (checkbounds)));
	}
	
	bool hitMirror()
	{
		if (Vector3.Distance(player.transform.position, transform.position) <= 2.2) {//if the player is in range
			Transform center = player.transform;									//gets player transform
			Vector3 centerScreenPos = Camera.main.WorldToScreenPoint (center.position);			//gets transform relative to screen
			Vector3 dir = Input.mousePosition - centerScreenPos;								// gets direction of where the mouse is pointing relative to player

			float mouseangle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;						//converts the mouse direction to a angle

			dir = transform.position - player.transform.position;								//gets direction relative to bullet
		
			float bulletangle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;						//converts bullet to angle

			float angle = mirror.GetComponent<Mirror>().angle;
			float anglediff = (mouseangle - bulletangle + angle + 360) % 360 - angle;

			if (anglediff <= mirror.GetComponent<Mirror>().angle && anglediff >= -mirror.GetComponent<Mirror>().angle)
			{
					return true;
			}
		}
		return false;

	}


	bool hitEnemy()
	{
		Bounds checkbounds;
		//checkbounds = GameObject.Find ("Enemy(Clone)").GetComponent<EnemyAI>().EnemyBounds();
		checkbounds = GameObject.Find ("Enemy(Clone)").GetComponent<EnemyAI>().EnemyBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}


}
