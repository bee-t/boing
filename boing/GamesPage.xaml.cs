namespace boing
{
    public partial class GamesPage : ContentPage
    {
        public GamesPage()
        {
            InitializeComponent();
            var games = new List<GameItem>
            {
                new GameItem { Title = "Shapes & Colors", Subtitle = "Match shapes and colors", Route = nameof(ShapesGamePage), Emoji = "🔷", AccentColor = Color.FromArgb("#AA96DA") },
                new GameItem { Title = "Alphabet Fun", Subtitle = "Learn letters A–Z", Route = nameof(AlphabetGamePage), Emoji = "🔤", AccentColor = Color.FromArgb("#4ECDC4") },
                new GameItem { Title = "Counting Time", Subtitle = "Count objects 1–10", Route = nameof(CountingGamePage), Emoji = "⭐", AccentColor = Color.FromArgb("#FFB347") }
            };
            GamesList.ItemsSource = games;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartBackgroundAnimations();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopBackgroundAnimations();
        }

        private void StartBackgroundAnimations()
        {
            if (GameBg1 == null) return;

            const double twoPi = 2 * Math.PI;

            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg1.TranslationX = 35 * (float)Math.Sin(a);
                GameBg1.TranslationY = 28 * (float)Math.Cos(a * 0.7);
            }, 0, 1).Commit(this, "bg1", 16, 8000, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg2.TranslationX = -30 * (float)Math.Cos(a * 1.1);
                GameBg2.TranslationY = -25 * (float)Math.Sin(a * 0.9);
            }, 0, 1).Commit(this, "bg2", 16, 7000, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg3.TranslationX = 22 * (float)Math.Sin(a * 1.2);
                GameBg3.TranslationY = 30 * (float)Math.Cos(a * 0.8);
            }, 0, 1).Commit(this, "bg3", 16, 9000, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg4.TranslationX = -25 * (float)Math.Cos(a * 0.9);
                GameBg4.TranslationY = 20 * (float)Math.Sin(a * 1.1);
            }, 0, 1).Commit(this, "bg4", 16, 7500, Easing.Linear, (_, __) => { }, () => true);
        }

        private void StopBackgroundAnimations()
        {
            this.AbortAnimation("bg1");
            this.AbortAnimation("bg2");
            this.AbortAnimation("bg3");
            this.AbortAnimation("bg4");
            if (GameBg1 != null) { GameBg1.TranslationX = 0; GameBg1.TranslationY = 0; }
            if (GameBg2 != null) { GameBg2.TranslationX = 0; GameBg2.TranslationY = 0; }
            if (GameBg3 != null) { GameBg3.TranslationX = 0; GameBg3.TranslationY = 0; }
            if (GameBg4 != null) { GameBg4.TranslationX = 0; GameBg4.TranslationY = 0; }
        }

        private async void OnGameTapped(object sender, TappedEventArgs e)
        {
            if (sender is BindableObject bo && bo.BindingContext is GameItem item && !string.IsNullOrEmpty(item.Route))
            {
                if (sender is View v)
                {
                    await v.ScaleTo(0.96, 80, Easing.CubicOut);
                    await v.ScaleTo(1, 150, Easing.CubicInOut);
                }
                await Shell.Current.GoToAsync(item.Route);
            }
        }

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        public class GameItem
        {
            public string Title { get; set; } = "";
            public string Subtitle { get; set; } = "";
            public string Route { get; set; } = "";
            public string Emoji { get; set; } = "🎮";
            public Color AccentColor { get; set; }
        }
    }
}
