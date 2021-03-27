using MASLOW.Entities.Items;
using MASLOW.Entities.Privileges;
using MASLOW.Entities.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities
{
    public class Place
    {
        public Address Address { get; set; }

        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Group> Groups { get; set; }

        public IEnumerable<User> Users { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
