using TechnoSewaMaui.ViewModel.Search;

namespace TechnoSewaMaui.Views.Search;

public partial class SearchPage : ContentPage
{
    public SearchPage(SearchViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
