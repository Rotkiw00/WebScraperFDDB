using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace WebScraperFDDB.Fruits
{
    internal class FruitsScraper : IScraper<FruitsBaseModel>
    {
        private const string BASE_URI = "https://fddb.info";
        private const string BASE_FRUITS_URL = "https://fddb.info/db/en/groups/fruits/index.html";

        private readonly HtmlWeb _web;
        private readonly HtmlDocument _htmlDocument;

        public FruitsScraper()
        {
            _web = new HtmlWeb();
            _htmlDocument = _web.Load(BASE_FRUITS_URL);
        }

        public IEnumerable<FruitsBaseModel> GetBaseDataFromHtmlTable()
        {
            var fruitsTables = _htmlDocument.QuerySelectorAll("table")[10]
                                            .QuerySelectorAll("td p");

            foreach (var item in fruitsTables)
            {
                var hrefs = item.QuerySelectorAll("a");

                foreach (var href in hrefs)
                {
                    string name = href.InnerText;
                    string hrefString = BASE_URI + href.Attributes["href"].Value;

                    yield return new FruitsBaseModel(name, hrefString);
                }
            }
        }
    }
}
