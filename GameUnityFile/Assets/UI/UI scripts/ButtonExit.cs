using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonExit : MonoBehaviour {
	

	// Use this for initialization
	void Start () {

	}

	void Update() {

	}

	public void Exit() {
		gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (playaudio(0.5f));
	}
	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.Quit ();
	}
}
