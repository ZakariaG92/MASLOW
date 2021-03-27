using MASLOW.Entities.Privileges;
using MASLOW.Entities.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities
{
    public interface ISensor { 

        public IEnumerable<string> Values { get; }

        public T GetValue<T>(string value, User user);
    }
}
