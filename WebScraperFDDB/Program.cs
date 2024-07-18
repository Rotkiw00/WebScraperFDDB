using WebScraperFDDB;
using WebScraperFDDB.Fruits;
using WebScraperFDDB.Model;
using WebScraperFDDB.Vegetables;


#region FRUITS
Console.WriteLine("Getting fruits..");

var fruitsBaseModels = new FruitsScraper().GetBaseDataFromHtmlTable();
Console.WriteLine($"{fruitsBaseModels.Count()} vegetable records were scraped.");

List<FVDetailModel> fruitsDetailModels = [];
foreach (var model in fruitsBaseModels)
{
    FVDetailModel fruitsDetail = FruitsScraper.GetDetailedDataFromHtmlTableByHref(model);
    fruitsDetailModels.Add(fruitsDetail);
}

string savePath_f = @"C:\Users\wkala\Documents\STUDIA [AKTUALNIE]\MGR - UŚ\Magisterka\--TABELE--\reference-fruits.json";
Utility.ConvertAndSaveModelAsJson(fruitsDetailModels, savePath_f);
#endregion

#region VEGETABLES
Console.WriteLine("Getting vegies..");

var vegeBaseModels = new VegetablesScraper().GetBaseDataFromHtmlTable();
Console.WriteLine($"{vegeBaseModels.Count()} vegetable records were scraped.");

List<FVDetailModel> vegeDetailModels = [];
foreach (var model in vegeBaseModels)
{
    FVDetailModel vegeDetail = VegetablesScraper.GetDetailedDataFromHtmlTableByHref(model);
    vegeDetailModels.Add(vegeDetail);
}

string savePath_v = @"C:\Users\wkala\Documents\STUDIA [AKTUALNIE]\MGR - UŚ\Magisterka\--TABELE--\reference-vegies.json";
Utility.ConvertAndSaveModelAsJson(vegeDetailModels, savePath_v);
#endregion

Console.WriteLine("Finished!");
Console.ReadKey();