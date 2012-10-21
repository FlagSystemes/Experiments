using System;

namespace CaliburnMicroWithNoBaseClass.ViewModels
{
    public class ExtendWithAttribute : Attribute
    {
        public ExtendWithAttribute(Type type)
        {
        }
    }
}