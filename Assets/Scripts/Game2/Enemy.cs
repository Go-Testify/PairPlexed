using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isActive = false; // 1 = on;
	public bool occupied = false; // 1 = on;
	public Color newColor;
	private Color origColor;

	public bool canChange = true;
	public SpriteRenderer spriteBody;
	public SpriteRenderer spriteMessage;
	public SpriteRenderer spriteHead;

	private GameManager gameManager;

	// Use this for initialization
	void Start () {
		origColor = Color.white;
		gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>() as GameManager;
		//this.renderer.material.color = newColor;
	}

	// Update is called once per frame
	void Update () {

	}
	
	public void activate(int playerNum) {

		occupied = true;
		//Change Colour
		spriteHead.enabled = true;
		if(playerNum==1)
			spriteHead.color = new Color(0f,243/255f,69/255f);
		else if(playerNum == 2)
			spriteHead.color = new Color(23/255f,0f,243/255f);

		if(!canChange) return;
		this.transform.localScale = Vector3.one * 1.1f;

		spriteBody.material.color = newColor;
		isActive = true;

		//Deactivate
		//StartCoroutine(deactivate());
	}

	public void freakOut(Sprite msgSprite) {
		spriteMessage.sprite = msgSprite;
		spriteMessage.enabled = true;
		StartCoroutine(showMessage());
	}

	IEnumerator showMessage() {
		yield return new WaitForSeconds(3f);
		spriteMessage.enabled = false;
	}
	
	IEnumerator deactivate() {

		yield return new WaitForSeconds(1f);

		spriteBody.enabled = false;
		spriteMessage.enabled = false;
	
		gameManager.currentGameState = GameManager.GameState.playing;
		//gameManager.c
		/*while(isActive)
		{
			yield return new WaitForSeconds(2f);
			if(canChange)
			{
				spriteBody.color = Color.white;
				spriteBody.material.color = Color.white;
				this.transform.localScale = Vector3.one;
			}
			isActive = false;
		}*/
	}

	public void reset() {
		this.transform.localScale = Vector3.one;
		spriteHead.enabled = false;
		spriteBody.color = Color.white;
		spriteBody.material.color = Color.white;
		isActive = false;
		occupied = false;
		StopCoroutine(deactivate());
	}

	public void deactivateCell() {
		//spriteBody.material.color = Color.gray;
		//spriteBody.enabled = false;
		//spriteMessage.enabled = false;
		canChange = false;
		isActive = false;
		StartCoroutine(deactivate());
	}

}
