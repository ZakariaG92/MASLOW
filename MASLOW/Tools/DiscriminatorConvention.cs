using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MASLOW.Tools
{
    class DiscriminatorConvention : IDiscriminatorConvention
    {
        public string ElementName
        {
            get { return "_t"; }
        }

        public Type GetActualType(IBsonReader bsonReader, Type nominalType)
        {
            var ret = nominalType;

            var bookmark = bsonReader.GetBookmark();
            bsonReader.ReadStartDocument();
            if (bsonReader.FindElement(ElementName))
            {
                var value = bsonReader.ReadString();

                ret = Type.GetType(value);

                if (ret == null)
                    throw new Exception("Could not find type " + value);
            }

            bsonReader.ReturnToBookmark(bookmark);

            return ret;
        }

        public BsonValue GetDiscriminator(Type nominalType, Type actualType)
        {
            return actualType.AssemblyQualifiedName;
        }
    }
}
