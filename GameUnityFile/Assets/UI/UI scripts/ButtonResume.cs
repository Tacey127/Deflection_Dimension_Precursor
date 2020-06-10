using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class ButtonResume : MonoBehaviour {
	
	public GameObject OpenMenu;
	public GameObject CloseMenu;



	// Use this for initialization
	void Start () {
		//Instantiate (OpenMenu);
		/*gameObject.GetComponent<AudioSource>().Play();
		StartCoroutine (playaudio(0.5f));
	}
	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime);*/
		Time.timeScale = 0;
	}
	
	// Update is called once per frame
	void Update () {
		/*if (Input.GetKey ("escape")) {
			Resume ();
		}*/
	}
	public void Resume() {
//		Instantiate (CloseMenu);
		Destroy(transform.parent.gameObject);
		Time.timeScale = 1;
		StartCoroutine (playaudio(0.5f));
	} 

	IEnumerator playaudio(float waitTime){
		yield return new WaitForSeconds(waitTime); 


	}
}
