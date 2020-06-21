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
using SightWordCards.Windows;
using SightWordCards.Controls;
using System.Drawing;

namespace SightWordCards.Controls
{
    /// <summary>
    /// Interaction logic for EditableList.xaml
    /// </summary>
    public partial class EditableList : UserControl
    {
        public event EventHandler ItemSelected;

        public EditableList()
        {
            InitializeComponent();
            ContentList.SelectionChanged += HandleItemSelected;
            CheckItemCount();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow AddWindow = new EditItemWindow()
            {
                Title = "Add Item" 
            };
            AddWindow.ShowDialog();
            if (!string.IsNullOrWhiteSpace(AddWindow.Text))
            {
                ContentList.Items.Add(new CheckLabel 
                { 
                    Text = AddWindow.Text, 
                    Tag = AddWindow.Text + ToHexColor(AddWindow.Text)
                });
                CheckItemCount();
            }
            AddWindow.Close();
        }

        private string ToHexColor(string colorstring)
        {
            System.Drawing.Color color = System.Drawing.Color.FromName(colorstring);
            return @"#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow EditWindow = new EditItemWindow() 
            {
                Title = "Edit Item", 
                Text = (ContentList.SelectedItem as CheckLabel).Text.ToString() 
            };
            EditWindow.ShowDialog();
            (ContentList.SelectedItem as CheckLabel).Text = EditWindow.Text;
            EditWindow.Close();
        }

        private void ContentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ContentList.SelectedItem == null)
            {
                EditButton.IsEnabled = false;
                RemoveButton.IsEnabled = false;
            }
            else
            {
                EditButton.IsEnabled = true;
                RemoveButton.IsEnabled = true;
            }
        }

        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult ConfirmationBox = MessageBox.Show(
                "Are you sure you want to permanently remove this deck?",
                "Confirm Removal",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);
            if (ConfirmationBox == MessageBoxResult.Yes)
            {
                ContentList.Items.Remove(ContentList.SelectedItem);
                CheckItemCount();
            }
        }

        private void HandleItemSelected(object sender, EventArgs e)
        {
            OnItemSelected(EventArgs.Empty);
        }

        protected virtual void OnItemSelected(EventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CheckItemCount() => SelectAllButton.IsEnabled = ContentList.Items.Count > 0;
    }
}
