using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace  OfferRedeemBot.API
{
    public static class GetOffersAPI
    {
        //private readonly string baseUrl = "....";

        public static List<string> GetOffersCustomerId(string customerId)
        {
            string baseUrl = "";
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create(baseUrl + "/" + customerId);
            webRequest.Proxy.Credentials = CredentialCache.DefaultCredentials;
            webRequest.UserAgent = "Awesome-Octocat-App";
            webRequest.UseDefaultCredentials = true;
            webRequest.Method = "GET";
            HttpWebResponse httpWebResponse = (HttpWebResponse)webRequest.GetResponse();

            var offersList = new List<string>();

            string resJSON = "";
            using (Stream stream = httpWebResponse.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                resJSON = streamReader.ReadToEnd();
                streamReader.Close();
            }

            JToken jToken = JToken.Parse(resJSON);

            var jsonArrayLength = ((Newtonsoft.Json.Linq.JArray)jToken).Count;

            for (int i = 0; i < jsonArrayLength; i++)
            {
                offersList.Add((string)jToken[i]["offerName"]);
            }

            return offersList;
        }
        
    }


}