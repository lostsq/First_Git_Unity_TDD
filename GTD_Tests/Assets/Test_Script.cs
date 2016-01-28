using UnityEngine;
using System.Collections;

public class Test_Script : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Test();
	}

    void Test()
    {

        //mouse let go/up.
        if (Input.GetMouseButtonUp(0))
            Debug.Log("Pressed left click.");

        //mouse click down.
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("Mouse Down");

            //Vector3 mousePosition = Input.mousePosition;
            //mousePosition.z = 5f;

            Vector2 v = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //Collider2D[] col = Physics2D.OverlapPointAll(Input.mousePosition);

            Collider2D[] col = Physics2D.OverlapPointAll(v);

                Debug.Log(col.Length);


                foreach (Collider2D c in col)
                {
                    //the sprite renderer attached to the collider object. Should be one for every 2d collider object.
                    SpriteRenderer T_Sprite = c.gameObject.GetComponent <SpriteRenderer>();

                    if (T_Sprite == null)
                    {
                        Debug.Log("Found Non_Sprite");
                    }

                    Debug.Log("Collided with: " + c.gameObject.tag);
                    Debug.Log("Sprite Layer = " + T_Sprite.sortingOrder);
                    //targetPos = c.collider2D.gameObject.transform.position;
                }
            }
    }

}
