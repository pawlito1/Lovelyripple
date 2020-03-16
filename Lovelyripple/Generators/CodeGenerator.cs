using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.IO;
using Lovelyripple.Models;
using Lovelyripple.Enums;

namespace Lovelyripple.Generators
{
    /// <summary>
    /// Based on class models soruce code will be generated
    /// </summary>
    public static class CodeGenerator
    {
        public static FileInfo SaveTo = null;
        static StringBuilder _sb = new StringBuilder();
        public static LogOutput LogMessage = DefaultLogMessage;
        public delegate void LogOutput(string msg);

        public static void CreateSourceCode(IEnumerable<ClassModel> classModels)
        {
            AddUsings(_sb);
            foreach (var model in classModels)
            {
                AddNamespace(model, _sb);
                OpenBrace(_sb);
                AddClassName(model, _sb);
                OpenBrace(_sb);
                AddProperties(model, _sb);
                AddBaseConstructor(model, _sb);
                if(model.Properties.Any())
                    AddConstructors(model, _sb);
                CloseBrace(_sb);
                CloseBrace(_sb);
            }
            if (SaveTo != null)
            {
                if (!SaveTo.Directory.Exists)
                    SaveTo.Directory.Create();
                try
                {
                    File.WriteAllText(SaveTo.FullName, _sb.ToString());
                }
                catch (Exception ex)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    LogMessage(ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            LogMessage(_sb.ToString());
        }

        // ----- PRIVATES -----
        static void DefaultLogMessage(string msg)
        {
            Console.WriteLine(msg);
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
            if (classModel.IsSerializable)
                sb.AppendLine("[Serializable]");
            if (classModel.AccessLevel == Enums.ClassAccessLevel.Public || classModel.AccessLevel == Enums.ClassAccessLevel.Private)
                sb.AppendLine($"{classModel.AccessLevel.ToString().ToLower()} class {classModel.Name}");
            if (classModel.AccessLevel == Enums.ClassAccessLevel.PublicStatic)
                sb.AppendLine($"public static class {classModel.Name}");
        }
        static void AddProperties(ClassModel classModel, StringBuilder sb)
        {
            string accessLevel;
            switch (classModel.AccessLevel)
            {
                case Enums.ClassAccessLevel.PublicStatic:
                    accessLevel = "public static";
                    break;
                default:
                    accessLevel = classModel.AccessLevel.ToString().ToLower();
                    break;
            }
            foreach (var property in classModel.Properties)
            {
                sb.Append($"{accessLevel} {property.TypeName} {property.Name} ");
                switch (property.Scope)
                {
                    case Enums.PropertyScope.GetSet:
                        sb.AppendLine($"{{get;set;}}");
                        break;
                    case Enums.PropertyScope.Get:
                            sb.AppendLine($"{{get => _{property.Name.ToLowerInvariant()}; }}");
                            sb.AppendLine($"{property.TypeName} _{property.Name.ToLowerInvariant()};");
                        break;
                    default:
                        break;
                }
            }
        }
        static void AddBaseConstructor(ClassModel classModel, StringBuilder sb)
        {
            if (classModel.Name.IndexOf('<') > 0) //class is generic so contructors can not have '<>'
                classModel.Name = classModel.Name.Substring(0, classModel.Name.IndexOf('<'));

            if (classModel.Properties.All(p => p.Scope == Enums.PropertyScope.Get))
                return;

            sb.AppendLine($"{classModel.AccessLevel.ToString().ToLower()} {classModel.Name}()");
            OpenBrace(sb);
            //initialize properties that do not require initialization but have a ValueModel
            foreach (var property in classModel.Properties.Where(x => !x.RequireInitialization && x.Value != null && x.Scope == Enums.PropertyScope.GetSet))
            {
                sb.Append($"this.{property.Name} = ");
                if(Type.GetType(property.TypeName) == typeof(string))
                    sb.AppendLine($"\"{property.Value.Content}\";");
                else if(Type.GetType(property.TypeName) == typeof(bool))
                    sb.AppendLine($"{property.Value.Content.ToString().ToLower()};");
                else
                    sb.AppendLine($"JsonConvert.DeserializeObject<{property.TypeName}>(\"{property.Value.Content}\");");
            }

            foreach (var property in classModel.Properties.Where(x => !x.RequireInitialization && x.Value != null && x.Scope == Enums.PropertyScope.Get))
            {
            //populate private fields
                sb.Append($"this._{property.Name.ToLowerInvariant()} = ");
                if (Type.GetType(property.TypeName) == typeof(string))
                    sb.AppendLine($"\"{property.Value.Content}\";");
                else if (Type.GetType(property.TypeName) == typeof(bool))
                    sb.AppendLine($"{property.Value.Content.ToString().ToLower()};");
                else
                    sb.AppendLine($"JsonConvert.DeserializeObject<{property.TypeName}>(\"{property.Value.Content}\");");
            }
            //initialize properties that does require initialization
            foreach (var property in classModel.Properties.Where(x => x.RequireInitialization))
            {
                var initValues = new List<string>();
                if (property.Values != null && property.Values.Count > 0)
                {
                    foreach (var value in property.Values)
                    {
                        if (Type.GetType(value.TypeName) == typeof(string))
                            initValues.Add($"\"{value.Content}\"");
                        else if (Type.GetType(value.TypeName) == typeof(bool))
                            initValues.Add($"{value.Content.ToString().ToLower()}");
                        else
                            initValues.Add($"JsonConvert.DeserializeObject<{value.TypeName}>(\"{value.Content}\")");
                    }
                }
                sb.AppendLine($"this.{property.Name} = new {property.TypeName}({String.Join(", ",initValues)});");
            }
            CloseBrace(sb);
        }
        static void AddConstructors(ClassModel classModel, StringBuilder sb)
        {
            sb.AppendLine($"{classModel.AccessLevel.ToString().ToLower()} {classModel.Name}({String.Join(", ",classModel.Properties.Select(x => x.TypeName + " " + x.Name.ToLowerInvariant()))})");
            OpenBrace(sb);
            foreach (var property in classModel.Properties)
            {
                if (property.Scope == Enums.PropertyScope.Get)
                    sb.AppendLine($"this._{property.Name.ToLowerInvariant()} = {property.Name.ToLowerInvariant()};");
                else
                    sb.AppendLine($"this.{property.Name} = {property.Name.ToLowerInvariant()};");
            }
            CloseBrace(sb);
        }
        static void AddUsings(StringBuilder sb)
        {
            sb.AppendLine($"using System;");
            sb.AppendLine($"using System.Collections.Generic;");
            sb.AppendLine($"using System.Linq;");
            sb.AppendLine($"using System.Text;");
            sb.AppendLine($"using System.IO;");
            sb.AppendLine($"using Newtonsoft.Json;");
        }
    }
}
