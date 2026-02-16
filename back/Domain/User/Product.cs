using lab_1_frog_aunyuh_team.back.Domain.Core;

namespace lab_1_frog_aunyuh_team.back.Domain.User;

public class Product
{
    public string Name { get; set; }
    public Money CurrentPrice { get; set; }
    public Money BasePrice { get; set; }
    public Quantity QuantityLeft { get; set; }
    public Quantity StockQuantity { get; set; }
    public PricingType PricingType { get; set; }
    public ProductCategory ProductCategory { get; set; }
    public PricingAdjustmentState PricingAdjustmentState { get; set; }

    public Product (string name, decimal price, double qnt, PricingType pt, ProductCategory pc, PricingAdjustmentState pas)
    {
        Name = name;
        CurrentPrice = new Money(price);
        BasePrice = new Money(price);
        QuantityLeft = new Quantity(qnt);
        StockQuantity = new Quantity(qnt);
        PricingType = pt;
        ProductCategory = pc;
        PricingAdjustmentState = pas;
    }

    public bool CanTake (double qnt)
    {
        if (qnt > QuantityLeft.Value)
            return false;

        if (PricingType == PricingType.PerKg)
        {
            if (qnt < 0.14)
                return false;
        }
        else if (PricingType == PricingType.PerItem)
        {
            int temp = (int)qnt;
            if (qnt < 1 || qnt != temp)
                return false;
        }


        return true;
    }
}