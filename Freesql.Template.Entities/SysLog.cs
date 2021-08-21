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

    [JsonObject(MemberSerialization.OptIn), Table(Name = "Sys_Log", DisableSyncStructure = true)]
    public partial class SysLog
    {

        [JsonProperty, Column(DbType = "int", IsPrimary = true, IsIdentity = true)]
        public int LogID { get; set; }

        [JsonProperty, Column(DbType = "timestamp")]
        public DateTime? CreateTime { get; set; }

        [JsonProperty, Column(DbType = "tinyint(1)")]
        public sbyte? DeleteFlag { get; set; }

        [JsonProperty, Column(StringLength = 2048)]
        public string Exception { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string IP { get; set; }

        [JsonProperty, Column(StringLength = 128)]
        public string Level { get; set; }

        [JsonProperty, Column(StringLength = 512)]
        public string Logger { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? LogType { get; set; }

        [JsonProperty, Column(StringLength = 64, IsNullable = false)]
        public string MemberName { get; set; }

        [JsonProperty, Column(StringLength = 2048)]
        public string Message { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? SubSysID { get; set; }

        [JsonProperty, Column(StringLength = 256)]
        public string SubSysName { get; set; }

        [JsonProperty, Column(StringLength = 128)]
        public string Thread { get; set; }

        [JsonProperty, Column(StringLength = 256)]
        public string Url { get; set; }

    }

}
