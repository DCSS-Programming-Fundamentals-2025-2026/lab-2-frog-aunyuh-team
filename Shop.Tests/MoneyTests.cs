using lab_1_frog_aunyuh_team.back.Domain.Core;

namespace Shop.Tests;

public class MoneyTests
{
    [Test]
    public void AddBalance_NegativeAmount_ThrowsException()
    {
        var money = new Money();
        var input = -14m;
        var ex = false;

        try
        {
            money.AddBalance(input);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    [Test]
    public void AddBalance_ZeroAmount_ThrowsException()
    {
        var money = new Money();
        var input = 0m;
        var ex = false;

        try
        {
            money.AddBalance(input);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    [Test]
    public void AddBalance_PositiveDecimalAmount_ReturnsUpdateMoney()
    {
        var money = new Money();
        var input = 129.4m;

        var result = money.AddBalance(input);

        Assert.That(result.Amount, Is.EqualTo(129.4m));
    }

    [Test]
    public void Constructor_RoundAmountToUp_ReturnsUpdateMoneyWithRoundAmountToUp()
    {
        var input = 123.135213123123m;

        var result = new Money(input);

        Assert.That(result.Amount, Is.EqualTo(123.14m));
    }

    [Test]
    public void Constructor_RoundAmountToDown_ReturnsUpdateMoneyWithRoundAmountToDown()
    {
        var input = 123.134223423432423443213123123m;

        var result = new Money(input);

        Assert.That(result.Amount, Is.EqualTo(123.13m));
    }

    [Test]
    public void PayProducts_PriceGreaterThanAmount_ThrowsException()
    {
        var money = new Money(14m);
        var input = 14.01m;
        var ex = false;

        try
        {
            money.PayProducts(input);
        }
        catch (InvalidOperationException)
        {
            ex = true;
        }

        Assert.That(ex, Is.True);
    }

    [Test]
    public void PayProducts_LessAmount_ReturnsUpdateMoney()
    {
        var money = new Money(28m);
        var input = 11m;

        var result = money.PayProducts(input);

        Assert.That(result.Amount, Is.EqualTo(17m));
    }

    [Test]
    public void PayProducts_NegativeAmount_ReturnsUpdateMoney()
    {
        var money = new Money(28m); 
        var input = -12m; 

        var result = money.PayProducts(input); 

        Assert.That(result.Amount, Is.EqualTo(40m));
    }

    [Test]
    public void PayProducts_ZeroAmount_ReturnsUpdateMoney()
    {
        var money = new Money(28m);
        var input = 0m;

        var result = money.PayProducts(input);

        Assert.That(result.Amount, Is.EqualTo(28m));
    }

    [Test]
    public void PayProducts_EqualsAmount_ReturnsUpdateMoney()
    {
        var money = new Money(17m);
        var input = 17m;

        var result = money.PayProducts(input);

        Assert.That(result.Amount, Is.EqualTo(0m));
    }
}