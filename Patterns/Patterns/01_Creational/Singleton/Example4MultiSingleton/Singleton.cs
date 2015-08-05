using System;
using System.Collections.Generic;

namespace Patterns.Creational.Singleton.Example4
{
    public class Singleton
    {
        static List<Singleton> singletons;
        public int ID { get; private set; }
        static int position = -1;

        protected Singleton(int id)
        {
            ID = id;
        }

        public static int SetLimit
        {
            get { return singletons.Count; }
            set
            {
                singletons = new List<Singleton>();

                for (int i = 0; i < value; i++)
                {
                    singletons.Add(new Singleton(i));
                }
            }
        }

        public static Singleton Instance()
        {
            if (singletons == null)
                SetLimit = 1;

            if (position < singletons.Count - 1)
                position++;
            else
                position = 0;

            return singletons[position];
        }
    }

}
