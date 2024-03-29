using System.Collections.Generic;
using System.Diagnostics;

namespace Patterns._02_Structural.Composite._003_RecursionComposition
{
    class Composite : Component
    {
        Dictionary<int, Component> nodes = new Dictionary<int, Component>();

        public Composite(string name)
            : base(name)
        {
        }

        int position = -1;
        
        // ����� ������������ ������� ����������� ���������� ��� ���������� ������.
        public override void Build(int[] rules)
        {
            if (position < rules.Length - 1)
            {
                int result = rules[++position] % 2;

                if (!nodes.ContainsKey(result))
                    nodes.Add(result, new Composite("branch " + result.ToString()));

                nodes[result].Add(position, new Leaf(rules[position].ToString()));
                
                Build(rules);
            }            
        }

        // ����� ������������ ������� ������������ ������ ������.
        public override void Operation()
        {
            Debug.WriteLine(name);

            foreach (var component in nodes)
                component.Value.Operation();
        }

        public override void Add(int key, Component component)
        {
            nodes.Add(key, component);
        }        
    }
}
