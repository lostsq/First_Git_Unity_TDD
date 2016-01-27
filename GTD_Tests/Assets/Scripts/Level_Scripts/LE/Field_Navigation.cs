using UnityEngine;
using System.Collections;

public class Field_Navigation : MonoBehaviour {

	// Use this for initialization
	void Awake () {
        //Debug.Log("Wake field called");

        Camera camera = Camera.main;
        Vector2 Start_Spot = camera.ScreenToWorldPoint(new Vector2(64, camera.pixelHeight - 64));
        this.transform.localPosition = Start_Spot;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
