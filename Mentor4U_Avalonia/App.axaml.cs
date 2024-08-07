using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Mentor4U_Avalonia.ViewModels.Windows;
using Mentor4U_Avalonia.Views.Windows;

namespace Mentor4U_Avalonia;

public class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new StartWindow();
            {
                DataContext = new StartWindowViewModel();
            }
            ;
        }

        base.OnFrameworkInitializationCompleted();
    }
}
