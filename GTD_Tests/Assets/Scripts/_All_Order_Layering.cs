using UnityEngine;
using System.Collections;

public class _All_Order_Layering : MonoBehaviour {

    //This script is to make sure that the z position matches up to the layer order of the sprite so that the clicks and such work correctly.
    //just make sure the camera is set very far back. like -1000.

    //This is the sprite renderer attached to every object this is attached to.
    SpriteRenderer SR;

	// Use this for initialization
	void Start () {

        SR = GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	}

    void FixedUpdate()
    {
        //check if the sorting/layer order is equal to the z posistion. have to times by negative 1 to be closer/further from camera.
        if (SR.sortingOrder != (transform.localPosition.z * -1))
        {
            //This is the spot with the correct z position.
            Vector3 temp_v = new Vector3(transform.localPosition.x, transform.localPosition.y, (SR.sortingOrder * -1));

            //set the position. this should only be called when the layering order changes.
            transform.localPosition = temp_v;
        }
    }
}
