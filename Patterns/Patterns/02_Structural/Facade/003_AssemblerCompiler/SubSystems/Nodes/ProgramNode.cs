using Patterns._02_Structural.Facade._003_AssemblerCompiler.Compiler;
using Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.CodeGeneration;

namespace Patterns._02_Structural.Facade._003_AssemblerCompiler.SubSystems.Nodes
{
    abstract class ProgramNode
    {
        public ByteCode ProgramByteCode { get; set; }
        public abstract void Traverse(CodeGenerator generator);
        public abstract void AddNode(ProgramNode node);
    }
}
