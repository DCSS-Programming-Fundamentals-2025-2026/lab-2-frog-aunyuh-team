using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;

namespace Shop.Tests;

class ProductCatalogTests
{
    [Test]
    public void SortByPriceAscending_ValidCategory_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceAscending(ProductCategory.Bakery);

        Assert.That(catalog.Length, Is.EqualTo(3));

        Assert.That(catalog[0].Name, Is.EqualTo("Baguette French"));
        Assert.That(catalog[0].CurrentPrice, Is.EqualTo(new Money(25m)));

        Assert.That(catalog[1].Name, Is.EqualTo("Donut"));
        Assert.That(catalog[1].CurrentPrice, Is.EqualTo(new Money(35m)));

        Assert.That(catalog[2].Name, Is.EqualTo("Croissant"));
        Assert.That(catalog[2].CurrentPrice, Is.EqualTo(new Money(40m)));
    }

    [Test]
    public void SortByPriceAscending_ValidCategoryFromSortedCollection_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceAscending(ProductCategory.Drinks);

        Assert.That(catalog.Length, Is.EqualTo(3));

        Assert.That(catalog[0].Name, Is.EqualTo("Fanta"));
        Assert.That(catalog[0].CurrentPrice, Is.EqualTo(new Money(15m)));

        Assert.That(catalog[1].Name, Is.EqualTo("Sprite"));
        Assert.That(catalog[1].CurrentPrice, Is.EqualTo(new Money(20m)));

        Assert.That(catalog[2].Name, Is.EqualTo("Water"));
        Assert.That(catalog[2].CurrentPrice, Is.EqualTo(new Money(25m)));
    }

    [Test]
    public void SortByPriceAscending_WithDuplicatePrices_ReturnsProductsSorted()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceAscending(ProductCategory.FruitsAndVegetables);

        Assert.That(catalog.Length, Is.EqualTo(3));

        Assert.That(catalog[0].CurrentPrice, Is.EqualTo(new Money(25m)));
        Assert.That(catalog[1].CurrentPrice, Is.EqualTo(new Money(25m)));
        Assert.That(catalog[2].CurrentPrice, Is.EqualTo(new Money(60m)));
    }

    [Test]
    public void SortByPriceAscending_ValidCategoryWithOneElement_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceAscending(ProductCategory.MeatAndFish);

        Assert.That(catalog.Length, Is.EqualTo(1));

        Assert.That(catalog[0].Name, Is.EqualTo("Steak Ribeye"));
        Assert.That(catalog[0].CurrentPrice, Is.EqualTo(new Money(1200m)));
    }

    [Test]
    public void SortByPriceAscending_EmptyCategory_ThrowsException()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];
        var ex = false;

        try
        {
            catalog = obj.SortByPriceAscending(ProductCategory.Grocery);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    [Test]
    public void SortByAlphabetical_ValidCategory_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByAlphabetical(ProductCategory.FruitsAndVegetables);

        Assert.That(catalog.Length, Is.EqualTo(3));

        Assert.That(catalog[0].Name, Is.EqualTo("Bananas"));
        Assert.That(catalog[1].Name, Is.EqualTo("Carrots"));
        Assert.That(catalog[2].Name, Is.EqualTo("Potatoes"));
    }

    [Test]
    public void SortByAlphabetical_ValidCategoryWithOneElement_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByAlphabetical(ProductCategory.MeatAndFish);

        Assert.That(catalog.Length, Is.EqualTo(1));
        Assert.That(catalog[0].Name, Is.EqualTo("Steak Ribeye"));
    }

    [Test]
    public void SortByAlphabetical_EmptyCategory_ThrowsException()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];
        var ex = false;

        try
        {
            catalog = obj.SortByAlphabetical(ProductCategory.Grocery);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    [Test]
    public void FixPrices_LessThanOneMinute_DoesNotApplyFixedPrice()
    {
        Session.LastRecalculation = DateTime.Now.AddSeconds(-30);
        var obj = new ProductCatalog();
        obj.products[0] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        Assert.That(obj.products[0].CurrentPrice, Is.EqualTo(new Money(38000)));
    }

    [Test]
    public void FixPrices_OneMinuteOrMore_StateNone_DoesNotChangePrice()
    {
        Session.LastRecalculation = DateTime.Now.AddMinutes(-2);
        var obj = new ProductCatalog();
        obj.products[0] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        obj.products[0].QuantityLeft = obj.products[0].QuantityLeft.ChangeValue(4);
        obj.FixPrices();

        Assert.That(obj.products[0].CurrentPrice, Is.EqualTo(new Money(38000)));
    }

    [Test]
    public void FixPrices_OneMinuteOrMore_StateDiscounted_DecreasesPrice()
    {
        Session.LastRecalculation = DateTime.Now.AddMinutes(-2);
        var obj = new ProductCatalog();
        obj.products[0] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        obj.products[0].QuantityLeft = obj.products[0].QuantityLeft.ChangeValue(1);
        obj.FixPrices();

        Assert.That(obj.products[0].CurrentPrice, Is.EqualTo(new Money(34200)));
    }

    [Test]
    public void FixPrices_OneMinuteOrMore_StateMarkedUp_IncreasesPrice()
    {
        Session.LastRecalculation = DateTime.Now.AddMinutes(-2);
        var obj = new ProductCatalog();
        obj.products[0] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        obj.products[0].QuantityLeft = obj.products[0].QuantityLeft.ChangeValue(6);
        obj.FixPrices();

        Assert.That(obj.products[0].CurrentPrice, Is.EqualTo(new Money(45600)));
    }

    [Test]
    public void SortByPriceDescending_ValidCategory_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceDescending(ProductCategory.Bakery);

        Assert.That(catalog.Length, Is.EqualTo(3));

        Assert.That(catalog[0].Name, Is.EqualTo("Croissant"));
        Assert.That(catalog[0].CurrentPrice, Is.EqualTo(new Money(40m)));

        Assert.That(catalog[1].Name, Is.EqualTo("Donut"));
        Assert.That(catalog[1].CurrentPrice, Is.EqualTo(new Money(35m)));

        Assert.That(catalog[2].Name, Is.EqualTo("Baguette French"));
        Assert.That(catalog[2].CurrentPrice, Is.EqualTo(new Money(25m)));
    }

    [Test]
    public void SortByPriceDescending_ValidCategoryWithOneElement_ReturnsSortedProducts()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];

        catalog = obj.SortByPriceDescending(ProductCategory.MeatAndFish);

        Assert.That(catalog.Length, Is.EqualTo(1));
        Assert.That(catalog[0].Name, Is.EqualTo("Steak Ribeye"));
    }

    [Test]
    public void SortByPriceDescending_EmptyCategory_ThrowsException()
    {
        var obj = AddProductsToProperty();
        var catalog = new Product[0];
        var ex = false;

        try
        {
            catalog = obj.SortByPriceDescending(ProductCategory.Grocery);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    private ProductCatalog AddProductsToProperty()
    {
        var pc = new ProductCatalog();

        pc.products[0] = new Product("MacBook Pro 14", 75000, 5, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);
        pc.products[1] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        pc.products[2] = new Product("Steak Ribeye", 1200, 10.0, PricingType.PerKg, ProductCategory.MeatAndFish, PricingAdjustmentState.None);

        pc.products[3] = new Product("Bananas", 60, 100.0, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        pc.products[4] = new Product("Potatoes", 25, 500.0, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        pc.products[5] = new Product("Carrots", 25, 500.0, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);

        pc.products[6] = new Product("Baguette French", 25, 15, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);
        pc.products[7] = new Product("Croissant", 40, 20, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);
        pc.products[8] = new Product("Donut", 35, 25, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);

        pc.products[9] = new Product("Fanta", 15, 15, PricingType.PerItem, ProductCategory.Drinks, PricingAdjustmentState.None);
        pc.products[10] = new Product("Sprite", 20, 20, PricingType.PerItem, ProductCategory.Drinks, PricingAdjustmentState.None);
        pc.products[11] = new Product("Water", 25, 25, PricingType.PerItem, ProductCategory.Drinks, PricingAdjustmentState.None);

        return pc;
    }
}