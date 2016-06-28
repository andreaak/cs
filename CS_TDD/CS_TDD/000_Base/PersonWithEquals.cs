using System;

namespace CS_TDD._000_Base
{
    public class PersonWithEquals : Person
    {
        public override int GetHashCode()
        {
            return (Name != null ? Name.GetHashCode() : 0);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
            {
                return false;
            }

            var p = obj as PersonWithEquals;

            if (p == null)
            {
                return false;
            }

            return (p.Name == Name);
        }
    }
}