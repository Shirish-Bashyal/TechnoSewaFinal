using TechnoSewaMaui.ViewModel.PostProblem;

namespace TechnoSewaMaui.Views.PostProblem;

public partial class PostProblemPage : ContentPage
{
    public PostProblemPage(PostProblemViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
