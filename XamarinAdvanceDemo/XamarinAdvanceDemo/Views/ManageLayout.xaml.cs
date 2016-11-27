using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAdvanceDemo.Services;
using Xamarin.Forms;
using XamarinAdvanceDemo.Models;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace XamarinAdvanceDemo.Views
{
    public partial class ManageLayout : ContentPage
    {

        List<MSP> msps = new List<MSP>();
        AzureCloudService azure;
        public ManageLayout()
        {
            InitializeComponent();
            this.azure = new AzureCloudService();
            AddNew.WidthRequest = Device.OnPlatform(200, 250, 250);
            AddNew.HeightRequest = Device.OnPlatform(60, 80, 80);

            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            init();
            AddNew.Clicked += addpeople;

        }

        public async void addpeople(object sender, EventArgs e)
        {
            // === TEST ===
            MediaFile photo = await CrossMedia.Current.PickPhotoAsync();  //DEBUG
            AzureCloudService acs = new AzureCloudService();

            await acs.addPersion("TEST1", photo);
            UserDialogs.Instance.ShowSuccess("OK");
            // === END TEST ===

        }
        public async void init()
        {
            //await azure.GenerateRandomData();
            this.msps = await azure.CurrentClient.GetTable<MSP>().ToListAsync();
            peoplelist.ItemsSource = this.msps;
            //var peoples = await fi.getPeoples();
            //myfriends = new List<MSP>();

            //foreach (var people in peoples)
            //{  
            //    myfriends.Add(new Person { Name = people.Name, PicNum = people.PersistedFaceIds.Count().ToString() + " train picture." });
            //}


        }
    }
}
