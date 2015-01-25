using UnityEngine;
using System.Collections;

public class HomeScreen : MonoBehaviour {
	
	public GameObject[] squares;

	public string stringToFind;
	public string stringSuffix;

	public Color green = new Color(0F, 1F, 0.2F, 1F);
	public Color blue = new Color(0.1F, 0F, 1F, 1F);

	int playerCounter1 = 0;
	int playerCounter2 = 0;

	public AudioSource keyPress1;
	public AudioSource keyPress2;
	public AudioSource player1Success;
	public AudioSource player2Success;

	// Use this for initialization
	void Start () {

		// Increase background music audio pitch every set amount of seconds
		//InvokeRepeating("IncreaseAudioPitch", 2f, 2f);
	
	}

	// Increase background music audio pitch every set amount of seconds
	//void IncreaseAudioPitch () {

		//homeScreenAudio.pitch += 0.1f;

	//}
	
	// Update is called once per frame
	void Update () {

		// W
		if (Input.GetKeyDown(KeyCode.W)) {

			stringSuffix = "W";
			highlightSquare (stringSuffix);

		}
		
		// A
		if (Input.GetKeyDown(KeyCode.A)) {
			stringSuffix = "A";
			highlightSquare (stringSuffix);

		}

		// S
		if (Input.GetKeyDown(KeyCode.S)) {
			stringSuffix = "S";
			highlightSquare (stringSuffix);

		}

		// D
		if (Input.GetKeyDown(KeyCode.D)) {
			stringSuffix = "D";
			highlightSquare (stringSuffix);

		}
		
		// Up Arrow
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			stringSuffix = "Up";
			highlightSquare (stringSuffix);

		}

		// Left Arrow
		if (Input.GetKeyDown(KeyCode.LeftArrow)) {
			stringSuffix = "Left";
			highlightSquare (stringSuffix);

		}
		
		// Down Arrow
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			stringSuffix = "Down";
			highlightSquare (stringSuffix);

		}

		// Right Arrow
		if (Input.GetKeyDown(KeyCode.RightArrow)) {
			stringSuffix = "Right";
			highlightSquare (stringSuffix);

		}
	}

	/// <summary>
	/// Highlights the square based on the key pressed
	/// </summary>
	void highlightSquare (string stringSuffix) {

		// Generate string based on key pressed
		stringToFind = "pixel_wht" + stringSuffix;

		// Loop through the array of game objects
		foreach (GameObject square in squares)
		{

			// Check current array object against key pressed string
			if (square.name == stringToFind)
			{

				// Change the squares sprite renderer color
				SpriteRenderer squareSpriteRenderer = square.GetComponent<SpriteRenderer>();

				if (stringToFind == "pixel_whtW" || stringToFind == "pixel_whtA" || stringToFind == "pixel_whtS" || stringToFind == "pixel_whtD") {

					if (squareSpriteRenderer.color != green) {

						squareSpriteRenderer.color = green;
						
						playerCounter1 ++;
						
						// Play sound and increase pitch
						keyPress1.Play();
						keyPress1.pitch += 0.1f;

					}

					// Check if player has pressed all keys successfully
					if (playerCounter1 == 4) {
						
						//player1Success.Play ();
						
					}

				}

				if (stringToFind == "pixel_whtUp" || stringToFind == "pixel_whtLeft" || stringToFind == "pixel_whtDown" || stringToFind == "pixel_whtRight") {

					if (squareSpriteRenderer.color != blue) {

						squareSpriteRenderer.color = blue;
					
						playerCounter2 ++;

						// Play sound and increase pitch
						keyPress2.Play ();
						keyPress2.pitch += 0.1f;

					}

					// Check if player has pressed all keys successfully
					if (playerCounter2 == 4) {

						//player2Success.Play ();

					}

				}

			}

			Debug.Log("Player1 score: " + playerCounter1 + " " + "Player2 score: " + playerCounter2);

			// Load the first level if both players have pressed all the start keys
			if (playerCounter1 == 4 && playerCounter2 == 4) {

				Application.LoadLevel("Level1_Pong");

			}

		}

	}

}
