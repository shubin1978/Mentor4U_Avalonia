using System;
using System.Collections.ObjectModel;
using System.Reactive;
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
    public InputControlViewModel InputSearch { get; set; } = new()
    {
        Label = "Search",
        Watermark = "Найти",
        IsFloatingWatermark = false
    };

    public ObservableCollection<Models.Role> RolesCollection { get; set; } = new();
    public ReactiveCommand<Unit, Unit> SearchCommand { get; }
    public ReactiveCommand<Unit, Unit> ViewAllCommand { get; }

    public Roles()
    {
        var canExecuteSearchCommand = this.WhenAnyValue(
            p => p.InputSearch.Input,
            (p) => !string.IsNullOrWhiteSpace(p));
        SearchCommand = ReactiveCommand.CreateFromTask(
            execute: async () =>
            {
                try
                {
                    var input  = InputSearch.Input;
                    var roles = new BLL.Roles(new DAL.Roles(CONNECTION_STRING));

                    Models.Role? role;
                    if (int.TryParse(input, out var id))
                    {
                        role = await roles.GetByIdAsync(id);
                    }
                    else
                    {
                        role = await roles.GetByNameAsync(input);
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
                    var roles = new BLL.Roles(new DAL.Roles(CONNECTION_STRING));
                    var collection  = await roles.GetAllAsync();
                    RolesCollection.Clear();
                    RolesCollection.AddRange(collection);
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
    }
}