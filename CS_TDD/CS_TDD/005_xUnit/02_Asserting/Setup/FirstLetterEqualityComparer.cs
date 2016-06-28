using System;
using System.Collections.Generic;
using CS_TDD._000_Base;

namespace CS_TDD._005_xUnit._02_Asserting.Setup
{
    public class FirstLetterEqualityComparer 
        : IEqualityComparer<PersonWithEquals>
    {
        public bool Equals(PersonWithEquals a, PersonWithEquals b)
        {
            return a.Name[0] == b.Name[0];
        }

        public int GetHashCode(PersonWithEquals obj)
        {
            throw new NotImplementedException();
        }
    }
}