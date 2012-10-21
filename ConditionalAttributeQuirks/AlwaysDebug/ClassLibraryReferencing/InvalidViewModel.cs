using ClassLibraryToReference;

namespace ClassLibraryReferencing
{
    public class InvalidViewModel : BaseViewModel
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                base.RaisePropertyChanged("name");
                name = value;
            }
        }
    }
}