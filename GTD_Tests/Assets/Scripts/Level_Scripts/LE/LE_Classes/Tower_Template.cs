using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Level_Scripts.LE
{
    //this will hold information for the tower.
    public class Tower_Template
    {
        //these are all the variables that the tower has.
        string Tower_Name;
        int Tower_Level;
        int Tower_Speed;
        int Tower_Power;
        int Tower_Range;

        //this is the sprite that is used for the tower, will be used to show a picture.
        Sprite Tower_Sprite;

        //when a tower is created it will need all of the information.
        public Tower_Template(string Passed_Name,int Passed_Level, int Passed_Speed, int Passed_Power, int Passed_Range, Sprite Passed_Sprite)
        {
            //we just set all the variables and that's about it.
            Tower_Name = Passed_Name;
            Tower_Level = Passed_Level;
            Tower_Power = Passed_Power;
            Tower_Range = Passed_Range;
            Tower_Speed = Passed_Speed;
            Tower_Sprite = Passed_Sprite;
        }





    }
}
