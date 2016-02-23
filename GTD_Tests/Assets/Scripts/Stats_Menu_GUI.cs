using UnityEngine;
using System.Collections;

public class Stats_Menu_GUI : MonoBehaviour
{

    public bool b_Stats_Menu_Enabled = false;
    bool b_Size_Menu_Open = false;
    string temp_String_01 = "";
    string temp_String_02 = "";
    //The tags used in the game.
    Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();
    //this is the up/down for scrolling.
    float f_scroll = 0;
    //allie stuff
    bool b_Ally_Menu = false;
    bool b_Add_Ally = false;

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
    //where the tower is.. is -1 is nothing.
    int i_Tower_Location_Number = -1;

    //This is the stats controller which holds all of the enemies/towers.
    LE_Stats_Controller Stats = null;

    // Use this for initialization
    void Start()
    {
        //get the stats thing we are working with to get the enemies.
        Stats = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
    }

    // Update is called once per frame
    void Update()
    {

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
                Temp_Tower = Stats.Tower_List[i];
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
        }


        //no tower, so we make one.
        if (Temp_Tower == null)
        {
            //Debug.Log("Made new temp enemy");
            Temp_Tower = new Assets.Scripts.Level_Scripts.LE.Tower_Template("Temp", 0, 0, 0, 0, 0, new Sprite());
            Temp_Tower.Tower_Sprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 0, 0), new Vector2(1, 1));
        }

        //the y offset.
        float y_offset = 100 + f_scroll;

        //NAME
        //name label
        //[sprite] [<-Fuze] [sprite]


        //Name
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Name");
        y_offset += i_Size_Height_Amount/2;
        //Name
        GUI.Box(new Rect(Screen.width / 2 - (i_Level_Size / 2), y_offset, i_Level_Size, i_Size_Height_Amount / 2), Temp_Tower.Tower_Name);
        //First Sprite Box



        //LEVEL
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Level");
        y_offset += i_Size_Height_Amount / 2;
        string Temp_Value = Temp_Tower.Tower_Level.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Level_Size / 2), y_offset, i_Level_Size, i_Size_Height_Amount / 2), Temp_Value, 4);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Level);

        y_offset += i_Size_Height_Amount;

        //POWER
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "HP");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Tower.Tower_Power.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Power_Size / 2), y_offset, i_Power_Size, i_Size_Height_Amount / 2), Temp_Value, 5);
        int.TryParse(Temp_Value, out Temp_Tower.Tower_Power);

        y_offset += i_Size_Height_Amount;




    }

    //this is the select ally menu that the user sees all the allies they can pick from.
    void Show_Select_Ally()
    {

    }


    void Stats_Menu_Enabled()
    {
        int x_Amount = Screen.width / 2;
        int y_Amount = 30;

        //Close Button.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, 20), "Close"))
        {
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

        y_Amount += 25;

        //we will now create the add button.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, 20), "Change Size"))
        {
            //disable the menu and enable the size menu.
            b_Stats_Menu_Enabled = false;
            b_Size_Menu_Open = true;
        }

        y_Amount += 25;

        //we will now create the add button.
        if (GUI.Button(new Rect(x_Amount, y_Amount, 120, 20), "Add Allies!"))
        {
            //disable the menu and enable the size menu.
            b_Stats_Menu_Enabled = false;
            b_Ally_Menu = true;
        }

        y_Amount += 25;


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
    }
}
