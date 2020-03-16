using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
namespace Norian.Configuration.Base
{
    [Serializable]
    public class AssetInformation
    {
        public System.String Group { get => _group; }
        System.String _group;
        public System.String Name { get => _name; }
        System.String _name;
        public System.String ValueType { get => _valuetype; }
        System.String _valuetype;
        public System.Boolean HasSuffix { get => _hassuffix; }
        System.Boolean _hassuffix;
        public System.Boolean IsPerRobot { get => _isperrobot; }
        System.Boolean _isperrobot;
        public System.Int32 NumOfAssets { get => _numofassets; }
        System.Int32 _numofassets;
        public AssetInformation(System.String group, System.String name, System.String valuetype, System.Boolean hassuffix, System.Boolean isperrobot, System.Int32 numofassets)
        {
            this._group = group;
            this._name = name;
            this._valuetype = valuetype;
            this._hassuffix = hassuffix;
            this._isperrobot = isperrobot;
            this._numofassets = numofassets;
        }
    }
}
namespace Norian.Configuration.Base
{
    [Serializable]
    public class TestDev
    {
        public System.String FirstName { get => _firstname; }
        System.String _firstname;
        public System.Int32 NumberOfPeople { get; set; }
        public Norian.Configuration.Base.AssetInformation AssetInfo { get; set; }
        public System.Net.NetworkCredential Creds { get; set; }
        public TestDev()
        {
            this.NumberOfPeople = JsonConvert.DeserializeObject<System.Int32>("123");
            this._firstname = "Tomasz";
            this.AssetInfo = new Norian.Configuration.Base.AssetInformation("Emails", "ToBusiness", "Text", false, true, JsonConvert.DeserializeObject<System.Int32>("321"));
            this.Creds = new System.Net.NetworkCredential("tpawlicki87", "ChangeMe1!");
        }
        public TestDev(System.String firstname, System.Int32 numberofpeople, Norian.Configuration.Base.AssetInformation assetinfo, System.Net.NetworkCredential creds)
        {
            this._firstname = firstname;
            this.NumberOfPeople = numberofpeople;
            this.AssetInfo = assetinfo;
            this.Creds = creds;
        }
    }
}