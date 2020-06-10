using UnityEngine;
using System.Collections;

public class Pickup : MonoBehaviour {
	Renderer rend;
	GameObject player;
	public int health = 2;
	int itemtype;
	public Sprite[] sprites;

	float resistance = 4.5f;
	float decelTime = 5;

	Animator anim;

	public bool[] collisionDirection;

	Vector3 movement;
	// Use this for initialization

	void Start () {
		rend = gameObject.GetComponent<Renderer> ();
		player = GameObject.Find ("Player(Clone)");
		movement = Vector3.zero;
	}

	public void setPickup(int pickup)
	{

		itemtype = pickup;
		anim = GetComponent<Animator>();
		gameObject.GetComponent<SpriteRenderer> ().sprite = sprites [pickup];
		StartCoroutine(AnimatePickup(1, pickup));
	}

	public Bounds ObjectBounds()
	{
		return rend.bounds;
	}
	// Update is called once per frame
	void FixedUpdate () {


			if (hitPlayer()) {
				{
				switch(itemtype){
				case 0:
					if (!player.GetComponent<PlayerControllerNew>().isFullHP()){//when the player needs health
					player.GetComponent<PlayerControllerNew>().ChangeHealth(health);
					GameObject.Destroy(this.gameObject);
					}
					break;
				case 1:
					player.GetComponent<PlayerControllerNew>().changeLife(health);
					player.GetComponent<PlayerControllerNew>().ChangeHealth(health);
					GameObject.Destroy(this.gameObject);
					break;
				case 2:
					player.GetComponent<PlayerControllerNew>().changeLife(-health);
					GameObject.Destroy(this.gameObject);
					break;
				case 3:
					player.GetComponent<PlayerControllerNew>().changeMirror(1);
					GameObject.Destroy(this.gameObject);
					break;
				case 4:
					player.GetComponent<PlayerControllerNew>().changeMirror(-1);
					GameObject.Destroy(this.gameObject);
					break;
				case 5:
					player.GetComponent<PlayerControllerNew>().changeStaminaRegen(-1);
					GameObject.Destroy(this.gameObject);
					break;
				case 6:
					player.GetComponent<PlayerControllerNew>().changeStaminaRegen(1);
					GameObject.Destroy(this.gameObject);
					break;
				case 7:
					player.GetComponent<PlayerControllerNew>().changeSpeed(-50);
					GameObject.Destroy(this.gameObject);
					break;
				case 8:
					player.GetComponent<PlayerControllerNew>().changeSpeed(50);
					GameObject.Destroy(this.gameObject);
					break;
				}
				//animation
				}
		}

		if (hitPlayer()) {
			{
				resistPlayer();
			}
		}

		checkCollision ();

		transform.position += movement *Time.deltaTime * resistance;
		if (movement != Vector3.zero)
			slowDownPosition ();
		else
			decelTime = 5;
	}

	void checkCollision()
	{
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
	}

	void resistPlayer()
	{
		movement = MoveFromTarget ();
	}
	void slowDownPosition(){
		movement = Vector3.Slerp(movement, Vector3.zero,5f);
	}

	Vector3 MoveFromTarget()
	{
		Vector3 dir = transform.position-player.transform.position;
		dir.Normalize();
		dir.z = 0;
		return dir;
	}

	bool hitPlayer()  
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	IEnumerator AnimatePickup(float waitTime, int pickup)
	{
		int set= pickup;
		//while (true) {
			switch (set) {
			case 0:anim.Play ("HealAnim");
					break;
			case 1:anim.Play ("HealthUpAnim");
				break;
			case 2:anim.Play ("HealthDownAnim");
				break;
			case 3:anim.Play ("MirrorUpAnim");
				break;
			case 4:anim.Play ("MirrorDownAnim");
				break;
			case 5:anim.Play ("StaminaDownAnim");
				break;
			case 6:anim.Play ("StaminaUpAnim");
				break;
			case 7:anim.Play ("SlowDownAnim");
				break;
			case 8:anim.Play ("SpeedUpAnim");
				break;
			default:
			break;
			}
			
			
			yield return new WaitForSeconds (waitTime);
		//}
	}

}
