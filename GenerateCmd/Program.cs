using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using Lovelyripple.Models;
using Lovelyripple.Enums;
using Lovelyripple.Generators;

namespace GenerateCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            var properties = new List<ClassModel.PropertyModel>();
            var firstNameValue = new ValueModel(typeof(string), "Tomasz");
            var numOfPpl = new ValueModel(typeof(int), 123);
            properties.Add(new ClassModel.PropertyModel("FirstName",typeof(string),false,firstNameValue,PropertyScope.Get));
            properties.Add(new ClassModel.PropertyModel("NumberOfPeople",typeof(int),false,numOfPpl,PropertyScope.GetSet));
            var classModel = new ClassModel("Norian.Configuration.Base","TestDev",null,properties,null);
            var models = new List<ClassModel>();
            //Console.WriteLine(JsonConvert.SerializeObject(classModel,Formatting.Indented));
            models.Add(classModel);
            CodeGenerator.CreateSourceCode(models);
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine("ready");
        }
    }
}
