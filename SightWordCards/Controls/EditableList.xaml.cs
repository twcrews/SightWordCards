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
using System.Text.RegularExpressions;

namespace SightWordCards.Controls
{
    /// <summary>
    /// Interaction logic for EditableList.xaml
    /// </summary>
    public partial class EditableList : UserControl
    {
        public delegate void CheckLabelEventHandler(object sender, CheckLabelEventArgs e);

        public event EventHandler ItemSelected;
        public event CheckLabelEventHandler ItemAdded;
        public event CheckLabelEventHandler ItemRemoved;
        public event CheckLabelEventHandler ItemChanged;

        public EditableList()
        {
            InitializeComponent();
            ContentList.SelectionChanged += HandleItemSelected;
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
                CheckLabel newCL = new CheckLabel
                {
                    Text = AddWindow.Text,
                    Tag = AddWindow.Text + ToHexColor(AddWindow.Text)
                };
                ContentList.Items.Add(newCL);
                HandleItemAdded(newCL, new CheckLabelEventArgs(newCL.Tag.ToString()));
                CheckItemCount();
            }
            AddWindow.Close();
        }

        private string ToHexColor(string colorstring)
        {
            System.Drawing.Color color = System.Drawing.Color.FromName(
                Regex.Replace(colorstring, @"\s+", ""));
            string returnString = 
                @"#" + color.R.ToString("X2") + 
                color.G.ToString("X2") + 
                color.B.ToString("X2");
            if (returnString == "#000000" && colorstring.ToLower() != "black")
            {
                return "#FFFFFF";
            }
            return returnString;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            EditItemWindow EditWindow = new EditItemWindow() 
            {
                Title = "Edit Item", 
                Text = (ContentList.SelectedItem as CheckLabel).Text.ToString() 
            };
            EditWindow.ShowDialog();
            HandleItemChanged(ContentList.SelectedItem, new CheckLabelEventArgs(
                EditWindow.Text + ToHexColor(EditWindow.Text),
                (ContentList.SelectedItem as CheckLabel).Tag.ToString()));
            (ContentList.SelectedItem as CheckLabel).Text = EditWindow.Text;
            (ContentList.SelectedItem as CheckLabel).Tag = EditWindow.Text + ToHexColor(EditWindow.Text);
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
                HandleItemRemoved(sender, new CheckLabelEventArgs(
                    (ContentList.SelectedItem as CheckLabel).Tag.ToString()));
                ContentList.Items.Remove(ContentList.SelectedItem);
                CheckItemCount();
            }
        }

        private void HandleItemSelected(object sender, EventArgs e)
        {
            OnItemSelected(EventArgs.Empty);
        }

        private void HandleItemRemoved(object sender, CheckLabelEventArgs e)
        {
            OnItemRemoved(e);
        }

        private void HandleItemAdded(object sender, CheckLabelEventArgs e)
        {
            OnItemAdded(e);
        }

        private void HandleItemChanged(object sender, CheckLabelEventArgs e)
        {
            OnItemChanged(e);
        }

        protected virtual void OnItemSelected(EventArgs e)
        {
            ItemSelected?.Invoke(this, e);
        }

        protected virtual void OnItemRemoved(CheckLabelEventArgs e)
        {
            ItemRemoved?.Invoke(this, e);
        }

        protected virtual void OnItemAdded(CheckLabelEventArgs e)
        {
            ItemAdded?.Invoke(this, e);
        }

        protected virtual void OnItemChanged(CheckLabelEventArgs e)
        {
            ItemChanged?.Invoke(this, e);
        }

        private void SelectAllButton_Click(object sender, RoutedEventArgs e)
        {
            int uncheckedCount = 0;
            foreach (CheckLabel item in ContentList.Items)
            {
                if (!item.IsChecked)
                {
                    item.IsChecked = true;
                    uncheckedCount++;
                }
            }
            if (uncheckedCount == 0)
            {
                foreach (CheckLabel item in ContentList.Items)
                {
                    item.IsChecked = false;
                }
            }
        }

        private void CheckItemCount() => SelectAllButton.IsEnabled = ContentList.Items.Count > 0;

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            CheckItemCount();
        }
    }

    public class CheckLabelEventArgs : EventArgs
    {
        public CheckLabelEventArgs(string tag, string oldtag = null)
        {
            Tag = tag;
            OldTag = oldtag;
        }

        public string Tag { get; set; }
        public string OldTag { get; set; }
    }
}
