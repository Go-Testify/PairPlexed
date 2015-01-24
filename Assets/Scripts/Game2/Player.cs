using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	bool canMoveHorizontal;
	bool canMoveVertical;

	private bool horizontalAxisInUse = false;

	// Use this for initialization
	void Start () {
		canMoveHorizontal = false;
		canMoveVertical = false;
	}

	// Update is called once per frame
	void Update () {

		//Move left
		if(Input.GetKey("a")==true)
		{
			if(horizontalAxisInUse==false && canMoveHorizontal==true)
			{
				//move all the bullets forward if they are on the screen
				iTween.MoveBy(this.gameObject,new Vector3(-1.5f,0,0),0);
				horizontalAxisInUse = true;
			}
		}

		//No moving on Horizontal Axis
		if(Input.GetKey("a") || Input.GetKey("d"))
			horizontalAxisInUse = false;

		//No moving on Vertical Axis
		/*if(Input.GetKey("w") || Input.GetKey("s"))
			horizontalAxisInUse = false;*/
	}

	void FixedUpdate() {
		
	}

	//Checks movement constraints on the player
	void OnCollisionStay(Collision collisionInfo)
	{
		Debug.Log("Hitting Enemy");
		if(collisionInfo.collider.CompareTag("Enemy"))
		{

			foreach (ContactPoint contact in collisionInfo.contacts) {
				print(contact.thisCollider.name + " hit " + contact.otherCollider.name);
				Debug.DrawRay(contact.point, contact.normal, Color.white);
			}
		}
	}
}
