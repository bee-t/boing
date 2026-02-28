namespace boing
{
    public partial class AlphabetGamePage : ContentPage
    {
        private readonly Random _rnd = new();
        private char _currentLetter;
        private string _currentChoices = "";
        private int _score;
        private bool _isAnimating;

        public AlphabetGamePage()
        {
            InitializeComponent();
            LoadNewLetter();
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
                GameBg1.TranslationX = 32 * (float)Math.Sin(a);
                GameBg1.TranslationY = 26 * (float)Math.Cos(a * 0.75);
            }, 0, 1).Commit(this, "alphaBg1", 16, 7500, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg2.TranslationX = -28 * (float)Math.Cos(a * 1.05);
                GameBg2.TranslationY = -22 * (float)Math.Sin(a * 0.9);
            }, 0, 1).Commit(this, "alphaBg2", 16, 8200, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg3.TranslationX = 24 * (float)Math.Sin(a * 1.15);
                GameBg3.TranslationY = 28 * (float)Math.Cos(a * 0.85);
            }, 0, 1).Commit(this, "alphaBg3", 16, 7800, Easing.Linear, (_, __) => { }, () => true);
            new Animation(t =>
            {
                double a = t * twoPi;
                GameBg4.TranslationX = -22 * (float)Math.Cos(a * 0.95);
                GameBg4.TranslationY = 20 * (float)Math.Sin(a * 1.1);
            }, 0, 1).Commit(this, "alphaBg4", 16, 7000, Easing.Linear, (_, __) => { }, () => true);
        }

        private void StopBackgroundAnimations()
        {
            this.AbortAnimation("alphaBg1");
            this.AbortAnimation("alphaBg2");
            this.AbortAnimation("alphaBg3");
            this.AbortAnimation("alphaBg4");
            if (GameBg1 != null) { GameBg1.TranslationX = 0; GameBg1.TranslationY = 0; }
            if (GameBg2 != null) { GameBg2.TranslationX = 0; GameBg2.TranslationY = 0; }
            if (GameBg3 != null) { GameBg3.TranslationX = 0; GameBg3.TranslationY = 0; }
            if (GameBg4 != null) { GameBg4.TranslationX = 0; GameBg4.TranslationY = 0; }
        }

        private void LoadNewLetter()
        {
            _currentLetter = (char)('A' + _rnd.Next(26));
            LetterLabel.Text = _currentLetter.ToString();

            var choices = new List<char> { _currentLetter };
            while (choices.Count < 4)
            {
                var c = (char)('A' + _rnd.Next(26));
                if (!choices.Contains(c)) choices.Add(c);
            }
            for (int i = 3; i >= 1; i--)
            {
                int j = _rnd.Next(i + 1);
                (choices[j], choices[i]) = (choices[i], choices[j]);
            }
            _currentChoices = new string(choices.ToArray());

            Choice0Label.Text = _currentChoices[0].ToString();
            Choice1Label.Text = _currentChoices[1].ToString();
            Choice2Label.Text = _currentChoices[2].ToString();
            Choice3Label.Text = _currentChoices[3].ToString();
        }

        private async void OnChoiceTapped(object sender, TappedEventArgs e)
        {
            if (_isAnimating) return;
            if (sender is not View view) return;
            Label? label = view is Label l ? l : (view as Frame)?.Content as Label;
            if (label == null) return;
            char chosen = label.Text.Length == 1 ? label.Text[0] : '\0';
            Frame? frame = view as Frame ?? view.Parent as Frame;

            _isAnimating = true;
            if (chosen == _currentLetter)
            {
                _score++;
                ScoreLabel.Text = $"Score: {_score}";
                if (frame != null)
                {
                    frame.BackgroundColor = Color.FromArgb("#4ECDC4");
                    await frame.ScaleTo(1.15, 120, Easing.CubicOut);
                    await frame.ScaleTo(1, 100, Easing.CubicInOut);
                    frame.BackgroundColor = GetCardBackgroundColor();
                }
                await LetterFrame.ScaleTo(1.08, 80);
                await LetterFrame.ScaleTo(1, 80);
                LoadNewLetter();
            }
            else
            {
                if (frame != null)
                {
                    frame.BackgroundColor = Color.FromArgb("#FF6B6B");
                    await frame.TranslateTo(-8, 0, 50);
                    await frame.TranslateTo(8, 0, 50);
                    await frame.TranslateTo(-6, 0, 40);
                    await frame.TranslateTo(0, 0, 40);
                    frame.BackgroundColor = GetCardBackgroundColor();
                }
            }
            _isAnimating = false;
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private static Color GetCardBackgroundColor()
        {
            var key = Application.Current?.RequestedTheme == AppTheme.Dark ? "CardBackgroundDark" : "CardBackgroundLight";
            return (Color)(Application.Current?.Resources[key] ?? Colors.White);
        }
    }
}
