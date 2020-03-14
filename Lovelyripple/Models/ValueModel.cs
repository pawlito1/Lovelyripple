using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lovelyripple.Models
{
    /// <summary>
    /// Value Content object has to be serializable!
    /// </summary>
    [Serializable]
    public class ValueModel
    {
        public string TypeName;
        public object Content;

        public ValueModel(string typeName, object value)
        {
            TypeName = typeName;
            if (!value.GetType().IsSerializable)
                throw new InvalidOperationException("Only serializable types are allowed.");

            Content = value;
        }
        public ValueModel(Type type, object value)
            :this(type?.FullName, value)
        { }
    }
}
