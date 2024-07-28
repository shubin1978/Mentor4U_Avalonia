using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Input;
using Avalonia.Interactivity;

namespace Mentor4U_Avalonia.Views.Windows;

public partial class StartWindow : Window
{
    public StartWindow()
    {
        InitializeComponent();
    }

    private void ButtonClose_OnClick(object? sender, RoutedEventArgs e)
    {
        (App.Current.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime).MainWindow.Close();
    }

    private void Auth(object? sender, RoutedEventArgs e)
    {
        var window = new AuthWindow();
        window.Show();
        Close();
    }
    private bool _mouseDownForWindowMoving = false;
    private PointerPoint _originalPoint;

    private void Window_OnPointerMoved(object? sender, PointerEventArgs e)
    {
        if (!_mouseDownForWindowMoving) return;

        PointerPoint currentPoint = e.GetCurrentPoint(this);
        Position = new PixelPoint(Position.X + (int)(currentPoint.Position.X - _originalPoint.Position.X),
            Position.Y + (int)(currentPoint.Position.Y - _originalPoint.Position.Y));
    }

    private void Window_OnPointerPressed(object? sender, PointerPressedEventArgs e)
    {
        if (WindowState == WindowState.Maximized || WindowState == WindowState.FullScreen) return;

        _mouseDownForWindowMoving = true;
        _originalPoint = e.GetCurrentPoint(this);
    }

    private void Window_OnPointerReleased(object? sender, PointerReleasedEventArgs e)
    {
        _mouseDownForWindowMoving = false;
    }
}