using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
using Crews.Education.Presentation.SightWordCards.Controls;
using Crews.Education.Presentation.SightWordCards.Properties;

namespace Crews.Education.Presentation.SightWordCards.Windows
{
    /// <summary>
    /// Interaction logic for DeckWindow.xaml
    /// </summary>
    public partial class DeckWindow : Window
    {
        private IniFile Decks { get; set; }

        public DeckWindow(IniFile decks)
        {
            InitializeComponent();
            Decks = decks;
            LoadDecks();
        }

        private void LoadDecks()
        {
            foreach (IniFile.IniSection section in Decks.Sections)
            {
                string sectionName = section.Name.Split('#')[0];
                CheckLabel newCL = new CheckLabel() { Tag = section.Name, Text = sectionName };
                newCL.IsChecked = Settings.Default.EnabledDecks.Contains(section.Name);
                DecksList.ContentList.Items.Add(newCL);
            }
        }

        private void OnItemSelected(object sender, EventArgs e)
        {
            string cardsString = string.Empty;
            if (DecksList.ContentList.SelectedItem != null)
            {
                string selectedItem = (DecksList.ContentList.SelectedItem as CheckLabel).Tag.ToString();
                if (Decks.GetSection(selectedItem) != null)
                {
                    foreach (IniFile.IniSection.IniKey key in Decks.GetSection(selectedItem).Keys)
                    {
                        cardsString += key.Name + '\n';
                    }
                    CardsTextBox.Text = cardsString.Trim();
                }
                CardsTextBox.IsEnabled = true;
            }
            else
            {
                CardsTextBox.IsEnabled = false;
            }
        }

        private void OnItemAdded(object sender, CheckLabelEventArgs e)
        {
            Decks.AddSection(e.Tag);
        }

        private void OnItemRemoved(object sender, CheckLabelEventArgs e)
        {
            CardsTextBox.Text = string.Empty;
            Decks.RemoveSection(e.Tag);
        }

        private void OnItemChanged(object sender, CheckLabelEventArgs e)
        {
            Decks.GetSection(e.OldTag).SetName(e.Tag);
        }

        private void UpdateCards(object sender, TextChangedEventArgs e)
        {
            List<string> CardsList = CardsTextBox.Text.Split(
            new[] { "\r\n", "\r", "\n" },
            StringSplitOptions.None
            ).ToList();
            string selectedItem = (DecksList.ContentList.SelectedItem as CheckLabel).Tag.ToString();
            if (Decks.GetSection(selectedItem) == null)
            {
            }
            Decks.GetSection(selectedItem)
                .RemoveAllKeys();
            foreach (string card in CardsList)
            {
                Decks.GetSection(selectedItem)
                    .AddKey(card);
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            List<string> enabledDecks = new List<string>();
            foreach (CheckLabel item in DecksList.ContentList.Items)
            {
                if (item.IsChecked)
                {
                    enabledDecks.Add(item.Tag.ToString());
                }
            }
            Settings.Default.EnabledDecks = new System.Collections.Specialized.StringCollection();
            Settings.Default.EnabledDecks.AddRange(enabledDecks.ToArray());
            Settings.Default.Save();
            Decks.Save(App.DataFile);
            Hide();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (!CardsTextBox.IsEnabled)
            {
                MessageBox.Show("You must select a deck first.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
