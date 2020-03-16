using System;
using System.Linq;
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
            public List<ValueModel> Values;
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
                        throw new ArgumentException("Value type does not match with property type.");
                }
                Value = valueModel;
                RequireInitialization = requireInit;
                Scope = propertyScope;
            }

            public PropertyModel(string propertyName, Type type, bool requireInit = true, ValueModel valueModel = null, PropertyScope propertyScope = PropertyScope.Get)
                : this(propertyName, type?.FullName, requireInit, valueModel, propertyScope)
            { }
            /// <summary>
            /// Constructor for properties that requires initialization
            /// </summary>
            /// <param name="propertyName">Name of a property</param>
            /// <param name="typeName">Full name of a propoerty type</param>
            /// <param name="valueModels">Initialization values for property, oreder matters!</param>
            /// <param name="propertyScope">What is the propoerty scope access level</param>
            public PropertyModel(string propertyName, string typeName, List<ValueModel> valueModels,PropertyScope propertyScope = PropertyScope.GetSet)
                : this(propertyName,typeName,true,null,propertyScope)
            {
                Values = valueModels == null || !valueModels.Any() ?
                    new List<ValueModel>()
                    : valueModels;
                RequireInitialization = true;
            }
            public PropertyModel(string propertyName, Type type, List<ValueModel> valueModels, PropertyScope propertyScope = PropertyScope.GetSet)
                : this(propertyName, type?.FullName,valueModels,propertyScope)
            { }
        }
    }
}
