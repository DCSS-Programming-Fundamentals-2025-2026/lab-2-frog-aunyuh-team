using lab_1_frog_aunyuh_team.back.Domain.Core;
using lab_1_frog_aunyuh_team.back.Domain.GroceryCart;

namespace lab_1_frog_aunyuh_team.back.Domain.User;

public class User
{
    public LineBase[] lines = new LineBase[100];
    public Money Balance { get; set; } = new Money(0);
       
}