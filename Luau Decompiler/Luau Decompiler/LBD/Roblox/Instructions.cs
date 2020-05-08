using System;
using System.Collections.Generic;
using System.Text;

namespace LBD.Roblox
{
    public class Instruction
    {
        public enum OpCode 
        { 
            GetGlobal,
            Loadk,
            Call,
            ClearStack,
            Return,
            GetTablek,
            LoadNumber,
            LoadBool,
            Closure,
            Init,
            None
        }
        public OpCode Code { get; private set; }
        public byte A { get; private set; }
        public byte B { get; private set; }
        public byte C { get; private set; }


        public static OpCode GetOpCode(byte Hexadecimal) 
        {
            OpCode OperationCode = new OpCode();
            switch (Hexadecimal) 
            {
                case 0xA4: // GETGLOBAL
                    OperationCode = OpCode.GetGlobal;
                    break; 
                case 0xA3: // CLEARSTACK or EMPTYSTACK
                    OperationCode = OpCode.ClearStack;
                    break;
                case 0x6F: // LOADK
                    OperationCode = OpCode.Loadk;
                    break;
                case 0x82: // RETURN
                    OperationCode = OpCode.Return;
                    break;
                case 0x9F: // CALL
                    OperationCode = OpCode.Call;
                    break; 
                case 0x4D: // GETTABLEK
                    OperationCode = OpCode.GetTablek;
                    break;
                case 0x8C: // LOADNUMBER 
                    OperationCode = OpCode.LoadNumber;
                    break;
                case 0xD9: // CLOSURE 
                    OperationCode = OpCode.Closure;
                    break;
                case 0xC0: // INIT  A9
                    OperationCode = OpCode.Init;
                    break;
                case 0xA9: // LOADBOOL
                    OperationCode = OpCode.LoadBool;
                    break;
                default:
                    OperationCode = OpCode.None;
                    break;
            }
            return OperationCode;
        }

        public Instruction(byte data, byte A, byte B, byte C) 
        {
            OpCode GetOp = GetOpCode(data);

            this.Code = GetOp;
            this.A = A;
            this.B = B;
            this.C = C;
        }
    }
}
