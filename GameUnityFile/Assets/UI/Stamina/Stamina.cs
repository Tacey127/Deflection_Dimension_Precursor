using UnityEngine;
using System.Collections;

public class Stamina : MonoBehaviour {

	public int currentStamina;
	public int maxStamina;
	
	public Texture2D staminaLit;
	public Texture2D staminaEmpty;

	public int sizeX;
	public int sizeY;

	int maxStaminaPerRow = 5;
	
	public float spacingX;
	public float spacingY;

	public float xOffset;
	public float yOffset;

	void OnGUI() {
		for (int i = 0; i < maxStamina; i++) {
			
			int y = Mathf.FloorToInt(i/maxStaminaPerRow);
			int x = i - y * maxStaminaPerRow;

			Vector2 place = new Vector2(x * spacingX + xOffset, y* spacingY + yOffset);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(sizeX, sizeY);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, staminaEmpty);
		}
		
		for (int i = 0; i < currentStamina; i++) {
			
			int y = Mathf.FloorToInt(i/maxStaminaPerRow);
			int x = i - y * maxStaminaPerRow;
			
			Vector2 place = new Vector2(x * spacingX + xOffset, y* spacingY + yOffset);//*spacingX,0); //y*spacingY);
			Vector2 size = new Vector2(sizeX, sizeY);
			
			Rect posRect = new Rect (place,size);
			GUI.DrawTexture (posRect, staminaLit);
		}
	}
}
