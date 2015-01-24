using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

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

	private Color[] colors = new Color[]{Color.black, Color.blue, Color.cyan, Color.gray, Color.green, Color.magenta, Color.red, Color.white, Color.yellow, 
		new Color(0/255f,100/255f,0/255f)};

	private int colorCount = 0;
	private int gameCount = 0;

	// Use this for initialization
	void Start () {
		enemyPositions.Add(enemyPositionsRow1);
		enemyPositions.Add(enemyPositionsRow2);
		//enemyPositions.Add(enemyPositionsRow3);
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
			Debug.Log ("Matching");

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
			Debug.Log ("End Game");

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
		switch(moveState)
		{
			case PlayerMoveState.left: {
				//what player is 
				if(playerNum==1 && player1Position.x>0) player1Position.x -= 1;
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
			
		}
		
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
				enemyRow[(int)player1Position.x].GetComponent<Enemy>().activate();
		}

		if(playerNum==2)
		{
			enemyRow = enemyPositions[(int)player2Position.y] as GameObject[];
			if(enemyRow[(int)player2Position.x] != null)
				enemyRow[(int)player2Position.x].GetComponent<Enemy>().activate();
		}
	}
}
