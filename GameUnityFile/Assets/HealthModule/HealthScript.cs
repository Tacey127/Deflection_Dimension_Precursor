using UnityEngine;
using System.Collections;

public class HealthScript : MonoBehaviour {

	public int lives = 6;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void changeHealth(int amount)
	{
		lives += amount;
		if (lives < 0)
			lives = 0;
	}

}
