using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XamarinAdvanceDemo.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            FindBtn.Clicked += OnFindBtnClicked;
        }

        public async void OnFindBtnClicked(object sender, EventArgs e)
        {
            //use media plugin
            await CrossMedia.Current.Initialize();
            MediaFile photo;
            if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported)
            {
                photo = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
                {
                    Directory = "MSP",
                    Name = "MSPPhoto.jpg"
                });
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync();
            }
            if (photo != null)
            {
                if (Device.OS != TargetPlatform.Windows)
                {
                    UserDialogs.Instance.ShowSuccess("Success !");
                }
                else
                {
                    await DisplayAlert("Success", "Picture　Taken", "OK");
                }
                CoverImage.Source = ImageSource.FromFile(photo.Path);

            }
        }
    }
}
