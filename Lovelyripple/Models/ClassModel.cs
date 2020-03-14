using System;
using System.Collections.Generic;
using System.Linq;
using Lovelyripple.Enums;

namespace Lovelyripple.Models
{
    [Serializable]
    public partial class ClassModel
    {
        public string Namespace;
        public string Name;
        public List<PropertyModel> Properties;
        public List<FieldModel> Fields;
        public List<MethodModel> Methods;
        public ClassAccessLevel AccessLevel;
        public bool IsSerializable;

        public ClassModel()
        {
            Properties = new List<PropertyModel>();
            Fields = new List<FieldModel>();
            Methods = new List<MethodModel>();
        }
        public ClassModel(string classNamespace, string className,IEnumerable<FieldModel> fields,IEnumerable<PropertyModel> properties,IEnumerable<MethodModel> methods,ClassAccessLevel classAccess = ClassAccessLevel.Public, bool serializable = true)
        {
            Namespace = String.IsNullOrWhiteSpace(classNamespace) ?
                throw new ArgumentNullException("Class namespace can not be empty, null or whitespace.", nameof(classNamespace))
                : classNamespace;
            Name = String.IsNullOrWhiteSpace(className) ?
                throw new ArgumentNullException("Class name can not be empty, null or whitespace.", nameof(className))
                : className;
            Properties = properties == null || !properties.Any() ?
                new List<PropertyModel>()
                : properties.ToList();
            Fields = fields == null || !fields.Any() ?
                new List<FieldModel>()
                : fields.ToList();
            Methods = methods == null || !methods.Any() ?
                new List<MethodModel>()
                : methods.ToList();
            IsSerializable = serializable;
        }

        public void AddMethod(MethodModel method)
        {
            Methods.Add(method);
        }
        public void AddMethods(IEnumerable<MethodModel> methods)
        {
            Methods.AddRange(methods);
        }

        public void AddProperty(PropertyModel property)
        {
            Properties.Add(property);
        }
        public void AddProperties(IEnumerable<PropertyModel> properties)
        {
            Properties.AddRange(properties);
        }

        public void AddField(FieldModel field)
        {
            Fields.Add(field);
        }
        public void AddFields(IEnumerable<FieldModel> fields)
        {
            Fields.AddRange(fields);
        }
    }
}
