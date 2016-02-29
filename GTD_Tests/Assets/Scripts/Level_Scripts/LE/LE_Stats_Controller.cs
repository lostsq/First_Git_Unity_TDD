using UnityEngine;
using System.Collections;
using Assets.Scripts.Level_Scripts.LE;
using Assets.Scripts.Level_Scripts.LE.LE_Classes;
using System.Collections.Generic;
using UnityEditor;

public class LE_Stats_Controller : MonoBehaviour {

    Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();
    

    //the name of the level
    public string s_Level_Name = "None";
    //Starting Energy: how much energy does the player start with. might have 100 be the max.
    public int i_starting_energy = 100;
    //Starting Health: how much hp the temple has.
    public int i_starting_HP = 100;
    public int i_max_HP = 100;
    //shared energy? used for story levels where the energy is shared between levels. false if stand alone level.
    public bool b_shared_Energy = true;
    //Shared allies. Used for when allies from previous unlocks are shared, false if only using what is unlocked.
    public bool b_shared_Allies = true;

    //Field Size: how big is the field.
    int i_field_width = 10;
    int i_field_height = 10;
    //the path number.
    int i_Path_Number = 0;
   

    GameObject[,] All_Field_Spots;
    public GameObject Empty_Field_Spot;
    public GameObject The_Field_Test;

    public Sprite Test_Sprite;

    //the tower and enemy lists. these hold the towers that are loaded/saved.
    public List<Tower_Template> Tower_List = new List<Tower_Template>();
    public List<Enemy_Template> Enemy_List = new List<Enemy_Template>();
    public List<Lock_Gem> Gem_Lock_List = new List<Lock_Gem>();
    //This is manually changed when gems are added/removed from the game as a whole.
    void Setup_Gem_Lock_List()
    {
        //Right now since the gem list is not finalized will just do like 2 or 3 temp ones.

        //RUBY
        Lock_Gem Ruby = new Lock_Gem("Ruby", false, 100);
        Gem_Lock_List.Add(Ruby);
        //TOPAZ
        Lock_Gem Topaz = new Lock_Gem("Topaz", false, 140);
        Gem_Lock_List.Add(Topaz);
        //OPAL
        Lock_Gem Opal = new Lock_Gem("Opal", false, 180);
        Gem_Lock_List.Add(Opal);

    }


