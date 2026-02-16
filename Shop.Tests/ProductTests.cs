using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;

namespace Shop.Tests;

class ProductTests
{
    [Test]
    public void CanTake_RequestMoreThanStock_ReturnsFalse()
    {
        var product = new Product("Apple", 100, 10, PricingType.PerItem, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        
        var result = product.CanTake(11);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CanTake_RequestValidAmount_ReturnsTrue()
    {
        var product = new Product("Apple", 100, 10, PricingType.PerItem, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        
        var result = product.CanTake(5);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanTake_PerKg_RequestLessThanMinLimit_ReturnsFalse()
    {
        var product = new Product("Potato", 20, 100, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        
        var result = product.CanTake(0.10);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CanTake_PerKg_RequestValidWeight_ReturnsTrue()
    {
        var product = new Product("Potato", 20, 100, PricingType.PerKg, ProductCategory.FruitsAndVegetables, PricingAdjustmentState.None);
        
        var result = product.CanTake(0.5);

        Assert.That(result, Is.True);
    }

    [Test]
    public void CanTake_PerItem_RequestDecimalAmount_ReturnsFalse()
    {
        var product = new Product("iPhone", 30000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);
        
        var result = product.CanTake(1.5);

        Assert.That(result, Is.False);
    }

    [Test]
    public void CanTake_PerItem_RequestIntegerAmount_ReturnsTrue()
    {
        var product = new Product("iPhone", 30000, 10, PricingType.PerItem, ProductCategory.Electronics, PricingAdjustmentState.None);
        
        var result = product.CanTake(2);

        Assert.That(result, Is.True);
    }
}