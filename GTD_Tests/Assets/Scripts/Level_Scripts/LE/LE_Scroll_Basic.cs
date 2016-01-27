using UnityEngine;
using System.Collections;

public class LE_Scroll_Basic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        //when the scrollwheel is moved while 

	     transform.Translate(Vector3.up * Input.GetAxis("Mouse ScrollWheel"));
	}
}
