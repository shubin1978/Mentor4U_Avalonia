using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class RolesTest
{
    private const string CONNECTION_STRING = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                                  "Database=mentor_db;Pooling=true;SearchPath=test";
    
    [Fact]
    public async Task GetAsync_PositiveTest()
    {
        var roles = new Roles(CONNECTION_STRING);
        var actual = await roles.GetAsync(1); // актуальные данные
        var expected= new Role() //ожидаемые данные
        {
            Id = 1,
            RoleName = "admin"
        };
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task GetAsync_NegativeTest()
    {
        var roles = new Roles(CONNECTION_STRING);
        var actual = await roles.GetAsync(0); // актуальные данные
        
        Assert.Null(actual);
    }
}