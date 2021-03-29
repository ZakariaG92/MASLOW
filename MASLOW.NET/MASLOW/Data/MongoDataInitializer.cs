using MASLOW.Entities.Items;
using MASLOW.Services;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Data
{
    public class MongoDataInitializer
    {
        private readonly MongoDatabaseService _dbService;

        public MongoDataInitializer(MongoDatabaseService dbService)
        {
            _dbService = dbService;
        }

        public void SeedData()
        {
            SeedItems();
        }

        public void SeedItems()
        {
            if(_dbService.Items.AsQueryable().Count() == 0)
            {

                _dbService.Items.InsertMany(new List<Item>()
                {
                    //The key object
                    new TheKeys.TheKeysItem()
                    {
                        Name = "The keys locker",
                        Payload = new Dictionary<string, string>()
                        {
                            {"login", "+33665387559" },
                            {"password", "a89f5909be" },
                            {"locker_id", "7619" },
                        }
                    },

                    //Arduino object
                    new Arduino.ArduinoItem()
                    {
                        Name = "Maslow Arduino",
                        Payload = new Dictionary<string, string>()
                        {
                            { "broker_url", "broker.hivemq.com" },
                            { "broker_port",  "1883"},
                            { "sensors_topic", "maslow/sensors" },
                            { "things_topic", "maslow/thing" },
                            { "callback_topic", "callback/maslow"  },
                            { "id_socker_1", "3b5708b079f247938f4096ee057fbc01"  },
                            { "id_socker_2", "6e72fcb15903465fafa3f28c9e5d0d1c" },
                            { "id_thermometer", "75fcc4da231942739d04dd56874f3bd9" },
                            { "id_light_sensor", "6b13a7131fe54e1096a7aac9ea77bb9d" }
                        }
                    },

                    new Tuya.TuyaItem()
                    {
                        Name = "Tuya Lamp",
                        Payload = new Dictionary<string, string>()
                        {
                            {"url", "http://localhost:3000" },
                            {"id", "bff4e48fb6b1ca7ed9xhom" },
                            {"key", "795e59888afaa8f6" },
                            {"dps", "20" }
                        }
                    }
                });
            }
        }
    }
}
