using UnityEngine;
using System.Collections;

public class Mirror : MonoBehaviour {

	public GameObject MirrorParticle;
	GameObject particleEffect;

	public Transform center;
	private Vector3 v;
	GameObject mirror;
	Mesh currentMesh;
	public float energyRegenMultipler = 1f;

	int maxEnergy = 2;
	public int energy = 2;
	bool fullEnergy = true;

	public float angle = 90;
	public float[] anglesMirrorSettings;
	public Mesh[] mirrorMeshes;

	Renderer mirrorRender;

	GameObject staminaUI;
	Health sScript;
	bool mirrorPSpawned = false;


	// Use this for initialization
	void Start () {
	//	center = getCenter ();
		energy = 2;
		staminaUI = GameObject.Find ("Health");
		sScript = staminaUI.GetComponent<Health>();
		sScript.currentStamina = energy;
		sScript.maxStamina = maxEnergy;

		mirror = this.gameObject;
		mirror.GetComponent<MeshFilter>().mesh = mirrorMeshes [0];
		v = (transform.position - center.position);
		mirrorRender = gameObject.GetComponent<Renderer> ();
	}

	public void changeMirrorState(int state)
	{
		switch (state) {
		case 0:
		case 1:
			angle = 45;
				break;
		case 2:
			angle = 90;
			break;
		case 3:
			angle = 100;
			break;
		default:
			break;
		}
		mirror.GetComponent<MeshFilter>().mesh = mirrorMeshes [state];
	}

	public void changeMirrorRegen(int regenChange)
	{
		maxEnergy += regenChange;
		energy += regenChange;
		if (maxEnergy < 1)
			maxEnergy = 1;




		if (energy < 0)
			energy = 0;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		sScript.currentStamina = energy;
		sScript.maxStamina = maxEnergy;

		if (energy < maxEnergy && fullEnergy) {
			fullEnergy = false;
			StartCoroutine (rechargeMirror (1.5f));
		}
		if (energy < 1) {
			debugChangeColour (0.4f, 0.4f, 0.4f);
			if(!mirrorPSpawned){
				particleEffect = (GameObject)Instantiate (MirrorParticle, transform.position + new Vector3 (0, 0, -1), Quaternion.identity);
				mirrorPSpawned = true;
			}
		}

		if (energy > 0) {
			debugChangeColour (1, 1, 1);
			mirrorPSpawned = false;
		}

		Vector3 centerScreenPos = Camera.main.WorldToScreenPoint (center.position);			//gets screen for mouse
		Vector3 dir = Input.mousePosition - centerScreenPos;								// gets mouse for direction
		float angle = Mathf.Atan2 (dir.y, dir.x) * Mathf.Rad2Deg;
		Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
		transform.position = center.position + q * v;
		transform.rotation = q;
	}

	void debugChangeColour(float r, float g, float b)
	{
		mirrorRender.material.color = new Color (r, g, b);
	}

	public Bounds ObjectBounds()
	{
		Renderer rend = gameObject.GetComponent<Renderer> ();
		return rend.bounds;
		
	}

	public Vector3 direction()
	{
		return this.transform.position - center.position;
	}

	IEnumerator rechargeMirror(float waitTime)
	{
		yield return new WaitForSeconds(waitTime);
			//if you want to stop the loop, use: break;
		energy += 1;
		fullEnergy = true;
	}  

}


//http://answers.unity3d.com/questions/794119/how-to-rotate-an-object-around-a-fixed-point-so-it.html