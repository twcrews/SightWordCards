using System;
using System.Collections.Generic;
using System.Windows.Media;

namespace SightWordCards
{
    public class Deck
    {
        private List<Card> SavedCards { get; set; } = new List<Card>();
        public List<Card> Cards { get; set; } = new List<Card>();
        public string Name { get; set; }

        public SolidColorBrush Color
        {
            get
            {
                return Cards[0].BackgroundColor;
            }

            set
            {
                foreach (Card swcard in Cards)
                    swcard.BackgroundColor = value;
            }
        }

        public Deck(List<Card> NewCards)
        {
            SavedCards.AddRange(NewCards);
            Cards.AddRange(SavedCards);
        }

        public void Shuffle()
        {
            var Rng = new Random();
            for (int CardIndex = 0, loopTo = Cards.Count - 1; CardIndex <= loopTo; CardIndex++)
            {
                int Index = Rng.Next(CardIndex, Cards.Count);
                if (CardIndex != Index)
                {
                    var Temp = Cards[CardIndex];
                    Cards[CardIndex] = Cards[Index];
                    Cards[Index] = Temp;
                }
            }
        }

        public void Reset()
        {
            Cards.Clear();
            Cards.AddRange(SavedCards);
        }

        public Card Draw()
        {
            var CardList = Draw(1);
            return CardList[0];
        }

        public List<Card> Draw(int number)
        {
            var CardList = new List<Card>();
            for (int Index = 1, loopTo = number; Index <= loopTo; Index++)
            {
                CardList.Add(Cards[0]);
                Cards.RemoveAt(0);
            }

            return CardList;
        }

        public void Insert(Card card)
        {
            Cards.Insert(0, card);
        }

        public void Insert(List<Card> newcards)
        {
            Cards.InsertRange(0, newcards);
        }
    }
}