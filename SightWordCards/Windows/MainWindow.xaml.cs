using SightWordCards.Windows;
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

namespace SightWordCards
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
        }

        private void OpenEditWindow(object sender, MouseButtonEventArgs e)
        {
            DeckWindow deckWindow = new DeckWindow(Decks);
            deckWindow.ShowDialog();
            deckWindow.Close();
            Decks.Load(App.DataFile);
        }

        private void RefreshCounter()
        {

        }
    }
}
