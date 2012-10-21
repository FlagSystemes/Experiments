using System.ComponentModel;

namespace WpfSample
{
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            var person = new Person();
            person.PropertyChanged += PropertyChanged;
            person.PropertyChanging += PropertyChanging;
            DataContext = person;
        }

        void PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var value = sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null);
            eventsTextBox.Text = string.Format("PropertyChanged fired. \r\n\tPropertyName='{0}'\r\n\tPropertyValue='{1}'\r\n", e.PropertyName, value) + eventsTextBox.Text;
        }

        void PropertyChanging(object sender, PropertyChangingEventArgs e)
        {
            var value = sender.GetType().GetProperty(e.PropertyName).GetValue(sender, null);
            eventsTextBox.Text = string.Format("PropertyChanging fired. \r\n\tPropertyName='{0}'\r\n\tPropertyValue='{1}'\r\n", e.PropertyName, value) + eventsTextBox.Text;
        }
    }
}
