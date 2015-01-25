using UnityEngine;
using System.Collections;

public class Paddles : MonoBehaviour {

	public GameManager2 gameManager2;
	public PhysicMaterial physicsMatBouncyMax;
	public PhysicMaterial physicsMatBouncyMed;

	public bool coroutineHasRun = false;

	float speedPongPaddles = 0.2f;



	GameObject ball;
	GameObject ballQuestionMark;
	GameObject ballExclamationMark;
	GameObject ballLightbulb; 
	GameObject player1;
	GameObject player2;
	GameObject borderBottomGap;

	string controlSystem;

	int jumpSpeed = 300;
	bool isFalling = false;
	float speed = 0.1f;
	
	// Use this for initialization
	void Start () {

		// Get a reference to the ball
		ball = GameObject.Find("Ball");
		ballQuestionMark = ball.transform.Find("Mark").gameObject;
		ballExclamationMark = ball.transform.Find("Exclamataion").gameObject;
		ballLightbulb = ball.transform.Find("Lightbulb").gameObject;
		player1 = GameObject.Find("Player1");
		player2 = GameObject.Find("Player2");

		borderBottomGap = GameObject.Find ("BorderBottomGap");
	}
	
	// Update is called once per frame
	void Update () {
	
		switch (gameManager2.currentGameState)
		{
		case GameManager2.GameState.pongPlaying: case GameManager2.GameState.pongAnimating: case GameManager2.GameState.pongPlaying2:
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

		// Check if score has hit set limit - then trigger gravity
		scoreCheck ();
	}
		
	//=========================  Pong Controls START =========================
	void pongControls () {
			
		// Move each player based on keyboard input
		if (gameObject.name == "Player1") {

			// Up
			if (Input.GetKey(KeyCode.W)) {
				
				transform.Translate(0, speedPongPaddles, 0);
			}
			
			// Down
			if (Input.GetKey(KeyCode.S)) {
				
				transform.Translate(0, -speedPongPaddles, 0);
			}

			// Set boundaries, top/bottom of the screen that the paddle can't cross
			Vector3 pos = transform.position;
			//pos.y = Mathf.Clamp(pos.y, 4.0f, 13.0f);
			pos.y = Mathf.Clamp(pos.y, 2.5f, 14.5f);
			transform.position = pos;

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

			// Set boundaries, top/bottom of the screen that the paddle can't cross
			Vector3 pos = transform.position;
			//pos.y = Mathf.Clamp(pos.y, 4.0f, 13.0f);
			pos.y = Mathf.Clamp(pos.y, 2.5f, 14.5f);
			transform.position = pos;

		}

	}

	//=========================  Pong Controls END =========================
	
	//=========================  Platform Controls START =========================
	void platformControls () {

		// Move each player based on keyboard input - START
		if (gameObject.name == "Player1") {
			
			// Jump
			if (Input.GetKeyDown(KeyCode.W) && isFalling == false) {

				gameObject.GetComponent<AudioSource>().Play();
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
					
				gameObject.GetComponent<AudioSource>().Play();
				rigidbody.AddForce(Vector3.up * jumpSpeed);

				//animation.Play();
				
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

		//Debug.Log ("OnCollisionStay");
		// this makes it only possible to jump when you are on the ground
		//isFalling = false;

	}

	//=========================  Platform Controls END =========================
	
	/// <summary>
	/// Raises the collision enter event to increase player score
	/// </summary>
	void OnCollisionEnter (Collision other) {

		if (other.gameObject.name == "BorderBottomRight" || other.gameObject.name == "Ball" || other.gameObject.name == "BorderBottomLeft" || other.gameObject.name == "Player1" || other.gameObject.name == "Player2")
		{
			isFalling = false;
		}

		Debug.Log (other.gameObject.name);

		if (other.gameObject.name == "Key")
		{
			other.gameObject.GetComponent<AudioSource>().Play();
			Destroy(other.gameObject);
			borderBottomGap.SetActive(false);
		}

		if (other.gameObject.name == "Ball")
		{
			if ((gameManager2.currentGameState == GameManager2.GameState.pongPlaying || gameManager2.currentGameState == GameManager2.GameState.pongPlaying2))
			{
				if (gameObject.name == "Player1") gameObject.GetComponent<AudioSource>().Play();
				else if (gameObject.name == "Player2") gameObject.GetComponent<AudioSource>().Play();
			}

			scoreCounter (other);
		}

		/*if (other.gameObject.name == "EndGameTrigger")
		{
			Debug.Log ("EndGameTrigger");
			gameManager2.gameOverCount++;
			if(gameManager2.gameOverCount == 2)
			{
				Debug.Log ("End Game");
				Application.LoadLevel(2);
			}
		}*/

	}
	
	void OnTriggerEnter (Collider other) {

		if (other.gameObject.name == "EndGameTrigger")
		{
			Debug.Log ("EndGameTrigger");
			gameManager2.gameOverCount++;
			if(gameManager2.gameOverCount == 2)
			{
				Debug.Log ("End Game");
				Application.LoadLevel(2);
			}
		}
		
	}
	
	/// <summary>
	/// Keeps track of player scores
	/// </summary>
	void scoreCounter (Collision other) {
		
		// Check which player paddle the ball has hit and increment their score
		if (other.gameObject.name == "Ball") {


			/*if (gameObject.name == "Player1") {
				
				gameManager2.player1Score++;
				gameManager2.player1ScoreTxt.text = gameManager2.player1Score + "";
			}
			else if (gameObject.name == "Player2") {
				
				gameManager2.player2Score++;
				gameManager2.player2ScoreTxt.text = gameManager2.player2Score + "";
			}*/

			gameManager2.totalHits++;
			
		}

	}

	/// <summary>
	/// Check if either player score 
	/// </summary>
	void scoreCheck () {

		if (gameManager2.totalHits == gameManager2.gravityTriggerHitCount && (gameManager2.currentGameState == GameManager2.GameState.pongPlaying || gameManager2.currentGameState == GameManager2.GameState.pongPlaying2)) {

			ball.rigidbody.useGravity = true;
			ball.rigidbody.drag = 1;
			gameManager2.totalHits = 0;
			gameManager2.player1Score = 0;
			gameManager2.player1ScoreTxt.text = gameManager2.player1Score + "0";
			gameManager2.player2Score = 0;
			gameManager2.player2ScoreTxt.text = gameManager2.player2Score + "0";
			StartCoroutine(CoroutineActions());
		}
		else if (gameManager2.currentGameState == GameManager2.GameState.pongEnding) 
		{
			gameManager2.currentGameState = GameManager2.GameState.platformPlaying;

			// Change Physics materials on various objects
			//player1.collider.sharedMaterial = physicsMatBouncyMed;
			//player2.collider.sharedMaterial = physicsMatBouncyMed;

			player1.collider.sharedMaterial = null;
			player2.collider.sharedMaterial = null;
			
			// Add gravity to the players so they fall
			player1.rigidbody.useGravity = true;
			player2.rigidbody.useGravity = true;
			
			// Return physics based movement to the players
			player1.rigidbody.isKinematic = false;
			player2.rigidbody.isKinematic = false;
		}

	}

	IEnumerator CoroutineActions ()
	{
		Debug.Log ("CoroutineActions");

		ball.collider.sharedMaterial = physicsMatBouncyMed;

		yield return new WaitForSeconds(4f);

		if (gameManager2.currentGameState == GameManager2.GameState.pongPlaying2)
			ballExclamationMark.SetActive(true);
		else
			ballQuestionMark.SetActive(true);

		yield return new WaitForSeconds(5f);

		// Only allow paddles to fall if you are in the 2nd pong play phase
		if (gameManager2.currentGameState == GameManager2.GameState.pongPlaying2) {

			// Change Physics materials on various objects
			//player1.collider.sharedMaterial = physicsMatBouncyMed;
			//player2.collider.sharedMaterial = physicsMatBouncyMed;

			player1.collider.sharedMaterial = null;
			player2.collider.sharedMaterial = null;

			// Add gravity to the players so they fall
			player1.rigidbody.useGravity = true;
			player2.rigidbody.useGravity = true;

			// Return physics based movement to the players
			player1.rigidbody.isKinematic = false;
			player2.rigidbody.isKinematic = false;
		}

		// Move to platform phase if you are in the 2nd pong play phase
		if (gameManager2.currentGameState == GameManager2.GameState.pongPlaying2) {
			gameManager2.currentGameState = GameManager2.GameState.platformPlaying;
			yield return new WaitForSeconds(2f);
			ballExclamationMark.SetActive(false);
			ball.collider.sharedMaterial = null;
			gameManager2.ShowEnd();
			yield return new WaitForSeconds(2f);
		}

		// Move to 2nd pong game phase if you are in the 1st pong play phase
		else if (gameManager2.currentGameState == GameManager2.GameState.pongPlaying) {
			ballQuestionMark.SetActive(false);
			yield return new WaitForSeconds(3f);
			ballLightbulb.SetActive(true);
			yield return new WaitForSeconds(2f);
			ballLightbulb.SetActive(false);
			player1.transform.Find("Question Mark").gameObject.SetActive(true);
			player2.transform.Find("Question Mark").gameObject.SetActive(true);
			yield return new WaitForSeconds(3f);
			player1.transform.Find("Question Mark").gameObject.SetActive(false);
			player2.transform.Find("Question Mark").gameObject.SetActive(false);
			gameManager2.currentGameState = GameManager2.GameState.pongAnimating;
			gameManager2.gravityTriggerHitCount = 10;
			//Play the cutscene
			gameManager2.PlayCutScene();
		}
	}

	IEnumerator CoroutineBallActions ()
	{
		ball.collider.sharedMaterial = physicsMatBouncyMed;

		    for (int i = 1; i >= 0; i += 1) {

				yield return new WaitForSeconds(4f);
				
				ballQuestionMark.SetActive(true);
				
				yield return new WaitForSeconds(1f);
				
				ballQuestionMark.SetActive(false);

		}

		
	}

}
