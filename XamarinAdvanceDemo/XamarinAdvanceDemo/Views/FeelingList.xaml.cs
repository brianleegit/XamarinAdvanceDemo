using Acr.UserDialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinAdvanceDemo.Models;
using XamarinAdvanceDemo.Services;

namespace XamarinAdvanceDemo.Views
{
    public partial class FeelingList : ContentPage
    {
        public FeelingList(String name)
        {
            InitializeComponent();
            this.Title = "Hello " +  name;
            UserDialogs.Instance.ShowLoading("Loading person", MaskType.Black);
            init();
            UserDialogs.Instance.HideLoading();
        }
        async void init()
        {
            AzureCloudService azure =  new AzureCloudService(); ;
            List<MSP> msps = await azure.CurrentClient.GetTable<MSP>().ToListAsync();
            foreach (var msp in msps)
            {
                TimeSpan last = DateTime.Now - msp.updatedAt;
                msp.emotion = msp.emotion + " last online "  + ((int)last.TotalMinutes).ToString() + " mins ago";
            }
            feelingshow.ItemsSource = msps;
        }
    }
}
