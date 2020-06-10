using UnityEngine;
using System.Collections;

public class HeartsScript : MonoBehaviour {

	//HEARTS
    public int maxLives = 6;
	public int lives = 3;
	public Texture2D heartDark;
	public Texture2D heartHalf;
	public Texture2D heartFull;
	private float heartWidth;
	private float heartHeight;
	private Resolution res;

	public GameObject[] heartSprite;

	//STAMINA
	private int maxStamina = 100; //Max stamina
	private float stamina = 0f; //Current stamina
	private int staminaBlocks = 3; //How many blocks of stamina
	private float staminaBlock; //The size of each block
	private float staminaBlockSize; //Visual size of each block
	private float staminaRechargeRate = 1; //Recharge rate
	private int staminaSize = 300; //The width of the stamina bar

    public GameObject staminaBar; //The stamina bar object
    public GameObject staminaBarDark; //The dark stamina bar object

	private RectTransform staminaBarRT = new RectTransform(); //Stamina bar transformation
	private RectTransform staminaBarDarkRT = new RectTransform(); //Dark stamina bar transformation




	// Use this for initialization
	void Start () {
		//HEARTS
		heartWidth = heartFull.width;
		heartHeight = heartFull.height;


		//STAMINA
		staminaBlock = maxStamina / staminaBlocks;
        staminaBlockSize = staminaSize / staminaBlocks;

        staminaBar = GameObject.Find("StaminaBar");
        staminaBarDark = GameObject.Find("StaminaBarDark");

        staminaBarRT = (RectTransform)staminaBar.transform;
        staminaBarDarkRT = (RectTransform)staminaBarDark.transform;


	}


	void OnGUI() {
		//HEARTS
		res = Screen.currentResolution;
		float widthPercent = res.width / 1920;
		float heightPercent = res.height / 1080;
		float percent; //The percentage scale
		if (widthPercent < heightPercent) //Which of them is smallest?
			percent = widthPercent;
		else
			percent = heightPercent;

		if (lives > 0) {
			Rect posRect = new Rect (30, 30, heartWidth * percent, heartHeight * percent);
			GUI.DrawTexture (posRect, heartFull);
		}
	}


	// Update is called once per frame
	void Update () {
        //STAMINA
		if (stamina < maxStamina) {
			stamina += staminaRechargeRate;
		}

        staminaBarDarkRT.sizeDelta = new Vector2((stamina/100f) * staminaSize, 50f);
        staminaBarRT.sizeDelta = new Vector2(Mathf.Floor(stamina / staminaBlock) * staminaBlockSize, 50f);

		/*
        if (stamina < maxStamina)
        {
            Debug.Log(Mathf.Floor(stamina / staminaBlock));
        }
        */
	}
}
