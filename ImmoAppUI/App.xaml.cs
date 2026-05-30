namespace ImmoAppUI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            Microsoft.Maui.Controls.Application.Current!.UserAppTheme = AppTheme.Light;

        }
    }
}
