using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NUnit.Framework;

[TestFixture]
public class Tests
{
    [Test]
    public void CompileTime()
    {
        var myClass = new MyClass();
        myClass.Method();
    }
    [Test]
    public void Delegate()
    {
        var myClass = new MyClass();
        var action = new Action(() => myClass.Method());
        CallAction(action);
    }

    void CallAction(Action action)
    {
        action();
    }

    [Test]
    public void Reflection()
    {
        var myClass = new MyClass();
        var method = typeof(MyClass).GetMethod("Method");
        method.Invoke(myClass, null);
    }

    [Test]
    public void Dynamic()
    {
        var myClass = new MyClass();
        var dynamic = (dynamic)myClass;
        dynamic.Method();
    }
}

    public class MyClass
    {
        public void Method([CallerMemberName] string name = "")
        {
            Debug.WriteLine(name);
        }
    }