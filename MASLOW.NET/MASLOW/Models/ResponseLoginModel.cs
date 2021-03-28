using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Models
{
    public class ResponseLoginModel
    {
        public string Email { get; set; }

        public string Token { get; set; }

        [Required]
        public string Message { get; set; }
    }
}
