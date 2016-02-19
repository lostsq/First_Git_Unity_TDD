using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Level_Scripts.LE;

public class Enemies_GUI : MonoBehaviour {

    //this will show/not show the gui/information.
    public bool b_StartingGUI = false;
    public bool b_Enabled = false;
    bool b_Add_Enemy_Enabled = false;
    bool b_Show_Enemy_Sprites = false;
    public bool b_Close_Enemy_Sprites = false;
    int i_Enemy_Edit_Number = -1;
    Enemy_Template Temp_Enemy = null;
    //This is used for when the sprite/name need to be changed.
    public GameObject Edit_GameObject_Temp;
    public GameObject Show_Enemy_Sprite_Table;
    //when enabled it will sort by the wave number.
    bool b_Sort_By_Wave = true;

    //this is the up/down for scrolling.
    float f_scroll = 0;
    //This is the stats controller which holds all of the enemies/towers.
    LE_Stats_Controller Stats = null;

    //This is the x value to keep everything centered.
    int i_To_Center_X_Start;
    //This is how wide each of the labels/containers will be.
    int i_Size_Enemy_Name = 100;
    int i_Size_Enemy_Reward_Single = 100;
    int i_Size_Enemy_Reward_Wave = 100;
    int i_Size_Enemy_Amount = 55;
    int i_Size_Enemy_Wave_Number = 55;
    int i_Size_Enemy_Start_After = 70;
    int i_Size_Enemy_HP = 50;
    int i_Size_Enemy_Power = 50;
    int i_Size_Enemy_Speed = 50;
    int i_Size_Enemy_Mod = 50;
    int i_Size_Edit = 50;

    int i_Size_Height_Amount = 50;


    int x_start = 0;


    // Use this for initialization
    void Start () {

        //get the center to look nice. will need to add another edit to be true but the more ont he left is nice.
        i_To_Center_X_Start = i_Size_Enemy_Name + i_Size_Enemy_Reward_Single + i_Size_Enemy_Reward_Wave + i_Size_Enemy_Amount + i_Size_Enemy_Wave_Number;
        i_To_Center_X_Start += i_Size_Enemy_Start_After + i_Size_Enemy_HP + i_Size_Enemy_Power + i_Size_Enemy_Speed + i_Size_Enemy_Mod + i_Size_Edit;
        i_To_Center_X_Start = (Screen.width / 2) - (i_To_Center_X_Start / 2);


    }
	
	// Update is called once per frame
	void Update () {

        if (b_StartingGUI)
        {
            Starting_GUI_Pre_Stuff();
        }


        f_scroll += Input.GetAxis("Mouse ScrollWheel")*80;
        Show_Enemy_Sprite_Table.transform.Translate((Vector3.up * Input.GetAxis("Mouse ScrollWheel") *2));
    }

    //This is called when the menu is enabled.
    private void Starting_GUI_Pre_Stuff()
    {
        //get the stats thing we are working with to get the enemies.
        Stats = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
        //set the start to false since we started it.
        b_StartingGUI = false;
        //set the scroll amount
        f_scroll = 0;

        
        
        //set the enabled to true since we are not starting.
        b_Enabled = true;


       

    }

    void OnGUI()
    {
        //This is enabled so we will show the GUI information.
        if (b_Enabled)
        {
            Show_Enemy_List_Menu();
        }
        //this is for the add enemy gui.
        else if (b_Add_Enemy_Enabled)
        {

            //show the add enemy menu but don't 
            Show_Add_Enemy_Menu(false);
        }
        else if (b_Show_Enemy_Sprites)
        {
            Show_Enemy_Sprites();
        }
    }

    //This sorts the enemies by wave.
    void Sort_By_Wave()
    {
        //this is the newly sorted list.
        List<Enemy_Template> New_Template_Sorted = new List<Enemy_Template>();
        //this is the wave number. start at 0 and work up.
        int Wave_Number = 0;
        //we will keep going till the same number of enemies is in both lists.
        while (New_Template_Sorted.Count != Stats.Enemy_List.Count)
        {
            //go through and find all the enemies with the same wave number then add them.
            for (int i = 0; i < Stats.Enemy_List.Count; i++)
            {
                if (Stats.Enemy_List[i].Enemy_Wave_Number == Wave_Number)
                {
                    New_Template_Sorted.Add(Stats.Enemy_List[i]);
                }
            }

            //increase the wave number.
            Wave_Number++;
        }
        //we set the sorted one as the default one.
        Stats.Enemy_List = New_Template_Sorted;
        b_Sort_By_Wave = false;
    }

