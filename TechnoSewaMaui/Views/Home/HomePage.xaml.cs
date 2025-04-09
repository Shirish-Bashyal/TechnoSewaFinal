using TechnoSewaMaui.ViewModel.Home;

namespace TechnoSewaMaui.Views.Home;

public partial class HomePage : ContentPage
{
    public HomePage(HomeViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private bool _isFirstLoad = true;

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (_isFirstLoad && BindingContext is HomeViewModel viewModel)
        {
            _isFirstLoad = false;
            if (viewModel.OnPageMount.CanExecute(null))
            {
                viewModel.OnPageMount.Execute(null);
            }
        }
    }
}
