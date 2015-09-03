using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Note.Domain.Common
{
    public interface IObjectsFactory
    {
        T GetService<T>();
    }

    public class ObjectsFactory
    {
        private static IObjectsFactory objectsFactory;
        public static IObjectsFactory Instance
        {
            get
            {
                if (objectsFactory == null)
                {
                    objectsFactory = new NinjectObjectsFactory();
                }
                return objectsFactory;
            }
        }
    }
}
