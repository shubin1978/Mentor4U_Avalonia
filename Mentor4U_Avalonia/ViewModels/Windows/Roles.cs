using System;
using System.Collections.ObjectModel;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia;
using DynamicData;
using Mentor4U_Avalonia.Components;
using Mentor4U_Avalonia.Components.ViewModel;
using Mentor4U_Avalonia.Models;
using ReactiveUI;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class Roles : ViewModelBase
{
    private const string CONNECTION_STRING = "User ID=postgres;Password=1234580;Host=localhost;Port=5432;" +
                                             "Database=mentor_db;Pooling=true;SearchPath=test";

    private readonly BLL.Roles _roles;

    private Role? _selectedRole;

    public Roles()
    {
        _roles = new BLL.Roles(new DAL.Roles(CONNECTION_STRING));

        var canExecuteSearchCommand = this.WhenAnyValue(
            p => p.InputSearch.InputComponent.Input,
            p => !string.IsNullOrWhiteSpace(p));
        var canExecuteNewCommand = this.WhenAnyValue(
            p => p.InputNew.InputComponent.Input,
            p => !string.IsNullOrWhiteSpace(p));

        InputSearch.Command = ReactiveCommand.CreateFromTask(
            async () =>
            {
                try
                {
                    var input = InputSearch.InputComponent.Input;

                    Role? role;
                    if (int.TryParse(input, out var id))
                        role = await _roles.GetByIdAsync(id);
                    else
                        role = await _roles.GetByNameAsync(input);

                    await ShowInfo($"{role.Id} - {role.RoleName}");
                }
                catch (Exception e)
                {
                    await ShowError(e.Message);
                }
            },
            canExecuteSearchCommand);
        InputNew.Command = ReactiveCommand.CreateFromTask(
            async () =>
            {
                try
                {
                    var input = InputNew.InputComponent.Input;

                    var role = new Role
                    {
                        RoleName = input!
                    };
                    var result = await _roles.CreateAsync(role);
                    if (result is not null)
                    {
                        await ShowInfo($"{result.Id} - {result.RoleName}");

                        await UpdateRolesCollectionsyncAsync();
                    }
                    else
                    {
                        await ShowInfo($"Роль с именем {role.RoleName} не создалась");
                    }
                }
                catch (Exception e)
                {
                    await ShowError(e.Message);
                }
            },
            canExecuteNewCommand);


        ViewAllCommand = ReactiveCommand.CreateFromTask(
            async () =>
            {
                try
                {
                    await UpdateRolesCollectionsyncAsync();
                }
                catch (Exception e)
                {
                    await ShowError(e.Message);
                }
            }
        );

        DeleteCommand = ReactiveCommand.CreateFromTask<Role, Unit>(
            async role =>
            {
                try
                {
                    await _roles.DeleteAsync(role);
                    await UpdateRolesCollectionsyncAsync();
                }
                catch (Exception e)
                {
                    await ShowError(e.Message);
                }

                return default;
            });
    }

    public InputControlWithButtonViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark = "Найти",
        IsFloatingWatermark = false,
        Button = "Поиск"
    };

    public InputControlWithButtonViewModel InputNew { get; set; } = new()
    {
        Label = "New",
        Watermark = "Add new role",
        IsFloatingWatermark = false,
        Button = "Save"
    };

    public ObservableCollection<Role> RolesCollection { get; set; } = new();

    public Role SelectedRole
    {
        get => _selectedRole;
        set => this.RaiseAndSetIfChanged(ref _selectedRole, value);
    }

    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }
    public ReactiveCommand<Role, Unit> DeleteCommand { get; }

    private async Task UpdateRolesCollectionsyncAsync()
    {
        var collection = await _roles.GetAllAsync();
        RolesCollection.Clear();
        RolesCollection.AddRange(collection);
    }

    private static async Task ShowError(string message)
    {
        await MessageBox.ShowError(
            $"{Application.Current.Resources["AppTitle"]} - Roles",
            message);
    }

    private static async Task ShowInfo(string message)
    {
        await MessageBox.ShowInfo(
            $"{Application.Current.Resources["AppTitle"]} - Roles",
            message);
    }
}
