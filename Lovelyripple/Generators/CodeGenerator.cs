using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using Lovelyripple.Models;

namespace Lovelyripple.Generators
{
    public static class CodeGenerator
    {
        static StringBuilder _sb = new StringBuilder();
        public static void CreateSourceCode(IEnumerable<ClassModel> classModels)
        {
            foreach (var model in classModels)
            {
                AddNamespace(model, _sb);
                OpenBrace(_sb);
                AddClassName(model, _sb);
                OpenBrace(_sb);
                AddProperties(model, _sb);
                CloseBrace(_sb);
                CloseBrace(_sb);
            }
            Console.WriteLine(_sb);
        }
        static void AddNamespace(ClassModel classModel,StringBuilder sb)
        {
            sb.AppendLine($"namespace {classModel.Namespace}");
        }
        static void OpenBrace(StringBuilder sb)
        {
            sb.AppendLine("{");
        }
        static void CloseBrace(StringBuilder sb)
        {
            sb.AppendLine("}");
        }
        static void AddClassName(ClassModel classModel, StringBuilder sb)
        {
            sb.AppendLine($"{classModel.AccessLevel.ToString()} {classModel.Name}");
        }
        static void AddProperties(ClassModel classModel, StringBuilder sb)
        {
            foreach (var property in classModel.Properties)
            {
                sb.AppendLine($"public {property.TypeName} {property.Name} {{get;set;}}");
            }
        }
    }
}
