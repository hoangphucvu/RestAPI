using KMS.TwitterClient.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;


namespace KMS.TwitterClient.Helper
{
    /// <summary>
    /// Provide method to get authorize user, get user timeline,
    /// post status and refresh to get new tweet
    /// </summary>
    public class TwitterAPI
    {
        /// <summary>
        /// User consumerkey
        /// </summary>
        private static string consumerKey = ConfigurationManager.AppSettings["ConsumerKey"].ToString();

        /// <summary>
        /// User consumer secret key
        /// </summary>
        private static string consumerSecret = ConfigurationManager.AppSettings["ConsumerSecret"].ToString();

        /// <summary>
        /// Access token provide permission for specific user
        /// </summary>
        private static string accessToken = ConfigurationManager.AppSettings["AccessToken"].ToString();

        /// <summary>
        /// Access token secret provide permission for specific user
        /// </summary>
        private static string accessTokenSecret = ConfigurationManager.AppSettings["AccessTokenSecret"].ToString();

        /// <summary>
        /// Encrypted method
        /// </summary>
        private static string oauthSignatureMethod = ConfigurationManager.AppSettings["OauthSignatureMethod"].ToString();

        /// <summary>
        /// Version authorize that API is using
        /// </summary>
        private static string oauthVersion = ConfigurationManager.AppSettings["OauthVersion"].ToString();

        /// <summary>
        /// API url to make get request for user timeline
        /// </summary>
        private static string userTimeline = ConfigurationManager.AppSettings["Twitter.UserTimeline"].ToString();

        /// <summary>
        /// Url for user to update tweet
        /// </summary>
        private static string newStatusUrl = ConfigurationManager.AppSettings["Twitter.UpdateTweet"].ToString();

        /// <summary>
        /// This variable is use to make sure that one request is not calling twice
        /// </summary>
        private string oauthNonce = Convert.ToBase64String(new ASCIIEncoding()
            .GetBytes(DateTime.Now.Ticks.ToString()));

        /// <summary>
        /// Create Signature for authorization
        /// </summary>
        /// <param name="url">API url to create signature string</param>
        /// <param name="oauthTimeStamp">oauthTimeStamp to currently time</param>
        /// <returns>Signature string</returns>
        public string CreateSignature(string url, string oauthTimeStamp , string method, string status = null)
        {
            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException("Url must not be null or empty");
            }

            if (string.IsNullOrEmpty(oauthTimeStamp))
            {
                throw new ArgumentNullException("OAuthTimeStamp must not be null or empty");
            }

            if (string.IsNullOrEmpty(method))
            {
                throw new ArgumentNullException("Method must not be null or empty");
            }

