using CommunityToolkit.Mvvm.ComponentModel;

namespace Mentor4U_Avalonia;

public class Person : ObservableObject
{
    private string _direction;
    private string _firstName;
    private int _id;

    private bool _isMentor = true;

    private string _lastName;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }

    public string LastName
    {
        get => _lastName;
        set => SetProperty(ref _lastName, value);
    }

    public string FirstName
    {
        get => _firstName;
        set => SetProperty(ref _firstName, value);
    }

    public string Direction
    {
        get => _direction;
        set => SetProperty(ref _direction, value);
    }

    public bool IsMentor
    {
        get => _isMentor;
        set => SetProperty(ref _isMentor, value);
    }
}
