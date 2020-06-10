using UnityEngine;
using System.Collections;
//using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonPlay : MonoBehaviour {
	
	public string Destination;
	
	// Use this for initialization
	void Start () {
		
	}
	
	void Update() {
		
	}
	
	public void LoadStage() {
		gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (playaudio(0.5f));
	}
	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime);
		Application.LoadLevel(Destination);
		
	}
}
