using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DomainObjects
{
    internal static class BatchFileDescriptionMap
    {
        public const string TableName = "dBD";

        public static class TableColumns
        {
            public const string Id = "iBDPk";
            public const string BatchFileId = "iBF";
            public const string Type = "iBDTyp";
            public const string PageCount = "iBDPgs";
            public const string FileSize = "iBDFlSz";
            public const string FileExtension = "iBDFlExt";
        }
    };

    [ActiveRecord(Table = BatchFileDescriptionMap.TableName)]
    public partial class BatchFileDescription : BaseDomainObject<BatchFileDescription>, IIdentifiable
    {
        [PrimaryKey(PrimaryKeyType.Sequence,
                        Column = BatchFileDescriptionMap.TableColumns.Id,
                        Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [BelongsTo(BatchFileDescriptionMap.TableColumns.BatchFileId)]
        public BatchFile BatchFile { get; set; }

        [Property(Column = BatchFileDescriptionMap.TableColumns.Type)]
        public int Type { get; set; }

        [Property(Column = BatchFileDescriptionMap.TableColumns.PageCount)]
        public int PageCount { get; set; }

        [Property(Column = BatchFileDescriptionMap.TableColumns.FileSize)]
        public int FileSize { get; set; }

        [Property(Column = BatchFileDescriptionMap.TableColumns.FileExtension)]
        public string FileExtension { get; set; }
    }
}
