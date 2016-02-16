using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts.Level_Scripts.LE;

public class Enemies_GUI : MonoBehaviour {

    //this will show/not show the gui/information.
    public bool b_StartingGUI = false;
    public bool b_Enabled = false;
    //this is the up/down for scrolling.
    float f_scroll = 0;
    //This is the stats controller which holds all of the enemies/towers.
    LE_Stats_Controller Stats = null;

    //This is the x value to keep everything centered.
    int i_To_Center_X_Start = 0;
    //This is how wide each of the labels/containers will be.
    int i_Size_Enemy_Name = 100;
    int i_Size_Enemy_Reward_Single = 50;
    int i_Size_Enemy_Reward_Wave = 50;
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
    }
	
	// Update is called once per frame
	void Update () {

        if (b_StartingGUI)
        {
            Starting_GUI_Pre_Stuff();
        }


        f_scroll += Input.GetAxis("Mouse ScrollWheel")*50;

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

        //we will get the center x to start variable.
        //for now we will set it to 200 just as temp while we figure everything out.
        i_To_Center_X_Start = 200;
        
        
        
        //set the enabled to true since we are not starting.
        b_Enabled = true;


       

    }

    void OnGUI()
    {
        //This is enabled so we will show the GUI information.
        if (b_Enabled)
        {
            


            //we will now create the Close button.
            if (GUI.Button(new Rect(20, 10, 80, 20), "Close"))
            {
                //this is where we will start the enemy add script,ect.
            }

            //we will now create the add button.
            if (GUI.Button(new Rect(20, 40, 80, 20), "Add Enemy"))
            {
                //this is where we will start the enemy add script,ect.
            }

            //now we will go through each of the enemies and list them out and display all their info along with an edit and remove button.
            for (int i = 0; i < Stats.Enemy_List.Count; i++)
            {
                x_start = 0;

                GUI.Box(new Rect(i_To_Center_X_Start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount),Stats.Enemy_List[i].Enemy_Sprite.texture);

                //Stats.Enemy_List[i]
                //first we need to make the edit button.
                if (GUI.Button(new Rect(i_To_Center_X_Start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount), "Edit"))
                {
                    //this is where we will start the enemy add script,ect.
                }

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Edit;

                //the label for the name. we have to add the edit's box size.
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Name, i_Size_Height_Amount/2), "Name");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount +(i_Size_Height_Amount/2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Name, i_Size_Height_Amount/2), Stats.Enemy_List[i].Enemy_Name);

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Name;

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

                //the Wave Number
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Wave_Number, i_Size_Height_Amount / 2), "Wave #");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Wave_Number, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Wave_Number.ToString());

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Wave_Number;

                //the Start After
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Start_After, i_Size_Height_Amount / 2), "Star After");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Start_After, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Start_After.ToString());

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Start_After;

                //the Mod
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Mod, i_Size_Height_Amount / 2), "MOD");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Mod, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Mod);

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Mod;

                //the Reward Single
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Single, i_Size_Height_Amount / 2), "Reward Single");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Single, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Reward_Single.ToString());

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Reward_Single;

                //the Reward Wave
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Wave, i_Size_Height_Amount / 2), "Reward Wave");
                GUI.Box(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i_Size_Height_Amount / 2) + (i * (i_Size_Height_Amount + 5)), i_Size_Enemy_Reward_Wave, i_Size_Height_Amount / 2), Stats.Enemy_List[i].Enemy_Reward_Wave.ToString());

                //This is to update the x spot with the new spot value.
                x_start += i_Size_Enemy_Reward_Wave;

                //now we add the delete button.
                if (GUI.Button(new Rect(i_To_Center_X_Start + x_start, f_scroll + i_Size_Height_Amount + (i * (i_Size_Height_Amount + 5)), i_Size_Edit, i_Size_Height_Amount), "Delete"))
                {
                    //this is the remove tower button. pretty simple.
                    Stats.Enemy_List.RemoveAt(i);
                }
            }
        }
    }
}
