using System.Windows;
using System.Windows.Controls;

namespace OfficeFileUpgrader.Views
{

    public partial class ShellView : Window
    {
        public ShellView()
        {
            InitializeComponent();
        }
        protected override void OnActivated(System.EventArgs e)
        {
            base.OnActivated(e);
        }
    }
}