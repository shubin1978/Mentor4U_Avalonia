using System.Reactive;
using Avalonia.Controls.ApplicationLifetimes;
using DynamicData.Tests;
using Mentor4U_Avalonia.Components;
using Mentor4U_Avalonia.Views;
using ReactiveUI;
using AuthWindow = Mentor4U_Avalonia.Views.Windows.AuthWindow;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class StartWindowViewModel : ViewModelBase
{ 
    public void CloseWindow()
    {
        (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    public void Auth()
    {
        var window = new AuthWindow();
        window.Show();
        CloseWindow();
    }
    
}