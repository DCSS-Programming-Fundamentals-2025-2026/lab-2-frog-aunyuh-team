using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.User;

namespace lab_1_frog_aunyuh_team.back.Domain.GroceryCart;

public abstract class LineBase : IPriceable
{
    public Product Product { get; set; }
    public Money Total { get; set; }
    public Quantity Quantity { get; set; }

    public LineBase(Product product, double qnt)
    {
        Product = product;
        Quantity = new Quantity(qnt);
    }

    public abstract Money CalculaceTotal(decimal cost, double qnt);
}