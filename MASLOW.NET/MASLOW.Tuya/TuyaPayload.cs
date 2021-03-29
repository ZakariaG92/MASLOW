using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MASLOW.Tuya
{
    public class TuyaPayload
    {
        public string id { get; set; }
        public string key { get; set; }
        public int dps { get; set; }
        public bool value { get; set; }
    }
}
