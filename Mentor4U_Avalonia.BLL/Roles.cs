using Logger;
using Logger.File;
using Mentor4U_Avalonia.DAL;
using Mentor4U_Avalonia.Models;

namespace Mentor4U_Avalonia.BLL;

public class Roles
{
    private readonly ICrud<Role> _data;
    private readonly ILogger _logger;

    public Roles(ICrud<Role> data)
    {
        _data = data;

        _logger = new LogToFile();
    }

    public async Task<Role?> GetByIdAsync(int id)
    {
        try
        {
            return await _data.GetAsync(id);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module:{nameof(Roles)}. Method:{GetByIdAsync}. Message:{e.Message}");
            throw;
        }
    }

    public async Task<Role?> GetByNameAsync(string name)
    {
        try
        {
            return (await _data.GetAllAsync() ?? Array.Empty<Role>())
                .SingleOrDefault(r => r.RoleName == name);
        }
        catch (Exception e)
        {
            _logger?.Error($"Module:{nameof(Roles)}. Method:{GetByNameAsync}. Message:{e.Message}");
            throw;
        }
    }

    public async Task<IEnumerable<Role>?> GetAllAsync()
    {
        try
        {
            return await _data.GetAllAsync();
        }
        catch (Exception e)
        {
            _logger?.Error($"Module:{nameof(Roles)}. Method:{GetAllAsync}. Message:{e.Message}");
            throw;
        }
    }

    public async Task<Role?> CreateAsync(Role role)
    {
        try
        {
            var result = await _data.InsertAsync(role);
            if (!result) return null;
            var newRole = await GetByNameAsync(role.RoleName);
            return newRole;
        }
        catch (Exception e)
        {
            _logger?.Error($"Module:{nameof(Roles)}. Method:{CreateAsync}. Message:{e.Message}");
            return null; //TODO Add Exception Handling
        }
    }

    public async Task<bool> DeleteAsync(Role role)
    {
        try
        {
            var result = await _data.DeleteAsync(role.Id);
            return result;
        }
        catch (Exception e)
        {
            _logger?.Error($"Module:{nameof(Roles)}. Method:{nameof(DeleteAsync)}. Message:{e.Message}");
            return false; //TODO Add Exception Handling
        }
    }
}
