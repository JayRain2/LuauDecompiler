using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Roblox
{
    public class Function
    {
        public Stack<Constant> Constants = new Stack<Constant>();

        public IList<Instruction> Instructions = new List<Instruction>();

        public IList<Local> Locals = new List<Local>();
    }
}
