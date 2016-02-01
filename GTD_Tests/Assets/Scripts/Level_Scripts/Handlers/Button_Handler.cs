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
    class Button_Handler : MonoBehaviour
    {
        //The tags used in the game.
        Assets.Scripts.Tag_Keeper G_Tags = new Assets.Scripts.Tag_Keeper();


        //Various variables used by the different menus/buttons.
        bool b_Menu_Stats_On = false;


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
            }
            else
            {
                b_Menu_Stats_On = true;
                Menu.transform.position = On;
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


    }
}
