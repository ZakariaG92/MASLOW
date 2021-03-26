using MASLOW.Entities.Items;
using MASLOW.Entities.Users;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities
{
    public class Place : Entity
    {
        public Address Address { get; set; }

        public Many<Item> Items { get; set; }

        public Many<Group> Groups { get; set; }

        public Place() : base(){
            this.InitOneToMany(() => Groups);
            this.InitOneToMany(() => Items);
        }
    }

    public class Address
    {
        public string Street { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
