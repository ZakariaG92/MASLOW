using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MASLOW.Entities.Items;

namespace MASLOW.Entities
{
    public class Place
    {
        [BsonId]
        public ObjectId Id { get; set; }
        
        public Address Address { get; set; }

        public List<Item> Items { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
