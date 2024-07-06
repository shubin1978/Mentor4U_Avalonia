using System.Threading.Tasks.Dataflow;
using Mentor4U_Avalonia.BLL;
using Mentor4U_Avalonia.Models;

namespace TestProject1;

public class RolesTest
{
    private const string CONNECTION_STRING_GOOD = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                                  "Database=mentor_db;Pooling=true;SearchPath=test";
    private const string ROLE_NAME = "guest";
    private const string ROLE_NAME_BAD = "admin";

    [Fact]
    public async Task GetById_PositiveTest()
    {
        // TODO Implement
        Assert.True(true);
    }
    [Fact]
    public async Task GetById_NegativeTest()
    {
        // TODO Implement
        Assert.False(false);
    }
    
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

    [Fact]

    public async Task GetAllAsync_positiveTest()
    {
        //TODO Implement
        Assert.True(true);
    }

    [Fact]
    public async Task GetAllAsync_NegativeTest()
    {
        //TODO Implement
        Assert.False(false);
    }

    [Fact]
    public async Task CreateAndDeleteAsync_PositiveTest()
    {
        var roles = new Roles(new Mentor4U_Avalonia.DAL.Roles(CONNECTION_STRING_GOOD));
        var role = new Role() { RoleName = ROLE_NAME };
        
        Role? roleNew = await roles.CreateAsync(role);
        Assert.NotNull(roleNew);
        
        var result  = await roles.DeleteAsync(roleNew);
        Assert.True(result);
    }
    [Fact]
    public async Task CreateAsync_NegativeTest()
    {
        var roles = new Roles(new Mentor4U_Avalonia.DAL.Roles(CONNECTION_STRING_GOOD));
        var role = new Role() { RoleName = ROLE_NAME_BAD };
        
        var roleNew = await roles.CreateAsync(role);
        Assert.Null(roleNew);
    }

    [Fact]
    public async Task DeleteAsync_PositiveTest()
    {
        
        
    }
    [Fact]
    public async Task DeleteAsync_NegativeTest()
    {
        
        
    }
}