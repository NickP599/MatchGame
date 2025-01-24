using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Input;

namespace MatchGame
{
    public partial class MainWindow : Window
    {
        private TextBlock? lastClickedTextBlock;
        private DispatcherTimer timer = new DispatcherTimer();

        private int tenthsOfSecondsElapsed;
        private int matchesFound;

        private bool findingMatch = false;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            timer.Start();
            matchesFound = 0;
            tenthsOfSecondsElapsed = 0;

            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            tenthsOfSecondsElapsed++;
            timeTextBlock.Text = (tenthsOfSecondsElapsed / 10f).ToString("0.0s");

            if (matchesFound == 8)
            {
                timer.Stop();
                timeTextBlock.Text = timeTextBlock.Text + " - Play Again?";
            }

        }

        private void SetUpGame()
        {
            List<string> animalEmoji = new List<string>()
                    {
                       "🐷 " , "🐷 ",
                       "🐨 " , "🐨 ",
                       "🐭 " , "🐭 ",
                       "🐩 " , "🐩 ",
                       "🐒 " , "🐒 ",
                       "🐗 " , "🐗 ",
                       "🦒 " , "🦒 ",
                       "🦀 " , "🦀 ",
                    };
            Random random = new Random();

            foreach (TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                if (textBlock.Name != "timeTextBlock")
                {
                    textBlock.Visibility = Visibility.Visible;
                    int index = random.Next(animalEmoji.Count);
                    string nextEmoji = animalEmoji[index];
                    textBlock.Text = nextEmoji;
                    animalEmoji.RemoveAt(index);
                }

            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock? textBlock = sender as TextBlock;

            if (textBlock == null) return;

            if (!findingMatch)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock = textBlock;
                findingMatch = true;
            }
            else if (textBlock.Text == lastClickedTextBlock?.Text)
            {
                matchesFound++;
                textBlock.Visibility = Visibility.Hidden;
                findingMatch = false;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch = false;
            }

        }


        private void TimeTextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            
            if (matchesFound == 8)
            {
                timer.Start();
                matchesFound = 0;
                tenthsOfSecondsElapsed = 0;

                SetUpGame();
            }
        }
    }
}