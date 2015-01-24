using UnityEngine;
using System.Collections;

public class EnemyRow : MonoBehaviour {

	bool isGamePlaying = true;
	public int direction = 1;
	public float moveXAmount = 0.25f;
	public float fireTime = 0.5f;
	public float maxBounds = 5f;

	public float firstFireDelay;
	private bool firstFire = true;

	// Use this for initialization
	void Start () {

		StartCoroutine(move());

	}

	// Update is called once per frame
	void Update () {
		if(isGamePlaying)
		{
			//Debug.Log ("localposition"+Mathf.Abs(this.transform.localPosition.x));

			//check if the enemy has hit their bounds && reverse the sign
			if(this.transform.localPosition.x >= maxBounds)
			{
				direction = -1;
			}
			else if(this.transform.localPosition.x <= -maxBounds)
			{
				direction = 1;
			}
		}
	}

	void FixedUpdate() {

	}


	//=====================  Main functionality  =================
	//Are we hitting anything yet

	IEnumerator move()
	{
		while ( isGamePlaying ) {

			if(firstFire) yield return new WaitForSeconds(firstFireDelay);
			firstFire = false;
			//Move the enemy row while the game is playing - direction dictates if we are moving forward or back
			Vector3 newPos = new Vector3(this.transform.position.x + (direction * moveXAmount), this.transform.position.y, this.transform.position.z);

			iTween.MoveTo(this.gameObject, newPos, 0);
			yield return new WaitForSeconds(fireTime);
		}
		
	}

	/*void OnTriggerEnter(Collider collider)
	{
		if(collider.CompareTag("Bounds"))
		{

		}
	}*/
}
