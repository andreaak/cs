using System;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.Compiler;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes
{
    class VariableNode : ProgramNode
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public byte[] Address { get; set; }


        public VariableNode(string name, int value)
        {
            Name = name;
            Value = value;
        }

        public override void AddNode(ProgramNode node)
        {
            throw new InvalidOperationException();
        }

        public override void Traverse(CodeGenerator generator)
        {
            generator.SetDataVariable(this);
        }
    }
}
