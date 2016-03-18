using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MongoStart
{
    /// <summary>
    ///实体映射到的表名
    /// </summary>
    [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Struct, AllowMultiple = false, Inherited = true)]
    public class MongoInfoMapAttribute : Attribute
    {
        public string TableName { get; set; }
        public MongoInfoMapAttribute(string tableName)
        {
            TableName = tableName;
        }
    }
}
