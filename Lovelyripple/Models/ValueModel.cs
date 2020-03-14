using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lovelyripple.Models
{
    /// <summary>
    /// Value object has to be serializable!
    /// </summary>
    [Serializable]
    public class ValueModel
    {
        public string TypeName;
        public object Value;
        private string _serialized;

        public ValueModel(string typeName, object value)
        {
            TypeName = typeName;
            Value = value;
            _serialized = JsonConvert.SerializeObject(value);
        }
        public ValueModel(Type type, object value)
            :this(type?.FullName, value)
        { }

        public T GetValue<T>()
        {
            return JsonConvert.DeserializeObject<T>(_serialized);
        }
    }
}
