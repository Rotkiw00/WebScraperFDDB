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

        public static FruitsDetailModel GetDetailedDataFromHtmlTableByHref(FruitsBaseModel model)
        {
            HtmlWeb htmlWeb = new();
            HtmlDocument detailedHtmlDocument = new();

            // need to get data from three tables
            /*
             * 1. Data for 100 g (Calorific value, Calories, Protein, Carbo., Fat),
             *  // those below - later
             * 2. Vitamins (any as a string),
             * 3. Minerals (any as a string),
             */

            detailedHtmlDocument = htmlWeb.Load(model.Href);

            List<string> dataFor100g_selectors =
            [
                "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(2)", // CALORIFIC VALUE [kJ]
                "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(3)", // CALORIES [kcal]
                "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(4)", // PROTEIN [g]
                "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(5)", // CARBOHYDARTES [g]
                "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(7)"  // FAT [g]
            ];
            
                        
            var dataFor100g = GetData100gBySelector(detailedHtmlDocument, dataFor100g_selectors).ToList();

            return new FruitsDetailModel(model.Name,
                                         dataFor100g[0],
                                         dataFor100g[1],
                                         dataFor100g[2],
                                         dataFor100g[3],
                                         dataFor100g[4]);
        }

        static IEnumerable<string> GetData100gBySelector(HtmlDocument detailedHtmlDocument,
                                                             List<string> dataFor100g_selectors)
        {
            foreach (string selector in dataFor100g_selectors)
            {
                yield return detailedHtmlDocument.QuerySelector(selector)
                                                 .QuerySelectorAll("div")[2].InnerText.Split(' ')
                                                                                      .FirstOrDefault();
            }
        }
    }
}
