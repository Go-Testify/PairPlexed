using UnityEngine;
using System.Collections;

public class GameManager2 : MonoBehaviour {

	public enum GameState {
		none, pongPlaying, platformPlaying
	}

	public GameState currentGameState;

	public int player1Score = 0;
	public int player2Score = 0;
	public int totalHits = 0;
	
	// Use this for initialization
	void Start () {
		//start game
		currentGameState = GameState.pongPlaying;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}
}
