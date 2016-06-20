using Newtonsoft.Json;
using System.Web.Mvc;

namespace KMS.TwitterClient.Models
{
    /// <summary>
    /// Store Client properties
    /// </summary>
    public class TwitterModel
    {
        [JsonProperty(PropertyName = "created_at")]
        public string CreatedAt { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Status { get; set; }

        public UserModel User { get; set; }
    }
}