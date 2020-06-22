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
    /// Interaction logic for MenuButton.xaml
    /// </summary>
    public partial class MenuButton : UserControl
    {
        public MenuButton()
        {
            InitializeComponent();
        }

        public string ButtonContent
        {
            get
            {
                return TextContent.Content.ToString();
            }
            set
            {
                TextContent.Content = value;
            }
        }

        public bool BlackText
        {
            set
            {
                TextContent.Foreground = value ? 
                    new SolidColorBrush(Colors.Black) : 
                    new SolidColorBrush(Colors.White);
            }
        }
    }
}
