using TechnoSewaMaui.ViewModel.Auth.SignIn;

namespace TechnoSewaMaui.Views.Auth.SignIn;

public partial class SigninPage : ContentPage
{
    public SigninPage(SignInViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
