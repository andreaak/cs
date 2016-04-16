using System.Collections.Generic;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.Compiler
{
    // Построитель узлов программы.
    internal class ProgramNodeBuilder
    {

        CodeGenerator codeGenerator = null;
        ProgramNode programNode = new ExpressionNode();

        public ProgramNodeBuilder(CodeGenerator codeGenerator)
        {
            this.codeGenerator = codeGenerator;
        }

        public void Build(List<Token> tokens)
        {

            codeGenerator.Initialize();
            programNode.Traverse(codeGenerator);
            programNode.ProgramByteCode = codeGenerator.GenerateByteCode();

        }

        public ProgramNode AddVariableNode(string name, int value)
        {
            VariableNode node = new VariableNode(name, value);
            programNode.AddNode(node);
            return node;
        }

        public ProgramNode AddStatementNode(string name, string[] parameters)
        {
            StatementNode node = new StatementNode(name, parameters);
            programNode.AddNode(node);
            return node;
        }

        public ProgramNode GetProgramNode()
        {
            return this.programNode;
        }
    }
}
