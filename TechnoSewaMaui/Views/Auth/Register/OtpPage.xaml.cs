using TechnoSewaMaui.ViewModel.Auth.Register;

namespace TechnoSewaMaui.Views.Auth.Register;

public partial class OtpPage : ContentPage
{
    public OtpPage(OtpPageViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
