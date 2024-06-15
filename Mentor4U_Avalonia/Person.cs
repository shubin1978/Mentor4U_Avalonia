using CommunityToolkit.Mvvm.ComponentModel;

namespace Mentor4U_Avalonia;

public class Person : ObservableObject
{
    private int _id;

    public int Id
    {
        get => _id;
        set => SetProperty(ref _id, value);
    }
    
    private string _lastName;

    public string LastName
    {
        get => _lastName; 
        set => SetProperty(ref _lastName, value);
    }
    private string _firstName;

    public string FirstName
    {
        get => _firstName; 
        set => SetProperty(ref _firstName, value);
    }

    private string _direction;

    public string Direction
    {
        get => _direction; 
        set => SetProperty(ref _direction, value);
    }
    
    private bool _isMentor = true;

    public bool IsMentor
    {
        get => _isMentor; 
        set => SetProperty(ref _isMentor, value);
    } 
}