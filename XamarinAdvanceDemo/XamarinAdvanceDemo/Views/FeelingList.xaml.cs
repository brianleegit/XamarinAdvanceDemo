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
                String lastString = (last.TotalMinutes < 60) ? ((int)last.TotalMinutes).ToString() + " mins ago" : msp.updatedAt.ToString("M/d h:mm tt");
                
                msp.emotion = eomToString(msp.emotion) + " last online "  + lastString;
                
            }
            feelingshow.ItemsSource = msps;
        }
        String eomToString(String emo)
        {
            String re ="";
            switch(emo)
            {
                case "Anger":
                    re = "😡";
                    break;
                case "Contempt":
                    re = "😒";
                    break;
                case "Disgust": 
                    re = "😧";
                    break;
                case "Fear":
                    re = "😖";
                    break;
                case "Happiness":
                    re = "😃";
                    break;
                case "Neutral":
                    re = "😐";
                    break;
                case "Sadness":
                    re = "😥";
                    break;
                case "Surprise":
                    re = "😱";
                    break;
            }
            return re;
        }
    }
}
