using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Source_Files
{
    public class CodeGenerator
    {
        private StringBuilder Script = new StringBuilder();
        private Stack<Roblox.RuntimeData> PublicStack = new Stack<Roblox.RuntimeData>();
        private bool IsInClosure = false;

        public string Construct(Roblox.Function Function)
        {          
            for (int i = 0; i < Function.Instructions.Count; i++) 
            {
                switch (Function.Instructions[i].Code) 
                {
                    case Roblox.Instruction.OpCode.GetGlobal:
                        Console.WriteLine($"OpCode: GETGLOBAL");

                        var b = Function.Constants.Pop().ToString().Trim('"');
                        Console.WriteLine(b);
                        PublicStack.Push(new Roblox.RuntimeData() 
                        { 
                            Value = b, 
                            Type = Roblox.RuntimeData.DataType.Function
                        });

                        break;
                    case Roblox.Instruction.OpCode.ClearStack:
                        Console.WriteLine($"OpCode: CLEARSTACK");
                        PublicStack.Clear();
                        break;
                    case Roblox.Instruction.OpCode.Closure:
                       // TODO: Check if closure is in spawn or not
                       // If it is then just pass it as a parameter
                       // else write it
                        break;
                    case Roblox.Instruction.OpCode.LoadNumber:
                        Console.WriteLine($"OpCode: LOADNUMBER");

                        PublicStack.Push(new Roblox.RuntimeData()
                        {
                            Value = Function.Constants.Pop().ToString(),
                            Type = Roblox.RuntimeData.DataType.Number
                        });
                        break;
                    case Roblox.Instruction.OpCode.Loadk:
                        Console.WriteLine($"OpCode: LOADK");

                        PublicStack.Push(new Roblox.RuntimeData()
                        {
                            Value = Function.Constants.Pop().ToString(),
                            Type = Roblox.RuntimeData.DataType.Constant
                        });
                        break;
                    case Roblox.Instruction.OpCode.GetTablek:
                        Console.WriteLine($"OpCode: GETTABLEK");

                        var Previous = PublicStack.Pop();

                        if (Previous.Type == Roblox.RuntimeData.DataType.Table)
                        {
                            IList<string> Table = ((List<string>)Previous.Value);
                            var a = Function.Constants.Pop().ToString().Trim('"');
                            Table.Add(a);

                            PublicStack.Push(new Roblox.RuntimeData() 
                            { 
                                Value = Table,
                                Type = Roblox.RuntimeData.DataType.Table
                            });
                        }
                        else 
                        {
                            IList<string> NewTable = new List<string>();
                            var a = Function.Constants.Pop().ToString().Trim('"');
                            NewTable.Add(Previous.Value.ToString());
                            NewTable.Add(a);
                            PublicStack.Push(new Roblox.RuntimeData()
                            {
                                Value = NewTable,
                                Type = Roblox.RuntimeData.DataType.Table
                            });
                        }
                        
                        break;
                    case Roblox.Instruction.OpCode.Call:
                        Console.WriteLine($"OpCode: CALL");
                        EmitCall(Function.Instructions[i]);
                        break;
                    case Roblox.Instruction.OpCode.Return:
                        break;

                }
            }
            Console.WriteLine("\n" + Script.ToString());
            return Script.ToString();
        }
        private void EmitCall(Roblox.Instruction Instruction)
        {
            int nargs = Instruction.B - 1;
            int a = nargs;

            IList<object> Args = new List<object>(nargs);

            for (int x = 0; x < nargs; x++)
            {
                Roblox.RuntimeData Argument = PublicStack.Pop();
                if (Argument.Type == Roblox.RuntimeData.DataType.Table)
                {
                    Args.Add(string.Join('.', (List<string>)Argument.Value));
                }
                else 
                {
                    Args.Add(Argument.Value);
                }
            }

            string FunctionName = PublicStack.Pop().Value.ToString();

            Script.Append(FunctionName + "(");

            for (int i = 0; i < nargs; i++) 
            {
                Script.Append(Args[(Args.Count - 1) - i]);

                if (a > 1) 
                    Script.Append(", ");

                a--;
            }
            Script.Append(")");
        }
    }
}
