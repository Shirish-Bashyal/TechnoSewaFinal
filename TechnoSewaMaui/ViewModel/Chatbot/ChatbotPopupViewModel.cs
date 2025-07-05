using Android.Media;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Plugin.Maui.Audio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnoSewaMaui.Model;
using TechnoSewaMaui.Services.Chatbot;
using TechnoSewaMaui.ViewModel.Base;
using Xamarin.KotlinX.Coroutines.Stream;
using AudioManager = Plugin.Maui.Audio.AudioManager;

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
        [ObservableProperty]
        private IAudioSource? recordedAudioSource;
        [ObservableProperty]
        private bool isRecordingMode = false;
        [ObservableProperty]
        private bool isAudioPreviewVisible = false;
        [ObservableProperty]
        private bool isAudioPlaying = false;
        [ObservableProperty]
        string audioFileName;
 

        private readonly ChatbotService _chatbotService;
        private readonly IAudioManager _audioManager;
        private readonly IAudioRecorder _audioRecorder;

        public ObservableCollection<ChatMessageModel> Messages { get; } = new();
        public ChatbotPopupViewModel(ChatbotService chatbotService, IAudioManager audioManager)
        {
            _chatbotService = chatbotService;
            _audioManager = audioManager;
            _audioRecorder = audioManager.CreateRecorder();
        }

        [RelayCommand]
        public async Task SelectAudio()
        {
            
        var result = await Shell.Current.DisplayActionSheet("ActionSheet: AUDIO?", "Cancel", null, "Upload", "Record");
        if (result != null)
        {
            if (result == "Upload")
            {
                await UploadAudio();
            }
            else
            {
                await RecordAudio();
            }
        }
           
        }

        [RelayCommand]
        public async Task RecordAudio()
        {
            IsRecordingMode = false;

            if (await Permissions.RequestAsync<Permissions.Microphone>() != PermissionStatus.Granted)
            {
                // TODO Inform your user
                return;
            }

            if (!_audioRecorder.IsRecording)
            {
                IsRecordingMode = true;
               
                await _audioRecorder.StartAsync();
            }
            else
            {
                var recordedAudio = await _audioRecorder.StopAsync();
                //var audioStream = recordedAudio.GetAudioStream();
                AudioFileName = "recordings.wav";
                RecordedAudioSource = recordedAudio;
                IsRecordingMode = false;
                IsAudioPreviewVisible = true;

                var player = AudioManager.Current.CreatePlayer(recordedAudio.GetAudioStream());
                player.Play();
            }
        }

        public async Task UploadAudio()
        {
            // customn file type for audios
            var customFileType = new FilePickerFileType(new Dictionary<DevicePlatform, IEnumerable<string>>
            {
            { DevicePlatform.iOS, new[] { "public.audio" } },     
            { DevicePlatform.Android, new[] { "audio/*" } },      
            { DevicePlatform.WinUI, new[] { ".mp3", ".wav" } },  
            { DevicePlatform.MacCatalyst, new[] { "public.audio" } }
            });
            var result = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Pick Audio",
                FileTypes = customFileType
            });
            if (result != null)
            {
                IsAudioPreviewVisible = true;
                var stream = await result.OpenReadAsync();
                if (stream != null)
                {
                    RecordedAudioSource = new FileAudioSource(result.FullPath);

                 
                    AudioFileName = Path.GetFileName(result.FullPath);
                }
            }
           
        }

        [RelayCommand]
        public async Task PlayChatAudio(IAudioSource audioSource)
        {
          

            var player = AudioManager.Current.CreatePlayer(audioSource.GetAudioStream());
            player.Play();

            
        }
        [RelayCommand]
        public async Task PlayAudio()
        {
            IsAudioPlaying = true;
          
                var player = AudioManager.Current.CreatePlayer(RecordedAudioSource.GetAudioStream());
                player.Play();
      
            IsAudioPlaying = false;
        }

        [RelayCommand]
        public async Task ClearAudio()
        {
            if (RecordedAudioSource != null)
            {
                RecordedAudioSource = null;
                IsAudioPreviewVisible = false;
            }
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
                            IsChat = true

                        });
                    }
                    else {
                        Messages.Add(new ChatMessageModel
                        {
                            Question = "Oops! Something went wrong",
                            IsSentByUser = false,
                            IsChat = true
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
                            IsChat = true

                        });
                        //PickedImage = null;
                        
                        IsImagePicked = false;
                    }
                    else
                    {
                        Messages.Add(new ChatMessageModel
                        {
                            Question = "Oops! Something went wrong",
                            IsSentByUser = false,
                            IsChat = true
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
        public async Task SendAudio()
        {
            if (RecordedAudioSource != null && AudioFileName != null)
            {

             
                var userMessage = new ChatMessageModel
                {
                    AudioFile = RecordedAudioSource,
                    IsSentByUser = true,
                    IsChat = false,
                };
                Messages.Add(userMessage);
               userMessage = null;
                Console.WriteLine($"Messages count before: {Messages.Count}");
                var audioStream = RecordedAudioSource.GetAudioStream(); // Assuming GetAudioStream() is available
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    await  audioStream.CopyToAsync(memoryStream);
                    byte[] audioBytes = memoryStream.ToArray();

                    var result =await _chatbotService.FormatAudioService(audioBytes, AudioFileName);
                    if (result != null)
                    {
                       
                        userMessage = new ChatMessageModel()
                        {
                            IsSentByUser = false,
                            Question = result,
                            IsChat = true
                        };
                        Messages.Add(userMessage);
                        Console.WriteLine($"Messages count before: {Messages.Count}");
                    }
                }
             
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
                        IsSentByUser = true,
                        IsChat = true
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
                        IsChat = true
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
