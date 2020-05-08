using Luau_Decompiler.LBD.Resources;
using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Source_Files
{
    // The decompiler simplifies the bytecode so it is simpler to Generate
    class Decompiler
    {
        public int sizep = 2;
        public byte[] Bytecode;

        public IList<string> TableItems;
        public Decompiler(byte[] Bytecode)       
        {
            this.Bytecode = Bytecode;
            this.TableItems = ReadStringTable();

            CodeGenerator Generator = new CodeGenerator();
            Generator.Construct(AnalizeBytecode());

        }

        // Here we get each string value out of the string table
        // Then it gets added to the stack
        // Thanks to King for helping me with this function :)
        public IList<string> ReadStringTable()       
        {
            StringBuilder b = new StringBuilder();
            IList<string> constants = new List<string>();

            byte Size = Bytecode[1];

            int SizeK = Bytecode[2];

            bool End = true;

            int val = 0;
            int Base = 1; // Applied to the index

            for (int i = 0; i < Size; i++) 
            {
                for (int idx = sizep + Base; idx < SizeK + sizep + 1; idx++) 
                {
                    b.Append(HexLib.ToChar(Bytecode[idx]));
                    val = idx;
                }
                if (End) 
                {
                    Base++;
                    End = false;
                }

                SizeK = Bytecode[val + 1] + 1;
                sizep = val++;

                constants.Add(b.ToString());
                b.Clear();
            }
            return constants;
        }

        public Roblox.Function AnalizeBytecode() 
        {
            Roblox.Function RobloxFunc = new Roblox.Function();

            RobloxFunc.Instructions = ReadInstructions();
            RobloxFunc.Constants = ReadConstants();

            return RobloxFunc;
        }

        public IList<Roblox.Instruction> ReadInstructions()
        {
            IList<Roblox.Instruction> Instructions = new List<Roblox.Instruction>();

            for (int i = sizep; i < Bytecode.Length; i++) 
            {
                if (Roblox.Instruction.GetOpCode(Bytecode[i]) != Roblox.Instruction.OpCode.None) 
                {
                    Console.WriteLine(Roblox.Instruction.GetOpCode(Bytecode[i]));
                    Instructions.Add(new Roblox.Instruction(Bytecode[i], Bytecode[i + 1], Bytecode[i + 2], Bytecode[i + 3]));
                }
            }
            return Instructions;
        }

        public Stack<Roblox.Constant> ReadConstants()
        {
            Stack<Roblox.Constant> Constants = new Stack<Roblox.Constant>();

            IList<Roblox.IntegerConstant> IntegerConstants = ReadIntegerConstants();
            IList<Roblox.StringConstant> StringConstants = ReadStringConstants();

            // Iterate and push each individual integer constant
            for (int i = 0; i < IntegerConstants.Count; i++)
            {
                Constants.Push(new Roblox.IntegerConstant(IntegerConstants[i].Value));
            }

            // Iterate and push each individual string constant
            for (int i = 0; i < StringConstants.Count; i++)
            {
                Constants.Push(new Roblox.StringConstant(StringConstants[i].Value));
            }

            return Constants;
        }

        public IList<Roblox.StringConstant> ReadStringConstants()
        {
            IList<Roblox.StringConstant> Constants = new List<Roblox.StringConstant>(TableItems.Count);

            for (int x = 0; x < TableItems.Count; x++)
            {
                Constants.Add(new Roblox.StringConstant(TableItems[(TableItems.Count - 1) - x]));
            }
            return Constants;
        }
        public IList<Roblox.IntegerConstant> ReadIntegerConstants()
        {
            IList<Roblox.IntegerConstant> Constants = new List<Roblox.IntegerConstant>();

            for (int i = sizep; i < Bytecode.Length; i++)
            {
                if (Roblox.Instruction.GetOpCode(Bytecode[i]) == Roblox.Instruction.OpCode.LoadNumber)
                {
                    Constants.Add(new Roblox.IntegerConstant(HexLib.ToInt(Bytecode[i + 1])));
                }
            }
            return Constants;
        }
        public IList<Roblox.BooleanConstant> ReadBooleanConstants()
        {
            IList<Roblox.BooleanConstant> Constants = new List<Roblox.BooleanConstant>();

            for (int i = sizep; i < Bytecode.Length; i++)
            {
                if (Roblox.Instruction.GetOpCode(Bytecode[i]) == Roblox.Instruction.OpCode.LoadBool)
                {
                    Constants.Add(new Roblox.BooleanConstant());
                }
            }
            return Constants;
        }
    }
}
