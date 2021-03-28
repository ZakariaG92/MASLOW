using MASLOW.Entities.Entities.Items;
using MASLOW.Entities.Items;
using MASLOW.Entities.Users;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MASLOW.Arduino
{
    public class ArduinoItem : Item
    {
        public override List<string> Actions => new List<string>()
        {
            "On",
            "Off",
        };

        public override List<string> Values => new List<string>()
        {
            "Temp",
            "Light",
        };

        public override Dictionary<string, DataType> ExpectedPayload => new Dictionary<string, DataType>()
        {
            { "broker_url", DataType.STRING },
            { "broker_port", DataType.INTEGER },
            { "sensors_topic", DataType.STRING },
            { "things_topic", DataType.STRING },
            { "callback_topic", DataType.STRING },
            { "id_socker_1", DataType.STRING },
            { "id_socker_2", DataType.STRING },
            { "id_thermometer", DataType.STRING },
            { "id_light_sensor", DataType.STRING }
        };

        public override bool DoAction(string action, Dictionary<string, string> payload, IUser user)
        {
            var mqttClient = CreateMQTTClient();

            var id = payload["socket"] == "1" ? Payload["id_socker_1"] : payload["socket"] == "2" ? Payload["id_socker_2"] : null;

            if(!Actions.Contains(action) || id == null)
            {
                return false;
            }

            try
            {
                 var text = SubcribleOnMQTT(mqttClient, $"{Payload["things_topic"]}/{id}", () =>
                 {
                     switch (action)
                     {
                         case "On":
                             PublishOnMQTT(mqttClient, id, "on");
                             break;
                         case "Off":
                             PublishOnMQTT(mqttClient, id, "off");
                             break;
                     }
                 });

                var result = JsonConvert.DeserializeObject<ThingModel>(text);
                switch(action){
                    case "On" :
                        return result.value == 0;
                    case "Off" :
                        return result.value == 1;
                    default :
                        return false;
                }
            }
            finally
            {
                DisconnectMQTT(mqttClient);
            }
        }

        public override string GetValue(string value)
        {
            var mqttClient = CreateMQTTClient();

            var id = value == "Temp" ? Payload["id_thermometer"] : value == "Light" ? Payload["id_light_sensor"] : null;

            if(id == null)
            {
                return "";
            }

            try
            {
                var text = SubcribleOnMQTT(mqttClient, $"{Payload["sensors_topic"]}/{id}", () =>
                {
                    PublishOnMQTT(mqttClient, id, value.ToLower());
                });

                var result = JsonConvert.DeserializeObject<SensorsModel>(text);

                return result.value.ToString();
            }
            finally
            {
                DisconnectMQTT(mqttClient);
            }
        }

        private IMqttClient CreateMQTTClient()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(Payload["broker_url"], int.Parse(Payload["broker_port"]))
                .Build();

            var mqttClient = new MqttFactory().CreateMqttClient();

            mqttClient.ConnectAsync(options, CancellationToken.None).Wait();

            return mqttClient;
        }

        private void PublishOnMQTT(IMqttClient mqttClient, string id, string action)
        {
            mqttClient.PublishAsync(Payload["callback_topic"], $"{id}/{action}").Wait();
        }

        private string SubcribleOnMQTT(IMqttClient mqttClient, string topic, Action action = null)
        {
            string response = null;

            mqttClient.UseApplicationMessageReceivedHandler(e =>
            {
                response = Encoding.UTF8.GetString(e.ApplicationMessage.Payload);
            });

            mqttClient.SubscribeAsync(new MqttClientSubscribeOptions()
            {
                TopicFilters = new List<MqttTopicFilter>() { new MqttTopicFilterBuilder().WithTopic(topic).Build() }
            }).Wait();

            if(action != null)
            {
                action.Invoke();
            }

            while (response == null) { }

            mqttClient.DisconnectAsync().Wait();

            return response;
        }

        private void DisconnectMQTT(IMqttClient mqttClient)
        {
            mqttClient.DisconnectAsync().Wait();
        }

    }
}
