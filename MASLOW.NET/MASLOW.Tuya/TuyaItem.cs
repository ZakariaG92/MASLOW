using MASLOW.Entities.Entities.Items;
using MASLOW.Entities.Items;
using MASLOW.Entities.Users;
using System;
using System.Collections.Generic;

namespace MASLOW.Tuya
{
    public class TuyaItem : Item
    {
        public override List<string> Actions => new List<string>()
        {

        };

        public override List<string> Values => new List<string>()
        {

        };

        public override Dictionary<string, DataType> ExpectedPayload => new Dictionary<string, DataType>()
        {

        };

        public override bool DoAction(string action, Dictionary<string, string> payload, IUser user)
        {
            return true;
        }

        public override string GetValue(string value)
        {
            return "";
        }
    }
}
