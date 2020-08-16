using System.Windows.Controls;
using System.Windows.Media;

namespace Crews.Education.Presentation.SightWordCards
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
                SolidColorBrush TextBrush = new SolidColorBrush
                {
                    Color = value.Color.R + value.Color.G + value.Color.B < 384 ?
                    Colors.White : Colors.Black
                };
                WordBox.Foreground = TextBrush;

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
