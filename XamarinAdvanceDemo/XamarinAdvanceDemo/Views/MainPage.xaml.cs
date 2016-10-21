using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
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
          /*  if (CrossMedia.Current.IsCameraAvailable && CrossMedia.Current.IsTakePhotoSupported )
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

            }*/
            photo = await CrossMedia.Current.PickPhotoAsync();  //DEBUG
            if (photo != null)
            {
                //UserDialogs.Instance.ShowSuccess("Success !");
                CoverImage.Source = ImageSource.FromFile(photo.Path);
                try
                {
                    UserDialogs.Instance.ShowLoading("Login...");
                
                    //Login
                    FaceServiceClient fc = new FaceServiceClient(Constant.FaceApiKey);
                    var faceResult = await fc.DetectAsync(photo.GetStream());
                    if (faceResult.Length != 1)
                    {
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowError(faceResult.Length + " face detected.");
                        return;
                    }
                    Guid[] ids = new Guid[1];
                    ids[0] = faceResult[0].FaceId;

                    var identyResult = (await fc.IdentifyAsync(Constant.DefaultGroupName, ids))[0].Candidates;

                    if (identyResult.Length != 1)
                    {
                        UserDialogs.Instance.HideLoading();
                        UserDialogs.Instance.ShowError("login failed.");
                        return;
                    }

                    var personID = identyResult[0].PersonId;
                    var persondetail = await fc.GetPersonAsync(Constant.DefaultGroupName, personID);
                    UserDialogs.Instance.ShowLoading("Detecting emotion...");
                    //Update Emotion database, should have only one face in photo
                    EmotionServiceClient ec = new EmotionServiceClient(Constant.EmotionApiKey);
                    Emotion[] emotionResult = await ec.RecognizeAsync(photo.GetStream());
                    var emotionlisit = emotionResult[0].Scores.ToRankedList().ToList();


                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.Alert(persondetail.Name + "\n" + emotionlisit[0].Key);

                }
                catch(Exception ex)
                {
                    UserDialogs.Instance.HideLoading();
                    UserDialogs.Instance.ShowError(ex.ToString());
                }
            }
        }
    }
}
