using System;
using System.Collections.Generic;
using Lovelyripple.Enums;

namespace Lovelyripple.Models
{
    public partial class ClassModel
    {
        [Serializable]
        public class PropertyModel
        {
            public string Name;
            public string TypeName;
            public bool RequireInitialization;
            public ValueModel Value;
            public PropertyScope Scope;

            public PropertyModel(string propertyName, string typeName, bool requireInit = true, ValueModel valueModel = null, PropertyScope propertyScope = PropertyScope.Get)
            {
                Name = String.IsNullOrWhiteSpace(propertyName) ?
                throw new ArgumentNullException("Class name can not be empty, null or whitespace.", nameof(propertyName))
                : propertyName;
                TypeName = String.IsNullOrWhiteSpace(typeName) ?
                    throw new ArgumentNullException("Class type name can not be empty, null or whitespace.", nameof(typeName))
                    : typeName;
                if (valueModel != null)
                {
                    if (valueModel.TypeName != TypeName)
                        throw new ArgumentException("Value type does not match with property type name.");
                }
                Value = valueModel;
                RequireInitialization = requireInit;
                Scope = propertyScope;
            }

            public PropertyModel(string propertyName, Type type, bool requireInit = true, ValueModel valueModel = null, PropertyScope propertyScope = PropertyScope.Get)
                : this(propertyName, type?.FullName, requireInit, valueModel, propertyScope)
            { }
        }
    }
}
