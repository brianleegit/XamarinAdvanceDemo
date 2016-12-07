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
        public FeelingList(string name)
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
                string lastString = (last.TotalMinutes < 60) ? ((int)last.TotalMinutes).ToString() + " mins ago" : msp.updatedAt.ToString("M/d h:mm tt");
                msp.emotionImg = eomToString(msp.emotion);
                msp.emotion = " last online "  + lastString;
            }
            feelingshow.ItemsSource = msps;
        }
        string eomToString(string emo)
        {

            string re ="";
            switch(emo)
            {
                case "Anger":
                    re = "anger.png";
                    break;
                case "Contempt":
                    re = "contempt.png";
                    break;
                case "Disgust":  
                    re = "disgust.png";
                    break;
                case "Fear":
                    re = "fear.png";
                    break;
                case "Happiness":
                    re = "happiness.png";
                    break;
                case "Neutral":
                    re = "neutral.png";
                    break;
                case "Sadness":
                    re = "sadness.png";
                    break;
                case "Surprise":
                    re = "surprise.png";
                    break;
            }
            //re = "surprise.png";
            return re;
        }
    }
}
