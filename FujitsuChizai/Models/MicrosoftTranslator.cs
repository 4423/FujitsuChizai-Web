using FujitsuChizai.Properties;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Xml;

namespace FujitsuChizai.Models
{
    public class MicrosoftTranslator : ITranslator
    {
        private AdmAccessToken admToken;
        private AdmAuthentication admAuth;
        private DateTime tokenAcquisitionDate;
        private bool isTokenExpire
            => DateTime.Now > tokenAcquisitionDate + TimeSpan.FromMinutes(9);
        private string accessHeader;

        public List<string> SupportedLanguageCode { get; private set; }

        public MicrosoftTranslator()
        {
            admAuth = new AdmAuthentication(Resources.ClientId, Resources.ClientSecret);
            tokenAcquisitionDate = new DateTime(1970, 1, 1); // isTokenExpire が false に初期化される値
            SupportedLanguageCode = GetSupportedLanguageCode();
        }
        

        private string GetAccessHeader()
        {
            tokenAcquisitionDate = DateTime.Now;
            admToken = admAuth.GetAccessToken();
            return "Bearer " + admToken.access_token;
        }

        private List<string> GetSupportedLanguageCode()
        {
            if (isTokenExpire) accessHeader = GetAccessHeader();

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/GetLanguagesForTranslate";
            WebRequest webRequest = WebRequest.Create(uri);
            webRequest.Headers.Add("Authorization", accessHeader);

            using (WebResponse response = webRequest.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    var dcs = new DataContractSerializer(typeof(List<string>));
                    return dcs.ReadObject(stream) as List<string>;
                }
            }
        }

        public string Translate(string text, string languageCodeForTranslate = "en")
        {
            if (isTokenExpire) accessHeader = GetAccessHeader();

            string url = String.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text={0}&to={1}", HttpUtility.UrlEncode(text), languageCodeForTranslate);
            WebRequest translationWebRequest = WebRequest.Create(url);
            translationWebRequest.Headers.Add("Authorization", accessHeader);
            
            using (WebResponse response = translationWebRequest.GetResponse())
            {
                using (Stream stream = response.GetResponseStream())
                {
                    var serializer = new DataContractSerializer(Type.GetType("System.String"));
                    return serializer.ReadObject(stream) as string;
                }
            }
        }


        [DataContract]
        private class AdmAccessToken
        {
            [DataMember]
            public string access_token { get; set; }
            [DataMember]
            public string token_type { get; set; }
            [DataMember]
            public string expires_in { get; set; }
            [DataMember]
            public string scope { get; set; }
        }


        private class AdmAuthentication
        {
            private static readonly string TranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
            private string clientId;
            private string clientSecret;
            private string requestDetails;
            
            public AdmAuthentication(string clientId, string clientSecret)
            {
                this.clientId = clientId;
                this.clientSecret = clientSecret;
                this.requestDetails = String.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", 
                    HttpUtility.UrlEncode(clientId), HttpUtility.UrlEncode(clientSecret));
            }


            public AdmAccessToken GetAccessToken()
            {
                WebRequest webRequest = WebRequest.Create(TranslatorAccessURI);
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";

                byte[] bytes = Encoding.ASCII.GetBytes(requestDetails);
                webRequest.ContentLength = bytes.Length;

                using (Stream outputStream = webRequest.GetRequestStream())
                {
                    outputStream.Write(bytes, 0, bytes.Length);
                }

                using (WebResponse webResponse = webRequest.GetResponse())
                {
                    var serializer = new DataContractJsonSerializer(typeof(AdmAccessToken));
                    return serializer.ReadObject(webResponse.GetResponseStream()) as AdmAccessToken;
                }
            }
        }
    }
}