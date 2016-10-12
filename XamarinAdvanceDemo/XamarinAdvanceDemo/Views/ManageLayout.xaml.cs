using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using XamarinAdvanceDemo.Models;

namespace XamarinAdvanceDemo.Views
{
    public partial class ManageLayout : ContentPage
    {
        public ManageLayout()
        {
            InitializeComponent();
            init();
        }
        public async void init()
        {
            Cognitive.FaceIdentify fi = new Cognitive.FaceIdentify();
            var peoples = await fi.getPeoples();
            var myfrineds = new List<Friends>();
            
            foreach (var people in peoples)
            {  
                myfrineds.Add(new Friends { Name = people.Name,PersonID = people.PersonId.ToString() });
            }

            peoplelist.ItemsSource = myfrineds;
        }
    }
}
