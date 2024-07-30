using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class RolesTest
{
    private const string CONNECTION_STRING = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                             "Database=mentor_db;Pooling=true;SearchPath=test";

    private const string ROLE_NAME = "guest";
    private const string ROLE_NAME_BAD = "admin";

    [Fact]
    public async Task GetAsync_PositiveTest()
    {
        var roles = new Roles(CONNECTION_STRING);
        var actual = await roles.GetAsync(1); // актуальные данные
        var expected = new Role() //ожидаемые данные
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

    [Fact]
    public async Task GetAllAsync_PositiveTest()
    {
        //TODO: Implement
        Assert.True(true);
    }

    [Fact]
    public async Task GetAllAsync_NegativeTest()
    {
        //TODO: Implement
        Assert.False(false);
    }


    [Fact]
    public async Task InsertAndDeleteAsync_PositiveTest()
    {
        var role = new Role { RoleName = ROLE_NAME };
        var roles = new Roles(CONNECTION_STRING);
        var result = await roles.InsertAsync(role);
        Assert.True(result);

        var id = await GetIdTestRoleAsync();
        Assert.NotNull(id);

        result = await roles.DeleteAsync(id.Value);
        Assert.True(result);
    }

    [Fact]
    public async Task InsertAsync_NegativeTest()
    {
        /*var role = new Role() { RoleName = ROLE_NAME_BAD };
        var roles = new Roles(CONNECTION_STRING);
        var result = await roles.InsertAsync(role);
        Assert.False(result);*/

        // TODO: Implement
        Assert.False(false);
    }

    private static async Task<int?> GetIdTestRoleAsync()
    {
        var roles = new Roles(CONNECTION_STRING);
        var res = await roles.GetAllAsync();
        return res?.SingleOrDefault(r => r.RoleName == ROLE_NAME)?.Id;
    }
}
