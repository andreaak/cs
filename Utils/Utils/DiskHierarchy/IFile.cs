﻿namespace Utils.DiskHierarchy
{
    public interface IDiskItem
    {
        string FullName { get; }
        string Name { get; }
    }

    public interface IFile : IDiskItem
    {
    }
}
