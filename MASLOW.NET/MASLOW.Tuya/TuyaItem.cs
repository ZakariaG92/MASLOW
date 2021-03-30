using MASLOW.Entities.Entities.Items;
using MASLOW.Entities.Items;
using MASLOW.Entities.Users;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace MASLOW.Tuya
{
    // Use Maslow  
    public class TuyaItem : Item
    {
        public override List<string> Actions => new List<string>()
        {
            "On",
            "Off"
        };

        public override List<string> Values => new List<string>()
        {
            "IsEnabled"
        };

        public override Dictionary<string, DataType> ExpectedPayload => new Dictionary<string, DataType>()
        {
            {"url", DataType.URL},
            {"id", DataType.STRING},
            {"key", DataType.STRING},
            {"dps", DataType.INTEGER}
        };

        public override bool DoAction(string action, Dictionary<string, string> payload, IUser user)
        {
            switch(action){
                case "On" :
                    return PostCall(true);
                case "Off":
                    return PostCall(false);
                default :
                    return false;
            }
        }

        public override string GetValue(string value)
        {
            if (value == "IsEnabled")
            {
                var url = $"{Payload["url"]}/api/things/{Payload["id"]}/{Payload["key"]}/{Payload["dps"]}";

                var client = new HttpClient();

                var request = client.GetAsync(url);
                request.Wait();

                var result = request.Result.Content.ReadAsStringAsync();
                result.Wait();

                return request.Result.StatusCode == HttpStatusCode.BadRequest ? "" : JsonConvert.DeserializeObject<TuyaResponse>(result.Result).status ? "on" : "off";
            }

            return "";
        }

        private bool PostCall(bool active)
        {
            var url = $"{Payload["url"]}/api/things/switch";
            var payload = new TuyaPayload()
            {
                id = Payload["id"],
                key = Payload["key"],
                dps = int.Parse(Payload["dps"]),
                value = active
            };

            var client = new HttpClient();

            var body = new StringContent(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");
            var request = client.PostAsync(url, body);
            request.Wait();

            var result = request.Result.Content.ReadAsStringAsync();
            result.Wait();

            return request.Result.StatusCode == HttpStatusCode.OK && JsonConvert.DeserializeObject<TuyaResponse>(result.Result).status == active;
        }
    }
}
