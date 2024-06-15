using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.DAL;

public class DbHelper
{
    public static Dictionary<Type, string> TableNames = new Dictionary<Type, string>()
    {
        {typeof(Role), "table_roles"}
    };
}