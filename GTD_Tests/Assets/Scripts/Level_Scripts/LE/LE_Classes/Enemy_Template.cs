using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Assets.Scripts.Level_Scripts.LE
{
    public class Enemy_Template
    {

        //these are all the variables that the tower has.
        public string Enemy_Name;
        public int Enemy_Reward_Single;
        public int Enemy_Reward_Wave;
        public int Enemy_Amount;
        public int Enemy_Wave_Number;
        public int Enemy_Start_After;
        public int Enemy_HP;
        public int Enemy_Power;
        public int Enemy_Speed;
        public string Enemy_Mod;

        //this is the sprite that is used for the enemy, will be used to show a picture.
        public Sprite Enemy_Sprite;

        //when an enemy is created all the items are passed to it so you must know what everything is.
        public Enemy_Template(string Passed_Name, int Passed_Reward_Single, int Passed_Reward_Wave, int Passed_Amount, int Passed_Wave_Number, int Passed_Start_After, int Passed_HP, int Passed_Speed, int Passed_Power, string Passed_Mod, Sprite Passed_Sprite)
        {
            //we just set all the variables and that's about it.
            Enemy_Name = Passed_Name;
            Enemy_Reward_Single = Passed_Reward_Single;
            Enemy_Reward_Wave = Passed_Reward_Wave;
            Enemy_Amount = Passed_Amount;
            Enemy_Wave_Number = Passed_Wave_Number;
            Enemy_Start_After = Passed_Start_After;
            Enemy_HP = Passed_HP;
            Enemy_Power = Passed_Power;
            Enemy_Speed = Passed_Speed;
            Enemy_Mod = Passed_Mod;
            Enemy_Sprite = Passed_Sprite;
        }


        public object Clone()
        {
            return this.MemberwiseClone();
        }
    }
}
