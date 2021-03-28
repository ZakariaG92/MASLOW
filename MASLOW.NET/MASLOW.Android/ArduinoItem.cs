using MASLOW.Entities.Items;
using MASLOW.Entities.Users;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace MASLOW.Arduino
{
    public class ArduinoItem : Item
    {
        public override IEnumerable<string> Actions => new List<string>()
        {
            "On",
            "Off"
        };

        public override IEnumerable<string> Values => new List<string>()
        {

        };

        public override bool DoAction(string action, Dictionary<string, string> payload, IUser user)
        {
            var mqttClient = CreateMQTTClient("broker.hivemq.com:1883");

            try
            {
                switch (action)
                {
                    case "On":
                        PublishOnMQTT(mqttClient, "callback/maslow", "on");
                        return true;
                    case "Off":
                        PublishOnMQTT(mqttClient, "callback/maslow", "off");
                        return true;
                    default:
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
            return "";
        }

        private IMqttClient CreateMQTTClient(string url)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(url)
                .Build();

            var mqttClient = new MqttFactory().CreateMqttClient();

            mqttClient.ConnectAsync(options, CancellationToken.None).Wait();

            return mqttClient;
        }

        private void PublishOnMQTT(IMqttClient mqttClient, string topic, string action)
        {
            mqttClient.PublishAsync(topic, $"3b5708b079f247938f4096ee057fbc01/{action}").Wait();
        }

        private string SubcribleOnMQTT(IMqttClient mqttClient, string url, string topic, Action action = null)
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
