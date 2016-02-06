using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Windows.Storage.Streams;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using FreakCat.MangaReader.Logic.Entities;

// Документацию по шаблону элемента "Пустая страница" см. по адресу http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace MangaReader
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private List<string> _mangaInfoTiles;
        public ObservableCollection<MangaInfoTile> Tiles { get; private set; }
        public MainPage()
        {
            this.InitializeComponent();
            Tiles = new ObservableCollection<MangaInfoTile>();
            DataContext = this;
        }

        private async Task<List<string>> LoadTop500Tiles()
        {
            List<string> col = new List<string>();
            for (int i = 0; i < 490; i += 70)
                col = ParseTagsTiles(await LoadListMangaTiles(i, 70), _mangaInfoTiles);
            col = ParseTagsTiles(await LoadListMangaTiles(490, 10), _mangaInfoTiles);
            return col;
        }

        private async void ParseHtmlToObject()
        {
            //LoadInfo.Visibility = Visibility.Visible;
            _mangaInfoTiles = await LoadTop500Tiles();
            if (_mangaInfoTiles?.Count == 0)
                return;
            foreach (var htmlTile in _mangaInfoTiles)
            {

                Tiles.Add(new MangaInfoTile() { Title = ParseTitle(htmlTile), Image = await GetImage(ParseImageUrl(htmlTile)) });
            }
            //LoadInfo.Visibility = Visibility.Collapsed;
        }

        private string ParseTitle(string htmlTile)
        {
            string patrn = "<h3>(\\W*)<a href=\"/(.*)\" title=\"(.*)\">(.*?)</a>(\\W*)</h3>";
            Regex r = new Regex(patrn, RegexOptions.Multiline);
            foreach (Match mat in r.Matches(htmlTile))
            {
                return mat.Groups[4].Value;
            }
            return string.Empty;
        }

        private string ParseImageUrl(string htmlTile)
        {
            string patrn = "<img(\\W*)src=\"(.*?)\"";
            Regex r = new Regex(patrn, RegexOptions.Multiline);
            foreach (Match mat in r.Matches(htmlTile))
            {
                return mat.Groups[2].Value;
            }
            return string.Empty;
        }

        private async Task<BitmapImage> GetImage(string url)
        {
            var b = await getImageByteAsync(url);
            var s = await ConvertTo(b);
            var ii = new BitmapImage();
            ii.SetSource(s);
            return ii;
        }

        private async Task<string> LoadListMangaTiles(int from, int count)
        {
            var parsStr = $"http://readmanga.me/list?type=&sortType=rate&offset={from}&max={count}";
            return await new HttpClient().GetStringAsync(parsStr);
        }

        private List<string> ParseTagsTiles(string tagsStr, List<string> tilesList)
        {
            if (tilesList == null)
                tilesList = new List<string>();
            string patrn = "<div class=\"tile col-sm-6\">(?<val>.*?)<div class=\"chapters\">";
            RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            Regex r = new Regex(patrn, options);
            foreach (Match mat in r.Matches(tagsStr))
            {
                tilesList.Add(mat.Groups["val"].Value);
            }
            return tilesList.Count == 0 ? null : tilesList;
        }

        private static async Task<byte[]> getImageByteAsync(string url)
        {
            return await new HttpClient().GetByteArrayAsync(url);
        }



        internal async Task<InMemoryRandomAccessStream> ConvertTo(byte[] arr)
        {
            InMemoryRandomAccessStream randomAccessStream = new InMemoryRandomAccessStream();
            await randomAccessStream.WriteAsync(arr.AsBuffer());
            randomAccessStream.Seek(0); // Just to be sure.
                                        // I don't think you need to flush here, but if it doesn't work, give it a try.
            return randomAccessStream;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            //var b = await getImageByteAsync(@"http://d.readmanga.ru/uploads/pics/00/13/770_p.jpg");
            //var s = await ConvertTo(b);
            //var ii = new BitmapImage();
            //ii.SetSource(s)
            Tiles.Clear();
            ParseHtmlToObject();
            //Tiles.Add(new MangaInfoTile() {Title = "asdasdasdasd"});
            //Image.Source = ii;

        }

        //private void HamburgerButton_OnClick(object sender, RoutedEventArgs e)
        //{
        //    SvMainMenu.IsPaneOpen = !SvMainMenu.IsPaneOpen;
        //}
    }
}
