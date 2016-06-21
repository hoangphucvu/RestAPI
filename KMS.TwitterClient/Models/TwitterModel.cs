using Newtonsoft.Json;
using System;
using System.Globalization;

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

        public DateTime Time
        {
            get
            {
                return DateTime.ParseExact(CreatedAt, "ddd MMM dd HH:mm:ss +ffff yyyy", CultureInfo.InvariantCulture);
            }
        }
    }
}