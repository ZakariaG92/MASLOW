using MASLOW.Entities;
using MASLOW.Entities.Entities.Items;
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

        public override List<string> Actions => new List<string>()
        {
            "Open",
            "Close"
        };

        public override List<string> Values => new List<string>()
        {
            "IsOpen"
        };

        public override Dictionary<string, DataType> ExpectedPayload => new Dictionary<string, DataType>()
        {
            { "login", DataType.STRING },
            { "password", DataType.STRING },
            { "locker_id", DataType.INTEGER }
        };

        public override string GetValue(string value)
        {
            if(value == "IsOpen")
            {
                return CallGet(GetToken(), "get");
            }

            return "";
        }

        public override bool DoAction(string action, Dictionary<string, string>? payload, IUser user)
        {
            switch (action)
            {
                case "Open" :
                    return CallPost(GetToken(), "remote_open");
                case "Close":
                    return CallPost(GetToken(), "remote_close");
                default :
                    return false;
            }
        }

        private string GetToken()
        {
            var url = "https://api.the-keys.fr/api/login_check";
            
            var formDictionary = new Dictionary<string, string>();
            formDictionary.Add("_format", "json");
            formDictionary.Add("_username", Payload["login"]);
            formDictionary.Add("_password", Payload["password"]);

            var client = new HttpClient();
            var data = new FormUrlEncodedContent(formDictionary);

            var request = client.PostAsync(url, data);
            request.Wait();
           
            var response = request.Result.Content.ReadAsStringAsync();
            response.Wait();

            var result = JsonConvert.DeserializeObject<TheKeysLoginModel>(response.Result);

            return result.access_token;
        }

        private string CallGet(string token, string action)
        {
            var id = Payload["locker_id"];
            var url = $"https://api.the-keys.fr/fr/api/v2/serrure/{action}/{id}?_format=json";

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var request = client.GetAsync(url);
            request.Wait();

            var body = request.Result.Content.ReadAsStringAsync();

            body.Wait();

            var result = JsonConvert.DeserializeObject<TheKeysStateModel>(body.Result);

            return result.data.etat;
        }

        private bool CallPost(string token, string action)
        {
            var id = Payload["locker_id"];
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
