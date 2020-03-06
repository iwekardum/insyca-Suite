
/* Unmerged change from project 'log4net.ElasticSearch (net45)'
Before:
using System;
using System.Collections.Generic;
using System.Reflection;
using log4net.ElasticSearch.Models;
After:
using log4net.ElasticSearch.Models;
using Newtonsoft.Json;
using System.Json.Serialization;
using System;
*/

/* Unmerged change from project 'log4net.ElasticSearch (netstandard2.0)'
Before:
using System;
using System.Collections.Generic;
using System.Reflection;
using log4net.ElasticSearch.Models;
After:
using log4net.ElasticSearch.Models;
using Newtonsoft.Json;
using System.Json.Serialization;
using System;
*/
using log4net.ElasticSearch.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Collections.Generic;
using System.Reflection;

namespace log4net.ElasticSearch
{
    public class CustomDataContractResolver : DefaultContractResolver
    {
        public Dictionary<string, string> FieldNameChanges { get; set; }
        public List<FieldValueReplica> FieldValueReplica { get; set; }

        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);
            if (property.DeclaringType != typeof(logEvent)) return property;

            if (FieldNameChanges.Count > 0 && FieldNameChanges.TryGetValue(property.PropertyName, out var newValue))
                property.PropertyName = newValue;

            return property;
        }
    }
}