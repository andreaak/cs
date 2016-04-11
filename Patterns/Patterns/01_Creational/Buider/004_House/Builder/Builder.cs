using System;

namespace Creational.Builder._004_House
{
    abstract class Builder
    {
        public abstract void BuildBasement();
        public abstract void BuildStorey();
        public abstract void BuildRoof();
        public abstract House GetResult();
    }
}
