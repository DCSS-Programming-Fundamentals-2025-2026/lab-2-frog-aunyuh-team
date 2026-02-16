namespace lab_1_frog_aunyuh_team.back.Domain.Core;

public enum PricingType
{
    PerItem,
    PerKg
}

public enum ProductCategory
{
    Dairy,
    MeatAndFish,
    FruitsAndVegetables,
    Bakery,
    Grocery,
    Drinks,
    SweetsAndSnacks,
    HouseholdChemicals,
    Electronics
}

public enum PricingAdjustmentState
{
    None,
    Discounted,
    MarkedUp
}