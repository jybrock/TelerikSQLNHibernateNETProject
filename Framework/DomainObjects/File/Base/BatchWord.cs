using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Castle.ActiveRecord;

namespace DomainObjects
{
    internal static class BatchWordMap
    {
        public const string TableName = "dBW";

        public static class TableColumns
        {
            public const string Id = "iBWPk";
            public const string BatchPageId = "iBPPK";
            public const string Word = "cBWWrd";
            public const string Top = "iBWTop";
            public const string Left = "iBWLft";
            public const string Bottom = "iBWBtm";
            public const string Right = "iBWRght";
            public const string FontName = "cBWFntNm";
            public const string FontSize = "iBWFntSz";
            public const string FontBold = "lBWFntBld";
            public const string FontItalic = "lBWFntItlc";
            public const string FontUnderline = "lBWFntUndrLn";
            public const string FontSubscript = "lBWFntSbScrpt";
            public const string FontSuperscript = "lBWFntSprScrpt";
            public const string FontSuspicious = "lWCSuspcs";
            public const string SmallCaps = "lBWSmlCps";
            public const string Proofed = "lBWPrfd";
            public const string WordConfidence = "iBWCnfdnc";
            public const string FromDictionary = "lBWDctnry";
            public const string ParagraphNumber = "iBWParagraph";
            public const string WordOrder = "iBWWordOrder";
            public const string IsTableBlock = "ilTableBlock";
            public const string IsTextBlock = "ilTextBlock";
            public const string BlockNumber = "iBWBlock";
            public const string CellNumber = "iBWCell";
        }
    };

    [ActiveRecord(Table = BatchWordMap.TableName)]
    public partial class BatchWord : BaseDomainObject<BatchWord>, IIdentifiable
    {
        [PrimaryKey(PrimaryKeyType.Sequence,
                        Column = BatchWordMap.TableColumns.Id,
                        Generator = PrimaryKeyType.Native)]
        public int Id { get; set; }

        [BelongsTo(BatchWordMap.TableColumns.BatchPageId)]
        public BatchPage BatchPage { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Word)]
        public string Word { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Top)]
        public long Top { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Left)]
        public long Left { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Bottom)]
        public long Bottom { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Right)]
        public long Right { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontName)]
        public string FontName { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontSize)]
        public long FontSize { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontBold)]
        public bool FontBold { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontItalic)]
        public bool FontItalic { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontUnderline)]
        public bool FontUnderline { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontSubscript)]
        public bool FontSubscript { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontSuperscript)]
        public bool FontSuperscript { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FontSuspicious)]
        public bool FontSuspicious { get; set; }

        [Property(Column = BatchWordMap.TableColumns.SmallCaps)]
        public bool SmallCaps { get; set; }

        [Property(Column = BatchWordMap.TableColumns.Proofed)]
        public bool Proofed { get; set; }

        [Property(Column = BatchWordMap.TableColumns.WordConfidence)]
        public bool? WordConfidence { get; set; }

        [Property(Column = BatchWordMap.TableColumns.FromDictionary)]
        public bool? FromDictionary { get; set; }

        [Property(Column = BatchWordMap.TableColumns.WordOrder)]
        public int WordOrder { get; set; }

        [Property(Column = BatchWordMap.TableColumns.ParagraphNumber)]
        public int ParagraphNumber { get; set; }

        [Property(Column = BatchWordMap.TableColumns.BlockNumber)]
        public int BlockNumber { get; set; }

        [Property(Column = BatchWordMap.TableColumns.CellNumber)]
        public int? CellNumber { get; set; }

        [Property(Column = BatchWordMap.TableColumns.IsTableBlock)]
        public bool? IsTableBlock { get; set; }

        [Property(Column = BatchWordMap.TableColumns.IsTextBlock)]
        public bool? IsTextBlock { get; set; }
    }
}
