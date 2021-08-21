using FreeSql.DatabaseModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Newtonsoft.Json;
using FreeSql.DataAnnotations;

namespace Freesql.Template.Entities
{

    [JsonObject(MemberSerialization.OptIn), Table(Name = "Sys_Address", DisableSyncStructure = true)]
    public partial class SysAddress
    {

        [JsonProperty, Column(DbType = "int", IsPrimary = true, IsIdentity = true)]
        public int ID { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string CityCode { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? LevelType { get; set; }

        [JsonProperty, Column(StringLength = 200)]
        public string MergerShortName { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Name { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? ParentId { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Remark { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string ShortName { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string ZipCode { get; set; }

    }

}
