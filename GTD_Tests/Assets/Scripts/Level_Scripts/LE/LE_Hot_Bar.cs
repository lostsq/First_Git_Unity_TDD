using UnityEngine;
using System.Collections;

public class LE_Hot_Bar : MonoBehaviour {

    float f_Scale_Amount = 1;


    //This is the bar box that will be along the hot bar.
    public GameObject go_Bar_Box;
    public GameObject go_Mid_Bar_Box;
    private GameObject This_GameObject;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}



    public void Place_Down(float Passed_Scale_Amount, GameObject Passed_This)
    {
        //we pass the scale amount that all the buttons in the hotbar will be.
        f_Scale_Amount = Passed_Scale_Amount;
        //Seems i have to do this or have more code to find this, because "this." or "gameObject" does not access this one.
        This_GameObject = Passed_This;
        //Debug.Log("Place Down Test");
        GameObject go_Cur_Mid_Box = Instantiate(go_Mid_Bar_Box);


        //ok now we need to determain how many spots we have.
        //get the collider of this hotbox.
        BoxCollider2D v_HB_Collider = This_GameObject.GetComponent<BoxCollider2D>();
        //figure out the side of the collider and the scale and do math stuffs.
        float f_HB_x_Size = v_HB_Collider.bounds.size.x;
        //the size of boxes
        float f_Bar_Box_Size = go_Cur_Mid_Box.GetComponent<BoxCollider2D>().bounds.size.x * f_Scale_Amount;
        //now with the size we divide to find out how many bar boxes we can have. it will round down. and we minus 1 and divide by 2 for even on both sides.


        int i_Box_Count = (int)(f_HB_x_Size / f_Bar_Box_Size);
        i_Box_Count = (i_Box_Count - 1) / 2;

        print(i_Box_Count);
        //now with the box count we can start creating them and placing them on the bar.
        //lets move the center box after scaling it.
        go_Cur_Mid_Box.transform.localScale = new Vector2(f_Scale_Amount, f_Scale_Amount);
        go_Cur_Mid_Box.transform.position = new Vector3(This_GameObject.transform.position.x,This_GameObject.transform.position.y,-1);

        float f_Space_Between = (f_HB_x_Size - f_Bar_Box_Size) / 2;
        float f_indi_space = (f_Space_Between / i_Box_Count) - f_Bar_Box_Size;

        //now place in and scale/position the various bar boxes.
        for (int i = 0; i < i_Box_Count; i++)
        {
            //create the two boxes to place on each side.
            GameObject go_Box_One = Instantiate(go_Bar_Box);
            GameObject go_Box_Two = Instantiate(go_Bar_Box);
            //modify the scale of them.
            go_Box_One.transform.localScale = new Vector2(f_Scale_Amount, f_Scale_Amount);
            go_Box_Two.transform.localScale = new Vector2(f_Scale_Amount, f_Scale_Amount);
            //move them to the correct area.
            go_Box_One.transform.position = new Vector2((i* (f_Bar_Box_Size + f_indi_space)) + f_Bar_Box_Size , go_Cur_Mid_Box.transform.position.y);
            go_Box_Two.transform.position = new Vector2(-1*((i * (f_Bar_Box_Size + f_indi_space)) + f_Bar_Box_Size), go_Cur_Mid_Box.transform.position.y);
        }


        //Debug.Log(i_Box_Count);




    }
}
