using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FreakCat.MangaReader.Parser
{
    public static class ParseMangaInfoTile
    {
        //private void LoadTop500Tiles()
        //{
        //    for (int i = 0; i < 490; i += 70)
        //        _mangaInfoTiles = PickOutTagsTiles(LoadListMangaTiles(i, 70), _mangaInfoTiles);
        //    _mangaInfoTiles = PickOutTagsTiles(LoadListMangaTiles(490, 10), _mangaInfoTiles);
        //}

        //private string LoadListMangaTiles(int from, int count)
        //{
        //    var parsStr = $"http://readmanga.me/list?type=&sortType=rate&offset={from}&max={count}";
        //    return new HttpClient().GetStringAsync(parsStr).Result;
        //}
        /// <summary>
        /// Выбирает HTML-классы относяшиеся именно к списку манги
        /// </summary>
        /// <param name="tagsStr">HTML страница для выборки</param>
        /// <param name="tilesList">Список уже имеющихся</param>
        /// <returns>Список манги в HTML тегах</returns>
        public static List<string> PickOutTagsTiles(string tagsStr, List<string> tilesList=null)
        {
            if (tilesList == null)
                tilesList = new List<string>();
            const RegexOptions options = RegexOptions.Compiled | RegexOptions.Singleline;
            var r = new Regex("<div class=\"tile col-sm-6\">(?<val>.*?)<div class=\"chapters\">", options);
            tilesList.AddRange(from Match mat in r.Matches(tagsStr) select mat.Groups["val"].Value);
            return tilesList.Count == 0 ? null : tilesList;
        }
    }
}
