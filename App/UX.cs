using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;
using lab_1_frog_aunyuh_team.back.Domain.GroceryCart;

public static class UX
{
    private const int MainWidth = 54;

    public static void DrawHeader(string title)
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("╔" + new string('═', MainWidth) + "╗");

        Console.Write("║");
        Console.BackgroundColor = ConsoleColor.DarkBlue;
        Console.ForegroundColor = ConsoleColor.Yellow;

        string cleanTitle = title.Length > MainWidth ? title.Substring(0, MainWidth) : title;
        int paddingLeft = (MainWidth - cleanTitle.Length) / 2;
        int paddingRight = MainWidth - cleanTitle.Length - paddingLeft;

        Console.Write(new string(' ', paddingLeft) + cleanTitle.ToUpper() + new string(' ', paddingRight));

        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("║");

        Console.WriteLine("╚" + new string('═', MainWidth) + "╝");
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void DrawTableHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌" + new string('─', MainWidth) + "┐");
        Console.WriteLine($"│ {"#", -2} {"NAME", -20} {"PRICE", 9} │ {"STOCK", -8} │");
        Console.WriteLine("├" + new string('─', MainWidth) + "┤");
        Console.ResetColor();
    }

    public static void DrawTableRow(int index, Product product)
    {
        string name = product.Name.Length > 20 ? product.Name.Substring(0, 17) + "..." : product.Name;

        string unit = product.PricingType == PricingType.PerItem ? "pc" : "kg";
        string stockInfo = $"{product.QuantityLeft.Value} {unit}";

        Console.Write($"│ {index, -2} {name, -20} {product.CurrentPrice.Amount, 9:N2} │ ");

        if (product.QuantityLeft.Value < 5)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        Console.Write($"{stockInfo, -8}");
        Console.ResetColor();
        Console.WriteLine(" │");
    }

    public static void DrawCartHeader()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("┌" + new string('─', MainWidth) + "┐");
        Console.WriteLine($"│ {"#", -2} {"ITEM", -20} {"QTY", -8} {"SUM", 12} │");
        Console.WriteLine("├" + new string('─', MainWidth) + "┤");
        Console.ResetColor();
    }

    public static void DrawCartRow(int index, LineBase line)
    {
        string name = line.Product.Name.Length > 20 ? line.Product.Name.Substring(0, 17) + "..." : line.Product.Name;
        string qty = line.Quantity.Value.ToString("0.##");

        string unit = line.Product.PricingType == PricingType.PerItem ? "pc" : "kg";

        Console.WriteLine($"│ {index, -2} {name, -20} {qty + " " + unit, -8} {line.Total.Amount, 12:N2} │");
    }

    public static void DrawTableFooter()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("└" + new string('─', MainWidth) + "┘");
        Console.ResetColor();
    }

    public static void DrawMenu(string[] items)
    {
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < items.Length; i++)
        {
            Console.WriteLine($" [ {i + 1} ]  {items[i]}");
        }
        Console.WriteLine(" [ 0 ]  Back / Exit");
        Console.ResetColor();
        Console.Write("\n>>> Choice: ");
    }

    public static void NotifySuccess(string message)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n[OK] {message}");
        Console.ResetColor();
        Pause();
    }

    public static void NotifyError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"\n[!] ERROR: {message}");
        Console.ResetColor();
        Pause();
    }

    private static void PrintLine(string text, bool center = false)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        if (text.Length > MainWidth)
        {
            text = text.Substring(0, MainWidth);
        }

        if (center)
        {
            int padding = (MainWidth - text.Length) / 2;
            text = new string(' ', padding) + text;
        }
        Console.Write(text.PadRight(MainWidth));
        Writer.ToWrite(text.PadRight(MainWidth));
        Console.ResetColor();
        Console.WriteLine();
    }

    private static void PrintRow(string leftText, string rightText)
    {
        Console.BackgroundColor = ConsoleColor.Gray;
        Console.ForegroundColor = ConsoleColor.Black;
        
        int availableSpace = MainWidth - rightText.Length - 1;

        if (leftText.Length > availableSpace)
        {
            leftText = leftText.Substring(0, availableSpace);
        }

        string finalLine = leftText + new string(' ', MainWidth - leftText.Length - rightText.Length) + rightText;
        Console.Write(finalLine);
        Writer.ToWrite(finalLine);
        Console.ResetColor();
        Console.WriteLine();
    }

    public static void PrintReceipt(LineBase[] lines, decimal total)
    {
        Console.Clear();
        try
        {
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                Console.Beep(2000, 150);
            }
        }
        catch { }

        Console.WriteLine();
        PrintLine(" ", true);
        PrintLine("*** FISCAL RECEIPT ***", true);
        PrintLine(new string('-', MainWidth), true);
        PrintLine($" Date: {DateTime.Now}");
        PrintLine(new string('-', MainWidth), true);

        foreach (LineBase line in lines)
        {
            if (line == null)
            {
                continue;
            }
            string nameInfo = $"{line.Product.Name} x{line.Quantity.Value}";
            PrintRow(" " + nameInfo, line.Total.Amount.ToString("N2"));
        }

        PrintLine(new string('-', MainWidth), true);
        PrintRow(" TOTAL:", total.ToString("N2") + " UAH");
        PrintLine(new string('-', MainWidth), true);

        string[] wishes =
        {
            "Bug-free code is life!", "You are my sunshine!", "Have a nice day!", 
            "Stay calm!", "It's harder to read code than to write it.", 
            "Confusion is part of programming.", "Good software, like wine, takes time.", 
            "Talk is cheap. Show me the code.", "eat(); sleep(); code(); repeat();"
        };
        Random rnd = new Random();
        PrintLine(" ", true);
        PrintLine(wishes[rnd.Next(wishes.Length)], true);
        PrintLine(" ", true);
        Console.WriteLine();
        Console.WriteLine("\nPress Enter...");
        Console.ReadLine();

        
    }

    public static void Pause()
    {
        Console.WriteLine("\nPress any key...");
        Console.ReadKey();
    }
}