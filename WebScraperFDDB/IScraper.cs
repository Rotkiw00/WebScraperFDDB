namespace WebScraperFDDB
{
    internal interface IScraper<T>
    {
        IEnumerable<T> GetBaseDataFromHtmlTable();
    }
}
