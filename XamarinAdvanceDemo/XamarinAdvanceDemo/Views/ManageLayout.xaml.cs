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
using Windows.UI.Xaml.Controls;

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
            //AddNew.Clicked += addpeople;
   
        }
    
        //public async void addpeople(object sender, EventArgs e)
        //{
        //    PromptConfig pc = new PromptConfig();
        //    pc.Title = "Enter User Name";
         
        //    PromptResult re = await UserDialogs.Instance.PromptAsync(pc);
        //    if (await fi.AddPerson(re.Text))
        //    {
        //        UserDialogs.Instance.ShowSuccess("Create Successful.");
        //        init();
        //    }
        //    else
        //        UserDialogs.Instance.ShowError("Create Fail.");

        //}
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
