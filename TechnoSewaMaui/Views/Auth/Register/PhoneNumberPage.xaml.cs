using TechnoSewaMaui.ViewModel.Auth.Register;

namespace TechnoSewaMaui.Views.Auth.Register;

public partial class PhoneNumberPage : ContentPage
{
    public PhoneNumberPage(PhoneNumberViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
