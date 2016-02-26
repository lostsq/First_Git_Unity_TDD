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
        //is the gem locked or not.
        public bool b_Locked;
        //how much does it cost to purchase this gem.
        public int Cost;

        public Lock_Gem(string Passed_Name, bool Passed_Lock, int Passed_Cost)
        {
            s_Name = Passed_Name;
            b_Locked = Passed_Lock;
            Cost = Passed_Cost;
        }

    }
}
