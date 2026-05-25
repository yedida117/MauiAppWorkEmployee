using MauiAppWorkEmployee.Views;

namespace MauiAppWorkEmployee
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ListPage", typeof(ListPage));
            Routing.RegisterRoute("DeletePage", typeof(DeletePage));
            Routing.RegisterRoute("UpdatePage", typeof(UpdatePage));
        }
    }
}
