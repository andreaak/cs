using System.Collections.Generic;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.Compiler;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes
{
    class ExpressionNode : ProgramNode
    {
        List<ProgramNode> nodes = new List<ProgramNode>();

        public override void AddNode(ProgramNode node)
        {
            nodes.Add(node);
        }

        public override void Traverse(CodeGenerator generator)
        {
            foreach (var item in nodes)
            {
                item.Traverse(generator);
            }
        }
    }
}
