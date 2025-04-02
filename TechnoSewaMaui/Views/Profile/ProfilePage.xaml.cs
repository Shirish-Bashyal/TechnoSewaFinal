using TechnoSewaMaui.ViewModel.Profile;

namespace TechnoSewaMaui.Views.Profile;

public partial class ProfilePage : ContentPage
{
    public ProfilePage(ProfileViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
