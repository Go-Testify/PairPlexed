using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Ball speed
	float speedX;
	float speedY;

	// Distance to the left/right edge of game
	int ballResetDistance = 15;

	Vector3 startposition;

	// Use this for initialization
	void Start () {

		// Set initial speed of ball on x and y axis
		speedX = 10;
		speedY = 6;

		// Get the starting position of the ball
		startposition = this.transform.position;

		// Start the ball moving
		startBall ();

	}

	// Update is called once per frame
	void Update () {

		// Check if the ball has left the game on the left/right
		if (transform.position.x >= startposition.x + ballResetDistance || transform.position.x <= startposition.x - ballResetDistance) {

			// Reset the ball to its starting posiiton
			startBall ();
			
		}
		
	}

	/// <summary>
	/// Starts the ball moving - resets ball to its original position before moving
	/// </summary>
	void startBall () {

		transform.position = startposition;

		rigidbody.velocity = new Vector3(speedX, speedY, 0);

	}
	
}
