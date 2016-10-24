using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XamarinAdvanceDemo.Models;

namespace XamarinAdvanceDemo.Services
{
    class AzureCloudService
    {
        IMobileServiceTable<MSP> mspTable;
        MobileServiceClient client;

        public AzureCloudService()
        {
            this.client = new MobileServiceClient(Constant.ApplicationURL);
            this.mspTable = client.GetTable<MSP>();
        }
       
        public MobileServiceClient CurrentClient
        {
            get { return client; }
        }
        public async void updateEmotion(String name, String emotion)
        {
            MSP per = (await mspTable.Where(p => p.Name == name).ToListAsync())[0];
            per.emotion = emotion;
            await mspTable.UpdateAsync(per);
        }
        public async Task GenerateRandomData()
        {
            List<MSP> resList = new List<MSP> {
                new MSP { Name = "EP", Title = "CEO", Description = "Chinese Name is Ying Ping. study in NTUST, nickname is EP", Image = "http://i.imgur.com/itGUmqim.jpg"},
                new MSP { Name = "Ann Lai", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/hAqvO2ym.jpg"},
                new MSP { Name = "Alice Hsu", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/I13ak7Lm.jpg"},
                new MSP { Name = "Becky Yang", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/JUjFM5gm.jpg"},
                new MSP { Name = "Aron Liu", Title = "CTO", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/a4ot7xhm.jpg"},
                new MSP { Name = "Justine Chan", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/PZREbeYm.jpg"},
                new MSP { Name = "Wiffer Lin", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/sgrOWP2m.jpg"},
                new MSP { Name = "Hsu che wei", Title = "Developer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/CT3Lv9qm.jpg"},
                new MSP { Name = "Jimmy Lu", Title = "Designer", Description = "ANNN ANN . I am Hung Ann Lai, study in NTUST, nickname is Jam", Image = "http://imgur.com/coTyYf6m.jpg"}
            };

            foreach (var res in resList)
            {
                await mspTable.InsertAsync(res);
            }
        }

        
    }
}
