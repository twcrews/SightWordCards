using Crews.Education.Presentation.SightWordCards.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Crews.Education.Presentation.SightWordCards
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private IniFile Decks { get; set; }

        public MainWindow(IniFile decks)
        {
            InitializeComponent();
            Decks = decks;
            RefreshCounter();
        }

        private void OpenEditWindow(object sender, MouseButtonEventArgs e)
        {
            DeckWindow deckWindow = new DeckWindow(Decks);
            deckWindow.ShowDialog();
            deckWindow.Close();
            Decks.Load(App.DataFile);
            RefreshCounter();
        }

        private void RefreshCounter()
        {
            StartButton.Visibility = Visibility.Visible;
            int DeckCount = Properties.Settings.Default.EnabledDecks.Count;
            if (DeckCount == 1)
            {
                DeckCounter.Content = "1 deck loaded";
            }
            else if (DeckCount == 0)
            {
                DeckCounter.Content = "No decks loaded";
                StartButton.Visibility = Visibility.Hidden;
            }
            else
            {
                DeckCounter.Content = DeckCount.ToString() + " decks loaded";
            }

            DeckPreviewTextBlock.Text = string.Empty;
            foreach (string item in Properties.Settings.Default.EnabledDecks)
            {
                DeckPreviewTextBlock.Text += item.Split('#')[0] + ", ";
            }
            DeckPreviewTextBlock.Text = DeckPreviewTextBlock.Text.TrimEnd(", ".ToCharArray());
        }

        private void MenuButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Hide();
            PresentationWindow presentation = new PresentationWindow(Decks);
            presentation.ShowDialog();
            Show();
        }
    }
}
