using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL.Test;

public class DbServiceTest
{
    private const string CONNECTION_STRING_GOOD = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                              "Database=mentor_db;Pooling=true;SearchPath=test";

    private const string CONNECTION_STRING_EMPTY = "";
    [Fact]
    public async Task ConnectAsync_PositiveTest_CONNECTION_STRING_GOOD()
    {
      var result = await Record.ExceptionAsync(async  () => 
                     { await DbService<Role>.ConnectAsync(CONNECTION_STRING_GOOD);});
      Assert.Null(result);
    }

    [Fact]
    public async Task ConnectAsync_NegativeTest_CONNECTION_STRING_EMPTY()
    {
        await Assert.ThrowsAsync<EmptyStringException>(async   ()  => 
                      { await DbService<Role>.ConnectAsync(CONNECTION_STRING_EMPTY);});
    }
}