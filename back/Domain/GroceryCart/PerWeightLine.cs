using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;

namespace lab_1_frog_aunyuh_team.back.Domain.GroceryCart;

public class PerWeightLine : LineBase
{
    public PerWeightLine(Product product, double qnt) : base(product, qnt) { }

    public override Money CalculaceTotal (decimal cost, double qnt)
    {
        return new Money(cost * (decimal)qnt);
    }
}