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
            CoverImage.Source = ImageSource.FromUri(new Uri("https://scontent-tpe1-1.xx.fbcdn.net/v/t1.0-9/14492398_1288636481167411_4214539296251359956_n.png?oh=e0ec2d72b1e79a2f481a53d7149deda0&oe=586449C4"));
            FindBtn.Clicked += OnFindBtnClicked;
            ManageBtn.Clicked += ManageBtn_Clicked;
        }

        private async void ManageBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Views.ManageLayout(), true); 
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
                    Name = Guid.NewGuid().ToString()+".jpg"
                });
            }
            else
            {
                photo = await CrossMedia.Current.PickPhotoAsync();
            }
            if (photo != null)
            {
                //UserDialogs.Instance.ShowSuccess("Success !");
                CoverImage.Source = ImageSource.FromFile(photo.Path);                         
            }
        }
    }
}
