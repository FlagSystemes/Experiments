using System.Diagnostics;

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

    public static class NamePrinter
    {
        public static void PrintName(dynamic person)
        {
            Trace.WriteLine(person.FirstName + " " + person.LastName);
        }
    }