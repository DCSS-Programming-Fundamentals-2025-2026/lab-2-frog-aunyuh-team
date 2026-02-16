using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;

public static class MockData
{
    public static void InitializeCatalog()
    {
        ProductCatalog catalog = Session.Catalog;

        catalog.products[0] = new Product("MacBook Pro 14", 75000, 5, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);
        catalog.products[1] = new Product("iPhone 15", 38000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);

        catalog.products[2] = new Product("Milk Yahotynske", 45, 50, PricingType.PerItem, ProductCategory.Dairy, PricingAdjustmentState.None);
        catalog.products[3] = new Product("Cheese Cheddar", 450, 20.0, PricingType.PerKg, ProductCategory.Dairy, PricingAdjustmentState.None);

        catalog.products[4] = new Product("Chicken Fillet", 160, 30.0, PricingType.PerKg, ProductCategory.MeatAndFish, PricingAdjustmentState.None);
        catalog.products[5] = new Product("Steak Ribeye", 1200, 10.0, PricingType.PerKg, ProductCategory.MeatAndFish, PricingAdjustmentState.MarkedUp);

        catalog.products[6] = new Product("Bananas", 60, 100.0, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        catalog.products[7] = new Product("Potatoes", 25, 500.0, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.Discounted);

        catalog.products[8] = new Product("Coca-Cola 0.5", 30, 100, PricingType.PerItem, ProductCategory.Drinks, PricingAdjustmentState.None);
        catalog.products[9] = new Product("Juice Rich", 70, 40, PricingType.PerItem, ProductCategory.Drinks, PricingAdjustmentState.None);
        
        catalog.products[10] = new Product("Baguette French", 25, 15, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);
        catalog.products[11] = new Product("Croissant", 40, 20, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);
        catalog.products[12] = new Product("Donut", 35, 25, PricingType.PerItem, ProductCategory.Bakery, PricingAdjustmentState.None);
    }
}