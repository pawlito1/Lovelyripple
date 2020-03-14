using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Lovelyripple.Models;
using Lovelyripple.Enums;
using Lovelyripple.Generators;
using Norian.Configuration.Base;

namespace GenerateCmd
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            var properties = new List<ClassModel.PropertyModel>();
            var firstNameValue = new ValueModel(typeof(string), "Tomasz");
            var numOfPpl = new ValueModel(typeof(int), 123);
            properties.Add(new ClassModel.PropertyModel("FirstName",typeof(string),false,firstNameValue,PropertyScope.Get));
            properties.Add(new ClassModel.PropertyModel("NumberOfPeople",typeof(int),false,numOfPpl,PropertyScope.GetSet));
            var assetValues = new List<ValueModel>();
            assetValues.Add(new ValueModel(typeof(string),"Emails"));
            assetValues.Add(new ValueModel(typeof(string), "ToBusiness"));
            assetValues.Add(new ValueModel(typeof(string), "Text"));
            assetValues.Add(new ValueModel(typeof(bool), false));
            assetValues.Add(new ValueModel(typeof(bool), true));
            assetValues.Add(new ValueModel(typeof(int), 321));
            properties.Add(new ClassModel.PropertyModel("AssetInfo","Norian.Configuration.Base.AssetInformation",assetValues,PropertyScope.GetSet));
            var credsValues = new List<ValueModel>();
            credsValues.Add(new ValueModel(typeof(string),"tpawlicki87"));
            credsValues.Add(new ValueModel(typeof(string), "ChangeMe1!"));
            properties.Add(new ClassModel.PropertyModel("Creds",typeof(NetworkCredential),credsValues,PropertyScope.GetSet));
            var classModel = new ClassModel("Norian.Configuration.Base","TestDev",null,properties,null);
            var models = new List<ClassModel>();

            var assetProps = new List<ClassModel.PropertyModel>();
            assetProps.Add(new ClassModel.PropertyModel("Group",typeof(string),false, null,PropertyScope.Get));
            assetProps.Add(new ClassModel.PropertyModel("Name", typeof(string), false, null, PropertyScope.Get));
            assetProps.Add(new ClassModel.PropertyModel("ValueType", typeof(string), false, null, PropertyScope.Get));
            assetProps.Add(new ClassModel.PropertyModel("HasSuffix", typeof(bool), false, null, PropertyScope.Get));
            assetProps.Add(new ClassModel.PropertyModel("IsPerRobot", typeof(bool), false, null, PropertyScope.Get));
            assetProps.Add(new ClassModel.PropertyModel("NumOfAssets", typeof(int), false, null, PropertyScope.Get));
            var assetInfoModel = new ClassModel("Norian.Configuration.Base","AssetInformation",null,assetProps,null);

            models.Add(assetInfoModel);
            models.Add(classModel);

            CodeGenerator.LogMessage = LogMsg;
            //CodeGenerator.SaveTo = new System.IO.FileInfo(Path.Combine(Directory.GetCurrentDirectory(),"test.cs"));
            CodeGenerator.CreateSourceCode(models);
            Console.WriteLine();
            Console.WriteLine("--------------------");
            Console.WriteLine("ready");
        }
        static void LogMsg(string msg)
        {
            Console.WriteLine(msg);
        }
    }
}
