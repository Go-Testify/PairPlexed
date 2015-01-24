using UnityEngine;
using System.Collections;

public class Paddles : MonoBehaviour {

	public GameManager2 gameManager2;
	public PhysicMaterial physicsMatBouncyMax;
	public PhysicMaterial physicsMatBouncyMed;

	bool coroutineHasRun = false;

	float speedPongPaddles = 0.2f;

	int playerScore1 = 0;
	int playerScore2 = 0;

	int gravityTriggerHitCount = 2;

	GameObject ball;
	GameObject ballQuestionMark;

	GameObject player1;
	GameObject player2;

	string controlSystem;

	int jumpSpeed = 300;
	bool isFalling = false;
	float speed = 0.1f;
	
	// Use this for initialization
	void Start () {

		// Get a reference to the ball
		ball = GameObject.Find("Ball");
		ballQuestionMark = ball.transform.Find("Mark").gameObject;

		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");
	}
	
	// Update is called once per frame
	void Update () {
	
		switch (gameManager2.currentGameState)
		{
		case GameManager2.GameState.pongPlaying:
			pongControls ();

			/*if (gameObject.name == "Player1") {

				Debug.Log("Player1 - ContolSystem - " + controlSystem);

			}*/

			break;

		case GameManager2.GameState.platformPlaying:
			platformControls ();

			/*if (gameObject.name == "Player2") {
				
				Debug.Log("Player2 - ContolSystem - " + controlSystem);
				
			}*/

			break;
		}
	
	}
		
	//=========================  Pong Controls START =========================
	void pongControls () {
			
		// Move each player based on keyboard input
		if (gameObject.name == "Player1") {
			
			//transform.Translate(0, Input.GetAxis("Horizontal") * speed, 0);

			// Up
			if (Input.GetKey(KeyCode.W)) {
				
				transform.Translate(0, speedPongPaddles, 0);
			}
			
			// Down
			if (Input.GetKey(KeyCode.S)) {
				
				transform.Translate(0, -speedPongPaddles, 0);
			}
			
		}
		else if (gameObject.name == "Player2") {
			
			// Up
			if (Input.GetKey(KeyCode.UpArrow)) {
				
				transform.Translate(0, speedPongPaddles, 0);
			}
			
			// Down
			if (Input.GetKey(KeyCode.DownArrow)) {
				
				transform.Translate(0, -speedPongPaddles, 0);
			}
		}
		
		// Check if score has hit set limit - then trigger gravity
		scoreCheck ();

	}
	//=========================  Pong Controls END =========================
	
	//=========================  Platform Controls START =========================
	void platformControls () {

		// Move each player based on keyboard input - START
		if (gameObject.name == "Player1") {
			
			// Jump
			if (Input.GetKeyDown(KeyCode.W) && isFalling == false) {
				
				rigidbody.AddForce(Vector3.up * jumpSpeed);
				
				isFalling = true;
			}
			
			// Left
			//if (Input.GetKey(KeyCode.S) && isFalling == false) {
			if (Input.GetKey(KeyCode.A)) {

				//rigidbody.velocity = new Vector3(-speed, rigidbody.velocity.y, 0);
				transform.Translate(-speed, 0, 0);
			}
			
			// Right
			//if (Input.GetKey(KeyCode.D) && isFalling == false) {
			if (Input.GetKey(KeyCode.D)) {

				//rigidbody.velocity = new Vector3(speed, rigidbody.velocity.y, 0);
				transform.Translate(speed, 0, 0);
			}
		}
		else if (gameObject.name == "Player2") {
			
			// Jump
			if (Input.GetKeyDown(KeyCode.UpArrow) && isFalling == false) {
			//if (Input.GetKey(KeyCode.UpArrow) && isFalling == false) {
					
				rigidbody.AddForce(Vector3.up * jumpSpeed);

				animation.Play();
				
				isFalling = true;
			}
			
			// Left
			//if (Input.GetKey(KeyCode.LeftArrow) && isFalling == false) {
			if (Input.GetKey(KeyCode.LeftArrow)) {
					
				transform.Translate(-speed, 0, 0);
			}
			
			// Right
			//if (Input.GetKey(KeyCode.RightArrow) && isFalling == false) {
			if (Input.GetKey(KeyCode.RightArrow)) {
					
				transform.Translate(speed, 0, 0);
			}
		}
		
	}

	void OnCollisionStay (){
		
		// this makes it only possible to jump when you are on the ground
		isFalling = false;
	}

	//=========================  Platform Controls END =========================
	
	/// <summary>
	/// Raises the collision enter event to increase player score
	/// </summary>
	void OnCollisionEnter (Collision other) {
		
		scoreCounter (other);
		
	}
	
	/// <summary>
	/// Keeps track of player scores
	/// </summary>
	void scoreCounter (Collision other) {
		
		// Check which player paddle the ball has hit and increment their score
		if (other.gameObject.name == "Ball") {
			
			if (gameObject.name == "Player1") {
				
				gameManager2.player1Score++;
				
			}
			else if (gameObject.name == "Player2") {
				
				gameManager2.player2Score++;
				
			}

			gameManager2.totalHits++;
			
		}

	}

	/// <summary>
	/// Check if either player score 
	/// </summary>
	void scoreCheck () {

		if (gameManager2.totalHits == gravityTriggerHitCount) {

			ball.rigidbody.useGravity = true;

			if (coroutineHasRun == false) {

				coroutineHasRun = true;

				// Start coroutine before display question mark
				StartCoroutine(CoroutineActions());

			}

		}

	}

	IEnumerator CoroutineActions ()
	{
		Debug.Log ("CoroutineActions");

		yield return new WaitForSeconds(4f);

		ballQuestionMark.SetActive(true);

		yield return new WaitForSeconds(2f);

		// Change Physics on various objects
		player1.collider.sharedMaterial = physicsMatBouncyMed;
		player2.collider.sharedMaterial = physicsMatBouncyMed;

		player1.rigidbody.useGravity = true;
		player2.rigidbody.useGravity = true;

		player1.rigidbody.isKinematic = false;
		player2.rigidbody.isKinematic = false;

		gameManager2.currentGameState = GameManager2.GameState.none;
			
		yield return new WaitForSeconds(2f);

		gameManager2.currentGameState = GameManager2.GameState.platformPlaying;
		
	}
	
}
