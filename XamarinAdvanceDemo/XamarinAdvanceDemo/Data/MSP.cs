using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XamarinAdvanceDemo.Models
{
    public class MSP
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get;  set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get;  set; } = String.Empty;
        [JsonProperty(PropertyName = "picUrl")]
        public string Image { get; set; } = String.Empty;
        [JsonIgnore]
        public Uri PicUrl { get { return new Uri(Image); }  }
        [JsonProperty(PropertyName = "description")]
        public string Description { get;  set; } = String.Empty;
        [JsonProperty(PropertyName = "title")]
        public string Title { get;  set; } = String.Empty;
        [Microsoft.WindowsAzure.MobileServices.Version]
        public string Version { get; set; } = string.Empty;
        [JsonProperty(PropertyName = "emotion")]
        public string emotion { get; set; } = String.Empty;
        [JsonProperty(PropertyName = "lastonline")]
        public string lastonline { get; set; } = String.Empty;
        [Microsoft.WindowsAzure.MobileServices.UpdatedAt]
        public DateTime updatedAt { get; set; }

    }
}
