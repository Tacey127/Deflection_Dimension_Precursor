using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {


	public int currentHealth;
	public int maxHealth;

	public Texture2D heart;
	public Texture2D emptyHeart;
	public Texture2D halfHeart;
	
	int maxHeartsPerRow = 5;
	
	public float spacingX;
	public float spacingY;

	GameObject player;

	public int currentStamina;
	public int maxStamina;
	
	public Texture2D staminaLit;
	public Texture2D staminaEmpty;
	
	public int sizeX;
	public int sizeY;
	
	int maxStaminaPerRow = 5;
	
	public float stSpacingX;
	public float stSpacingY;
	
	public float xOffset;
	public float yOffset;


	void OnGUI() {

		//heartOutlines
		for (int i = 0; i < maxHealth/2; i++) {
			
			int y = Mathf.FloorToInt(i/maxHeartsPerRow);
			int x = i - y * maxHeartsPerRow;
			//if(x > maxHeartsPerRow)
			//	;
			Vector2 place = new Vector2(x * spacingX, y* spacingY);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(58, 58);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, emptyHeart);
		}


		//drawHearts
		for (int i = 0; i < currentHealth/2; i++) {

			int y = Mathf.FloorToInt(i/maxHeartsPerRow);
			int x = i - y * maxHeartsPerRow;
			//if(x > maxHeartsPerRow)
			//	;
			Vector2 place = new Vector2(x * spacingX, y* spacingY);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(58, 58);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, heart);
		}

		//drawHalfHeart
		
		if (currentHealth % 2 == 1) {
			int i = currentHealth/2;
			
			int y = Mathf.FloorToInt (i / maxHeartsPerRow);
			int x = i - y * maxHeartsPerRow;
			
			Vector2 place = new Vector2 (x * spacingX, y * spacingY);
			Vector2 size = new Vector2 (58, 58);

			Rect posRect = new Rect (place, size);
			GUI.DrawTexture (posRect, halfHeart);
		}

		//drawStaminaEmpty
		for (int i = 0; i < maxStamina; i++) {
			
			int y = Mathf.FloorToInt(i/maxStaminaPerRow);
			int x = i - y * maxStaminaPerRow;
			
			Vector2 place = new Vector2(x * stSpacingX + xOffset, y* stSpacingY + yOffset);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(sizeX, sizeY);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, staminaEmpty);
		}

		//drawStaminaFull
		for (int i = 0; i < currentStamina; i++) {
			
			int y = Mathf.FloorToInt(i/maxStaminaPerRow);
			int x = i - y * maxStaminaPerRow;
			
			Vector2 place = new Vector2(x * stSpacingX + xOffset, y* stSpacingY + yOffset);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(sizeX, sizeY);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, staminaLit);
		}



	}

}
