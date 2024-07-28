using MsBox.Avalonia;
using MsBox.Avalonia.Enums;

namespace Mentor4U_Avalonia.Components;

public static class MessageBox
{
    public static async Task ShowInfo(string title, string text)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard(
                title: title,
                text: text,
                ButtonEnum.Ok,
                Icon.Info);
        await box.ShowAsync();
    }
    public static async Task ShowError(string title, string text)
    {
        var box = MessageBoxManager
            .GetMessageBoxStandard(
                title: title,
                text: text,
                ButtonEnum.Ok,
                Icon.Error);
        await box.ShowAsync();
    }

    public static void ShowInfo()
    {
        throw new NotImplementedException();
    }
}