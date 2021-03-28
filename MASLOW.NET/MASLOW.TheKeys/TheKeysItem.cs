using MASLOW.Entities;
using MASLOW.Entities.Items;
using MASLOW.Entities.Privileges;
using MASLOW.Entities.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web;

namespace MASLOW.TheKeys
{
    public class TheKeysItem : Item
    {

        public override IEnumerable<string> Actions => new List<string>()
        {
            "Open",
            "Close"
        };

        public override IEnumerable<string> Values => new List<string>()
        {
            "IsOpen"
        };

        public override string GetValue(string value)
        {
            return "The Keys";
        }

        public override bool DoAction(string action, Dictionary<string, string>? payload, IUser user)
        {
            switch (action)
            {
                case "Open" :
                    return CallApi(GetToken(), "remote_open");
                case "Close":
                    return CallApi(GetToken(), "remote_close");
                default :
                    return false;
            }
        }

        private string GetToken()
        {
            var url = "https://api.the-keys.fr/api/login_check";
            
            var formDictionary = new Dictionary<string, string>();
            formDictionary.Add("_format", "json");
            formDictionary.Add("_username", "+33665387559");
            formDictionary.Add("_password", "a89f5909be");

            var client = new HttpClient();
            var data = new FormUrlEncodedContent(formDictionary);

            var request = client.PostAsync(url, data);
            request.Wait();
           
            var response = request.Result.Content.ReadAsStringAsync();
            response.Wait();

            var result = JsonConvert.DeserializeObject<TheKeysLoginModel>(response.Result);

            return result.access_token;
        }

        private bool CallApi(string token, string action)
        {
            var id = "7619";
            var url = $"https://api.the-keys.fr/fr/api/v2/serrure/{action}/{id}";

            var formDictionary = new Dictionary<string, string>();
            formDictionary.Add("_format", "json");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var data = new FormUrlEncodedContent(formDictionary);

            var request = client.PostAsync(url, data);
            request.Wait();

            return request.Result.StatusCode == HttpStatusCode.OK;
        }
    }
}
