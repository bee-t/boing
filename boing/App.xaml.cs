using Microsoft.Extensions.DependencyInjection;

namespace boing
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            // Use system light/dark theme (default when UserAppTheme is Unspecified)
            Application.Current!.UserAppTheme = AppTheme.Unspecified;
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}