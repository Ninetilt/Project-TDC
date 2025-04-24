using TDC.Views.Login;

namespace TDC
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("ToDoListPage", typeof(ListView));
            Routing.RegisterRoute("LoginPage", typeof(LoginPage));
        }
        private async void LoginButton_Clicked(object sender, EventArgs e)
        {
            // Navigiere zur LoginPage über ihre registrierte Route
            // "//" sorgt für absolute Navigation vom Shell-Root aus
            await Shell.Current.GoToAsync(nameof(LoginPage));
        }
    }

}
