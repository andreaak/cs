﻿using System;
using System.Collections.Generic;

namespace Patterns.Creational.Singleton.Example6.RegistSingletonGen
{
    class SingletonSmall : Singleton
    {
        protected SingletonSmall() { }

        public static new SingletonSmall Instance()
        {
            try
            {
                return Lookup<SingletonSmall>();
            }
            catch (KeyNotFoundException)
            {
                Register(new SingletonSmall());
                return Lookup<SingletonSmall>();
            }
        }
    }
}