using System;
using System.ComponentModel;
using Caliburn.Micro;

namespace OfficeFileUpgrader.ViewModels
{
    public class HomeViewModel : Screen, IDataErrorInfo
    {

        public string TargetDirectory { get; set; }

        public string this[string columnName]
        {
            get { return "sdsd"; }
        }

        public string Error
        {
            get { return "sdsdsd"; }
        }
    }
}
