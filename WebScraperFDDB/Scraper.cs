using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;
using WebScraperFDDB.Model;

namespace WebScraperFDDB
{
    public class Scraper
    {
        private const string BASE_URI = "https://fddb.info";

        protected readonly HtmlWeb HtmlWebScraper;

        public Scraper()
        {
            HtmlWebScraper = new HtmlWeb();
        }

        public IEnumerable<FVBaseModel> GetBaseDataFromHtmlTable(string htmlDocumentUri)
        {
            HtmlDocument htmlDocument = HtmlWebScraper.Load(htmlDocumentUri);

            var vegeTables = GetHtmlNodes(htmlDocument);

            foreach (var item in vegeTables)
            {
                var hrefs = item.QuerySelectorAll("a");

                foreach (var href in hrefs)
                {
                    string name = href.InnerText;
                    string hrefString = BASE_URI + href.Attributes["href"].Value;

                    yield return new FVBaseModel(name, hrefString);
                }
            }
        }

        protected virtual IList<HtmlNode> GetHtmlNodes(HtmlDocument htmlDocument) => throw new NotImplementedException();

        public FVDetailModel GetDetailedDataFromHtmlTableByHref(FVBaseModel model)
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

            List<string> dataFor100g_selectors = CreateCssSelectorsDataFor100g();

            var dataFor100g = GetData100gBySelector(detailedHtmlDocument, dataFor100g_selectors).ToList();

            return new FVDetailModel(model.Name,
                                    dataFor100g[0],
                                    dataFor100g[1],
                                    dataFor100g[2],
                                    dataFor100g[3],
                                    dataFor100g[4]);
        }

        protected virtual List<string> CreateCssSelectorsDataFor100g() => throw new NotImplementedException();

        static IEnumerable<string> GetData100gBySelector(HtmlDocument detailedHtmlDocument, List<string> dataFor100g_selectors)
        {
            foreach (string selector in dataFor100g_selectors)
            {
                yield return detailedHtmlDocument.QuerySelector(selector).QuerySelectorAll("div")[2].InnerText.Split(' ').FirstOrDefault();
            }
        }
    }
}
