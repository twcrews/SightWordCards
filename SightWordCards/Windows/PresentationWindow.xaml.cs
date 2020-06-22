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
using System.Windows.Shapes;

namespace SightWordCards.Windows
{
    /// <summary>
    /// Interaction logic for PresentationWindow.xaml
    /// </summary>
    public partial class PresentationWindow : Window
    {
        private IniFile DataFile { get; set; }

        private List<Deck> Decks { get; set; }

        public PresentationWindow(IniFile decksfile, bool shuffleAll)
        {
            InitializeComponent();
            DataFile = decksfile;
            Decks = new List<Deck>();
            foreach(IniFile.IniSection item in DataFile.Sections)
            {
                if (Properties.Settings.Default.EnabledDecks.Contains(item.Name))
                {
                    List<Card> deckList = new List<Card>();
                    foreach (IniFile.IniSection.IniKey word in DataFile.GetSection(item.Name).Keys)
                    {
                        deckList.Add(new Card() { Text = word.Name });
                    }
                    Deck newDeck = new Deck(deckList) 
                    {
                        Color = new SolidColorBrush((Color)ColorConverter.
                        ConvertFromString(@"#" + item.Name.Split('#')[1])),
                        Name = item.Name.Split('#')[0]
                    };
                    Decks.Add(newDeck);
                }
            }

            PresentCards(shuffleAll);
        }

        private void PresentCards(bool shuffleAll)
        {

        }

        private void MenuButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void Viewbox_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
