using UnityEngine;
using System.Collections;

public class LE_Invin_Button : MonoBehaviour {

    private string textFieldString = "text field";
    public GameObject Menu_To_Activate;

    bool b_Menu_On = false;

    // Use this for initialization
    void Start()
    {

        //Debug.Log("Start_Test");
        //this will find a game object with that name. so can't have more than one, but for these menus there should never be more than 1.
        Menu_To_Activate = GameObject.Find("LE_Invintory");


        //If i don't do this the button's parent's collider messes things up when the grid is introduced. removing the parent fixes this... for now.
        //one work around i think i found is just have empty game object and place all these items in it wihtout having it as a parent.
        transform.parent = null;
    }

    // Update is called once per frame
    void Update()
    {

    }



    void OnMouseEnter()
    {
        //Debug.Log("On_Mouse_Enter_Test_Button");

    }

    void OnMouseDown()
    {
        //this will make the vectors for moving the menu.
        Vector3 On = new Vector3(0, 0);
        Vector3 Off = new Vector3(500, 0);

        if (b_Menu_On)
        {
            b_Menu_On = false;
            Menu_To_Activate.transform.position = Off;
        }
        else
        {
            b_Menu_On = true;
            Menu_To_Activate.transform.position = On;
        }

    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log("Collision!" + other.gameObject.name);
    }
}
