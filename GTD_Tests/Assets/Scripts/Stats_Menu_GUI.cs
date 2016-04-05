using UnityEngine;
using System.Collections;

public class Stats_Menu_GUI : MonoBehaviour
{

    public bool b_Stats_Menu_Enabled = false;
    bool b_Size_Menu_Open = false;
    string temp_String_01 = "";
    string temp_String_02 = "";
    string Temp_Value = "";

    bool b_Startup_Config = true;
    //The tags used in the game.
    Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();
    //this is the up/down for scrolling.
    float f_scroll = 0;
    //allie stuff
    GameObject Ally_Sprite_Table;
    bool b_Ally_Menu = false;
    bool b_Add_Ally = false;
    bool b_Ally_Pick_Screen = false;
    bool b_First_Sprite = false;
    bool b_Lock_Gem_Screen = false;

    //This is the x value to keep everything centered.
    int i_To_Center_X_Start = 200;
    //how tall the buttons and such will be in pixles.
    int i_Size_Height_Amount = 50;
    //sizes
    int i_Sprite_Edit_Size = 50;
    int i_Name_Size = 120;
    int i_Level_Size = 80;
    int i_Power_Size = 80;
    int i_Speed_Size = 80;
    int i_Range_Size = 80;
    int i_Free_Points_Size = 80;
    int i_Delete_Size = 50;

    //the tower to edit if any.
    Assets.Scripts.Level_Scripts.LE.Tower_Template Temp_Tower = null;
    //the second sprite if any to add to the fuse area.
    public Sprite Temp_Sprite = null;
    public string Temp_Name = "";
    public bool b_Finished_Picking = false;
    Sprite Second_Sprite = null;
    string Second_Name = "";
    //where the tower is.. is -1 is nothing.
    int i_Tower_Location_Number = -1;

    //This is the stats controller which holds all of the enemies/towers.
    LE_Stats_Controller Stats = null;

    // Use this for initialization
    void Start()
    {
        //get the stats thing we are working with to get the enemies.
        Stats = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
        Ally_Sprite_Table = GameObject.Find("LE_Allies_Background");

    }

    // Update is called once per frame
    void Update()
    {
            f_scroll += Input.GetAxis("Mouse ScrollWheel") * 80;
            Ally_Sprite_Table.transform.Translate((Vector3.up * Input.GetAxis("Mouse ScrollWheel") * 2));
    }

    void Size_Change_Menu()
    {


        int t_x;
        int t_y;

        //move the background to center.
        //GameObject.Find("Solid_Background").transform.position = new Vector2(0, 0);

        // Make a background box// 100x100 pixles in the center of the screen.
        GUI.Box(new Rect((Screen.width / 2) - 50, (Screen.height / 2) - 50, 100, 130), "Grid Size"); //(Screen.width / 2) + 50, (Screen.height / 2) + 50), "Loader Menu");

        GUI.Label(new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 30, 15, 20), "X:");
        GUI.Label(new Rect((Screen.width / 2) - 45, (Screen.height / 2) - 0, 15, 20), "Y:");

