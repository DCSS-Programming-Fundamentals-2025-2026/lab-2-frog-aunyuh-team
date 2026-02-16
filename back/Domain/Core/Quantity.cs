namespace lab_1_frog_aunyuh_team.back.Domain.Core;

public struct Quantity
{
    public double Value { get; }
    
    public Quantity (double data)
    {
        Value = Math.Round(data, 2);
    }

    public Quantity ChangeValue (double data)
    {
        return new Quantity(Value - data);
    }
}