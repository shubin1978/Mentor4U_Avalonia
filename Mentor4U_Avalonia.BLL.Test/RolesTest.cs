using System.Threading.Tasks.Dataflow;
using Mentor4U_Avalonia.BLL;
using Mentor4U_Avalonia.Models;

namespace TestProject1;

public class RolesTest
{
    private const string CONNECTION_STRING_GOOD = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                                  "Database=mentor_db;Pooling=true;SearchPath=test";
    
    [Fact]
    public async Task GetByNameAsync_positiveTest()
    {
        var roles = new Roles(new Mentor4U_Avalonia.DAL.Roles(CONNECTION_STRING_GOOD));
        var actual = await roles.GetByNameAsync("admin");
        var expected = new Role()
        {
            Id = 1,
            RoleName = "admin"
        };
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public async Task GetByNameAsync_NegativeTest()
    {
        var roles  = new Roles(new Mentor4U_Avalonia.DAL.Roles(CONNECTION_STRING_GOOD));
        var actual  = await roles.GetByNameAsync("admin1");
        Assert.Null(actual);
    }
}