using WebScraperFDDB;
using WebScraperFDDB.Fruits;


// ======================================= FRUITS ======================================= 
var fruitsBaseModels = new FruitsScraper().GetBaseDataFromHtmlTable();

List<FruitsDetailModel> fruitsDetailModels = [];
foreach (var model in fruitsBaseModels)
{
    FruitsDetailModel fruitsDetail = FruitsScraper.GetDetailedDataFromHtmlTableByHref(model);
    fruitsDetailModels.Add(fruitsDetail);
}

string savePath = @"C:\Users\wkala\Documents\STUDIA [AKTUALNIE]\MGR - UŚ\Magisterka\--TABELE--\reference-fruits.json";
Utility.ConvertAndSaveModelAsJson(fruitsDetailModels, savePath);
// =======================================================================================

Console.WriteLine("Finished!");
Console.ReadKey();