using UnityEngine;
using System.Collections;

public class Shooter : MonoBehaviour {

	Vector2 currentPos;
	Vector2 newPos;

	//----- Movement Vars  -----
	public bool isGamePlaying = true;

	public float timeToMove = 0.5f;
	
	public float boundsX_Left = -13f;
	public float boundsX_Right = 13f;

	public float minRandomWaitingtime = 2f;
	public float maxRandomWaitingtime = 3f;
	
	private ArrayList wallPositionsBounds = new ArrayList();

	//----- Shoting Vars -----
	private bool isFiring = true;
	public float fireTime = 5f;
	public GameObject bulletPrefab;

	// Use this for initialization
	void Start () {
		init();
	}

	// Update is called once per frame
	void Update () {

	}

	void FixedUpdate() {

	}

	//=====================  Main functionality  =================

	//init the game scene
	void init()
	{
		//Setup all the wall Positions for the shooter to avoid
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");

		foreach(GameObject wall in walls)
		{
			wallPositionsBounds.Add(new Vector2(wall.renderer.bounds.min.x, wall.renderer.bounds.max.x));
		}

		startGame();
	}

	//Sets up the logic to start the game
	void startGame()
	{
		//Start moving the shooter and firing
		StartCoroutine(setNewRandomPosition());
		//StartCoroutine(fireBullet());
	}

	//Will move to the position in between the walls - keep trying to a new position until it is happy
	//it wont hit walls when it shoots - shooter AI
	float getNewShooterXPosition()
	{
		float attemptedNewXPosition = Random.Range(boundsX_Right, boundsX_Left);
		
		//Are we gonna hit any walls
		bool isInWallBounds = false;
		foreach(Vector2 wallBounds in wallPositionsBounds)
		{
			//check the x position is not within the bounds 
			if(attemptedNewXPosition >= wallBounds.x && attemptedNewXPosition <= wallBounds.y)
			{
				isInWallBounds = true;
				break;
			}
		}
		
		if(isInWallBounds == false) return attemptedNewXPosition;
		else return getNewShooterXPosition();
	}
	
	//=====================  Events  =================

	IEnumerator setNewRandomPosition()
	{
		while ( isGamePlaying ) {

			//work out the max position it will move within the bounds it has available
			float newXPosition = getNewShooterXPosition();
			newPos = new Vector3(newXPosition, this.transform.localPosition.y, this.transform.localPosition.z);

			float nextWaitTime = Random.Range(minRandomWaitingtime, maxRandomWaitingtime);
			iTween.MoveTo(this.gameObject, newPos, timeToMove);

			yield return new WaitForSeconds(nextWaitTime);
		}

	}

	IEnumerator fireBullet()
	{
		while ( isGamePlaying ) {
			//Instiate a shooting game object at center and apply a force to fire it
			Vector3 bulletPos = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y + 0.5f, this.transform.localPosition.z);
			GameObject.Instantiate(bulletPrefab, transform.position + (transform.forward * 2), Quaternion.identity);

			yield return new WaitForSeconds(fireTime);
		}
		
	}

}
