using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using CinemaDirector;

public class GameManager2 : MonoBehaviour {

	public enum GameState {
		none, pongPlaying, pongAnimating, pongPlaying2, pongEnding, platformPlaying
	}

	public GameState currentGameState;

	public int player1Score = 0;
	public int player2Score = 0;
	public int totalHits = 0;
	public int gravityTriggerHitCount = 5;

	public Cutscene cutscene;
	public GameObject endKey;

	public int gameOverCount = 0;

	public Text player1ScoreTxt;
	public Text player2ScoreTxt;

	// Use this for initialization
	void Start () {
		//start game
		currentGameState = GameState.pongPlaying;
	}
	
	// Update is called once per frame
	void Update () {
	
	
	}

	public void PlayCutScene () {
		cutscene.Play();
	}

	public void ShowEnd () {

		currentGameState = GameState.pongEnding;
		endKey.SetActive(true);
	}
}
