using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	bool isActive = true; // 1 = on;
	Color currentColor;

	// Use this for initialization
	void Start () {

	}

	// Update is called once per frame
	void Update () {

	}

	public void activate() {
		StartCoroutine(deactivate());
	}

	IEnumerator deactivate() {
		while(isActive)
		{
			isActive = false;
			yield return new WaitForSeconds(2f);
			//this.renderer.material.color
		}
	}

}
