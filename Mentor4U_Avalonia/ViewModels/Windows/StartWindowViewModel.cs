using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Mentor4U_Avalonia.Views.Windows;

namespace Mentor4U_Avalonia.ViewModels.Windows;

public class StartWindowViewModel : ViewModelBase
{
    public void CloseWindow()
    {
        (Application.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    public void Auth()
    {
        var window = new AuthWindow();
        window.Show();
        CloseWindow();
    }
}
