using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.GroceryCart;
using lab_1_frog_aunyuh_team.back.Domain.User;

namespace Shop.Tests;

class PerWeightLineTests
{
    [Test]
    public void CalculateTotalPerWeight_ValidInput_ReturnsCorrectMoney()
    {
        var product = GetProduct();
        var obj = new PerWeightLine(product, 0);
        var inputCostProduct = 265.51m;
        var inputQuantityProduct = 1.7;

        var result = obj.CalculaceTotal(inputCostProduct, inputQuantityProduct);

        Assert.That(result.Amount, Is.EqualTo(451.37));
    }

    private Product GetProduct()
    {
        string name = null;
        decimal price = 0m;
        double quantity = 0;
        PricingType pt = PricingType.PerItem;
        ProductCategory pc = ProductCategory.Bakery;
        PricingAdjustmentState pas = PricingAdjustmentState.None;

        return new Product(name, price, quantity, pt, pc, pas);
    }
}