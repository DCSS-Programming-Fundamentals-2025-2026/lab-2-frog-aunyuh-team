using System.Text;

public static class Writer
{
    public static void ToWrite(string text)
    {
        using (StreamWriter writer = new StreamWriter("receipts.txt", true, Encoding.UTF8))
        {
            writer.WriteLine(text);
        }
    }

    public static void DeleteReceipts(string text)
    {
        using (StreamWriter writer = new StreamWriter("receipts.txt", false, Encoding.UTF8))
        {
            writer.WriteLine(text);
        }
    }

    public static void PrintAllReceipts()
    {
        using (StreamReader reader = new StreamReader("receipts.txt", Encoding.UTF8))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                Console.WriteLine(line);
            }
        }
    }
}