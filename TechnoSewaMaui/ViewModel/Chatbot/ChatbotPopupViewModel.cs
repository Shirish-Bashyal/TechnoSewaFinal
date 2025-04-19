using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Services.Chatbot;
using TechnoSewaMaui.ViewModel.Base;

namespace TechnoSewaMaui.ViewModel.Chatbot
{
   public partial class ChatbotPopupViewModel : BaseViewModel
    {
        [ObservableProperty]
        private string messageText;
        
        private readonly ChatbotService _chatbotService;

        public ObservableCollection<ChatMessageModel> Messages { get; } = new();
        public ChatbotPopupViewModel(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        public async Task SendMessage(SendMessageModel message)
        {
            try {
                //IsSentByUser = false;
                if (!string.IsNullOrWhiteSpace(message.Question))
                {
                    var request = await _chatbotService.FormatMessageService(message);
                    if (request != null)
                    {
                        Messages.Add(new ChatMessageModel
                        {
                            Question =  request,
                            IsSentByUser = false,

                        });
                    }
                    else {
                        Messages.Add(new ChatMessageModel
                        {
                            Question = "Oops! Something went wrong",
                            IsSentByUser = false
                        });
                    }
                }
                
            }
            catch { }
            
        }


        [RelayCommand]
        public async void Send()
        {
            if (!string.IsNullOrWhiteSpace(MessageText))
            {
                var userMessage = new ChatMessageModel
                {
                    Question = MessageText,
                    IsSentByUser = true
                };
                Messages.Add(userMessage);


                var sendMessageModel = new SendMessageModel()
                { 
                    Question = MessageText,
                
                };
                await SendMessage(sendMessageModel);


               

                MessageText = string.Empty;
            }

        }
    }
}
