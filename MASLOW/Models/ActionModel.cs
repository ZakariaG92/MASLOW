using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Models
{
    public class ActionModel
    {
        [Required]
        public string ItemId { get; set; }

        [Required]
        public string Action { get; set; }

        public Dictionary<string, string>? Payload { get; set; }
    }
}
