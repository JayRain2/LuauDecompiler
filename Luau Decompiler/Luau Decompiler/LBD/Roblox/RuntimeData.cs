using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Roblox
{
    public class RuntimeData
    {
        public enum DataType 
        { 
            Number,
            Bool,
            Constant,
            Table,
            Function,
            Closure
        }
        public object Value { get; set; }
        public DataType Type { get; set; }
    }
}
