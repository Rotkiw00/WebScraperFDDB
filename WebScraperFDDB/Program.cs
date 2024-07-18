using WebScraperFDDB;
using WebScraperFDDB.Fruits;
using WebScraperFDDB.Vegetables;
using WebScraperFDDB.Model;


#region FRUITS
Scraper fruitsScraper = new FruitsScraper();

Console.WriteLine("Getting fruits..");

var fruitsBaseModels = fruitsScraper.GetBaseDataFromHtmlTable("https://fddb.info/db/en/groups/fruits/index.html");

Console.WriteLine($"{fruitsBaseModels.Count()} vegetable records were scraped.");

List<FVDetailModel> fruitsDetailModels = [];
foreach (var model in fruitsBaseModels)
{
    FVDetailModel fruitsDetail = fruitsScraper.GetDetailedDataFromHtmlTableByHref(model);
    fruitsDetailModels.Add(fruitsDetail);
}

string savePath_f = @"C:\Users\wkala\Documents\STUDIA [AKTUALNIE]\MGR - UŚ\Magisterka\--TABELE--\reference-fruits.json";
Utility.ConvertAndSaveModelAsJson(fruitsDetailModels, savePath_f);
#endregion

#region VEGETABLES
Scraper vegiesScraper = new VegetablesScraper();

Console.WriteLine("Getting vegies..");

var vegeBaseModels = vegiesScraper.GetBaseDataFromHtmlTable("https://fddb.info/db/en/groups/vegetables/index.html");

Console.WriteLine($"{vegeBaseModels.Count()} vegetable records were scraped.");

List<FVDetailModel> vegeDetailModels = [];
foreach (var model in vegeBaseModels)
{
    FVDetailModel vegeDetail = vegiesScraper.GetDetailedDataFromHtmlTableByHref(model);
    vegeDetailModels.Add(vegeDetail);
}

string savePath_v = @"C:\Users\wkala\Documents\STUDIA [AKTUALNIE]\MGR - UŚ\Magisterka\--TABELE--\reference-vegies.json";
Utility.ConvertAndSaveModelAsJson(vegeDetailModels, savePath_v);
#endregion

Console.WriteLine("Finished!");

Console.ReadKey();