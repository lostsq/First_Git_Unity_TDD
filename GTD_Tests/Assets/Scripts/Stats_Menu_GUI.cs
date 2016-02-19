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
    //allie stuff
    bool b_Ally_Menu = false;
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
        //the resize menu is now open.
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
    }
}
