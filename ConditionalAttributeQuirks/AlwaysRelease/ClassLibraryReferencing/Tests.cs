using System;
using NUnit.Framework;

namespace ClassLibraryReferencing
{
    [TestFixture]
    public class Tests
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Invalid()
        {
            new InvalidViewModel {Name = "Foo"};
        }
        [Test]
        public void Valid()
        {
            new ValidViewModel {Name = "Foo"};
        }
    }
}
