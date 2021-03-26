using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities.Items
{

    public abstract class Item : Entity
    {
        public string Name { get; set; }

        public One<Place> Place { get; set; }
    }
}
