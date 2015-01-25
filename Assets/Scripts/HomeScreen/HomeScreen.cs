using UnityEngine;
using System.Collections;

public class HomeScreen : MonoBehaviour {


	public GameObject[] squares;

	string stringToFind;
	string stringSuffix;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		// W
		if (Input.GetKeyDown(KeyCode.W)) {

			stringSuffix = "W";
			highlightSquare (stringSuffix);

		}
		
		// A
		if (Input.GetKey(KeyCode.A)) {
			stringSuffix = "A";
			highlightSquare (stringSuffix);

		}

		// S
		if (Input.GetKey(KeyCode.S)) {
			stringSuffix = "S";
			highlightSquare (stringSuffix);

		}

		// D
		if (Input.GetKey(KeyCode.D)) {
			stringSuffix = "D";
			highlightSquare (stringSuffix);

		}
		
		// Up Arrow
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			stringSuffix = "Up";
			highlightSquare (stringSuffix);

		}
		
		// Left Arrow
		if (Input.GetKey(KeyCode.LeftArrow)) {
			stringSuffix = "Left";
			highlightSquare (stringSuffix);

		}
		
		// Down Arrow
		if (Input.GetKey(KeyCode.DownArrow)) {
			stringSuffix = "Down";
			highlightSquare (stringSuffix);

		}

		// Right Arrow
		if (Input.GetKey(KeyCode.RightArrow)) {
			stringSuffix = "Right";
			highlightSquare (stringSuffix);

		}
	}


	void highlightSquare (string stringSuffix) {

		stringToFind = "pixel_wht" + stringSuffix;

		foreach (GameObject square in squares)
		{

			Debug.Log (square.name);
			//if (x.Equals (stringToCheck))
			//{
			//	MessageBox.Show("Find the string ..." + x);
			//}
		}



	}

}
