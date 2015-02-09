using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructuresAndAlgorithms.HashTable
{
    class StringHashingSamples
    {
        // Sums the characters in the string
        // Terrible hashing function!
        private static int AdditiveHash(string input)
        {
            int currentHashValue = 0;

            foreach (char c in input)
            {
                unchecked
                {
                    currentHashValue += (int)c;
                }
            }

            return currentHashValue;
        }

        // Hashing function first reported by Dan Bernstein 
        // http://www.cse.yorku.ca/~oz/hash.html
        private static int Djb2(string input)
        {
            int hash = 5381;

            foreach (int c in input.ToCharArray())
            {
                unchecked
                {
                    /* hash * 33 + c */
                    hash = ((hash << 5) + hash) + c;
                }
            }

            return hash;
        }

        // Treats each four characters as an integer so
        // "aaaabbbb" hashes differently than "bbbbaaaa"
        private static int FoldingHash(string input)
        {
            int hashValue = 0;

            int startIndex = 0;
            int currentFourBytes;

            do
            {
                currentFourBytes = GetNextBytes(startIndex, input);
                unchecked
                {
                    hashValue += currentFourBytes;
                }

                startIndex += 4;
            } while (currentFourBytes != 0);


            return hashValue;
        }

        // Gets the next four bytes of the string converted to an
        // integer - If there are not enough characters, 0 is used.
        private static int GetNextBytes(int startIndex, string str)
        {
            int currentFourBytes = 0;

            currentFourBytes += GetByte(str, startIndex);
            currentFourBytes += GetByte(str, startIndex + 1) << 8;
            currentFourBytes += GetByte(str, startIndex + 2) << 16;
            currentFourBytes += GetByte(str, startIndex + 3) << 24;

            return currentFourBytes;
        }

        private static int GetByte(string str, int index)
        {
            if (index < str.Length)
            {
                return (int)str[index];
            }

            return 0;
        }
    }
}
