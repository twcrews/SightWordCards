using System.Windows.Controls;
using System.Windows.Media;

namespace SightWordCards
{
    /// <summary>
    /// Card UserControl partial class.
    /// </summary>
    public partial class Card : UserControl
    {
        public Card()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Background color of the card.
        /// </summary>
        public SolidColorBrush BackgroundColor
        {
            get
            {
                return (SolidColorBrush)BgBorder.Background;
            }

            set
            {
                BgBorder.Background = value;
            }
        }

        public string Text
        {
            get
            {
                return WordBox.Content.ToString();
            }

            set
            {
                WordBox.Content = value;
            }
        }
    }
}
