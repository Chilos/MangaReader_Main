using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace FreakCat.MangaReader.Logic.Entities
{
    public class MangaInfoTile
    {
        public string Url { get; set; }
        public BitmapImage Image { get; set; }
        public string Title { get; set; }
        public string SubTitle { get; set; }
        public string AutorName { get; set; }
        public double Rating { get; set; }
        public List<string> Genres { get; set; }
        public int ViewCount { get; set; }

        private bool LoadImage(string url)
        {
            Image = new BitmapImage(new Uri(url));
            return true;
        }

        //public static async Task<MangaInfoTile> CreateTileAsync(string tagsTile)
        //{
        //    //TODO все парсеры вызвать по очереди
        //    var ingUrl = "";
        //    var tile = new MangaInfoTile();
        //    await tile.LoadImage(ingUrl);

        //}
    }
}
