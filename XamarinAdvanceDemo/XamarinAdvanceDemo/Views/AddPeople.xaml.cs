using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using XamarinAdvanceDemo.Models;
using XamarinAdvanceDemo.Services;
using Acr.UserDialogs;
using Plugin.Media;
using Plugin.Media.Abstractions;

namespace XamarinAdvanceDemo.Views
{
    public partial class AddPeople : ContentPage
    {
        public AddPeople()
        {
            InitializeComponent();
            AddBtn.Clicked += OnAddBtnClicked;
        }
        public async void OnAddBtnClicked(object sender, EventArgs e)
        {
            
            AzureCloudService acs = new AzureCloudService();

            UserDialogs.Instance.ShowLoading("Loading", MaskType.Black);
            await acs.addPerson(name.Text, picUrl.Text, title.Text, description.Text);
            UserDialogs.Instance.HideLoading();

            UserDialogs.Instance.ShowSuccess("Person Added");
            await Navigation.PushAsync(new Views.ManageLayout(), true);
            
        }
    }
}
