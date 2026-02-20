using System.Collections;
using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;
using lab_1_frog_aunyuh_team.back.Domain.GroceryCart;

class Program
{
    public static string[] categories = {
        "Dairy", "Meat & Fish", "Fruits & Vegetables",
        "Bakery", "Grocery", "Drinks",
        "Sweets", "Household", "Electronics"
    };
    
    static void Main(string[] args)
    {
        Console.Title = "Frog Team Shop";
        Console.CursorVisible = false;

        Writer.DeleteReceipts("");

        MockData.InitializeCatalog();

        bool isRunning = true;
        while (isRunning)
        {
            UX.DrawHeader($"SUPERMARKET | BALANCE: {Session.CurrentUser.Balance.Amount:N2} UAH");
            string[] menuItems = {
                "Catalog",
                "My Cart",
                "Top Up Balance",
                "Checkout",
                "History (All Receipts)",
                "All Products (Enumerator Demo)",
                "Stats"
            };
            UX.DrawMenu(menuItems);

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    CategoryMenu();
                    break;
                case "2":
                    ManageCart();
                    break;
                case "3":
                    TopUpBalance();
                    break;
                case "4":
                    Checkout();
                    break;
                case "5":
                    ShowHistory();
                    break;
                case "6":
                    ShowProductEnumeratorDemo();
                    break;
                case "7":
                    ShowStats();
                    break;
                case "0":
                    isRunning = false;
                    break;
                default:
                    UX.NotifyError("Invalid choice.");
                    break;
            }
        }
    }

    static void CategoryMenu()
    {
        bool inCategoryMenu = true;
        while (inCategoryMenu)
        {
            UX.DrawHeader("DEPARTMENTS");
            
            UX.DrawMenu(categories);

            if (int.TryParse(Console.ReadLine(), out int choice))
            {
                if (choice == 0)
                {
                    inCategoryMenu = false;
                    return;
                }

                if (choice >= 1 && choice <= 9)
                {
                    ProductCategory selectedCategory = (ProductCategory)(choice - 1);
                    ShowProductsInCategory(selectedCategory);
                }
                else
                {
                    UX.NotifyError("Category not found.");
                }
            }
        }
    }

    static Product[] ShowProductsInCategory(ProductCategory category)
    {
        bool inProductMenu = true;
        Product[] productsToShow = GetProductsByCategory(category);

        while (inProductMenu)
        {
            UX.DrawHeader($"DEPT: {category}");
            UX.DrawTableHeader();

            if (productsToShow.Length == 0)
            {
                Console.WriteLine("   (Empty)");
            }
            else
            {
                for (int i = 0; i < productsToShow.Length; i++)
                {
                    UX.DrawTableRow(i + 1, productsToShow[i]);
                }
            }
            UX.DrawTableFooter();

            Console.WriteLine("\nActions:");
            Console.WriteLine(" [ 1..N ] Buy item");
            Console.WriteLine(" [ S ]    Sort (Cheaper -> Expensive)");
            Console.WriteLine(" [ D ]    Sort (Expensive -> Cheaper)");
            Console.WriteLine(" [ A ]    Sort (A -> Z)");
            Console.WriteLine(" [ 0 ]    Back");
            Console.Write(">>> ");

            string input = Console.ReadLine().ToUpper();

            if (input == "0")
            {
                return productsToShow;
            }
            else if (input == "S")
            {
                productsToShow = Session.Catalog.SortByPriceAscending(category);
            }
            else if (input == "D")
            {
                productsToShow = Session.Catalog.SortByPriceDescending(category);
            }
            else if (input == "A")
            {
                productsToShow = Session.Catalog.SortByAlphabetical(category);
            }
            else if (int.TryParse(input, out int index))
            {
                if (index > 0 && index <= productsToShow.Length)
                {
                    AddToCartLogic(productsToShow[index - 1]);
                }
                else
                {
                    UX.NotifyError("Invalid number.");
                }
            }
        }

        return productsToShow;
    }

    static Product[] GetProductsByCategory(ProductCategory pc)
    {
        int count = 0;
        foreach (Product p in Session.Catalog.products)
        {
            if (p != null && p.ProductCategory == pc)
            {
                count++;
            }
        }

        Product[] filtered = new Product[count];
        int j = 0;
        foreach (Product p in Session.Catalog.products)
        {
            if (p != null && p.ProductCategory == pc)
            {
                filtered[j++] = p;
            }
        }

        return filtered;
    }

    static void AddToCartLogic(Product product)
    {
        Console.WriteLine($"\nSelected: {product.Name}");

        double alreadyInCart = GetQuantityInCart(product);
        double realStock = product.QuantityLeft.Value;
        double available = realStock - alreadyInCart;

        string unit = product.PricingType == PricingType.PerItem ? "pc" : "kg";

        Console.WriteLine($"Available: {realStock} {unit} (In Cart: {alreadyInCart} {unit})");

        if (available <= 0)
        {
            UX.NotifyError("Out of stock!");
            return;
        }

        Console.Write($"Enter amount (max {available} {unit}): ");
        string input = Console.ReadLine();
        decimal decimalQty;

        try
        {
            decimalQty = InputHelper.ParseDecimal(input);
        }
        catch
        {
            UX.NotifyError("Invalid format.");
            return;
        }

        if (product.PricingType == PricingType.PerItem)
        {
            if (decimalQty % 1 != 0)
            {
                UX.NotifyError("Invalid amount! This item can only be bought as a whole piece.");
                return;
            }
        }

        double quantity = (double)decimalQty;

        if (quantity <= 0)
        {
            UX.NotifyError("Amount must be > 0");
            return;
        }

        if (quantity > available)
        {
            UX.NotifyError($"Only {available} {unit} available");
            return;
        }

        if (!product.CanTake(quantity))
        {
            UX.NotifyError("Error: Quantity invalid for this product type.");
            return;
        }

        LineBase newLine;
        if (product.PricingType == PricingType.PerKg)
        {
            newLine = new PerWeightLine(product, quantity);
        }
        else
        {
            newLine = new PerItemLine(product, quantity);
        }

        newLine.Total = newLine.CalculaceTotal(product.CurrentPrice.Amount, quantity);

        bool added = false;
        for (int i = 0; i < Session.CurrentUser.lines.Length; i++)
        {
            if (Session.CurrentUser.lines[i] == null)
            {
                Session.CurrentUser.lines[i] = newLine;
                added = true;
                break;
            }
        }

        if (added)
        {
            UX.NotifySuccess("Added to cart!");
        }
        else
        {
            UX.NotifyError("Cart is full!");
        }
    }

    static double GetQuantityInCart(Product productToCheck)
    {
        double sum = 0;
        foreach (LineBase line in Session.CurrentUser.lines)
        {
            if (line != null && line.Product == productToCheck)
            {
                sum += line.Quantity.Value;
            }
        }
        return sum;
    }

    static void ManageCart()
    {
        bool inCart = true;
        while (inCart)
        {
            UX.DrawHeader("YOUR CART");
            LineBase[] lines = Session.CurrentUser.lines;
            decimal totalSum = 0;
            bool isEmpty = true;

            UX.DrawCartHeader();
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i] != null)
                {
                    isEmpty = false;
                    UX.DrawCartRow(i + 1, lines[i]);
                    totalSum += lines[i].Total.Amount;
                }
            }
            UX.DrawTableFooter();

            if (isEmpty)
            {
                Console.WriteLine("   (Empty)");
            }
            else
            {
                Console.WriteLine($"   TOTAL: {totalSum:N2} UAH");
            }

            Console.WriteLine("\n[ Number ] Remove item");
            Console.WriteLine("[ 0 ]      Back");
            Console.Write(">>> ");

            if (int.TryParse(Console.ReadLine(), out int index))
            {
                if (index == 0)
                {
                    inCart = false;
                    return;
                }

                if (index > 0 && index <= 100 && lines[index - 1] != null)
                {
                    string name = lines[index - 1].Product.Name;
                    lines[index - 1] = null;

                    for (int i = index - 1; i < lines.Length - 1; i++)
                    {
                        lines[i] = lines[i + 1];     
                    }

                    lines[lines.Length - 1] = null;

                    UX.NotifySuccess($"Removed '{name}'!");
                }
                else
                {
                    UX.NotifyError("Invalid number.");
                }
            }
        }
    }

    static void TopUpBalance()
    {
        UX.DrawHeader("TOP UP BALANCE");
        Console.Write("Enter amount: ");
        
        string input = Console.ReadLine();
        decimal amount;

        try
        {
            amount = InputHelper.ParseDecimal(input);
        }
        catch
        {
            UX.NotifyError("Invalid format.");
            return;
        }

        if (amount > 0)
        {
            try
            {
                Session.CurrentUser.Balance = Session.CurrentUser.Balance.AddBalance(amount);
                UX.NotifySuccess($"Balance updated! Current: {Session.CurrentUser.Balance.Amount:N2} UAH");
            }
            catch
            {
                UX.NotifyError("Error adding balance.");
            }
        }
        else
        {
            UX.NotifyError("Amount must be > 0.");
        }
    }

    static void Checkout()
    {
        decimal total = 0;
        foreach (LineBase line in Session.CurrentUser.lines)
        {
            if (line != null)
            {
                total += line.Total.Amount;
            }
        }

        if (total == 0)
        {
            UX.NotifyError("Cart is empty.");
            return;
        }

        Console.Clear();
        UX.DrawHeader("CHECKOUT");
        Console.WriteLine($"Total to pay:   {total:N2} UAH");
        Console.WriteLine($"Your balance:   {Session.CurrentUser.Balance.Amount:N2} UAH");

        if (Session.CurrentUser.Balance.Amount < total)
        {
            UX.NotifyError("Insufficient funds! Please top up.");
            return;
        }

        Console.WriteLine("\nPress Enter to pay...");
        Console.ReadLine();

        try
        {
            Session.CurrentUser.Balance = Session.CurrentUser.Balance.PayProducts(total);

            UX.PrintReceipt(Session.CurrentUser.lines, total);

            foreach (LineBase line in Session.CurrentUser.lines)
            {
                if (line != null)
                {
                    Product productInCatalog = line.Product;
                    Quantity newQty = productInCatalog.QuantityLeft.ChangeValue(line.Quantity.Value);
                    productInCatalog.QuantityLeft = newQty;
                }
            }
            Session.CurrentUser.lines = new LineBase[100];
        }
        catch
        {
            UX.NotifyError("Transaction failed.");
        }

        if (DateTime.Now - Session.LastRecalculation >= TimeSpan.FromMinutes(1))
        {
            Session.Catalog.FixPrices();
            Session.LastRecalculation = DateTime.Now;
        }
    }

    static void ShowHistory()
    {
        Console.Clear();
        UX.DrawHeader("RECEIPT HISTORY");
        Writer.PrintAllReceipts();
        UX.Pause();
    }

    static void ShowProductEnumeratorDemo()
    {
        ProductCollection collection = new ProductCollection();
        foreach (Product p in Session.Catalog.products)
        {
            if (p != null)
            {
                collection.Add(p);
            }
        }

        UX.DrawHeader("ALL PRODUCTS (ENUMERATOR DEMO)");
        UX.DrawTableHeader();

        int index = 1;
        IEnumerator<Product> it = collection.GetEnumerator();
        while (it.MoveNext())
        {
            UX.DrawTableRow(index, it.Current);
            index++;
        }

        UX.DrawTableFooter();
        Console.WriteLine($"\nTotal items: {collection.Count}");
        UX.Pause();
    }

    static void ShowStats()
    {
        int totalValue = 0;
        int totalProductCount = 0;
        Hashtable perCategoryValue = new Hashtable();
        Hashtable perCategoryQuantity = new Hashtable();
        Hashtable averagePricePerCategory = new Hashtable();
        Hashtable minPriceItemPerCategory = new Hashtable();
        Hashtable maxPriceItemPerCategory = new Hashtable();
        
        UX.DrawHeader("STATS FOR ALL CATEGORIES");
        

        for (int i = 0; i <= 8; i++)
        {
            int categoryValue = 0;
            int categoryProductCount = 0;
            ProductCategory currentProductCategory = (ProductCategory)i;
            Product[] productsToShow = GetProductsByCategory(currentProductCategory);

            if (productsToShow.Length == 0)
            {
                continue;
            }
            
            decimal minPricePerCategory = productsToShow[0].BasePrice.Amount;
            decimal maxPricePerCategory = productsToShow[0].BasePrice.Amount;
            foreach (Product product in productsToShow)
            {
                categoryValue += (int)(product.BasePrice.Amount * (decimal)product.StockQuantity.Value);
                totalValue += (int)(product.BasePrice.Amount * (decimal)product.StockQuantity.Value);
                categoryProductCount+=(int)product.StockQuantity.Value;
                totalProductCount+=(int)product.StockQuantity.Value;

                if (product.BasePrice.Amount < minPricePerCategory)
                {
                    minPricePerCategory = product.BasePrice.Amount;
                }
                if (product.BasePrice.Amount > maxPricePerCategory)
                {
                    maxPricePerCategory = product.BasePrice.Amount;
                }
            }
            
            perCategoryValue.Add(currentProductCategory, categoryValue);
            perCategoryQuantity.Add(currentProductCategory, categoryProductCount);
            averagePricePerCategory.Add(currentProductCategory, (decimal)categoryValue / categoryProductCount);
            minPriceItemPerCategory.Add(currentProductCategory, minPricePerCategory);
            maxPriceItemPerCategory.Add(currentProductCategory, maxPricePerCategory);
            
        }


        Console.WriteLine($"Total value: {totalValue}");
        Console.WriteLine($"Total products: {totalProductCount}");
        Console.WriteLine();
        
        for (int i = 0; i <= 8; i++)
        {
            ProductCategory currentProductCategory= (ProductCategory)i;

            object? quantity = perCategoryQuantity[currentProductCategory];
            if (quantity is null)
            {
                continue;
            }
            
            if (quantity is int intQuantity)
            {
                if (intQuantity == 0)
                {
                    continue;
                }
            }
            
            Console.WriteLine($"[ {currentProductCategory} ]");
            Console.WriteLine($"├─ Value:        {perCategoryValue[currentProductCategory]}");
            Console.WriteLine($"├─ Count:        {perCategoryQuantity[currentProductCategory]}");
            Console.WriteLine($"└─ Pricing:      AVG: {((decimal)averagePricePerCategory[currentProductCategory]!):F2}  |  Min: {minPriceItemPerCategory[currentProductCategory]}  |  Max: {maxPriceItemPerCategory[currentProductCategory]}");
            Console.WriteLine();
        }

        UX.Pause();
    }
}