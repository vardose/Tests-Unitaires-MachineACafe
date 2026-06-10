namespace MachineACafé.Test;

public class CoinTest
{
    [Theory]
    [InlineData(1)]
    [InlineData(2)]
    [InlineData(5)]
    [InlineData(10)]
    [InlineData(20)]
    [InlineData(50)]
    [InlineData(100)]
    [InlineData(200)]
    public void ValidCoinValues(ushort valueInCents)
    {
        Assert.Equal(valueInCents, new Coin(valueInCents).ValueInCents);
    }

    [Theory]
    [InlineData(0)]
    [InlineData(3)]
    [InlineData(4)]
    [InlineData(6)]
    [InlineData(9)]
    [InlineData(11)]
    [InlineData(19)]
    [InlineData(21)]
    [InlineData(49)]
    [InlineData(51)]
    [InlineData(99)]
    [InlineData(101)]
    [InlineData(199)]
    [InlineData(201)]
    [InlineData(ushort.MaxValue)]
    public void InvalidCoinValues(ushort valueInCents)
    {
        Assert.Throws<ArgumentOutOfRangeException>(() => new Coin(valueInCents));
    }
}