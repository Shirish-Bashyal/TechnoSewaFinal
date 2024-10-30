using TechnoSewaMaui.ViewModel.Auth.Register;

namespace TechnoSewaMaui.Views.Auth.Register;

public partial class PhoneNumberPage : ContentPage
{
    public PhoneNumberPage(PhoneNumberPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
