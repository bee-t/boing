namespace boing
{
    public partial class ShapesGamePage : ContentPage
    {
        private static readonly Color[] Palette = new[]
        {
            Color.FromArgb("#FF9A3C"),
            Color.FromArgb("#4ECDC4"),
            Color.FromArgb("#2196F3"),
            Color.FromArgb("#4CAF50"),
            Color.FromArgb("#FF6B6B"),
            Color.FromArgb("#9C27B0"),
            Color.FromArgb("#FFE66D"),
            Color.FromArgb("#AA96DA"),
            Color.FromArgb("#FF5722"),
            Color.FromArgb("#F8B4D9")
        };

        private readonly Random _rnd = new();
        private Color _targetColor;
        private Color[] _optionColors = Array.Empty<Color>();
        private int _score;
        private bool _isAnimating;

        public ShapesGamePage()
        {
            InitializeComponent();
            SetupRound();
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
                GameBg1.TranslationX = 28 * (float)Math.Sin(a);
                GameBg1.TranslationY = 24 * (float)Math.Cos(a * 0.78);
            }, 0, 1).Commit(this, "shapeBg1", 16, 7700, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg2.TranslationX = -30 * (float)Math.Cos(a * 1.08);
                GameBg2.TranslationY = -26 * (float)Math.Sin(a * 0.92);
            }, 0, 1).Commit(this, "shapeBg2", 16, 8100, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg3.TranslationX = 26 * (float)Math.Sin(a * 1.12);
                GameBg3.TranslationY = 28 * (float)Math.Cos(a * 0.88);
            }, 0, 1).Commit(this, "shapeBg3", 16, 7900, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg4.TranslationX = -24 * (float)Math.Cos(a * 0.92);
                GameBg4.TranslationY = 24 * (float)Math.Sin(a * 1.08);
            }, 0, 1).Commit(this, "shapeBg4", 16, 7300, Easing.Linear, (_, __) => { }, () => true);
        }

        private void StopBackgroundAnimations()
        {
            this.AbortAnimation("shapeBg1");
            this.AbortAnimation("shapeBg2");
            this.AbortAnimation("shapeBg3");
            this.AbortAnimation("shapeBg4");
            if (GameBg1 != null) { GameBg1.TranslationX = 0; GameBg1.TranslationY = 0; }
            if (GameBg2 != null) { GameBg2.TranslationX = 0; GameBg2.TranslationY = 0; }
            if (GameBg3 != null) { GameBg3.TranslationX = 0; GameBg3.TranslationY = 0; }
            if (GameBg4 != null) { GameBg4.TranslationX = 0; GameBg4.TranslationY = 0; }
        }

        private void SetupRound()
        {
            _targetColor = Palette[_rnd.Next(Palette.Length)];
            TargetShape.Color = _targetColor;

            var options = new List<Color> { _targetColor };
            while (options.Count < 4)
            {
                var c = Palette[_rnd.Next(Palette.Length)];
                if (!options.Contains(c)) options.Add(c);
            }
            for (int i = 3; i >= 1; i--)
            {
                int j = _rnd.Next(i + 1);
                (options[j], options[i]) = (options[i], options[j]);
            }
            _optionColors = options.ToArray();

            Opt0Box.Color = _optionColors[0];
            Opt1Box.Color = _optionColors[1];
            Opt2Box.Color = _optionColors[2];
            Opt3Box.Color = _optionColors[3];
        }

        private async void OnOptionTapped(object sender, TappedEventArgs e)
        {
            if (_isAnimating) return;
            if (sender is not View view) return;
            Frame? frame = view as Frame ?? view.Parent as Frame;
            BoxView? box = null;
            int index = -1;
            if (frame == Opt0) { box = Opt0Box; index = 0; }
            else if (frame == Opt1) { box = Opt1Box; index = 1; }
            else if (frame == Opt2) { box = Opt2Box; index = 2; }
            else if (frame == Opt3) { box = Opt3Box; index = 3; }
            if (box == null || index < 0) return;

            _isAnimating = true;
            bool correct = box.Color == _targetColor;

            if (correct)
            {
                _score++;
                ScoreLabel.Text = $"Score: {_score}";
                if (frame != null)
                {
                    await frame.ScaleTo(1.2, 120, Easing.CubicOut);
                    await frame.ScaleTo(1, 100, Easing.CubicInOut);
                }
                await TargetFrame.ScaleTo(1.1, 80);
                await TargetFrame.ScaleTo(1, 80);
                SetupRound();
            }
            else
            {
                if (frame != null)
                {
                    await frame.TranslateTo(-6, 0, 40);
                    await frame.TranslateTo(6, 0, 40);
                    await frame.TranslateTo(-4, 0, 30);
                    await frame.TranslateTo(0, 0, 30);
                }
            }
            _isAnimating = false;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}
