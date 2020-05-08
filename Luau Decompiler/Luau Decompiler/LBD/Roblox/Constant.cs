using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Roblox
{
    public enum Type 
    { 
        String,
        Integer,
        BooleanTrue,
        BooleanFalse,
        Nil
    }
    public class Constant { }

    public class StringConstant : Constant
    {
        public string Value;

        public StringConstant(string Value) 
        {
            this.Value = Value;
        }

        public override string ToString()
        {
            return "\"" + Value + "\"".ToString();
        }
    }
    public class IntegerConstant : Constant
    {
        public int Value;

        public IntegerConstant(int Value)
        {
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
    public class BooleanConstant : Constant
    {
        public bool Value;

        public BooleanConstant(bool Value)
        {
            this.Value = Value;
        }

        public override string ToString()
        {
            return Value.ToString().ToLower();
        }
    }
    public class NilConstant : Constant
    {
        public NilConstant() { }

        public override string ToString()
        {
            return "nil";
        }
    }
}
