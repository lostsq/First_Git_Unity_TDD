using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Level_Scripts.LE.LE_Classes
{

    //each locked gem will have a name, lock/unlock value, and a cost value.
    public class Lock_Gem
    {
        //the name of the gem.
        public string s_Name;
        //Tower Gem?
        public bool b_Tower_Gem = false;
        //is this gem an enemy gem.
        public bool b_Enemy_Gem = true;
        //how much does it cost to purchase this gem.
        public int Cost;

        public Lock_Gem(string Passed_Name, int Passed_Cost)
        {
            s_Name = Passed_Name;
            Cost = Passed_Cost;
        }

    }
}
