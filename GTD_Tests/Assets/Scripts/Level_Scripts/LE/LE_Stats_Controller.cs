using UnityEngine;
using System.Collections;

public class LE_Stats_Controller : MonoBehaviour {

    //Field Size: how big is the field.
    int i_field_width = 10;
    int i_field_height = 10;

    GameObject[,] All_Field_Spots;
    public GameObject Empty_Field_Spot;
    public GameObject The_Field_Test;

    //Starting Energy: how much energy does the player start with. might have 100 be the max.
    int i_starting_energy = 100;

    //Starting Health: how much hp the temple has.
    int i_starting_HP = 100;
    int i_max_HP = 100;


    //this is the gems that are unlocked. right now don't have any listed cause need to get them.



    // Use this for initialization
    void Start()
    {
        //Debug.Log("Start field called");

        //This will be called once to create the field initially.
        All_Field_Spots = new GameObject[i_field_width, i_field_height];

        //go through each of the field spots and give it the game object and then update it.
        for (int i = 0; i <= All_Field_Spots.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= All_Field_Spots.GetUpperBound(1); j++)
            {
                //Create/initilze a spot for this spot.
                GameObject New_Game_Field_Spot = Instantiate(Empty_Field_Spot);
                New_Game_Field_Spot.transform.parent = The_Field_Test.transform;
                //now we add the spot to the spot on the all field spots.
                All_Field_Spots[i, j] = New_Game_Field_Spot;

                //now we tell that spot to move it's gameobject to the correct location on said grid.
                //we use 1.28 because the sprite is 256 pixles, but devided by half so 128. so 1.28 is how far apart they are.
                Vector2 New_Vect = new Vector2(i * 1.28f, j * -1.28f);
                New_Game_Field_Spot.transform.localPosition = New_Vect;

            }
        }

    }

    // Update is called once per frame
    void Update () {
	
	}


    //This will generate out the field based off of the size, and add/remove any spots. if the spot has something and it is removed then that is removed with it.
    private void Update_Field_Size()
    {
        
    }

}
