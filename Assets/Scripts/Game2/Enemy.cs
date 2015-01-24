using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public bool isActive = false; // 1 = on;
	public Color newColor;
	private Color origColor;

	public bool canChange = true;

	// Use this for initialization
	void Start () {
		origColor = this.renderer.material.color;
		//this.renderer.material.color = newColor;
	}

	// Update is called once per frame
	void Update () {

	}
	
	public void activate() {

		if(!canChange) return;
		this.transform.localScale = Vector3.one * 1.2f;

		//Change Colour
		this.renderer.material.color = newColor;
		isActive = true;

		//Deactivate
		StartCoroutine(deactivate());
	}
	
	IEnumerator deactivate() {
		while(isActive)
		{
			yield return new WaitForSeconds(2f);
			if(canChange)
			{
				this.renderer.material.color = origColor;
				this.transform.localScale = Vector3.one;
			}
			isActive = false;
		}
	}

	public void deactivateCell() {
		this.renderer.material.color = Color.gray;
		canChange = false;
		isActive = false;
	}

}
