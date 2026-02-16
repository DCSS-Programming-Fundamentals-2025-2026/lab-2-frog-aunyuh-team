using lab_1_frog_aunyuh_team.back.Domain.Core;

namespace Shop.Tests;

class QuantityTests
{
    [Test]
    public void Constructor_RoundToUpValue_ReturnsUpdateQuantityWithRoundToUp()
    {
        var input = 149.227;

        var result = new Quantity(input);

        Assert.That(result.Value, Is.EqualTo(149.23));
    }

    [Test]
    public void Constructor_RoundToDownValue_ReturnsUpdateQuantityWithRoundToDown()
    {
        var input = 149.224;

        var result = new Quantity(input);

        Assert.That(result.Value, Is.EqualTo(149.22));
    }

    [Test]
    public void ChangeValue_PositiveInputValue_ReturnsUpdateQuantity()
    {
        var baseQuantity = new Quantity(19);
        var input = 132;

        var result = baseQuantity.ChangeValue(input);

        Assert.That(result.Value, Is.EqualTo(-113));
    }

    [Test]
    public void ChangeValue_ZeroInputValue_ReturnsUpdateQuantity()
    {
        var baseQuantity = new Quantity(124);
        var input = 0;

        var result = baseQuantity.ChangeValue(input);

        Assert.That(result.Value, Is.EqualTo(124));
    }

    [Test]
    public void ChangeValue_NegativeInputValue_ReturnsUpdateQuantity()
    {
        var baseQuantity = new Quantity(90);
        var input = -10;

        var result = baseQuantity.ChangeValue(input);

        Assert.That(result.Value, Is.EqualTo(100));
    }
}