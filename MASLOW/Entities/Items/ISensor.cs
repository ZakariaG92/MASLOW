using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Entities
{
    public interface ISensor
    {
        public List<string> Values { get; }

        public T GetValue<T>(string value, User user);
    }
}
