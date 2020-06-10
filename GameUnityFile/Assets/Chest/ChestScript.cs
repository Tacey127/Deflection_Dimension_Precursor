using UnityEngine;
using System.Collections;

public class ChestScript : MonoBehaviour {

	public GameObject CoinParticles;

	Renderer rend;
	GameObject player;
	GameObject gameController;
	Animator anim;
	public GameObject pickup;
	bool open = false;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
		player = GameObject.Find ("Player(Clone)");
		gameController = GameObject.Find ("GameController");
		rend = gameObject.GetComponent<Renderer> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate(){
		if (hitPlayer () && !open) {
			StartCoroutine(OpenChest ());
		}
	}

	IEnumerator OpenChest()
	{
		open = true;
		anim.Play ("ChestAnimation");
		Instantiate (CoinParticles,gameObject.transform.position,Quaternion.identity);
		yield return new WaitForSeconds (1.5f);
		spawnAPickup ();
		
	}

	void spawnAPickup (){
	
		gameController.GetComponent<GameController> ().spawnPickup(this.transform.position + new Vector3 (0, 0, -0.6f),Random.Range(1,9));
	}

	public Bounds ObjectBounds()
	{
		return rend.bounds;
	}

	bool hitPlayer()
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0.01f, 0.01f, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}


}
