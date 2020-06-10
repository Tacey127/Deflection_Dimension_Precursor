using UnityEngine;
using System.Collections;

public class TrapDoor : MonoBehaviour {

	Animator anim;
	Renderer rend;
	GameObject player;
	public bool doorIsOpen;
	bool alreadyOpened = false;
	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Player(Clone)");
		rend = gameObject.GetComponent<Renderer> ();
		anim = GetComponent<Animator> ();
	}

	void FixedUpdate()
	{
		if (hitPlayer ()&& !alreadyOpened) {
			anim.Play("OpenDoor");
			StartCoroutine(OpenWait(1f));
			alreadyOpened = true;
		}
		if (doorIsOpen && hitPlayer()) {
			GameObject.Find ("GameController").GetComponent<GameController>().gameType++;
			GameObject.Find ("GameController").GetComponent<GameController>().removeDungeon();
			GameObject.Find ("GameController").GetComponent<GameController>().SpawnLevel();
			GameObject.Find ("GameController").GetComponent<GameController>().resetPlayerPosition();
			//Application.LoadLevel("ControllerBase");
		}

	}

	// Update is called once per frame
	void Update () {
	
	}

	public Bounds ObjectBounds()
	{
		return rend.bounds;
	}

	bool hitPlayer()
	{
		Bounds checkbounds;
		checkbounds = player.GetComponent<PlayerControllerNew>().PlayerBounds();
		checkbounds.Expand (new Vector3 (0, 0, 10));
		return ObjectBounds ().Intersects (checkbounds);
	}

	IEnumerator OpenWait(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
		doorIsOpen = true;
	}



}
