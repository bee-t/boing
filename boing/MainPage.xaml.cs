namespace boing
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            StartBackgroundAnimations();
            _ = AnimateHero();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            StopBackgroundAnimations();
        }

        private void StartBackgroundAnimations()
        {
            if (BgBlob1 == null) return;

            const double twoPi = 2 * Math.PI;

            new Animation(t =>
            {
                double a = t * twoPi;
                BgBlob1.TranslationX = 40 * (float)Math.Sin(a);
                BgBlob1.TranslationY = 35 * (float)Math.Cos(a * 0.8);
            }, 0, 1).Commit(this, "mainBg1", 16, 9000, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                BgBlob2.TranslationX = -38 * (float)Math.Cos(a * 1.1);
                BgBlob2.TranslationY = -30 * (float)Math.Sin(a * 0.9);
            }, 0, 1).Commit(this, "mainBg2", 16, 8500, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                BgBlob3.TranslationX = 32 * (float)Math.Sin(a * 1.2);
                BgBlob3.TranslationY = 38 * (float)Math.Cos(a * 0.7);
            }, 0, 1).Commit(this, "mainBg3", 16, 9500, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                BgBlob4.TranslationX = -28 * (float)Math.Cos(a * 0.9);
                BgBlob4.TranslationY = 32 * (float)Math.Sin(a * 1.15);
            }, 0, 1).Commit(this, "mainBg4", 16, 8000, Easing.Linear, (_, __) => { }, () => true);

            new Animation(t =>
            {
                double a = t * twoPi;
                BgBlob5.TranslationX = 30 * (float)Math.Sin(a * 0.85);
                BgBlob5.TranslationY = -28 * (float)Math.Cos(a * 1.05);
            }, 0, 1).Commit(this, "mainBg5", 16, 8800, Easing.Linear, (_, __) => { }, () => true);
        }

        private void StopBackgroundAnimations()
        {
            this.AbortAnimation("mainBg1");
            this.AbortAnimation("mainBg2");
            this.AbortAnimation("mainBg3");
            this.AbortAnimation("mainBg4");
            this.AbortAnimation("mainBg5");
            if (BgBlob1 != null) { BgBlob1.TranslationX = 0; BgBlob1.TranslationY = 0; }
            if (BgBlob2 != null) { BgBlob2.TranslationX = 0; BgBlob2.TranslationY = 0; }
            if (BgBlob3 != null) { BgBlob3.TranslationX = 0; BgBlob3.TranslationY = 0; }
            if (BgBlob4 != null) { BgBlob4.TranslationX = 0; BgBlob4.TranslationY = 0; }
            if (BgBlob5 != null) { BgBlob5.TranslationX = 0; BgBlob5.TranslationY = 0; }
        }

        private async void OnPlayGamesClicked(object? sender, EventArgs e)
        {
            if (sender is View btn)
            {
                await btn.ScaleTo(0.94, 80, Easing.CubicOut);
                await btn.ScaleTo(1, 120, Easing.CubicInOut);
            }
            await Shell.Current.GoToAsync(nameof(GamesPage));
        }

        private async Task AnimateHero()
        {
            if (HeroFrame == null) return;
            HeroFrame.Opacity = 0.7;
            HeroFrame.Scale = 0.95;
            await Task.WhenAll(
                HeroFrame.FadeTo(1, 500, Easing.CubicOut),
                HeroFrame.ScaleTo(1, 450, Easing.CubicOut)
            );
        }
    }
}
