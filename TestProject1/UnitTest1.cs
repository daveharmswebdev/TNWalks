namespace TestProject1;

[Collection("DatabaseCollection")]
public class UnitTest1
{
    private readonly DbContextFixture _fixture;

    public UnitTest1(DbContextFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public async Task Test1()
    {
        var result = await _fixture.AddressComponent.GetAddresses();
        
        Assert.NotNull(result);
        Assert.Equal(2, result.Count);
    }

     [Fact]
     public async Task GetAddressById_ValidId_ReturnsAddressModel()
     {
         var result = await _fixture.AddressComponent.GetAddressById(1);
         
         Assert.NotNull(result);
         Assert.Equal("123 Street", result.Line1);
     }
}