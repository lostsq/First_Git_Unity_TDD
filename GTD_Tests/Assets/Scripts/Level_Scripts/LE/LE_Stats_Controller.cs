using UnityEngine;
using System.Collections;

public class LE_Stats_Controller : MonoBehaviour {

    Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();


    //Field Size: how big is the field.
    int i_field_width = 10;
    int i_field_height = 10;
    //the path number.
    int i_Path_Number = 0;

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

                //SpriteRenderer New_Path_Renderer = New_Game_Field_Spot.GetComponent<SpriteRenderer>();
                //Sprite Temp_Sprite = Resources.Load("Temp_Sprite", typeof(Sprite)) as Sprite;
                //New_Path_Renderer.sprite = Temp_Sprite;

                //now we tell that spot to move it's gameobject to the correct location on said grid.
                //we use 1.28 because the sprite is 256 pixles, but devided by half so 128. so 1.28 is how far apart they are.
                Vector2 New_Vect = new Vector2(i * 1.28f, j * -1.28f);
                New_Game_Field_Spot.transform.localPosition = New_Vect;

            }
        }

    }

    // Update is called once per frame
    void Update () {
        //Update_Path();
    }


    //This will generate out the field based off of the size, and add/remove any spots. if the spot has something and it is removed then that is removed with it.
    private void Update_Field_Size()
    {
        
    }


    public void Update_Path()
    {
        GameObject Path_Maker = null;
        int Path_Home_x = 0;
        int Path_Home_y = 0;

        //first we find where the path mover is.
        for (int x = 0; x <= All_Field_Spots.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= All_Field_Spots.GetUpperBound(1); y++)
            {
                for (int t = 0; t < All_Field_Spots[x, y].gameObject.transform.childCount; t++)
                {
                    //get/check each child game object and check if it's our path creator.
                    GameObject T_Child = All_Field_Spots[x, y].gameObject.transform.GetChild(t).gameObject;
                    if (T_Child.name == G_Tags.Tag_Path_Maker)
                    {
                        //path maker found.
                        Path_Maker = T_Child;
                        //path home found.
                        Path_Home_x = x;
                        Path_Home_y = y;
                    }
                }
            }
        }

        if (Path_Maker != null)
        {
            //Debug.Log("X_Pos: " + Path_Home_x);
            //Debug.Log("Y_Pos: " + Path_Home_y);
        }

        

    }


    //This will remove the last path created by the path maker if it can.
    private void Remove_Last_Path(GameObject Maker, int Home_X, int Home_Y)
    {
        int i_last_x = 0;
        int i_last_y = 0;

        GameObject Last_Spot = null;
        char Set_Remove_Char = 'x';

        //first we need to find the last path that we are going to remove and move the maker to.
        for (int x = 0; x <= All_Field_Spots.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= All_Field_Spots.GetUpperBound(1); y++)
            {
                for (int t = 0; t < All_Field_Spots[x, y].gameObject.transform.childCount; t++)
                {
                    //get/check each child game object and check if it's our path creator.
                    GameObject T_Child = All_Field_Spots[x, y].gameObject.transform.GetChild(t).gameObject;
                    //check if the name contains the path number.
                    if (T_Child.name.Contains("_" + (i_Path_Number - 1)))
                    {
                        //last found.
                        Last_Spot = T_Child;
                        //last found.
                        i_last_x = x;
                        i_last_y = y;
                    }
                    if (T_Child.name.Contains("_" + (i_Path_Number - 2)))
                    {
                        if (T_Child.name.Contains("w_"))
                        {
                            Set_Remove_Char = 'w';
                        }
                        else if (T_Child.name.Contains("s_"))
                        {
                            Set_Remove_Char = 's';
                        }
                        else if (T_Child.name.Contains("a_"))
                        {
                            Set_Remove_Char = 'a';
                        }
                        else if (T_Child.name.Contains("d_"))
                        {
                            Set_Remove_Char = 'd';
                        }

                    }
                }
            }
        }

        //make sure the last spot is not null, if it is then something is wrong.
        if (Last_Spot != null)
        {
            //first we will do a paht number update.
            i_Path_Number--;
            //move the maker to the last spot.
            Maker.transform.position = Last_Spot.transform.position;
            //set the parent.
            Maker.transform.parent = Last_Spot.transform.parent;


           


            //if the path is back to 0 we will let the user drag the path.
            if (i_Path_Number == 0)
            {
                //since it's 0 we allow the user to mvoe again.
                Maker.GetComponent<LE_Path_Creator>().Reset_Children('r');
                //set the tag.
                Maker.tag = G_Tags.Tag_Drag + "_" + G_Tags.Tag_Path_Placement;

            }
            else
            {
                //now we need to set the maker to whatever the next spot will be for the x/remove thing.
                Maker.GetComponent<LE_Path_Creator>().Reset_Children(Set_Remove_Char);
            }


            //remove the last spot.
            GameObject.Destroy(Last_Spot);

        }


    }

    //This will take the path (if it exsists) and attempt a move and update everything. w/up,s/down,a/left,d/right,x/remove last.
    public void Move_Path(char Direction_Moving)
    {
        //Debug.Log("Move_Path Hit");

        //will do a test here of moving right.

        GameObject Path_Maker = null;
        int Path_Home_x = 0;
        int Path_Home_y = 0;

        //first we find where the path mover is.
        for (int x = 0; x <= All_Field_Spots.GetUpperBound(0); x++)
        {
            for (int y = 0; y <= All_Field_Spots.GetUpperBound(1); y++)
            {
                for (int t = 0; t < All_Field_Spots[x,y].gameObject.transform.childCount; t++)
                {
                    //get/check each child game object and check if it's our path creator.
                    GameObject T_Child = All_Field_Spots[x, y].gameObject.transform.GetChild(t).gameObject;
                    if (T_Child.name == G_Tags.Tag_Path_Maker)
                    {
                        //path maker found.
                        Path_Maker = T_Child;
                        //path home found.
                        Path_Home_x = x;
                        Path_Home_y = y;
                    }
                }
            }
        }

        //path maker is found.
        if (Path_Maker != null)
        {
            //get the path creator script.
            LE_Path_Creator Path_Creator_Script = Path_Maker.GetComponent<LE_Path_Creator>();
            //this is the x/y spot we are moving to.
            int i_Move_Spot_X = Path_Home_x;
            int i_Move_Spot_Y = Path_Home_y;
            //This is a bool to determain if a move can even happen.
            bool b_Can_Make_Path = true;
            //this is the sprite that is going to be used for the next location.
            Sprite New_Sprite = Path_Creator_Script.Sprite_Remove;

            //now we determain what direction the next path will be made in and get things going.
            switch (Direction_Moving)
            {
                case 'w':
                    i_Move_Spot_Y--;
                    New_Sprite = Path_Creator_Script.Sprite_Arrow_Up;

                    break;
                case 's':
                    i_Move_Spot_Y++;
                    New_Sprite = Path_Creator_Script.Sprite_Arrow_Down;
                    break;
                case 'a':
                    i_Move_Spot_X--;
                    New_Sprite = Path_Creator_Script.Sprite_Arrow_Left;

                    break;
                case 'd':
                    i_Move_Spot_X++;
                    New_Sprite = Path_Creator_Script.Sprite_Arrow_Right;

                    break;
                //This is a special case since it's removing the last one.
                case 'r':
                    //we use a different method to remove the last path just to keep the code more clean.
                    Remove_Last_Path(Path_Maker, Path_Home_x, Path_Home_y);
                    //set the path make bool to false.
                    b_Can_Make_Path = false;
                    break;

                default:
                    break;
            }

            //now we check if there is anything in that location.
            for (int t = 0; t < All_Field_Spots[i_Move_Spot_X, i_Move_Spot_Y].gameObject.transform.childCount; t++)
            {
                //get/check each child game object and check if it's our path creator.
                GameObject T_Child = All_Field_Spots[i_Move_Spot_X, i_Move_Spot_Y].gameObject.transform.GetChild(t).gameObject;
                //check the name.
                if (T_Child.tag.Contains(G_Tags.Tag_Path_Placement))
                {
                    //there is a spot in this location with the path placement tag so we can't move here.

                    //Extra logic needed here for skipping over this path.

                    //set the path make bool to false.
                    b_Can_Make_Path = false;
                }
            }

            //check if we can make a path.
            if (b_Can_Make_Path)
            {
                //first we need to create the object that will be placed in the current spot.
                GameObject New_Path = Instantiate(Empty_Field_Spot);
                //set the tag on the gameobject so we know what it is.
                New_Path.tag = G_Tags.Tag_Path_Placed_Down;
                //give the path a name.
                New_Path.name = Direction_Moving + "_" + i_Path_Number;
                //up the path number.
                i_Path_Number++;
                //get the sprite renderer for that object.
                SpriteRenderer New_Path_Renderer = New_Path.GetComponent<SpriteRenderer>();
                //set the sprite
                New_Path_Renderer.sprite = New_Sprite;
                //set the layering
                SpriteRenderer Parent_Sprite = Path_Maker.transform.parent.gameObject.GetComponent<SpriteRenderer>();
                New_Path_Renderer.sortingOrder = Parent_Sprite.sortingOrder + 1;
                //move that gameobject to the new spot.
                New_Path.transform.position = Path_Maker.transform.position;
                New_Path.transform.parent = Path_Maker.transform.parent;

                //we move the path maker to the next spot and update it.
                Path_Maker.transform.parent = null;
                Path_Maker.transform.position = All_Field_Spots[i_Move_Spot_X, i_Move_Spot_Y].transform.position;
                Path_Maker.transform.parent = All_Field_Spots[i_Move_Spot_X, i_Move_Spot_Y].transform;
                //need to change the path maker's tag so it can't be moved.
                Path_Maker.tag = "Untagged";

                //we tell the path maker to make the last direction the remove while the other ones back.
                Path_Creator_Script.Reset_Children(Direction_Moving);

            }


        }

    }

    //this will return the gameobject of the spot you are looking for.
    private GameObject Get_Spot(int px, int py)
    {
        //Set up the gameobject to return. will be null if there is nothing there.
        GameObject Return_This = null;



        return Return_This;
    }

}
