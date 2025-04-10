namespace CSTest._04_Class._15_ExtensionMethods
{
    public static class ExtensionClass
    {
        public static void Extension(this SealedClass obj)
        {
            if (obj != null)
            {
                //int nonCompiled = obj.protectedInt;//protectedInt is inaccessible due to its protection level
                //int nonCompiled = obj.privateInt;//privateInt is inaccessible due to its protection level
                int publicInt = obj.publicInt;

            }
        }
    }
}