        temp_String_01 = GUI.TextField(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 30, 40, 20), temp_String_01, 2);
        temp_String_02 = GUI.TextField(new Rect((Screen.width / 2) - 30, (Screen.height / 2) - 0, 40, 20), temp_String_02, 2);

        //button to confirm.
        if (GUI.Button(new Rect((Screen.width / 2) - 40, (Screen.height / 2) + 25, 80, 20), "Confirm"))
        {
            //move the background.
            //GameObject.Find("Solid_Background").transform.position = new Vector2(500, 500);

            //check the sizes that are picked and make sure they are number.
            //the bool is to see if we need to update the size of the field.
            bool Size_Update = false;
            if (int.TryParse(temp_String_01, out t_x))
            {
                Size_Update = true;
            }
            if (int.TryParse(temp_String_02, out t_y))
            {
                Size_Update = true;
            }
            //check if we need to update the size of the field.
            if (Size_Update)
            {
                //we will update the field size.
                GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>().Update_Field_Size(t_x, t_y);
                //Change menus.
                b_Stats_Menu_Enabled = true;
                b_Size_Menu_Open = false;
            }

        }
        //button to cancel.
        if (GUI.Button(new Rect((Screen.width / 2) - 40, (Screen.height / 2) + 50, 80, 20), "Cancel"))
        {
            //Change menus.
            b_Stats_Menu_Enabled = true;
            b_Size_Menu_Open = false;
            //move the background.
            //GameObject.Find("Solid_Background").transform.position = new Vector2(500, 500);
        }

    }

    void Ally_Menu()
    {
        //Close Button.
        if (GUI.Button(new Rect(20, 20, 120, 20), "Close"))
        {
            b_Stats_Menu_Enabled = true;
            b_Ally_Menu = false;
        }

        //Add a new ally Button.
        if (GUI.Button(new Rect(20, 50, 120, 20), "Add Ally"))
        {
            b_Add_Ally = true;
            b_Ally_Menu = false;
        }

        //we will go through and show all the allies the player has.
        //now we will go through each of the enemies and list them out and display all their info along with an edit and remove button.
        for (int i = 0; i < Stats.Tower_List.Count; i++)
        {
            //the x offset.
            int x_start = 0;

            //the sprite box.
            GUI.Box(new Rect(i_To_Center_X_Start, f_scroll + i_Sprite_Edit_Size + (i * (i_Sprite_Edit_Size + 5)), i_Sprite_Edit_Size, i_Size_Height_Amount), Stats.Tower_List[i].Tower_Sprite.texture);

            //the edit box over the sprite box.
            if (GUI.Button(new Rect(i_To_Center_X_Start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Sprite_Edit_Size, i_Size_Height_Amount), "Edit"))
            {
                //set what tower we are editing.
                i_Tower_Location_Number = i;
                //we set the temp enemy to this one to edit.
                Temp_Tower = (Assets.Scripts.Level_Scripts.LE.Tower_Template)Stats.Tower_List[i].Clone();
                //reset scroll.
                f_scroll = 0;

                //still need to open said menu here.
                b_Add_Ally = true;
                b_Ally_Menu = false;
            }

            //This is to update the x spot with the new spot value.
            x_start += i_Sprite_Edit_Size;

            //Name
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Name_Size, i_Size_Height_Amount / 2), "Name");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Name_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Name);

            //This is to update the x spot with the new spot value.
            x_start += i_Name_Size;

            //Level
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Level_Size, i_Size_Height_Amount / 2), "Level");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Level_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Level.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Level_Size;

            //Power
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Power_Size, i_Size_Height_Amount / 2), "Power");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Power_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Power.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Power_Size;

            //Speed
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Speed_Size, i_Size_Height_Amount / 2), "Speed");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Speed_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Speed.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Speed_Size;

            //Range
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Range_Size, i_Size_Height_Amount / 2), "Range");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Range_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Range.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Range_Size;

            //Points
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Free_Points_Size, i_Size_Height_Amount / 2), "Points");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Free_Points_Size, i_Size_Height_Amount / 2), Stats.Tower_List[i].Tower_Points.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Free_Points_Size;

            //now we add the delete button.
            if (GUI.Button(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Delete_Size, i_Size_Height_Amount), "Delete"))
            {
                //this is the remove tower button. pretty simple.
                Stats.Tower_List.RemoveAt(i);
            }
        }
    }

    //this will show the add ally menu where the user adds/edits allies.
    void Add_Ally()
    {
        //Close Button.
        if (GUI.Button(new Rect(20, 20, 120, 20), "Close"))
        {
            b_Ally_Menu = true;
            b_Add_Ally = false;
            Temp_Tower = null;
            i_Tower_Location_Number = -1;
            f_scroll = 0;
        }

        //Save Button.
        if (GUI.Button(new Rect(20, 50, 120, 20), "Save"))
        {
            //save the temp tower.
            if (i_Tower_Location_Number != -1)
            {
                //set the tower in that place.
                Stats.Tower_List[i_Tower_Location_Number] = Temp_Tower;
            }
            else
            {
                //we add the new tower if has a name. no name means no tower has been selected.
                if (Temp_Tower.Tower_Name != "")
                {
                    Stats.Tower_List.Add(Temp_Tower);
                }
            }

            b_Ally_Menu = true;
            b_Add_Ally = false;
            Temp_Tower = null;
            i_Tower_Location_Number = -1;
            f_scroll = 0;
        }


        //no tower, so we make one.
        if (Temp_Tower == null)
        {
            //Debug.Log("Made new temp enemy");
            Temp_Tower = new Assets.Scripts.Level_Scripts.LE.Tower_Template("Temp", 1, 1, 1, 1, 1, new Sprite());
            Temp_Tower.Tower_Sprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 0, 0), new Vector2(1, 1));
        }
        //no temp sprite so make a placeholder one.
        if (Second_Sprite == null)
        {
            Second_Sprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 0, 0), new Vector2(1, 1));
        }

        //the y offset.
        float y_offset = 20 + f_scroll;

        //NAME
        //name label
        //[sprite] [<-Fuze] [sprite] we use the tower sprite, and then the temp sprite.



        //Name
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Name");
        y_offset += i_Size_Height_Amount / 2;
        //Name
        GUI.Box(new Rect(Screen.width / 2 - (i_Level_Size / 2), y_offset, i_Level_Size, i_Size_Height_Amount / 2), Temp_Tower.Tower_Name);
        y_offset += i_Size_Height_Amount / 2;
        //First Sprite Box
        if (GUI.Button(new Rect(Screen.width / 2 - ((i_Sprite_Edit_Size) / 2) - (i_Sprite_Edit_Size * 1.5f), y_offset, i_Sprite_Edit_Size, i_Size_Height_Amount), Temp_Tower.Tower_Sprite.texture))
        {
            //open the sprite menu for the first sprite.
            b_Ally_Pick_Screen = true;
            b_First_Sprite = true;
            b_Add_Ally = false;
            Ally_Sprite_Table.transform.position = new Vector3(0, 0);
        }
        //button for fuze
        if (GUI.Button(new Rect(Screen.width / 2 - ((i_Sprite_Edit_Size) / 2) - (i_Sprite_Edit_Size * .5f), y_offset, i_Sprite_Edit_Size * 2, i_Size_Height_Amount), "<<Fuse!"))
        {
            //we will perform a fuse as long as it can be.. this logic will be manually entered with each combination.
        }
        //Second sprite box.
        if (GUI.Button(new Rect(Screen.width / 2 - ((i_Sprite_Edit_Size) / 2) + (i_Sprite_Edit_Size * 1.5f), y_offset, i_Sprite_Edit_Size, i_Size_Height_Amount), Second_Sprite.texture))
        {
            //open the sprite menu for the second sprite.
            b_Ally_Pick_Screen = true;
            b_First_Sprite = false;
            b_Add_Ally = false;
            Ally_Sprite_Table.transform.position = new Vector3(0, 0);
        }
        y_offset += i_Size_Height_Amount * 1.5f;

        //LEVEL
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Level");
        y_offset += i_Size_Height_Amount / 2;
        string Temp_Value = Temp_Tower.Tower_Level.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Level_Size / 2), y_offset, i_Level_Size, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Level);

        y_offset += i_Size_Height_Amount;

        //POWER
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Power");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Tower.Tower_Power.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Power_Size / 2), y_offset, i_Power_Size, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Power);

        y_offset += i_Size_Height_Amount;

        //SPEED
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Speed");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Tower.Tower_Speed.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Speed_Size / 2), y_offset, i_Speed_Size, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Speed);

        y_offset += i_Size_Height_Amount;

        //RANGE
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Range");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Tower.Tower_Range.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Range_Size / 2), y_offset, i_Range_Size, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Range);

        y_offset += i_Size_Height_Amount;

        //POINTS
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "HP");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Tower.Tower_Points.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Free_Points_Size / 2), y_offset, i_Free_Points_Size, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Points);

        y_offset += i_Size_Height_Amount;

    }

    //this is the select ally menu that the user sees all the allies they can pick from.
    void Show_Select_Ally()
    {

        //the close button.
        //Close Button.
        if (GUI.Button(new Rect(20, 20, 120, 20), "Close"))
        {
            b_Ally_Pick_Screen = false;
            b_Add_Ally = true;
            //move the ally select stuff back.
            Ally_Sprite_Table.transform.position = new Vector3(500, 500);
            f_scroll = 0;
        }

        //the finish picking is true so now we set up the sprites that were picked.
        if (b_Finished_Picking)
        {
            //check if it's the main/first sprite or the second one.
            if (b_First_Sprite)
            {
                //set the sprite
                Temp_Tower.Tower_Sprite = Temp_Sprite;
                //set the name
                Temp_Tower.Tower_Name = Temp_Name;

            }
            else
            {
                Second_Sprite = Temp_Sprite;
                Second_Name = Temp_Name;
            }
            //clear the name.
            Temp_Name = "";
            //clear the sprite.
            Temp_Sprite = null;

            //if it's the second one we can leave it alone since it's already set. and close this up.
            b_Ally_Pick_Screen = false;
            b_Add_Ally = true;
            b_Finished_Picking = false;
            //move the ally select stuff back.
            Ally_Sprite_Table.transform.position = new Vector3(500, 500);
            f_scroll = 0;
        }


    }

    //this is the locked gem menu.
    void Show_Locked_Gems()
    {
        //Close Button.
        if (GUI.Button(new Rect(20, 20, 120, 20), "Close"))
        {
            b_Stats_Menu_Enabled = true;
            b_Lock_Gem_Screen = false;
            f_scroll = 0;
        }

        float Ty = 20 + f_scroll;

        //go through each of the locked gems and show/modify them as needed.
        for (int i = 0; i < Stats.Gem_Lock_List.Count; i++)
        {
            float X_Size = 120;
            float X_Start = Screen.width / 2 - ((X_Size * 3) / 2);


        //NAME
        GUI.Box(new Rect(X_Start, Ty, X_Size, i_Size_Height_Amount / 2), "Name");
        GUI.Box(new Rect(X_Start, Ty + i_Size_Height_Amount / 2, X_Size, i_Size_Height_Amount / 2), Stats.Gem_Lock_List[i].s_Name);
        X_Start += X_Size;
        //LOCKED
        GUI.Box(new Rect(X_Start, Ty, X_Size, i_Size_Height_Amount / 2), "Locked");
        if (GUI.Button(new Rect(X_Start, Ty + i_Size_Height_Amount / 2, X_Size, i_Size_Height_Amount / 2), Stats.Gem_Lock_List[i].b_Locked.ToString()))
        {
            if (Stats.Gem_Lock_List[i].b_Locked)
            {
                Stats.Gem_Lock_List[i].b_Locked = false;
            }
            else
            {
                Stats.Gem_Lock_List[i].b_Locked = true;
            }
        }
        X_Start += X_Size;
        //COST
        GUI.Box(new Rect(X_Start, Ty, X_Size, i_Size_Height_Amount / 2), "Cost");
        string Temp_Value_Gem_Cost = Stats.Gem_Lock_List[i].Cost.ToString();
        Temp_Value_Gem_Cost = GUI.TextField(new Rect(X_Start, Ty + i_Size_Height_Amount / 2, X_Size, i_Size_Height_Amount / 2), Temp_Value_Gem_Cost, 5);
        int.TryParse(Temp_Value_Gem_Cost, out Stats.Gem_Lock_List[i].Cost);


        Ty += i_Size_Height_Amount + 5;

    }


}


    void Stats_Menu_Enabled()
    {

        if (b_Startup_Config)
        {
            b_Startup_Config = false;
            f_scroll = 0;

        }

        int x_Amount = Screen.width / 2;
        float y_Amount = 10 + f_scroll;
        int i_Temp_Height = 25;

        //Close Button.
        if (GUI.Button(new Rect(20, 20, 120, 20), "Close"))
        {
            b_Startup_Config = true;
            b_Stats_Menu_Enabled = false;
            //set the collider to add the tag.
            GameObject Temp = new GameObject();
            //set the tag, and add compenets so we can pass it without problem.
            Temp.tag = G_Tags.Tag_Button_Menu_Stats;
            Temp.AddComponent<SpriteRenderer>();
            Temp.AddComponent<BoxCollider2D>();
            //need to find the script and tell it to button press.
            GameObject.Find("LE_SCRIPTS").GetComponent<Mouse_Interaction_Script>().Button_Handler.Button_Called(Temp.GetComponent<Collider2D>());
            //destory the object. muahahaha... hmm don't think i need to, but meh.
            GameObject.Destroy(Temp);
        }
        y_Amount += i_Temp_Height + 5;


        //Name
        GUI.Box(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Name");
        y_Amount += i_Temp_Height;
        Stats.s_Level_Name = GUI.TextField(new Rect(x_Amount, y_Amount, 120, 20), Stats.s_Level_Name, 20);
        /*
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_HP.ToString();
        Temp_Value = 
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_HP);
        */
        y_Amount += i_Temp_Height + 5;

        //Starting Energy
        GUI.Box(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Energy Starting");
        y_Amount += i_Temp_Height;
        Temp_Value = Stats.i_starting_energy.ToString();
        Temp_Value = GUI.TextField(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), Temp_Value, 20);
        int.TryParse(Temp_Value, out Stats.i_starting_energy);
        y_Amount += i_Temp_Height;
        //Shared energy?
        GUI.Box(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Shared Energy");
        y_Amount += i_Temp_Height;
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), Stats.b_shared_Energy.ToString()))
        {
            if (Stats.b_shared_Energy)
            {
                Stats.b_shared_Energy = false;
            }
            else
            {
                Stats.b_shared_Energy = true;
            }
        }
        y_Amount += i_Temp_Height + 5;

        //Max HP
        GUI.Box(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Max HP");
        y_Amount += i_Temp_Height;
        Temp_Value = Stats.i_max_HP.ToString();
        Temp_Value = GUI.TextField(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), Temp_Value, 20);
        int.TryParse(Temp_Value, out Stats.i_max_HP);
        y_Amount += i_Temp_Height + 5;

        //Starting HP
        GUI.Box(new Rect(x_Amount, y_Amount, 120, 20), "Starting HP");
        y_Amount += i_Temp_Height;
        Temp_Value = Stats.i_starting_HP.ToString();
        Temp_Value = GUI.TextField(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), Temp_Value, 20);
        int.TryParse(Temp_Value, out Stats.i_starting_HP);
        y_Amount += i_Temp_Height + 5;

        //we will now create the add button.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Change Size"))
        {
            //disable the menu and enable the size menu.
            b_Stats_Menu_Enabled = false;
            b_Size_Menu_Open = true;
            f_scroll = 0;
        }

        y_Amount += i_Temp_Height;

        //we will now create the add button.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Add Allies!"))
        {
            //disable the menu and enable the size menu.
            b_Stats_Menu_Enabled = false;
            b_Ally_Menu = true;
            f_scroll = 0;
        }

        y_Amount += i_Temp_Height;

        //Locked Allies.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Unlocked Allies"))
        {
            //disable the menu and enable the size menu.
            b_Stats_Menu_Enabled = false;
            b_Lock_Gem_Screen = true;
            f_scroll = 0;
        }

        y_Amount += i_Temp_Height;
        //Shared energy?
        GUI.Box(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Shared Allies");
        y_Amount += i_Temp_Height;
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), Stats.b_shared_Allies.ToString()))
        {
            if (Stats.b_shared_Allies)
            {
                Stats.b_shared_Allies = false;
            }
            else
            {
                Stats.b_shared_Allies = true;
            }
        }

        y_Amount += i_Temp_Height *2;

        //Save
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Save Level"))
        {
            Stats.Save_Level();
        }
        y_Amount += i_Temp_Height;

        //Load.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, i_Temp_Height), "Load Level"))
        {
            Stats.Load_Level();
        }

    }

    

    void OnGUI()
    {
        //Find what menu is open and show it.
        if (b_Size_Menu_Open)
        {
            Size_Change_Menu();
        }
        if (b_Stats_Menu_Enabled)
        {
            Stats_Menu_Enabled();
        }
        if (b_Ally_Menu)
        {
            Ally_Menu();
        }
        if (b_Add_Ally)
        {
            Add_Ally();
        }
        if (b_Ally_Pick_Screen)
        {
            Show_Select_Ally();
        }
        if (b_Lock_Gem_Screen)
        {
            Show_Locked_Gems();
        }
    }


    //This is where the fusion logic will come into play and be used. i'll manually place in every combination and what i want it to become.
    void Attempt_Fuse(string Name_One, string Name_Two)
    {

    }

}
