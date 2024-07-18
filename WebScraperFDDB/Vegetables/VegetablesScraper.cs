using HtmlAgilityPack;
using HtmlAgilityPack.CssSelectors.NetCore;

namespace WebScraperFDDB.Vegetables
{
    internal class VegetablesScraper : Scraper
    {
        public VegetablesScraper() : base()
        {
        }

        protected override IList<HtmlNode> GetHtmlNodes(HtmlDocument htmlDocument)
        {
            var vegeTables = htmlDocument.QuerySelectorAll("table")[17]
                                         .QuerySelectorAll("td p");
            return vegeTables;
        }

        protected override List<string> CreateCssSelectorsDataFor100g() =>
        [
            "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(2)", // CALORIFIC VALUE [kJ]
            "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(3)", // CALORIES [kcal]
            "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(4)", // PROTEIN [g]
            "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(5)", // CARBOHYDARTES [g]
            "#content > div.mainblock > div.leftblock > div > div > div:nth-child(2) > div:nth-child(6)"  // FAT [g]
        ];
    }
}
