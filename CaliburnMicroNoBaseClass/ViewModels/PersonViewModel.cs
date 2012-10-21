using System.ComponentModel;

namespace CaliburnMicroWithNoBaseClass.ViewModels
{

	[ExtendWith(typeof(ValidationTemplate))]
	public class PersonViewModel :INotifyPropertyChanged
	{
        public string GivenNames { get; set; }
        public string FamilyName { get; set; }

        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", GivenNames, FamilyName);
            }
        }

		public event PropertyChangedEventHandler PropertyChanged;
	}
}
