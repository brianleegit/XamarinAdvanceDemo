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
            var groups = await client.ListPersonGroupsAsync();
            var exist = false;
            foreach (var group in groups)
            {
                if (group.PersonGroupId.Equals(Constant.DefaultGroupName))
                {
                    exist = true;
                    break;
                }
            }
            if(!exist)
            {
                await client.CreatePersonGroupAsync(Constant.DefaultGroupName, Constant.DefaultGroupName);
            }
        }

        public async Task<Microsoft.ProjectOxford.Face.Contract.Person[]> getPeoples()
        {
            return await client.GetPersonsAsync(Constant.DefaultGroupName);
        }

    }
}
