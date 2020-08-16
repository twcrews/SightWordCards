using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
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

namespace Crews.Education.Presentation.SightWordCards.Windows
{
    /// <summary>
    /// Interaction logic for PresentationWindow.xaml
    /// </summary>
    public partial class PresentationWindow : Window
    {
        private IniFile DataFile { get; set; }

        private List<Deck> Decks { get; set; }

        private Deck FinalDeck { get; set; }

        private Card CurrentCard { get; set; }

        private Card NextCard { get; set; }

        private bool LastCard { get; set; }

        public PresentationWindow(IniFile decksfile)
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

            if (Decks.Count > 1)
            {
                List<Card> TempList = new List<Card>();
                foreach (Deck deck in Decks)
                {
                    TempList.AddRange(deck.Cards);
                }
                Deck newDeck = new Deck(TempList);
                newDeck.Shuffle();
                FinalDeck = newDeck;
            }
            else
            {
                FinalDeck = Decks.First();
                FinalDeck.Shuffle();
            }

            CurrentCard = FinalDeck.Draw();
            NextCard = FinalDeck.Draw();

            PresentCards();
        }

        private void PresentCards()
        {
            FrontCard.BackgroundColor = CurrentCard.BackgroundColor;
            FrontCard.Text = CurrentCard.Text;
            BackCard.BackgroundColor = NextCard.BackgroundColor;
            BackCard.Text = NextCard.Text;
        }

        private void MenuButton_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Close();
        }

        private void DoubleAnimation_Completed(object sender, EventArgs e)
        {
            CurrentCard = NextCard;
            if (FinalDeck.Cards.Count != 0)
            {
                NextCard = FinalDeck.Draw();
            }
            else
            {
                LastCard = true;
            }
            PresentCards();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (LastCard)
            {
                Close();
            }
        }
    }
}
