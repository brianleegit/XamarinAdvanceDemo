using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face;

namespace XamarinAdvanceDemo.Cognitive
{
    class FaceIdentify
    {
        FaceServiceClient client;
        public FaceIdentify()
        {
            client = new FaceServiceClient(Constant.FaceApiKey);
        }
        public string GetFaceID()
        {

            return "";
        }

        // check whether the dedault group is created, if not create it.
        public async void checkGroupSetting()
        {
            Boolean found = true;
            try
            {
                var exist = await client.GetPersonGroupAsync(Constant.DefaultGroupName);
            }
            catch (Exception e)
            {
                found = false;
            }

            if (!found)
            {            
                await client.CreatePersonGroupAsync(Constant.DefaultGroupName, Constant.DefaultGroupName);
            }
        }

        public async Task<Microsoft.ProjectOxford.Face.Contract.Person[]> getPeoples()
        {
            return await client.GetPersonsAsync(Constant.DefaultGroupName);
        }

        public async Task<bool> AddPerson(string name)
        {
            try
            {
                await client.CreatePersonAsync(Constant.DefaultGroupName, name);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