    // Use this for initialization
    void Start()
    {
        //we need to set up every gem that there is in the gem lock list.
        Setup_Gem_Lock_List();

        //test for enemy gui.
        Enemy_List.Add(new Enemy_Template("Test1", 1, 2, 3, 4, 5, 6, 7, 8, "Test2", Test_Sprite));
        Enemy_List.Add(new Enemy_Template("Test4", 11, 22, 33, 44, 55, 66, 77, 88, "Test5", Test_Sprite));
        //test for allies/towers.
        Tower_List.Add(new Tower_Template("Tower1", 1, 2, 3, 4, 5, Test_Sprite));
        Tower_List.Add(new Tower_Template("Tower2", 12, 22, 32, 42, 52, Test_Sprite));


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

    void OnGUI()
    {

        
    }

    


    //This will generate out the field based off of the size, and add/remove any spots. if the spot has something and it is removed then that is removed with it.
    public void Update_Field_Size(int new_x, int new_y)
    {
        i_field_width = new_x;
        i_field_height = new_y;
        //This is the highest path spot that is placed on the new field, 0 means the path maker is not on the field at all and can ignore.
        int i_Highest_Path_Spot = 0;
        int[] i_All_Spots = new int[2000];
        GameObject Path_Maker_Temp = null;

        //we make a new field that will eventually replace the old field.
        GameObject[,] New_All_Field_Spots = new GameObject[i_field_width, i_field_height];

        //Debug.Log("Newx: " + i_field_width);
        //Debug.Log("Newy: " + i_field_height);

        //Debug.Log("Oldx: " + All_Field_Spots.GetUpperBound(0));
        //Debug.Log("Oldy: " + All_Field_Spots.GetUpperBound(1));

        //go through each of the field spots and give it the game object and then update it.
        for (int i = 0; i <= New_All_Field_Spots.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= New_All_Field_Spots.GetUpperBound(1); j++)
            {
                //Create/initilze a spot for this spot.
                GameObject New_Game_Field_Spot = Instantiate(Empty_Field_Spot);
                New_Game_Field_Spot.transform.parent = The_Field_Test.transform;
                //now we add the spot to the spot on the all field spots.
                New_All_Field_Spots[i, j] = New_Game_Field_Spot;

                //now we tell that spot to move it's gameobject to the correct location on said grid.
                //we use 1.28 because the sprite is 256 pixles, but devided by half so 128. so 1.28 is how far apart they are.
                Vector2 New_Vect = new Vector2(i * 1.28f, j * -1.28f);
                New_Game_Field_Spot.transform.localPosition = New_Vect;

            }
        }

        //see if the x or y is lower to know if we need to check for path moves.
        if (New_All_Field_Spots.GetUpperBound(0) < All_Field_Spots.GetUpperBound(0) || New_All_Field_Spots.GetUpperBound(1) < All_Field_Spots.GetUpperBound(1))
        {
            //this will be what we will check through x/y wise.
            int Check_x = All_Field_Spots.GetUpperBound(0);
            int Check_y = All_Field_Spots.GetUpperBound(1);

            //we get the lower of the two values.
            if (New_All_Field_Spots.GetUpperBound(0) < All_Field_Spots.GetUpperBound(0))
            {
                Check_x = New_All_Field_Spots.GetUpperBound(0);
            }
            if (New_All_Field_Spots.GetUpperBound(1) < All_Field_Spots.GetUpperBound(1))
            {
                Check_y = New_All_Field_Spots.GetUpperBound(1);
            }



            //going to go through all the places with the check x/y and see if there is a path, then if so we will move the maker to the highest spot.
            //Then we need to make sure everything is moved and if it can't be moved it's removed.
            //Only the path maker and start will not be removed, they are at worse moved to the invintory.

            //get the path maker if it exsists
            for (int i = 0; i <= All_Field_Spots.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= All_Field_Spots.GetUpperBound(1); j++)
                {
                    //go through each of the children for that spot and move them to the new spot.
                    for (int q = 0; q < All_Field_Spots[i, j].gameObject.transform.childCount; q++)
                    {
                        if (All_Field_Spots[i, j].gameObject.transform.GetChild(q).name == G_Tags.Name_Path_Maker)
                        {
                            //this is the path maker.
                            Path_Maker_Temp = (All_Field_Spots[i, j].gameObject.transform.GetChild(q).gameObject);
                        }
                        if (All_Field_Spots[i, j].gameObject.transform.GetChild(q).name == G_Tags.Name_Start_Point)
                        {
                            //make sure it's within the x/y.
                            if (i > Check_x || j > Check_y)
                            {
                                //it's outside the new field so we move it back to invintory.
                                Move_To_Invintory(All_Field_Spots[i, j].gameObject.transform.GetChild(q).gameObject);
                            }

                        }
                    }
                }
            }

                    //go through and move everything but path while counting how high path goes.
                    //go through each of the field spots and give it the game object and then update it.
                    for (int i = 0; i <= Check_x; i++)
            {
                for (int j = 0; j <= Check_y; j++)
                {
                    //Debug.Log("Running_Check");

                    //go through each of the children for that spot and move them to the new spot.
                    for (int q = 0; q < All_Field_Spots[i, j].gameObject.transform.childCount; q++)
                    {
                        //Debug.Log("Child_FOund");
                        //check if the object is part of the path. ONLY FOR PATH SEGEMENTS.
                        if (All_Field_Spots[i, j].gameObject.transform.GetChild(q).tag.Contains(G_Tags.Tag_Path_Placement))
                        {
                            //need to check the path number.
                            int i_Temp_Path_Number = int.Parse((All_Field_Spots[i, j].gameObject.transform.GetChild(q).name.Split('_')[1]));
                            //Debug.Log("Number_Path_Found = " + i_Temp_Path_Number);
                            //add it to all spots to find the highest path.
                            i_All_Spots[i_Highest_Path_Spot] = i_Temp_Path_Number;
                            //add temp path, this is just used for counting here, will reset after done.
                            i_Highest_Path_Spot++;

                        }


                        //we move it to the parent's location.
                        All_Field_Spots[i, j].gameObject.transform.GetChild(q).transform.position = New_All_Field_Spots[i, j].gameObject.transform.position;
                        //we set the new parent.
                        All_Field_Spots[i, j].gameObject.transform.GetChild(q).transform.parent = New_All_Field_Spots[i, j].gameObject.transform;

                        //now everything is over on the new all field.
                    }
                }
            }


            //check if there is a path maker.
            if (Path_Maker_Temp != null)
            {
                //reset the path number
                i_Highest_Path_Spot = 0;

                


                
                i_Highest_Path_Spot = 0;
                //need to find the highest path that does not break the chain.
                for (int i = 0; i < i_All_Spots.GetLength(0); i++)
                {
                    //check if the next link is found.
                    if (i_All_Spots[i] == i_Highest_Path_Spot + 1)
                    {
                        //increase the highest path spot by 1.
                        i_Highest_Path_Spot++;
                        //reset i back to 0 to keep checking. if it gets through then max has been found.
                        i = 0;
                    }
                }

                //we need to set the path number to the correct spot for adding/removing future paths!
                i_Path_Number = i_Highest_Path_Spot;

                //Debug.Log("Highest_Spot = " + i_Highest_Path_Spot);


                //now check if there is a highest path
                if (i_Highest_Path_Spot > 0)
                {

                    //now we need to remove all the numbers greater than the highest path and move the path maker to that spot.
                    for (int i = 0; i <= New_All_Field_Spots.GetUpperBound(0); i++)
                    {
                        for (int j = 0; j <= New_All_Field_Spots.GetUpperBound(1); j++)
                        {
                            //go through each of the children for that spot and move them to the new spot.
                            for (int q = 0; q < New_All_Field_Spots[i, j].gameObject.transform.childCount; q++)
                            {
                                //check if the object is part of the path. ONLY FOR PATH SEGEMENTS.
                                if (New_All_Field_Spots[i, j].gameObject.transform.GetChild(q).tag.Contains( G_Tags.Tag_Path_Placement))
                                {
                                    //need to check the path number.
                                    int i_Temp_Path_Number = int.Parse((New_All_Field_Spots[i, j].gameObject.transform.GetChild(q).name.Split('_')[1]));

                                    //now check if the spot is higher than the highest path.
                                    if (i_Temp_Path_Number > i_Highest_Path_Spot)
                                    {
                                        //we remove this spot since it was cut off.
                                        //we destory the gameobject.
                                        GameObject.Destroy(New_All_Field_Spots[i, j].gameObject.transform.GetChild(q).gameObject);
                                    }

                                    //check if it is the highest.
                                    if (i_Temp_Path_Number == i_Highest_Path_Spot)
                                    {
                                        //we need to move the path maker here and remove this path spot then update the path maker.

                                        //get the character.
                                        char char_Temp = New_All_Field_Spots[i, j].gameObject.transform.GetChild(q).name.Split('_')[0][0];
                                        Path_Maker_Temp.GetComponent<LE_Path_Creator>().Reset_Children(char_Temp);

                                        //remove old.
                                        GameObject.Destroy(New_All_Field_Spots[i, j].gameObject.transform.GetChild(q).gameObject);
                                        //now move the path maker there.
                                        Path_Maker_Temp.transform.position = New_All_Field_Spots[i, j].gameObject.transform.position;
                                        Path_Maker_Temp.transform.parent = New_All_Field_Spots[i, j].gameObject.transform;
                                        //will do the update for remove at the end.
                                    }
                                }
                            }
                        }
                    }
                }
                //we move the path maker back to the invintory.
                else
                {
                    Move_To_Invintory(Path_Maker_Temp);
                }

            }

        }
        //both values are larger or equal so we can just do a simple move without worry.
        else
        {
            //go through each of the field spots and give it the game object and then update it.
            for (int i = 0; i <= All_Field_Spots.GetUpperBound(0); i++)
            {
                for (int j = 0; j <= All_Field_Spots.GetUpperBound(1); j++)
                {
                    //go through each of the children for that spot and move them to the new spot.
                    for (int q = 0; q < All_Field_Spots[i, j].gameObject.transform.childCount; q++)
                    {
                        //we move it to the parent's location.
                        All_Field_Spots[i, j].gameObject.transform.GetChild(q).transform.position = New_All_Field_Spots[i, j].gameObject.transform.position;
                        //we set the new parent.
                        All_Field_Spots[i, j].gameObject.transform.GetChild(q).transform.parent = New_All_Field_Spots[i, j].gameObject.transform;
                    }
                }
            }
        }

