using UnityEngine;
using System.Collections;

public class ButtonExitToMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ExitToMainMenu() {
		Time.timeScale = 1;
		gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (playaudio(0.5f));
	}
	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel ("MainMenu");
	}
}
