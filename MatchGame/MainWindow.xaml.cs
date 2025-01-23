using System.Text;
using System.Windows;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MatchGame
{
    public partial class MainWindow : Window
    {
        private TextBlock lastClickedTextBlock;
        private DispatcherTimer timer = new DispatcherTimer();

        private  int tenthsOfSecondsElapsed;
        private int matchesFound;

        private bool findingMatch = false;

        public MainWindow()
        {
            InitializeComponent();

            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += Timer_Tick;

            SetUpGame();
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            throw new NotImplementedException();
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

            foreach(TextBlock textBlock in mainGrid.Children.OfType<TextBlock>())
            {
                int index = random.Next(animalEmoji.Count);
                string nextEmoji = animalEmoji[index];
                textBlock.Text = nextEmoji;
                animalEmoji.RemoveAt(index);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock textBlock = sender as TextBlock;


            if (textBlock == null) return;

            if (!findingMatch)
            {
                textBlock.Visibility = Visibility.Hidden;
                lastClickedTextBlock = textBlock;
                findingMatch = true;
            }
            else if(textBlock.Text ==  lastClickedTextBlock.Text)
            {
                textBlock.Visibility = Visibility.Hidden;
                findingMatch= false;
            }
            else
            {
                lastClickedTextBlock.Visibility = Visibility.Visible;
                findingMatch= false;
            }

        }
    }
}