using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Models
{
    public class ItemModel
    {
        public string Name { get; set; }

        public Dictionary<string, string> Payload { get; set; }

        public string ItemType { get; set; }
    }
}
