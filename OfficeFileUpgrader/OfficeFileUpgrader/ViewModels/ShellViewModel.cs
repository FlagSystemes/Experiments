using Caliburn.Micro;

namespace OfficeFileUpgrader.ViewModels
{
    public class ShellViewModel : Conductor<Screen>
    {
        public string ImportantText
        {
            get { return "Super Important Text! So Read It!"; }
        }
        protected override void OnActivate()
        {
            base.OnActivate();
            ActivateItem(new HomeViewModel());
        }
    }
}
