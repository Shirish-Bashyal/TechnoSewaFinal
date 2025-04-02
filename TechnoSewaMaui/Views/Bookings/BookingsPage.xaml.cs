using TechnoSewaMaui.ViewModel.Bookings;

namespace TechnoSewaMaui.Views.Bookings;

public partial class BookingsPage : ContentPage
{
    public BookingsPage(BookingsViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
