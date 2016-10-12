using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinAdvanceDemo.Models;
using Acr.UserDialogs;
using Windows.UI.Xaml.Controls;

namespace XamarinAdvanceDemo.Views
{
    public partial class ManageLayout : ContentPage
    {
        Cognitive.FaceIdentify fi;
        List<Friends> myfrineds = new List<Friends>();
        public ManageLayout()
        {
            InitializeComponent();
            fi = new Cognitive.FaceIdentify();
            fi.checkGroupSetting();

            init();
            AddNew.Clicked += addpeople;
   
        }
    
        public async void addpeople(object sender, EventArgs e)
        {
            PromptConfig pc = new PromptConfig();
            pc.Title = "Enter User Name";
         
            PromptResult re = await UserDialogs.Instance.PromptAsync(pc);
            if (await fi.AddPerson(re.Text))
            {
                UserDialogs.Instance.ShowSuccess("Create Successful.");
                init();
            }
            else
                UserDialogs.Instance.ShowError("Create Fail.");

        }
        public async void init()
        {
           
            var peoples = await fi.getPeoples();
            myfrineds = new List<Friends>();
            
            foreach (var people in peoples)
            {  
                myfrineds.Add(new Friends { Name = people.Name, PicNum = people.PersistedFaceIds.Count().ToString() + " train picture." });
            }

            peoplelist.ItemsSource = myfrineds;
        }
    }
}
