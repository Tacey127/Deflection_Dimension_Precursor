using UnityEngine;
using System.Collections;

public class ButtonRetry : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Retry() {
		gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (playaudio(0.5f));
	}
	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel ("ControllerBase");
	}
}
