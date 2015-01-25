using UnityEngine;
using System.Collections;

public class Ball : MonoBehaviour {

	// Ball speed
	float speedX;
	float speedY;

	// Distance to the left/right edge of game
	int ballResetDistance = 14;

	public GameManager2 gameManager2;

	Vector3 startposition;

	private int numMentalMoves = 0;
	public PhysicMaterial physicsMatBouncyMax;
	public GameObject ballQuestionMark;
	public GameObject ballExclamationMark;
	// Use this for initialization
	void Start () {

		// Set initial speed of ball on x and y axis
		speedX = 15;
		speedY = 6;

		// Get the starting position of the ball
		startposition = this.transform.position;

		// Start the ball moving
		startBall ();

	}

	// Update is called once per frame
	void Update () {

		// Check if the ball has left the game on the left/right
		if ((transform.position.x >= startposition.x + ballResetDistance || transform.position.x <= startposition.x - ballResetDistance) && gameManager2.currentGameState != GameManager2.GameState.pongAnimating) {

			//Play Score Sound
			this.GetComponent<AudioSource>().Play();

			if (transform.position.x >= startposition.x + ballResetDistance) {
				
				gameManager2.player1Score++;
				gameManager2.player1ScoreTxt.text = gameManager2.player1Score + "";
			}
			if (transform.position.x <= startposition.x - ballResetDistance) {
				
				gameManager2.player2Score++;
				gameManager2.player2ScoreTxt.text = gameManager2.player2Score + "";
			}

			/*if(gameManager2.currentGameState == GameManager2.GameState.pongPlaying2 && gameManager2.totalHits > 2)
			{
				numMentalMoves++;

				if(numMentalMoves > 50)
				{
					Destroy(this.gameObject);
					CancelInvoke();
					gameManager2.ShowEnd();
				}
			}*/

			transform.position = startposition;

			// Reset the ball to its starting posiiton
			if(!rigidbody.useGravity)
				startBall();
		}
	}

	/// <summary>
	/// Starts the ball moving - resets ball to its original position before moving
	/// </summary>
	void startBall () {

		rigidbody.velocity = new Vector3(speedX, speedY, 0);

		//rigidbody.AddForce(10,6,0, ForceMode.Impulse);
		/*if(gameManager2.currentGameState == GameManager2.GameState.pongPlaying2 && gameManager2.totalHits > 2)
		{
			ballQuestionMark.SetActive(true);
			InvokeRepeating("IncreaseBallVelocity",0,2);
		}*/

	}

	public void StartPong2()
	{
		gameManager2.currentGameState = GameManager2.GameState.pongPlaying2;
		transform.position = startposition;
		rigidbody.useGravity = false;
		rigidbody.drag = 0;
		ballQuestionMark.SetActive(false);
		speedX *= 1.5f;
		speedY *= 1.5f;
		this.collider.sharedMaterial = physicsMatBouncyMax;
		startBall();
	}

	void IncreaseBallVelocity () {

		rigidbody.velocity = new Vector3(speedX *= 1.2f, speedY, 0);
			
	}
	
}
