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

    [JsonObject(MemberSerialization.OptIn), Table(Name = "Sys_JobLog", DisableSyncStructure = true)]
    public partial class SysJobLog
    {

        [JsonProperty, Column(DbType = "int", IsPrimary = true, IsIdentity = true)]
        public int JobLogID { get; set; }

        [JsonProperty, Column(DbType = "timestamp")]
        public DateTime? CreateTime { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? JobLogType { get; set; }

        [JsonProperty, Column(StringLength = 4000)]
        public string Message { get; set; }

        [JsonProperty, Column(StringLength = 50)]
        public string Name { get; set; }

        [JsonProperty, Column(StringLength = 32)]
        public string ServerIP { get; set; }

        [JsonProperty, Column(DbType = "int")]
        public int? TaskLogType { get; set; }

    }

}
