using CommunityToolkit.Maui.Views;
using TechnoSewaMaui.ViewModel.Chatbot;

namespace TechnoSewaMaui.Views.Chatbot;

public partial class ChatBotPopup : Popup
{
	public ChatBotPopup(ChatbotPopupViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
	public async void OnNoButtonClicked(object? sender, EventArgs e)
    {
        var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
        await CloseAsync(false, cts.Token);
    }
}