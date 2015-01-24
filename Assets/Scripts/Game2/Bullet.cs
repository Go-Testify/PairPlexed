using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

	bool isGamePlaying = true;
	public float shootingSpeed = 0.2f;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {
		if(isGamePlaying)
		{
			//move all the bullets forward if they are on the screen
			transform.Translate(transform.up * shootingSpeed);
		}
	}

	void FixedUpdate() {
		
	}

	//=====================  Main functionality  =================
	//Are we hitting anything yet
	void OnTriggerEnter(Collider collider)
	{

		if(collider.CompareTag("Enemy"))
		{
			Destroy(collider.gameObject);
		}
		/*else if(collider.CompareTag("Bounds"))
		{
			Destroy(this.gameObject);
		}*/

		Destroy(this.gameObject);
	}
}
