using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

namespace Assets.Scripts.Level_Scripts.Handlers
{
    /// <summary>
    /// This class will handle all the button interactions for single click/do objects.
    /// </summary>
    public class Button_Handler : MonoBehaviour
    {
        //The tags used in the game.
        Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();


        //Various variables used by the different menus/buttons.
        bool b_Menu_Stats_On = false;
        bool b_Menu_Enemies_On = false;


        //This will be called when a button is pressed and the collider of the button is passed so interactions can start.
        public void Button_Called(Collider2D Passed_Button)
        {
            //get the tag we will be working with.
            string s_tag = Passed_Button.gameObject.tag;

            //now depending on the tag we will perform the various actions.
            if (s_tag == G_Tags.Tag_Button_Menu_Stats)
            {
                Menu_Stats_Clicked();
            }
            else if(s_tag == G_Tags.Tag_Button_Menu_Items)
            {
                Menu_Items_Clicked();
            }
            else if (s_tag == G_Tags.Tag_Button_Menu_Enemies)
            {
                Menu_Enemy_Clicked();
            }
            else if (s_tag == G_Tags.Button_Remove_Decoration)
            {
                //we are killing the parent to this button which is a decoration.
                GameObject.Destroy(Passed_Button.transform.parent.gameObject);
            }
            else if (s_tag == G_Tags.Tag_Button_Grid_Size)
            {
                //Find the stats controller and call the grid size change.
                //GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>().Start_Size_Change();
            }
            else if (s_tag == G_Tags.Tag_Button_Path_Up)
            {
                //here we call the field path move.
                LE_Stats_Controller Cur = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
                //move  pass.
                Cur.Move_Path('w');
            }
            else if (s_tag == G_Tags.Tag_Button_Path_Down)
            {
                //here we call the field path move.
                LE_Stats_Controller Cur = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
                //move  pass.
                Cur.Move_Path('s');
            }
            else if (s_tag == G_Tags.Tag_Button_Path_Left)
            {
                //here we call the field path move.
                LE_Stats_Controller Cur = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
                //move  pass.
                Cur.Move_Path('a');
            }
            else if (s_tag == G_Tags.Tag_Button_Path_Right)
            {
                //here we call the field path move.
                LE_Stats_Controller Cur = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
                //move  pass.
                Cur.Move_Path('d');
            }
            else if (s_tag == G_Tags.Tag_Button_Path_Remove)
            {
                //here we call the field path move.
                LE_Stats_Controller Cur = GameObject.Find("LE_SCRIPTS").GetComponent<LE_Stats_Controller>();
                //move  pass.
                Cur.Move_Path('r');
            }
            //All gems for selecting for both enemies and allies.
            else if (s_tag == G_Tags.Button_Gem_Sprite)
            {
                //check the enemies then the allies to find out what is open.
                Enemies_GUI Cur_Ene = GameObject.Find(G_Tags.Name_Enemies_Menu).GetComponent<Enemies_GUI>();

                //check if enemy.
                if (Cur_Ene.b_Show_Enemy_Sprites)
                {
                    Cur_Ene.Edit_GameObject_Temp = Passed_Button.gameObject;
                    //close the menu.
                    Cur_Ene.b_Close_Enemy_Sprites = true;
                }
                //if not it's a tower.
                else
                {
                    //get the allies GUI
                    Stats_Menu_GUI Cur = GameObject.Find(G_Tags.Name_Stats_Menu).GetComponent<Stats_Menu_GUI>();
                    //set the temp sprite.
                    Cur.Temp_Sprite = Passed_Button.gameObject.GetComponent<SpriteRenderer>().sprite;
                    //set the name.
                    Cur.Temp_Name = Passed_Button.name;
                    //set the close to true.
                    Cur.b_Finished_Picking = true;
                }
            }
            else
            {
                //this is if there is no action for a tag to let us know.
                Debug.Log("There is no action for tag:" + s_tag);
            }
        }


        //This will open/close the menu for stats.
        void Menu_Stats_Clicked()
        {
            //this will make the vectors for moving the menu.
            Vector3 On = new Vector3(0, 0);
            Vector3 Off = new Vector3(500, 0);
            //This is the stats menu.
            GameObject Menu = GameObject.Find("Stats_Menu");
            //Debug.Log("Test_Menu_Stats_Clicked");

            if (b_Menu_Stats_On)
            {
                b_Menu_Stats_On = false;
                Menu.transform.position = Off;
                Menu.GetComponent<Stats_Menu_GUI>().b_Stats_Menu_Enabled = false;
            }
            else
            {
                b_Menu_Stats_On = true;
                Menu.transform.position = On;
                Menu.GetComponent<Stats_Menu_GUI>().b_Stats_Menu_Enabled = true;
            }
        }


        //This will open/close the menu for items.
        void Menu_Items_Clicked()
        {
            //this will make the vectors for moving the menu.
            Vector3 On = new Vector3(0, 0);
            Vector3 Off = new Vector3(500, 0);
            //This is the stats menu.
            GameObject Menu = GameObject.Find("Items_Menu");
            //Debug.Log("Test_Menu_Stats_Clicked");

            if (b_Menu_Stats_On)
            {
                b_Menu_Stats_On = false;
                Menu.transform.position = Off;
            }
            else
            {
                b_Menu_Stats_On = true;
                Menu.transform.position = On;
            }
        }

        //This will open/close the menu for stats.
        public void Menu_Enemy_Clicked()
        {
            //this will make the vectors for moving the menu.
            Vector3 On = new Vector3(0, 0);
            Vector3 Off = new Vector3(500, 0);
            //This is the stats menu.
            GameObject Menu = GameObject.Find("Enemies_Menu");
            
            //Debug.Log("Test_Menu_Stats_Clicked");

            if (b_Menu_Enemies_On)
            {
                b_Menu_Enemies_On = false;
                Menu.GetComponent<Enemies_GUI>().b_Enabled = false;
                Menu.transform.position = Off;
            }
            else
            {
                b_Menu_Enemies_On = true;
                Menu.GetComponent<Enemies_GUI>().b_StartingGUI = true;
                Menu.transform.position = On;
            }
        }

    }
}
