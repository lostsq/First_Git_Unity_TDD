using UnityEngine;
using System.Collections;

public class LE_Start : MonoBehaviour {


    //This will is what is passed the paramaters for

    //THis is the size of the field.
    int i_Field_Size_x;
    int i_Field_Size_y;
    //This will use the invintory type system for grabbing what you want to place down from decor to what waves will be what.
    //along with money starting with and gems starting with. the editor needs to be a great work. no half assing.
    public GameObject g_Background_Parent;
    public GameObject g_Background_Child; 


	// Use this for initialization
	void Start () {
        //right now just default setting for field size. these will be passed/changed on command.
        i_Field_Size_x = 15;
        i_Field_Size_y = 10;
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void Create_Background_Field()
    {

    }

}
