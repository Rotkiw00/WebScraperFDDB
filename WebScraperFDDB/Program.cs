using WebScraperFDDB;
using WebScraperFDDB.Fruits;

IScraper<FruitsBaseModel> fruitsScraper = new FruitsScraper();
var fruitsBaseModel = fruitsScraper.GetBaseDataFromHtmlTable();

Console.ReadKey();