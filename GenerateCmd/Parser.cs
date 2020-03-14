using System;
using Newtonsoft.Json;
namespace GenerateCmd
{
    public static class Parser
    {
        public static T GetValue<T>(string value)
        {
            if (!typeof(T).IsSerializable)
                throw new InvalidOperationException("Only serializable types are allowed.");
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}