using System;
using System.Collections.Generic;
using System.Linq;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.Compiler;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.Exceptions;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.CodeGeneration;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes
{
    class StatementNode : ProgramNode
    {
        public StatementNode()
        {
            Offsets = new List<int>();
        }

        public StatementNode(string name, string[] parameters)
        {
            Name = name;
            Parameters = parameters;
            Offsets = new List<int>();
        }

        public string Name { get; set; }
        public List<int> Offsets { get; set; }
        public int Address { get; set; }
        public string[] Parameters { get; set; }

        public override void AddNode(ProgramNode node)
        {
            throw new InvalidOperationException();
        }

        public override void Traverse(CodeGenerator generator)
        {
            ByteCode byteCode = null;

            Command parsedCommand = (Command)Enum.Parse(typeof(Command), Name);
            
            switch (parsedCommand)
            {
                case Command.MOV:
                    byteCode = generator.GetMovCode(Parameters);
                    break;
                case Command.ADD:
                    byteCode = generator.GetAddCode(Parameters);
                    break;
                case Command.SUB:
                    byteCode = generator.GetSubCode(Parameters);
                    break;
                case Command.XOR:
                    byteCode = generator.GetXorCode(Parameters);
                    break;
                case Command.CALL:
                    {
                        StatementNode function = Enumerable.Where<StatementNode>(generator.functions, x => x.Name == Parameters[0]).FirstOrDefault();

                        if (function != null)
                        {
                            function.Offsets.Add(generator.programPointer + 1);
                        }
                        else
                        {
                            function = new StatementNode { Name = Parameters[0] };
                            function.Offsets.Add(generator.programPointer + 1);
                            generator.functions.Add(function);
                        }

                        byteCode = generator.GetCallCode(Parameters);
                    } break;
                case Command.NOP:
                    byteCode = generator.GetNopCode();
                    break;
                default:
                    throw new ProgramNodeBuilderExeption(string.Format("Unexpected token: {0}", parsedCommand));
            }

            Array.Copy((Array) byteCode.Code, (int) 0, (Array) generator.Body, (int) generator.programPointer, byteCode.Code.Length);
            generator.programPointer += byteCode.Code.Length;

            Array.Copy((Array) generator.FunctionDifenitions, (int) 0, (Array) generator.Body, (int) generator.programPointer, (int) generator.FunctionDifenitions.Length);
        }
    }
}
