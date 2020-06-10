using UnityEngine;
using System.Collections;

public class DestroySound : MonoBehaviour {

	private AudioSource thisAudioSource;

	// Use this for initialization
	void Start () {
		thisAudioSource = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (!thisAudioSource.isPlaying)
		Destroy (gameObject);
	}
}
