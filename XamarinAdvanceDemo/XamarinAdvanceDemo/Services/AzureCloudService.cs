﻿using Acr.UserDialogs;
using Microsoft.ProjectOxford.Face;
using Microsoft.WindowsAzure.MobileServices;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.Net.Http;
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

        public IMobileServiceTable<MSP> CurrentTable
        {
            get { return mspTable; }
        }
        public async void updateEmotion(String id, String emotion)
        {
            try
            {
                IMobileServiceTableQuery<MSP> query = this.mspTable.Where(p => p.Personid == id); //different name from azure table and face API
                List<MSP> per = await query.ToListAsync();
                if (per.Count > 0)
                {
                    per[0].emotion = emotion;
                    await mspTable.UpdateAsync(per[0]);
                }
                else
                {
                    UserDialogs.Instance.Toast("Can't find your data");
                }

            }
            catch (Exception e)
            {

                UserDialogs.Instance.Toast("Unable to update your emotion");
            }
        }
        public async Task addPerson(String name, String picUrl = "", String title = "", String description = "")
        {

            FaceServiceClient fc = new FaceServiceClient(Constant.FaceApiKey);
            // create persion and get persion id -> add photo -> train
            var id = (await fc.CreatePersonAsync(Constant.DefaultGroupName, name)).PersonId;
            await fc.AddPersonFaceAsync(Constant.DefaultGroupName, id, picUrl);
            await fc.TrainPersonGroupAsync(Constant.DefaultGroupName);
            // register to azure
            MSP data = new MSP { Name = name, Title = title, Description = description, Personid = id.ToString() , Image = picUrl , emotion = "Happiness" };
            await mspTable.InsertAsync(data);
        }
        /*  
            upload to msp azure blob account
            server code: https://github.com/oscar60310/mspimg
        */
        public async Task<String> uploadimg(MediaFile photo)
        {
        
            var client = new HttpClient();
            byte[] b = new byte[photo.GetStream().Length];
            await photo.GetStream().ReadAsync(b, 0, b.Length);
            var message = await client.PostAsync("https://msp11.azurewebsites.net/image", new ByteArrayContent(b));
            var recall = await message.Content.ReadAsStringAsync();
            return recall;
        }
      
        /*
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
        }*/


    }
}
