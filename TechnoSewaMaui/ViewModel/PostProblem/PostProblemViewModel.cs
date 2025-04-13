using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using TechnoSewaMaui.Services.PostProblem;
using TechnoSewaMaui.ViewModel.Base;
using TechnoSewaMaui.Views.Home;

namespace TechnoSewaMaui.ViewModel.PostProblem
{
    [QueryProperty(nameof(SelectedLocation), "SelectedLocation")]
    public partial class PostProblemViewModel : BaseViewModel
    {
        public Command OnSubmitTapped { get; }
        public Command OnLocationTapped { get; }
        public Command OnAddPhotosTapped { get; }
        private ImageSource _pickedImage;

        public ImageSource PickedImage
        {
            get => _pickedImage;
            set
            {
                _pickedImage = value;
                OnPropertyChanged(nameof(PickedImage));

                OnPropertyChanged(nameof(IsImageSelected));
            }
        }
        public bool IsImageSelected => PickedImage != null;

        [ObservableProperty]
        public bool isImageNotSelected = true;

        [ObservableProperty]
        string selectedCategory;
        Location _selectedLocation;

        public Location SelectedLocation
        {
            get => _selectedLocation;
            set { SetProperty(ref _selectedLocation, value); }
        }

        public ObservableCollection<string> Categories { get; } = ["Plumbing", "Electrician"];

        [ObservableProperty]
        public string title;

        [ObservableProperty]
        public string description;

        private readonly PostProblemService _postProblemsService;

        public PostProblemViewModel(PostProblemService postProblemsService)
        {
            _postProblemsService = postProblemsService;
            OnSubmitTapped = new Command(async () => await SubmitTapped());
            OnLocationTapped = new Command(async () => await LocationTapped());

            OnAddPhotosTapped = new Command(async () => await AddPhotosTapped());
        }

        public async Task SubmitTapped()
        {
            if (string.IsNullOrWhiteSpace(Title) || string.IsNullOrWhiteSpace(Description))
            {
                await Shell.Current.DisplayAlert("Error", "Please fill all fields", "OK");
                return;
            }
            var result = await _postProblemsService.PostProblem(
                SelectedCategory,
                Title,
                Description,
                _selectedLocation.Latitude,
                _selectedLocation.Longitude,
                _imageData,
                PickedImageName
            );

            if (result != null)
            {
                await Shell.Current.DisplayAlert("Post Successfull", $"{result}", "Ok");
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Post Failed", "Failed", "Ok");
            }

            //goto problem service and send request to backend
        }

        public async Task LocationTapped()
        {
            await Shell.Current.GoToAsync(nameof(MapPage));
        }

        private byte[] _imageData;
        public string PickedImageName;

        public async Task AddPhotosTapped()
        {
            var result = await FilePicker.PickAsync(
                new PickOptions
                {
                    PickerTitle = "Pick Image",
                    FileTypes = FilePickerFileType.Images
                }
            );

            if (result != null)
            {
                PickedImageName = result.FileName;

                using (var stream = await result.OpenReadAsync())
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        await stream.CopyToAsync(memoryStream);
                        _imageData = memoryStream.ToArray();
                    }
                }
                PickedImage = ImageSource.FromStream(() => new MemoryStream(_imageData));
            }

            //opens the gallery and  can select an image
        }
    }
}