        //now we go through all of the old spots and remove/delete them and then do the switch.
        //go through each of the field spots and give it the game object and then update it.
        for (int i = 0; i <= All_Field_Spots.GetUpperBound(0); i++)
        {
            for (int j = 0; j <= All_Field_Spots.GetUpperBound(1); j++)
            {
                //go through each of the children for that spot and move them to the new spot.
                for (int q = 0; q < All_Field_Spots[i, j].gameObject.transform.childCount; q++)
                {
                    //destory any children. might be decorations/left over path, ect, ect.
                    GameObject.Destroy(All_Field_Spots[i, j].gameObject.transform.GetChild(q).gameObject);
                }
                    //we destory the gameobject.
                    GameObject.Destroy(All_Field_Spots[i, j].gameObject);
            }
        }
        //now we do the switch.
        All_Field_Spots = New_All_Field_Spots;


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
                    if (T_Child.name == G_Tags.Name_Path_Maker)
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
                    if (T_Child.name == G_Tags.Name_Path_Maker)
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
                //if there is a child. that means something is there so we do not make a path.
                b_Can_Make_Path = false;
                //the rest below was a test to see about having a tag for where you can't place a path. it does work, but not as easy.


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

    //this will move the gameobject back to the invintory. just centering it.
    private void Move_To_Invintory(GameObject Move_This)
    {
        //first find the invintory.
        GameObject Temp_Invintory = GameObject.Find(G_Tags.Name_Items_Invintory);
        SpriteRenderer Temp_Invin_Rend = Temp_Invintory.GetComponent<SpriteRenderer>();

        //now we move the object to it's location/centered.
        Move_This.transform.position = Temp_Invintory.transform.position;
        //now we change the parent and set the layering order.
        Move_This.transform.parent = Temp_Invintory.transform;
        Move_This.GetComponent<SpriteRenderer>().sortingOrder = Temp_Invin_Rend.sortingOrder + 1;

    }


    //save the level to the location the user picks.
    public void Save_Level()
    {

        EditorUtility.DisplayDialog(
                    "Select Save Location",
                    "You Must Select where to save the level at!",
                    "Ok");

        var path = EditorUtility.SaveFilePanel(
                    "Save level as Txt file", "",
                    s_Level_Name + ".txt",
                    "txt");
        if (path.Length != 0)
        {

            Debug.Log(path.ToString());

            using (System.IO.StreamWriter file =
            new System.IO.StreamWriter(path))
            {
                file.WriteLine(s_Level_Name);
            }
        }
    }

    //loads the level from the location the user picked.
    public void Load_Level()
    {
        EditorUtility.DisplayDialog(
                    "Select Save Location",
                    "You Must Select where to save the level at!",
                    "Ok");

        var path = EditorUtility.OpenFilePanel(
                    "Overwrite with txt",
                    "",
                    "txt");
        if (path.Length != 0)
        {
            Debug.Log(path.ToString());
        }
    }

}
