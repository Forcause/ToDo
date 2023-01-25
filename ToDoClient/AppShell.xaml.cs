using ToDoClient.View;

namespace ToDoClient
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(ManageToDoView), typeof(ManageToDoView));
        }
    }
}