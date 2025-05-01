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

        [ObservableProperty]
        private ImageSource pickedImage = null;
        [ObservableProperty]
        bool isImagePicked = false;
        [ObservableProperty]
        byte[] imageBytes = null;
        [ObservableProperty]
        string imageName = null;
        [ObservableProperty]
        bool isMsgWithImageSent = false;

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
                IsMsgWithImageSent = false;
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

        public async Task SendMessageWithImage(SendMessageWithImageModel request)
        {
            try
            {
                //IsSentByUser = false;
                if (!string.IsNullOrWhiteSpace(request.Question) && IsImagePicked == true)
                {
                    var response = await _chatbotService.FormatMessageWithImageService(request);
                    if (request != null)
                    {
                        Messages.Add(new ChatMessageModel
                        {
                            Question = response,
                            IsSentByUser = false,

                        });
                        //PickedImage = null;
                        
                        IsImagePicked = false;
                    }
                    else
                    {
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
        public async Task PickImage()
        {
            try
            {
                if (MediaPicker.Default.IsCaptureSupported)
                {
                    var file = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
                    {
                        Title = "Please select a photo"
                    });

                    if (file != null)
                    {
                        var stream = await file.OpenReadAsync();
                        using (var memoryStream = new MemoryStream())
                        {
                            await stream.CopyToAsync(memoryStream);
                            ImageBytes = memoryStream.ToArray();
                            PickedImage = ImageSource.FromStream(() => new MemoryStream(ImageBytes));
                        }
                         ImageName = file.FileName;
                     
                        IsImagePicked = true;

                        //using var ms = new MemoryStream();
                        //await stream.CopyToAsync(ms);
                        //byte[] imageBytes = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
            
                Console.WriteLine($"Error: {ex.Message}");
                IsImagePicked = false;
            }
        }




        [RelayCommand]
        public async void Send()
        {
            if (!string.IsNullOrWhiteSpace(MessageText))
            {
                //var userMessage = new ChatMessageModel
                //{
                //    Question = MessageText,
                //    IsSentByUser = true
                //};
                //Messages.Add(userMessage);

                if (IsImagePicked == false)
                {
                    var userMessage = new ChatMessageModel
                    {
                        Question = MessageText,
                        IsSentByUser = true
                    };
                    IsMsgWithImageSent = false;
                    Messages.Add(userMessage);
                    var sendMessageModel = new SendMessageModel()
                    {
                        Question = MessageText,

                    };
                    await SendMessage(sendMessageModel);

                }
                else
                {
                    var userMessage = new ChatMessageModel
                    {
                        Question = MessageText,
                        IsSentByUser = true,
                        ImageSource = PickedImage,
                    };
                    IsMsgWithImageSent = true;
                    Messages.Add(userMessage);
                    var sendMessageModel = new SendMessageWithImageModel()
                    {
                        Question = MessageText,
                        ImageFile = ImageBytes,
                        ImageName = ImageName,

                    };
                    await SendMessageWithImage(sendMessageModel);
                    //ImageBytes = null;
                    //ImageName = null;
                }
               

                MessageText = string.Empty;
            }

        }
    }
}