            try
            {
                //// append all the key value 
                var stringBuilder = new StringBuilder();
                if (method == "GET")
                {
                    stringBuilder.Append("GET&");
                    stringBuilder.Append(Uri.EscapeDataString(url));
                    stringBuilder.Append("&");
                }

                if (method == "POST")
                {
                    stringBuilder.Append("status=");
                    stringBuilder.Append(Uri.EscapeDataString(status));
                }

                ////sorted the key value 
                var dictionary = new SortedDictionary<string, string>
                {
                   { "oauth_version" , oauthVersion },
                   { "oauth_consumer_key", consumerKey },
                   { "oauth_nonce" , oauthNonce },
                   { "oauth_signature_method" , oauthSignatureMethod },
                   { "oauth_timestamp" , oauthTimeStamp },
                   { "oauth_token" , accessToken }
                };

                foreach (var keyValuePair in dictionary)
                {
                    stringBuilder.Append(Uri.EscapeDataString(string.Format("{0}={1}&", keyValuePair.Key, keyValuePair.Value)));
                }

                string signatureBaseString = stringBuilder.ToString().Substring(0, stringBuilder.Length - 3);

                ////generate signature key 
                string signatureKey =
                Uri.EscapeDataString(consumerSecret) + "&" +
                Uri.EscapeDataString(accessTokenSecret);

                var hmacsha1 = new HMACSHA1(new ASCIIEncoding().GetBytes(signatureKey));

                    ////hash the values
                string signatureString = Convert.ToBase64String(
                    hmacsha1.ComputeHash(
                        new ASCIIEncoding().GetBytes(signatureBaseString)));

                return signatureString;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Create authorize header then we can get list of user tweet
        /// </summary>
        /// <param name="signature">Signature String </param>
        /// <param name="timeStamp">The current time</param>
        /// <returns>authorization string</returns>
        public string CreateAuthorizationHeader(string signature, string timeStamp)
        {
            if (string.IsNullOrEmpty(signature))
            {
                throw new Exception("Singature is empty");
            }

            if (string.IsNullOrEmpty(timeStamp))
            {
                throw new Exception("TimeStamp is empty");
            }

            StringBuilder authorizationHeader = new StringBuilder();
            authorizationHeader = new StringBuilder("OAuth ");
            authorizationHeader.AppendFormat("oauth_consumer_key=\"{0}\",", Uri.EscapeDataString(consumerKey));
            authorizationHeader.AppendFormat("oauth_nonce=\"{0}\",", Uri.EscapeDataString(oauthNonce));
            authorizationHeader.AppendFormat("oauth_signature=\"{0}\",", Uri.EscapeDataString(signature));
            authorizationHeader.AppendFormat("oauth_signature_method=\"{0}\",", Uri.EscapeDataString(oauthSignatureMethod));
            authorizationHeader.AppendFormat("oauth_timestamp=\"{0}\",", Uri.EscapeDataString(timeStamp));
            authorizationHeader.AppendFormat("oauth_token=\"{0}\",", Uri.EscapeDataString(accessToken));
            authorizationHeader.AppendFormat("oauth_version=\"{0}\"", Uri.EscapeDataString(oauthVersion));
            return authorizationHeader.ToString();
        }

        /// <summary>
        /// Get client tweet on timeline using get request
        /// Convert from JSON to model 
        /// </summary>
        /// <returns>Return JSON data convert to object model</returns>
        public IList<TwitterModel> GetTweet()
        {
            try
            {
                var authorizeHeader= this.GetAuthorizeHeader("GET");
                //// Get user timeline
                WebRequest timeLineRequest = WebRequest.Create(userTimeline);
                timeLineRequest.Headers.Add("Authorization", authorizeHeader);
                timeLineRequest.Method = "GET";
                timeLineRequest.ContentType = "application/x-www-form-urlencoded";
                WebResponse timeLineResponse = timeLineRequest.GetResponse();
                var timeLineJson = string.Empty;
                List<TwitterModel> clientModel;

                using (timeLineResponse)
                {
                    using (var reader = new StreamReader(timeLineResponse.GetResponseStream()))
                    {
                        timeLineJson = reader.ReadToEnd();
                        clientModel = JsonConvert.DeserializeObject<List<TwitterModel>>(timeLineJson);
                    }
                }

                return clientModel;

            }
            catch (Exception)
            {
                throw;
            }


        }


        private string GetAuthorizeHeader(string method, string status = null )
        {
            TimeSpan timeSpan = DateTime.UtcNow -
            new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
            string oauthTimeStamp = Convert.ToInt64(timeSpan.TotalSeconds).ToString();
            var signature = this.CreateSignature(userTimeline, oauthTimeStamp , method ,status);
            var authorizeHeader = this.CreateAuthorizationHeader(signature, oauthTimeStamp);
            return authorizeHeader;
        }


        /// <summary>
        /// Post new tweet to user timeline using Post request
        /// </summary>
        /// <param name="content">Value of the post</param>
        /// <returns></returns>
        public WebResponse UpdateUserTweet(string status)
        {
            if (string.IsNullOrEmpty(status))
            {
                throw new Exception("Content is empty");
            }

            try
            {
                var authorizeHeader = this.GetAuthorizeHeader("POST", status);
                var postBody = "status=" + Uri.EscapeDataString(status);
                HttpWebRequest requestPostStatus = (HttpWebRequest)WebRequest.Create(newStatusUrl);
                requestPostStatus.Headers.Add("Authorization", authorizeHeader);
                requestPostStatus.Method = "POST";
                requestPostStatus.ContentType = "application/x-www-form-urlencoded";
                using (Stream stream = requestPostStatus.GetRequestStream())
                {
                    byte[] content = ASCIIEncoding.ASCII.GetBytes(postBody);
                    stream.Write(content, 0, content.Length);
                    WebResponse response = requestPostStatus.GetResponse();
                    return response;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}