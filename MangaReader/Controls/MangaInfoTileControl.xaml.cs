using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace MangaReader.Controls
{
    public sealed partial class MangaInfoTileControl : UserControl
    {
        public MangaInfoTileControl()
        {
            this.InitializeComponent();
        }

        public Image Image
        {
            get { return Image1; }
            set { Image1 = value; }
        }

        public TextBlock Title
        {
            get { return Title1; }
            set { Title1 = value; }
        }

        public TextBlock SubTitle
        {
            get { return SubTitle1; }
            set { SubTitle1 = value; }
        }
    }
}
