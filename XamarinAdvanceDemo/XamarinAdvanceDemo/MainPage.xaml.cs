using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinAdvanceDemo
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            test();
        }
        async void test()
        {
            await Navigation.PushAsync(new Views.ManageLayout(), true);  //open list page 
           // Cognitive.FaceIdentify fi = new Cognitive.FaceIdentify();
           // fi.checkGroupSetting();
        }
    }
}
