using KMS.TwitterClient.Models;
using System.Collections.Generic;
using System.Net;

namespace KMS.TwitterClient.API
{
    public interface ITwitterServices
    {
        IList<TwitterModel> GetTweet();

        WebResponse UpdateUserTweet(string status);
    }
}