using HtmlAgilityPack;

namespace RickAndMortyWebApp.Tests.TestUtilities
{
    public class HtmlAssert
    {
        public static void TableRowCount(string html, int expectedRowCount, string? tableSelector = "//table//tbody//tr")
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            var rows = doc.DocumentNode.SelectNodes(tableSelector);

            if (expectedRowCount == 0)
            {
                Assert.True(rows == null || rows.Count == 0, "Expected no rows, but some were found.");
            }
            else
            {
                Assert.NotNull(rows);
                Assert.Equal(expectedRowCount, rows.Count);
            }
        }
    }
}