    void Show_Enemy_List_Menu()
    {
        if (b_Sort_By_Wave)
        {
            Sort_By_Wave();
        }

        //we will now create the Close button.
        if (GUI.Button(new Rect(20, 10, 80, 20), "Close"))
        {
            //need to find the script and tell it to button press.
            GameObject.Find("LE_SCRIPTS").GetComponent<Mouse_Interaction_Script>().Button_Handler.Menu_Enemy_Clicked();
        }

        //we will now create the add button.
        if (GUI.Button(new Rect(20, 40, 80, 20), "Add Enemy"))
        {
            //this is where we will start the enemy add script,ect.
            b_Enabled = false;
            b_Add_Enemy_Enabled = true;
            //
            //Temp_Enemy = new Enemy_Template(Temp_Enemy,0,0,0,0,0,0,0,0,"Null",);
        }

        //now we will go through each of the enemies and list them out and display all their info along with an edit and remove button.
        for (int i = 0; i < Stats.Enemy_List.Count; i++)
        {
            x_start = 0;

            GUI.Box(new Rect(i_To_Center_X_Start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount), Stats.Enemy_List[i].Enemy_Sprite.texture);

            //Stats.Enemy_List[i]
            //first we need to make the edit button.
            if (GUI.Button(new Rect(i_To_Center_X_Start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount), "Edit"))
            {
                //this is where we will start the enemy add script,ect.
                //this is where we will start the enemy add script,ect.
                b_Enabled = false;
                b_Add_Enemy_Enabled = true;
                //need to have this enemy as the one.
                i_Enemy_Edit_Number = i;
                //we set the temp enemy to this one to edit.
                Temp_Enemy = Stats.Enemy_List[i];
                f_scroll = 0;
            }

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Edit;

            //the label for the name. we have to add the edit's box size.
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Name, i_Size_Height_Amount / 2), "Name");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Name, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Name);

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Name;

            //the Wave Number
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Wave_Number, i_Size_Height_Amount / 2), "Wave #");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Wave_Number, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Wave_Number.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Wave_Number;
            
            //the HP
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_HP, i_Size_Height_Amount / 2), "HP");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_HP, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_HP.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_HP;

            //the Speed
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Speed, i_Size_Height_Amount / 2), "Speed");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Speed, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Speed.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Speed;

            //the Power
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Power, i_Size_Height_Amount / 2), "Power");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Power, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Power.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Power;

            //the Amount
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Amount, i_Size_Height_Amount / 2), "Amount");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Amount, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Amount.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Amount;
            
            //the Start After
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Start_After, i_Size_Height_Amount / 2), "Star After");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Start_After, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Start_After.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Start_After;

            //the Reward Single
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Single, i_Size_Height_Amount / 2), "Kill Reward");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Single, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Reward_Single.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Reward_Single;

            //the Reward Wave
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Wave, i_Size_Height_Amount / 2), "Wave Reward");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Wave, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Reward_Wave.ToString());

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Reward_Wave;

            //the Mod
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Mod, i_Size_Height_Amount / 2), "MOD");
            GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Mod, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Mod);

            //This is to update the x spot with the new spot value.
            x_start += i_Size_Enemy_Mod;

            

            //now we add the delete button.
            if (GUI.Button(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount), "Delete"))
            {
                //this is the remove tower button. pretty simple.
                Stats.Enemy_List.RemoveAt(i);
            }
        }
    }

    
    void Show_Add_Enemy_Menu(bool Edit_Enemy)
    {
        //we will now create the Close button.
        if (GUI.Button(new Rect(20, 10, 80, 20), "Close"))
        {
            //close this menu and open the main menu.
            b_Add_Enemy_Enabled = false;
            b_Enabled = true;
            //reset the enemy to be empty.
            Temp_Enemy = null;
            i_Enemy_Edit_Number = -1;

            f_scroll = 0;

        }

        //if there is no temp enemy it means we are not editing one so we set up a temp one to save later.
        if (Temp_Enemy == null)
        {
            Debug.Log("Made new temp enemy");
            Temp_Enemy = new Enemy_Template("None", 0, 0, 0, 0, 0, 0, 0, 0, "None", new Sprite());
            Temp_Enemy.Enemy_Sprite = Sprite.Create(new Texture2D(1, 1), new Rect(0, 0, 0, 0), new Vector2(1, 1));
        }

        //now we gui all the boxes and set them up to perform their actions using the temp enemy for the data.
        //string stringToEdit = GUI.TextField(new Rect(10, 10, 200, 20), stringToEdit, 25);
        float y_offset = 100 + f_scroll;


        //first will be the sprite/tower/name.
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Name");
        y_offset+= i_Size_Height_Amount / 2;
        GUI.Box(new Rect(Screen.width / 2 - ((i_Size_Enemy_Name) / 2), y_offset, i_Size_Enemy_Name, i_Size_Height_Amount / 2), Temp_Enemy.Enemy_Name);
        y_offset += i_Size_Height_Amount / 2;
        if(GUI.Button(new Rect(Screen.width / 2 - ((i_Size_Edit) / 2), y_offset, i_Size_Edit, i_Size_Height_Amount), Temp_Enemy.Enemy_Sprite.texture))
        {
            //select tower/button pressed.
            //enable the b_Show_Enemy_Sprites bool
            b_Show_Enemy_Sprites = true;
            //disable this menu.
            b_Add_Enemy_Enabled = false;
            //move the enemy's sprite object to the center.
            Show_Enemy_Sprite_Table.transform.position = new Vector3(0, 0);

        }
        GUI.Box(new Rect(Screen.width / 2 - ((i_Size_Edit) / 2), y_offset, i_Size_Edit, i_Size_Height_Amount), "Edit");
        y_offset += i_Size_Height_Amount / 2;


        y_offset += i_Size_Height_Amount;


        //1. Wave
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Wave");
        y_offset += i_Size_Height_Amount / 2;
        string Temp_Value = Temp_Enemy.Enemy_Wave_Number.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_HP / 2), y_offset, i_Size_Enemy_Wave_Number, i_Size_Height_Amount / 2), Temp_Value, 4);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Wave_Number);

        y_offset += i_Size_Height_Amount;

        //2. HP
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "HP");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_HP.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_HP / 2), y_offset, i_Size_Enemy_HP, i_Size_Height_Amount/2), Temp_Value, 5);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_HP);

        y_offset += i_Size_Height_Amount;

        //3. Speed
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Speed");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Speed.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Speed / 2), y_offset, i_Size_Enemy_Speed, i_Size_Height_Amount / 2), Temp_Value, 2);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Speed);

        y_offset += i_Size_Height_Amount;

        //4. Power
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Power");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Power.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Power / 2), y_offset, i_Size_Enemy_Power, i_Size_Height_Amount / 2), Temp_Value, 2);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Power);

        y_offset += i_Size_Height_Amount;

        //5. Amount In Wave
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Amount In Wave");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Amount.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Amount / 2), y_offset, i_Size_Enemy_Amount, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Amount);

        y_offset += i_Size_Height_Amount;

        //6. Start After
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width/2, i_Size_Height_Amount / 2), "Start How Many Seconds After Last Wave");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Start_After.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Start_After / 2), y_offset, i_Size_Enemy_Start_After, i_Size_Height_Amount / 2), Temp_Value, 3);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Start_After);

        y_offset += i_Size_Height_Amount;

        //7. Reward Single
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Reward For Each Kill");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Reward_Single.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Reward_Single / 2), y_offset, i_Size_Enemy_Reward_Single, i_Size_Height_Amount / 2), Temp_Value, 5);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Reward_Single);

        y_offset += i_Size_Height_Amount;

        //7. Reward Wave
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "Reward For After Wave Ends");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Reward_Wave.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Reward_Wave / 2), y_offset, i_Size_Enemy_Reward_Wave, i_Size_Height_Amount / 2), Temp_Value, 6);
        int.TryParse(Temp_Value, out Temp_Enemy.Enemy_Reward_Wave);

        y_offset += i_Size_Height_Amount;

        //8. The Mod
        GUI.Box(new Rect(Screen.width / 2 / 2, y_offset, Screen.width / 2, i_Size_Height_Amount / 2), "What Mod Is Used If Any");
        y_offset += i_Size_Height_Amount / 2;
        Temp_Value = Temp_Enemy.Enemy_Mod.ToString();
        Temp_Value = GUI.TextField(new Rect(Screen.width / 2 - (i_Size_Enemy_Mod / 2), y_offset, i_Size_Enemy_Mod, i_Size_Height_Amount / 2), Temp_Value, 6);
        Temp_Enemy.Enemy_Mod = Temp_Value;

        y_offset += i_Size_Height_Amount;


        //Wave,HP,Speed,Power,amount,Star after, mod, reward single, reward wave,

        //the save/add button.
        if (GUI.Button(new Rect(20, 80, 80, 20), "Save"))
        {
            //not equal to -1 so we are editing.
            if (i_Enemy_Edit_Number != -1)
            {
                //we get the enemy and set it.
                Stats.Enemy_List[i_Enemy_Edit_Number] = Temp_Enemy;
            }
            else
            {
                //add the new enemy.
                Stats.Enemy_List.Add(Temp_Enemy);
            }

            //close this menu and open the main menu.
            b_Add_Enemy_Enabled = false;
            b_Enabled = true;
            //reset the enemy to be empty.
            Temp_Enemy = null;
            i_Enemy_Edit_Number = -1;
            f_scroll = 0;
            //to orginize the waves.
            b_Sort_By_Wave = true;
        }

    }


    void Show_Enemy_Sprites()
    {

        if (b_Close_Enemy_Sprites)
        {
            //we switch the sprites.
            Temp_Enemy.Enemy_Sprite = Edit_GameObject_Temp.GetComponent<SpriteRenderer>().sprite;
            Temp_Enemy.Enemy_Name = Edit_GameObject_Temp.name;
            //close this menu and open the main menu.
            b_Add_Enemy_Enabled = true;
            b_Show_Enemy_Sprites = false;
            b_Close_Enemy_Sprites = false;

            Show_Enemy_Sprite_Table.transform.position = new Vector3(500, 500);
        }


        //we will now create the Cancel button.
        if (GUI.Button(new Rect(20, 10, 80, 20), "Cancel"))
        {
            //close this menu and open the main menu.
            b_Add_Enemy_Enabled = true;
            b_Show_Enemy_Sprites = false;

            Show_Enemy_Sprite_Table.transform.position = new Vector3(500, 500);

        }
    }

}
