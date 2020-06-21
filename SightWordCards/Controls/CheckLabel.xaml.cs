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

namespace SightWordCards.Controls
{
    /// <summary>
    /// Interaction logic for CheckLabel.xaml
    /// </summary>
    public partial class CheckLabel : UserControl
    {
        public CheckLabel()
        {
            InitializeComponent();
            MainCheckBox.Checked += HandleChecked;
        }

        public event EventHandler Checked;

        public string Text
        {
            get
            {
                return ContentLabel.Content.ToString();
            }
            set
            {
                ContentLabel.Content = value;
            }
        }

        public bool IsChecked
        {
            get
            {
                return (bool)MainCheckBox.IsChecked;
            }
            set
            {
                MainCheckBox.IsChecked = value;
            }
        }

        private void HandleChecked(object sender, EventArgs e)
        {
            OnChecked(EventArgs.Empty);
        }

        protected virtual void OnChecked(EventArgs e)
        {
            Checked?.Invoke(this, e);
        }
    }
}
