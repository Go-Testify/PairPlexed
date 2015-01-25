using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameManager : MonoBehaviour {

	public enum GameState {
		intro, playing, matching, outroanimation
	}
	enum PlayerMoveState {
		left, right, up, down
	}
	
	private bool horizontalAxisInUsePlayer1 = false;
	private bool verticalAxisInUsePlayer1 = false;

	private bool horizontalAxisInUsePlayer2 = false;
	private bool verticalAxisInUsePlayer2 = false;

	Vector2 player1Position = new Vector2(3,0);
	Vector2 player2Position = new Vector2(4,0);

	private ArrayList enemyPositions = new ArrayList();
	public GameObject[] enemyPositionsRow1;
	public GameObject[] enemyPositionsRow2;
	//public GameObject[] enemyPositionsRow3;

	public Sprite[] randomMessages;
	public AudioSource randomSound;
	public AudioSource successSound;

	private Color[] colors = new Color[]{Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow, 
		new Color(0/255f,100/255f,0/255f)};

	private int colorCount = 0;
	private int gameCount = 0;

	public GameState currentGameState = GameState.playing;

	private bool isFirstPlayer1Move = true;
	private bool isFirstPlayer2Move = true;

	public GameObject[] deleteObjects;
	public GameObject inAppPurchaseText;

	// Use this for initialization
	void Start () {
		enemyPositions.Add(enemyPositionsRow1);
		enemyPositions.Add(enemyPositionsRow2);
		//enemyPositions.Add(enemyPositionsRow3);

		StartCoroutine(randomEnemyFreakOut());

	}

	IEnumerator randomEnemyFreakOut()
	{
		while(currentGameState == GameState.playing)
		{
			yield return new WaitForSeconds(4f);

			//choose a random message
			int randMessageNum = Random.Range(0,randomMessages.Length-1);
			//get the random enemy
			int randRow = Random.Range(0,enemyPositions.Count);
			int randColumn = Random.Range(0,enemyPositionsRow1.Length-1);

			GameObject[] enemyRow = enemyPositions[randRow] as GameObject[];
			Enemy enemy = enemyRow[randColumn].GetComponent<Enemy>();

			//call freakout - play sound
			randomSound.Play();
			enemy.freakOut(randomMessages[randMessageNum]);
		}
	}

	void checkMatchState()
	{
		//Dont check same positions
		if(player1Position.x == player2Position.x && player1Position.y==player2Position.y)
			return;

		GameObject[] enemyRow1 = enemyPositions[(int)player1Position.y] as GameObject[];
		if(enemyRow1[(int)player1Position.x]==null) return;
		Enemy player1Enemy = enemyRow1[(int)player1Position.x].GetComponent<Enemy>();

		GameObject[] enemyRow2 = enemyPositions[(int)player2Position.y] as GameObject[];
		if(enemyRow2[(int)player2Position.x]==null) return;
		Enemy player2Enemy = enemyRow2[(int)player2Position.x].GetComponent<Enemy>();

		if(player1Enemy.newColor == player2Enemy.newColor && player1Enemy.isActive && player2Enemy.isActive)
		{
			successSound.Play();

			Debug.Log ("Matching");

			currentGameState = GameState.matching;

			//delete both enemies
			//Destroy(player1Enemy.gameObject);
			//Destroy(player2Enemy.gameObject);

			player1Enemy.deactivateCell();
			player2Enemy.deactivateCell();

			gameCount++;
			Debug.Log(gameCount);
		}

		//
		if(gameCount == 8)
		{
			foreach(GameObject go in deleteObjects)
			{
				Destroy(go);
				inAppPurchaseText.SetActive(true);
			}
			Debug.Log ("End Game");
		}

		/*int numRows = enemyPositions.Count;
		int numColumns = enemyPositionsRow1.Length;

		for(int i=0; i<numColumns; i++)
		{
			for(int j=0; j<numRows; j++)
			{
				Debug.Log("i" + i);
				Debug.Log("j" + j);
				Debug.Log("colorCount" + colorCount);

				GameObject[] enemyRow = enemyPositions[j] as GameObject[];
				Enemy enemy = enemyRow[i].GetComponent<Enemy>();

				//Set the color if it doesnt have one, then go and set another random squares colour
				if(enemy.currentColor == Color.clear) 
				{
					enemy.setColor(colors[colorCount]);
					setMatchingEnemyColour();

					if(colorCount == colors.Length-1)
						break;
				}
			}

			if(colorCount == colors.Length-1)
				break;
		}*/
	}

	//recursively searches for another cell
	/*bool setMatchingEnemyColour()
	{

		//Randomly select another square
		int randColumn = Random.Range(0,enemyPositionsRow1.Length-1);
		int randRow = Random.Range(0,enemyPositions.Count-1);

		Debug.Log ("setMatchingEnemyColour randRow - " + randRow);
		Debug.Log ("setMatchingEnemyColour randColumn - " + randColumn);

		GameObject[] enemyRow = enemyPositions[randRow] as GameObject[];
		Enemy enemy = enemyRow[randColumn].GetComponent<Enemy>();
		if(enemy.currentColor == Color.clear) 
		{
			enemy.setColor(colors[colorCount]);
			colorCount++;
			return true;
		}
		else
		{
			return setMatchingEnemyColour();
		}
	}*/
	
	// Update is called once per frame
	void Update () {

		if(currentGameState != GameState.playing) return;

		//// PLAYER ONE
		 /// 
		//Move left
		if(Input.GetKeyDown("a")==true)
		{
			if(horizontalAxisInUsePlayer1==false)
			{
				horizontalAxisInUsePlayer1 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.left, 1);

			}
		}

		//Move right
		if(Input.GetKeyDown("d")==true)
		{
			if(horizontalAxisInUsePlayer1==false)
			{
				horizontalAxisInUsePlayer1 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.right, 1);

			}
		}

		//Move up
		if(Input.GetKeyDown("w")==true)
		{
			if(verticalAxisInUsePlayer1==false)
			{
				verticalAxisInUsePlayer1 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.up, 1);
				
			}
		}
		
		//Move down
		if(Input.GetKeyDown("s")==true)
		{
			if(verticalAxisInUsePlayer1==false)
			{
				verticalAxisInUsePlayer1 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.down, 1);
				
			}
		}
		
		//No moving on Horizontal Axis
		if(Input.GetKeyDown("a")==false || Input.GetKeyDown("d")==false)
		{
			horizontalAxisInUsePlayer1 = false;
		}
		
		//No moving on Vertical Axis
		if(Input.GetKeyDown("w")==false || Input.GetKeyDown("s")==false)
		{
			verticalAxisInUsePlayer1 = false;
		}

		//// PLAYER TWO 


		//Move left
		if(Input.GetKeyDown("left")==true)
		{
			if(horizontalAxisInUsePlayer2==false)
			{
				horizontalAxisInUsePlayer2 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.left, 2);
				
			}
		}
		
		//Move right
		if(Input.GetKeyDown("right")==true)
		{
			if(horizontalAxisInUsePlayer2==false)
			{
				horizontalAxisInUsePlayer2 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.right, 2);
				
			}
		}
		
		//Move up
		if(Input.GetKeyDown("up")==true)
		{
			if(verticalAxisInUsePlayer2==false)
			{
				verticalAxisInUsePlayer2 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.up, 2);
				
			}
		}
		
		//Move down
		if(Input.GetKeyDown("down")==true)
		{
			if(verticalAxisInUsePlayer2==false)
			{
				verticalAxisInUsePlayer2 = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.down, 2);

			}
		}
		
		//No moving on Horizontal Axis
		if(Input.GetKeyDown("left")==false || Input.GetKeyDown("right")==false)
		{
			horizontalAxisInUsePlayer2 = false;
		}
		
		//No moving on Vertical Axis
		if(Input.GetKeyDown("up")==false || Input.GetKeyDown("down")==false)
		{
			verticalAxisInUsePlayer2 = false;
		}

		//cehck if two objects are standing on t he same square
		checkMatchState();
	}

	void movePlayerPosition(PlayerMoveState moveState, int playerNum)
	{

		Vector2 tempPlayerPositions = new Vector2();
		Vector2 origPlayerPositions = new Vector2();
		if(playerNum==1) tempPlayerPositions = origPlayerPositions = player1Position;
		else if(playerNum==2) tempPlayerPositions = origPlayerPositions = player2Position;

		//Move player
		/*switch(moveState)
		{
			case PlayerMoveState.left: {
				//what player is 
				if(playerNum==1 && tempPlayerPositions.x>0) tempPlayerPositions.x -= 1;
				else if(playerNum==2 && player2Position.x>0) player2Position.x -= 1;
				break;
			}
			case PlayerMoveState.right: {
				if(playerNum==1 && player1Position.x<enemyPositionsRow1.Length-1) player1Position.x += 1;
				else if(playerNum==2 && player2Position.x<enemyPositionsRow1.Length-1) player2Position.x += 1;
				break;
			}
			case PlayerMoveState.up: {
				//what player is 
				if(playerNum==1 && player1Position.y>0) player1Position.y -= 1;
				else if(playerNum==2 && player2Position.y>0) player2Position.y -= 1;
				break;
			}
			case PlayerMoveState.down: {
				if(playerNum==1 && player1Position.y<enemyPositions.Count-1) player1Position.y += 1;
				else if(playerNum==2 && player2Position.y<enemyPositions.Count-1) player2Position.y += 1;
				break;
			}
		}*/

		switch(moveState)
		{
		case PlayerMoveState.left: {
			//what player is 
			if(playerNum==1 && tempPlayerPositions.x>0) tempPlayerPositions.x -= 1;
			else if(playerNum==2 && tempPlayerPositions.x>0) tempPlayerPositions.x -= 1;
			break;
		}
		case PlayerMoveState.right: {
			if(playerNum==1 && tempPlayerPositions.x<enemyPositionsRow1.Length-1) tempPlayerPositions.x += 1;
			else if(playerNum==2 && tempPlayerPositions.x<enemyPositionsRow1.Length-1) tempPlayerPositions.x += 1;
			break;
		}
		case PlayerMoveState.up: {
			//what player is 
			if(playerNum==1 && tempPlayerPositions.y>0) tempPlayerPositions.y -= 1;
			else if(playerNum==2 && tempPlayerPositions.y>0) tempPlayerPositions.y -= 1;
			break;
		}
		case PlayerMoveState.down: {
			if(playerNum==1 && tempPlayerPositions.y<enemyPositions.Count-1) tempPlayerPositions.y += 1;
			else if(playerNum==2 && tempPlayerPositions.y<enemyPositions.Count-1) tempPlayerPositions.y += 1;
			break;
		}
		}

		//Only move it not occupied
		GameObject[] enemyRow;
		enemyRow = enemyPositions[(int)tempPlayerPositions.y] as GameObject[];
		if(enemyRow[(int)tempPlayerPositions.x] != null)
		{
			if(enemyRow[(int)tempPlayerPositions.x].GetComponent<Enemy>().occupied) return;
		}

		//Reset the old state before move
		enemyRow = enemyPositions[(int)origPlayerPositions.y] as GameObject[];
		if(enemyRow[(int)origPlayerPositions.x] != null)
		{
			enemyRow[(int)origPlayerPositions.x].GetComponent<Enemy>().reset();
		}

		//all good - just move player
		if(playerNum==1) player1Position = tempPlayerPositions;
		else if(playerNum==2) player2Position = tempPlayerPositions;

		//Set the player positions in the game
		setPlayerPositions(playerNum);
	}

	void setPlayerPositions(int playerNum)
	{

		GameObject[] enemyRow;

		//Set player 1 position with the enemy
		if(playerNum==1)
		{
			enemyRow = enemyPositions[(int)player1Position.y] as GameObject[];
			if(enemyRow[(int)player1Position.x] != null)
				enemyRow[(int)player1Position.x].GetComponent<Enemy>().activate(playerNum);
		}

		if(playerNum==2)
		{
			enemyRow = enemyPositions[(int)player2Position.y] as GameObject[];
			if(enemyRow[(int)player2Position.x] != null)
				enemyRow[(int)player2Position.x].GetComponent<Enemy>().activate(playerNum);
		}
	}
}
