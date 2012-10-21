    using System;
    using System.Diagnostics;

internal class Program
{
    static void Main()
    {
        Debug.WriteLine("Calling Method1");
        Method1();
        Debug.WriteLine("Calling Method2");
        Method2();
    }

    [LogMethodEntry]
    static void Method1()
    {
        Debug.WriteLine("Code inside Method1");
    }

    static void Method2()
    {
        Debug.WriteLine("Code inside Method2");
    }

}