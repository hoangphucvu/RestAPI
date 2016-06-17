using System;
using System.Collections.Generic;
using System.Web;

namespace KMS.TwitterClient.Models
{ 
    /// <summary>
    /// Client properties
    /// </summary>
    public class TwitterModel
    {
        /// <summary>
        /// User ID
        /// </summary>
        public string ID { get; set; }

        /// <summary>
        /// Time user post the tweet
        /// </summary>
        public string CreatedAt { get; set; }

        /// <summary>
        /// Content of the tweet
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// User class
        /// </summary>
        public UserModel User { get; set; }
    }
}