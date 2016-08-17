using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;
using NHibernate;

namespace DomainObjects
{
    internal static class BatchFileMap
    {
        public const string TableName = "dBF";

        public static class TableColumns
        {
            public const string Id = "iBFPk";
            public const string ClientId = "iCl";
            public const string FileName = "cBFOrgnlFlNm";
            public const string PageCount = "iBFPgs";
            public const string FileSize = "iBFFlSz";
            public const string Image = "tBFImg";
            public const string Status = "Status";
        }
    };

    [ActiveRecord(Table = BatchFileMap.TableName)]
    public partial class BatchFile : BaseDomainObject<BatchFile>, IIdentifiable
    {
        [PrimaryKey(PrimaryKeyType.Sequence,
                        Column = BatchFileMap.TableColumns.Id,
                        Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [Property(Column = BatchFileMap.TableColumns.ClientId)]
        public int ClientId { get; set; }

        [Property(Column = BatchFileMap.TableColumns.FileName)]
        public string FileName { get; set; }

        [Property(Column = BatchFileMap.TableColumns.PageCount)]
        public int PageCount { get; set; }

        [Property(Column = BatchFileMap.TableColumns.FileSize)]
        public int FileSize { get; set; }

        [Property(Column = BatchFileMap.TableColumns.Status)]
        public int Status { get; set; }

        [Property(Column = BatchFileMap.TableColumns.Image, Length = 1048576)]
        public byte[] Image { get; set; }

        //Map(x => x.Image).CustomType("BinaryBlob").Length(1048576).Nullable();
    }
}
