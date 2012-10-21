using System.ComponentModel;

namespace INPCThreadingIssue
{
    public class Model : INotifyPropertyChanged
    {
        public string Property;
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
