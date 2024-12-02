using Android.OS;
using TechnoSewaMaui.ViewModel.Auth.Register;

namespace TechnoSewaMaui.Views.Auth.Register;

public partial class RegisterPage : ContentPage
{
    public RegisterPage(RegisterViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
