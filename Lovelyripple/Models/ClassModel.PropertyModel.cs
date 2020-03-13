using System;
using System.Collections.Generic;

namespace Lovelyripple.Models
{
    public partial class ClassModel
    {
        public class PropertyModel
        {
            public string Name;
            public string TypeName;
            public bool RequireInitialization;
            

            public PropertyModel(string propertyName, string typeName, bool requireInit = false)
            {
                Name = String.IsNullOrWhiteSpace(propertyName) ?
                throw new ArgumentNullException("Class name can not be empty, null or whitespace.", nameof(propertyName))
                : propertyName;
                TypeName = String.IsNullOrWhiteSpace(typeName) ?
                    throw new ArgumentNullException("Class type name can not be empty, null or whitespace.", nameof(typeName))
                    : typeName;
                RequireInitialization = requireInit;
            }
        }
    }
}
