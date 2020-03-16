namespace Lovelyripple.Models
{
    public partial class ClassModel
    {
        public class MethodModel
        {
            public string Name;
            public string TypeName;
            public string Implementation;


            public MethodModel(string name, string typeName, string implementation)
            {
                this.Name = name;
                this.TypeName = typeName;
                this.Implementation = implementation;
            }
        }
    }
}
