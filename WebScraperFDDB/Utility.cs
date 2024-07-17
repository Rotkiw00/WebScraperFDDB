using Newtonsoft.Json;

namespace WebScraperFDDB
{
    public static class Utility
    {
        public static void ConvertAndSaveModelAsJson(object objectModel, string savePath)
        {
            var jsonModel = JsonConvert.SerializeObject(objectModel);
            File.WriteAllText(savePath, jsonModel);
        }
    }
}
