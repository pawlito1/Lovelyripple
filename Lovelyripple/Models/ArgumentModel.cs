using System;
namespace Lovelyripple.Models
{
    public class ArgumentModel<T>
    {
        public string Name;
        public T Value;
        public string TypeName { get { return typeof(T).FullName; } }

        public ArgumentModel(string argumentName, T value)
        {
            Name = argumentName;
            Value = value;
        }

    }
}
