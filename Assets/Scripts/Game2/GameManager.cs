using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	enum PlayerMoveState {
		left, right, up, down
	}
	
	private bool horizontalAxisInUse = false;
	private bool verticalAxisInUse = false;

	Vector2 player1Position = new Vector2(3,1);
	Vector2 player2Position = new Vector2(5,1);

	private ArrayList enemyPositions = new ArrayList();
	public GameObject[] enemyPositionsRow1;
	public GameObject[] enemyPositionsRow2;
	public GameObject[] enemyPositionsRow3;
	public GameObject[] enemyPositionsRow4;

	// Use this for initialization
	void Start () {
		enemyPositions.Add(enemyPositionsRow1);
		enemyPositions.Add(enemyPositionsRow2);
		enemyPositions.Add(enemyPositionsRow3);
		enemyPositions.Add(enemyPositionsRow4);
	}
	
	// Update is called once per frame
	void Update () {
		
		//Move left
		if(Input.GetKeyDown("a")==true)
		{
			if(horizontalAxisInUse==false)
			{
				horizontalAxisInUse = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.left, 1);

			}
		}

		//Move right
		if(Input.GetKeyDown("d")==true)
		{
			if(horizontalAxisInUse==false)
			{
				horizontalAxisInUse = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.right, 1);

			}
		}

		//Move up
		if(Input.GetKeyDown("w")==true)
		{
			if(verticalAxisInUse==false)
			{
				verticalAxisInUse = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.up, 1);
				
			}
		}
		
		//Move down
		if(Input.GetKeyDown("s")==true)
		{
			if(horizontalAxisInUse==false)
			{
				verticalAxisInUse = true;
				//move all the bullets forward if they are on the screen
				movePlayerPosition(PlayerMoveState.down, 1);
				
			}
		}
		
		//No moving on Horizontal Axis
		if(Input.GetKeyDown("a")==false || Input.GetKeyDown("d")==false)
		{
			horizontalAxisInUse = false;
		}
		
		//No moving on Vertical Axis
		if(Input.GetKeyDown("w")==false || Input.GetKeyDown("s")==false)
		{
			verticalAxisInUse = false;
		}
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
		Debug.Log((int)player1Position.x);
		Debug.Log((int)player1Position.y);

		GameObject[] enemyRow;
		//Set player 1 position with the enemy
		if(playerNum==1)
		{
			enemyRow = enemyPositions[(int)player1Position.y] as GameObject[];
			enemyRow[(int)player1Position.x].SetActive(false);
		}

		if(playerNum==2)
		{
			enemyRow = enemyPositions[(int)player2Position.y] as GameObject[];
			enemyRow[(int)player2Position.x].SetActive(false);
		}
	}
}
