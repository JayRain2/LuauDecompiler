using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Roblox
{
    public class Local
    {
        static int LocalIndex;
        Constant Value;
        public string Name;

        public Local(Constant Value)    
        {
            this.Value = Value;
            LocalIndex++;
            Name = "var" + LocalIndex.ToString();
        }
    }
}
