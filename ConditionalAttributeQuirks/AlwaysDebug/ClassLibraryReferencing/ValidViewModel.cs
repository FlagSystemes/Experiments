using ClassLibraryToReference;

namespace ClassLibraryReferencing
{
    public class ValidViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                base.RaisePropertyChanged("Name");
                name = value;
            }
        }
    }
}
