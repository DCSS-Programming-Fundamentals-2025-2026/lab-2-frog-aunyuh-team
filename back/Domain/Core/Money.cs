namespace lab_1_frog_aunyuh_team.back.Domain.Core;

public struct Money
{
    public decimal Amount { get; }
    public Money(decimal data)
    {
        Amount = Math.Round(data, 2);
    }

    public Money AddBalance (decimal data)
    {
        if (data <= 0)
            throw new InvalidOperationException();

        return new Money(Amount + data);
    }

    public Money PayProducts (decimal price)
    {
        if (Amount < price)
            throw new InvalidOperationException();

        return new Money(Amount - price);
    }
}