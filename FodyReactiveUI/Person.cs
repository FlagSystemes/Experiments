using ReactiveUI;

namespace WpfSample
{
    public class Person : ReactiveObject
    {
        public string GivenNames { get; set; }
        public string FamilyName { get; set; }
    }
}
