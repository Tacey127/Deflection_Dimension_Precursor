using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CutsceneScript : MonoBehaviour {

	public Image cutsceneImage;
	public Sprite[] cutsceneSprites = new Sprite[7];

	// Use this for initialization
	void Start () {
		StartCoroutine (cutscene ());
	}

	IEnumerator cutscene() {
		for (int i = 0; i < 7; ++i) {
			cutsceneImage.sprite = cutsceneSprites [i];
			yield return new WaitForSeconds(2f);
		}
		startGame ();
	}

	// Update is called once per frame
	void Update () {
		if (Input.anyKey)
			startGame ();
	}

	void startGame() {
		Application.LoadLevel ("ControllerBase");
	}
}
