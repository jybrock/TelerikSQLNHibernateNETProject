using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate;

namespace DomainObjects
{
    internal static class ReferenceMap
    {
        public const string TableName = "xRf";

        public static class TableColumns
        {
            public const string Id = "iRf";
            public const string Type = "cRfTyp";
            public const string Name = "cRfNm";
        }
    };

    [ActiveRecord(Table = ReferenceMap.TableName)]
    public partial class Reference : BaseDomainObject<Reference>, IIdentifiable
    {
        [PrimaryKey(PrimaryKeyType.Sequence,
                        Column = ReferenceMap.TableColumns.Id,
                        Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [Property(Column = ReferenceMap.TableColumns.Type)]
        public string Type { get; set; }

        [Property(Column = ReferenceMap.TableColumns.Name)]
        public string Name { get; set; }

    }
}
