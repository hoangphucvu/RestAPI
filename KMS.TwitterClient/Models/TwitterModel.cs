using System;
using System.Collections.Generic;
using System.Web;

namespace KMS.TwitterClient.Models
{ 
    /// <summary>
    /// Store Client properties
    /// </summary>
    public class TwitterModel
    {
        public string ID { get; set; }
        public string CreatedAt { get; set; }
        /// <summary>
        /// Tweet Content
        /// </summary>
        public string Text { get; set; }
        public UserModel User { get; set; }
    }
}