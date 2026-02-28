namespace boing
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(GamesPage), typeof(GamesPage));
            Routing.RegisterRoute(nameof(ShapesGamePage), typeof(ShapesGamePage));
            Routing.RegisterRoute(nameof(AlphabetGamePage), typeof(AlphabetGamePage));
            Routing.RegisterRoute(nameof(CountingGamePage), typeof(CountingGamePage));
        }
    }
}
