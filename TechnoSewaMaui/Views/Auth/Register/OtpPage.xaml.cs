using TechnoSewaMaui.ViewModel.Auth.Register;

namespace TechnoSewaMaui.Views.Auth.Register;

public partial class OtpPage : ContentPage
{
    public OtpPage(OtpViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
