using TechnoSewaMaui.ViewModel.Notification;

namespace TechnoSewaMaui.Views.Notification;

public partial class NotificationPage : ContentPage
{
    public NotificationPage(NotificationViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
