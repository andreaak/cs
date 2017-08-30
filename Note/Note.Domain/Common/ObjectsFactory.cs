using System.Collections.Generic;

namespace Note.Domain.Common
{
    public interface IObjectsFactory
    {
        T GetService<T>(params KeyValuePair<string, object>[] parameters);

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
