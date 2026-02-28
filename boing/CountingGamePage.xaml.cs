namespace boing
{
    public partial class CountingGamePage : ContentPage
    {
        private int _count;

        public CountingGamePage()
        {
            InitializeComponent();
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
                GameBg1.TranslationX = 30 * (float)Math.Sin(a);
                GameBg1.TranslationY = 28 * (float)Math.Cos(a * 0.8);
            }, 0, 1).Commit(this, "countBg1", 16, 7600, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg2.TranslationX = -26 * (float)Math.Cos(a * 1.1);
                GameBg2.TranslationY = -24 * (float)Math.Sin(a * 0.85);
            }, 0, 1).Commit(this, "countBg2", 16, 8000, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg3.TranslationX = 22 * (float)Math.Sin(a * 1.2);
                GameBg3.TranslationY = 26 * (float)Math.Cos(a * 0.9);
            }, 0, 1).Commit(this, "countBg3", 16, 8400, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg4.TranslationX = -24 * (float)Math.Cos(a * 0.9);
                GameBg4.TranslationY = 22 * (float)Math.Sin(a * 1.05);
            }, 0, 1).Commit(this, "countBg4", 16, 7200, Easing.Linear, (_, __) => { }, () => true);
        }

        private void StopBackgroundAnimations()
        {
            this.AbortAnimation("countBg1");
            this.AbortAnimation("countBg2");
            this.AbortAnimation("countBg3");
            this.AbortAnimation("countBg4");
            if (GameBg1 != null) { GameBg1.TranslationX = 0; GameBg1.TranslationY = 0; }
            if (GameBg2 != null) { GameBg2.TranslationX = 0; GameBg2.TranslationY = 0; }
            if (GameBg3 != null) { GameBg3.TranslationX = 0; GameBg3.TranslationY = 0; }
            if (GameBg4 != null) { GameBg4.TranslationX = 0; GameBg4.TranslationY = 0; }
        }

        private async void OnStarTapped(object sender, TappedEventArgs e)
        {
            if (sender is not View view) return;
            _count++;
            CountLabel.Text = _count.ToString();

            await view.ScaleTo(0.88, 60, Easing.CubicOut);
            await view.ScaleTo(1, 120, Easing.CubicInOut);

            if (_count == 5 || _count == 10)
            {
                CelebrationLabel.Text = _count == 10 ? "🎉 Amazing! 10!" : "🌟 5 stars!";
                CelebrationLabel.IsVisible = true;
                await CelebrationLabel.FadeTo(1, 200);
                await CelebrationLabel.ScaleTo(1.2, 300, Easing.CubicOut);
                await Task.Delay(800);
                await CelebrationLabel.FadeTo(0, 250);
                CelebrationLabel.IsVisible = false;
                CelebrationLabel.Scale = 1;
            }
        }

        private void OnResetClicked(object sender, EventArgs e)
        {
            _count = 0;
            CountLabel.Text = "0";
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
