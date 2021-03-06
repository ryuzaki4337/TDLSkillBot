// <auto-generated>
// Code generated by luis:generate:cs
// Tool github: https://github.com/microsoft/botframework-cli
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;
namespace OfferRedeemBot.CognitiveModels
{
    public partial class OfferRedeem: IRecognizerConvert
    {
        [JsonProperty("text")]
        public string Text;

        [JsonProperty("alteredText")]
        public string AlteredText;

        public enum Intent {
            Myaccount_Update_Address,
            Myaccount_Update_DateOfBirth,
            Myaccount_Update_MobileNumber,
            Myaccount_Update_Name,
            None,
            Offer_Unable_To_Redeem,
            TCP_Loyality_Points
        };
        [JsonProperty("intents")]
        public Dictionary<Intent, IntentScore> Intents;

        public class _Entities
        {
            // Built-in entities
            public DateTimeSpec[] datetime;
            public string[] personName;

            // Lists
            public string[][] BrandType;
            public string[][] DateOfBirth;
            public string[][] NameType;

            // Regex entities
            public string[] ContactNumber;
            public string[] ProductID;
            public string[] otp;


            // Composites
            public class _InstanceContact
            {
                public InstanceData[] ContactType;
                public InstanceData[] PhoneNumber;
            }
            public class ContactClass
            {
                public string[] ContactType;
                public string[] PhoneNumber;
                [JsonProperty("$instance")]
                public _InstanceContact _instance;
            }
            public ContactClass[] Contact;

            // Instance
            public class _Instance
            {
                public InstanceData[] BrandType;
                public InstanceData[] Contact;
                public InstanceData[] ContactNumber;
                public InstanceData[] ContactType;
                public InstanceData[] DateOfBirth;
                public InstanceData[] NameType;
                public InstanceData[] PhoneNumber;
                public InstanceData[] ProductID;
                public InstanceData[] datetime;
                public InstanceData[] otp;
                public InstanceData[] personName;
            }
            [JsonProperty("$instance")]
            public _Instance _instance;
        }
        [JsonProperty("entities")]
        public _Entities Entities;

        [JsonExtensionData(ReadData = true, WriteData = true)]
        public IDictionary<string, object> Properties {get; set; }

        public void Convert(dynamic result)
        {
            var app = JsonConvert.DeserializeObject<OfferRedeem>(
                JsonConvert.SerializeObject(
                    result,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore, Error = OnError }
                )
            );
            Text = app.Text;
            AlteredText = app.AlteredText;
            Intents = app.Intents;
            Entities = app.Entities;
            Properties = app.Properties;
        }

        private static void OnError(object sender, ErrorEventArgs args)
        {
            // If needed, put your custom error logic here
            Console.WriteLine(args.ErrorContext.Error.Message);
            args.ErrorContext.Handled = true;
        }

        public (Intent intent, double score) TopIntent()
        {
            Intent maxIntent = Intent.None;
            var max = 0.0;
            foreach (var entry in Intents)
            {
                if (entry.Value.Score > max)
                {
                    maxIntent = entry.Key;
                    max = entry.Value.Score.Value;
                }
            }
            return (maxIntent, max);
        }
    }
}