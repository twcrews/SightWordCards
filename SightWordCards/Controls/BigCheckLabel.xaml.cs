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
    /// Interaction logic for BigCheckLabel.xaml
    /// </summary>
    public partial class BigCheckLabel : UserControl
    {
        public event EventHandler Checked;
        public event EventHandler Unchecked;

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

        public bool Black
        {
            set
            {
                SolidColorBrush NewBrush = new SolidColorBrush
                {
                    Color = value ? Colors.Black : Colors.White
                };

                Box.Stroke = NewBrush;
                CheckGlyph.Fill = NewBrush;
                ContentLabel.Foreground = NewBrush;
            }
        }

        public bool IsChecked
        {
            get
            {
                return CheckGlyph.Visibility == Visibility.Visible;
            }
            set
            {
                CheckGlyph.Visibility = value ? Visibility.Visible : Visibility.Hidden;

                if (value)
                {
                    HandleChecked(this, EventArgs.Empty);
                }
                else
                {
                    HandleUnchecked(this, EventArgs.Empty);
                }
            }
        }

        public BigCheckLabel()
        {
            InitializeComponent();
        }

        private void HandleChecked(object sender, EventArgs e)
        {
            OnChecked(EventArgs.Empty);
        }

        protected virtual void OnChecked(EventArgs e)
        {
            Checked?.Invoke(this, e);
        }

        private void HandleUnchecked(object sender, EventArgs e)
        {
            OnUnchecked(EventArgs.Empty);
        }

        protected virtual void OnUnchecked(EventArgs e)
        {
            Unchecked?.Invoke(this, e);
        }

        private void UserControl_MouseUp(object sender, MouseButtonEventArgs e)
        {
            IsChecked = !IsChecked;
        }

        private void UserControl_MouseEnter(object sender, MouseEventArgs e)
        {
            Box.Fill = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#40FFFFFF"));
        }

        private void UserControl_MouseLeave(object sender, MouseEventArgs e)
        {
            Box.Fill = null;
        }
    }
}
