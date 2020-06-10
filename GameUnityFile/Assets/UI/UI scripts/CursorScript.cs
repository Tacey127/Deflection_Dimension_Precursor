using UnityEngine;
using System.Collections;

public class CursorScript : MonoBehaviour {

	public GameObject audio;


	// Use this for initialization
	void Start () {
		Cursor.visible = false;
		gameObject.GetComponent<AudioSource>().Play ();
	}
	
	// Update is called once per frame
	void Update () {
		this.transform.position = Input.mousePosition;
	}
}
