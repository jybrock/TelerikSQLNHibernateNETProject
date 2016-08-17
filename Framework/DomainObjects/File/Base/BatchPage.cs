using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DomainObjects
{
    internal static class BatchPageMap
    {
        public const string TableName = "dBP";

        public static class TableColumns
        {
            public const string Id = "iBPPk";
            public const string BatchDescriptionId = "iBD";
            public const string PageNum = "iBPPg";
        }
    };

    [ActiveRecord(Table = BatchPageMap.TableName)]
    public partial class BatchPage : BaseDomainObject<BatchPage>, IIdentifiable
    {
        [PrimaryKey(PrimaryKeyType.Sequence,
                        Column = BatchPageMap.TableColumns.Id,
                        Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [Property(Column = BatchPageMap.TableColumns.BatchDescriptionId)]
        public int BatchDescriptionId { get; set; }

        [Property(Column = BatchPageMap.TableColumns.PageNum)]
        public int PageNum { get; set; }

        //[BelongsTo(BatchPageMap.TableColumns.PageNum)]
        //public ReportTemplate ReportTemplate { get; set; }

    }
}
