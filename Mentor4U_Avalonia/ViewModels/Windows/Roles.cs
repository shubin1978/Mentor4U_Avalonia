using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using DynamicData;
using Mentor4U_Avalonia.Models;
using Mentor4U_Avalonia.ViewModels.Controls;
using MsBox.Avalonia;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class Roles : ViewModelBase
{
    private const string CONNECTION_STRING = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                                  "Database=mentor_db;Pooling=true;SearchPath=test";

    private readonly BLL.Roles _roles;
    public InputControlViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark = "Найти",
        IsFloatingWatermark = false
    };
    
    public InputControlViewModel InputNew { get; set; } = new()
    {
        Label = "New",
        Watermark = "Add new role",
        IsFloatingWatermark = false
    };

    public ObservableCollection<Models.Role> RolesCollection { get; set; } = new();
    
    private Models.Role? _selectedRole;
    public Models.Role SelectedRole
    {
        get => _selectedRole;
        set => this.RaiseAndSetIfChanged( ref _selectedRole, value); 
    }
    public ReactiveCommand<Unit, Unit> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }
    public ReactiveCommand<Unit, Unit> NewCommand { get; }
    public ReactiveCommand<Role, Unit> DeleteCommand { get; }

    public Roles()
    {
        _roles = new BLL.Roles(new DAL.Roles(CONNECTION_STRING));
        
        var canExecuteSearchCommand = this.WhenAnyValue(
            p => p.InputSearch.Input,
            (p) => !string.IsNullOrWhiteSpace(p));
        var canExecuteNewCommand = this.WhenAnyValue(
            p => p.InputNew.Input,
            (p) => !string.IsNullOrWhiteSpace(p));
        
        SearchCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input  = InputSearch.Input;

                    Models.Role? role;
                    if (int.TryParse(input, out var id))
                    {
                        role = await _roles.GetByIdAsync(id);
                    }
                    else
                    {
                        role = await _roles.GetByNameAsync(input);
                    }
                    var box = MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{role.Id} - {role.RoleName}");
                    await box.ShowAsync();
                }
                catch (Exception e)
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{e.Message}");
                    await box.ShowAsync();
                }
                
            },
            canExecute: canExecuteSearchCommand);

        ViewAllCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    await UpdateRolesCollectionsyncAsync();
                }
                catch (Exception e)
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{e.Message}");
                    await box.ShowAsync();
                }   
            }
            );
        NewCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input  = InputNew.Input;

                    var role = new Models.Role()
                    {
                        RoleName = input!
                    };
                    var result = await _roles.CreateAsync(role);
                    if (result is not null)
                    {
                        var box = MessageBoxManager
                            .GetMessageBoxStandard(
                                title: $"{App.Current.Resources["AppTitle"]} - Roles",
                                text: $"{result.Id} - {result.RoleName}");
                        await box.ShowAsync();

                        await UpdateRolesCollectionsyncAsync();
                    }
                    else
                    {
                        var box = MessageBoxManager
                                                .GetMessageBoxStandard(
                                                    title: $"{App.Current.Resources["AppTitle"]} - Roles",
                                                    text: $"Роль с именем {role.RoleName} не создалась");
                                            await box.ShowAsync();
                    }
                }
                catch (Exception e)
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{e.Message}");
                    await box.ShowAsync();
                }
                
            },
            canExecute: canExecuteNewCommand);
        DeleteCommand = ReactiveCommand.CreateFromTask<Role, Unit>(
            execute: async (role) =>
            {
                try
                {
                    await _roles.DeleteAsync(role);
                    await UpdateRolesCollectionsyncAsync(); 
                }
                catch (Exception e)
                {
                    var box = MessageBoxManager
                        .GetMessageBoxStandard(
                            title: $"{App.Current.Resources["AppTitle"]} - Roles",
                            text: $"{e.Message}");
                    await box.ShowAsync(); 
                }

                return default;
            });
    }

    private async Task UpdateRolesCollectionsyncAsync()
    {
        var collection  = await _roles.GetAllAsync();
        RolesCollection.Clear();
        RolesCollection.AddRange(collection);
    }
}