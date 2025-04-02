using TechnoSewaMaui.ViewModel.Home;

namespace TechnoSewaMaui.Views.Home;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (Navigation != null && Navigation.NavigationStack.Count > 0)
        {
            NavigationPage.SetHasNavigationBar(this, false);
        }
    }
}
