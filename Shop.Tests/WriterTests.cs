using System.Text;

namespace Shop.Tests;

[TestFixture]
class WriterIntegrationTests
{
    [Test]
    public void ToWrite_WritesTextToFile()
    {
        Writer.DeleteReceipts("");
        var input = "Hello World";

        Writer.ToWrite(input);

        using (StreamReader reader = new StreamReader("receipts.txt", Encoding.UTF8))
        {
            string skip = reader.ReadLine();
            Assert.That(input, Is.EqualTo(reader.ReadLine()));     
        }
    }

    [Test]
    public void DeleteReceipts_ClearsFileContent()
    {
        var initialText = "Old Receipt Data";
        Writer.ToWrite(initialText);

        Writer.DeleteReceipts("");

        using (StreamReader reader = new StreamReader("receipts.txt", Encoding.UTF8))
        {
            string line = reader.ReadLine();
            
            Assert.That(line, Is.Not.EqualTo(initialText));
            Assert.That(line, Is.EqualTo(""));
        }
    }
}