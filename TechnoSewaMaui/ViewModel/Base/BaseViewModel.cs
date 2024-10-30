using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using IntelliJ.Lang.Annotations;

namespace TechnoSewaMaui.ViewModel.Base
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel() { }

        [ObservableProperty]
        bool isBusy;

        [ObservableProperty]
        string title;

        public bool IsNotBusy => IsBusy;

        [ObservableProperty]
        bool isFingerPrintEnabled;
    }
}
