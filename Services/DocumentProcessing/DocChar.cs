using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbbyTest
{
  internal class DocChar
  {
    internal char Character{get;set;}
    internal string FontName { get; set; }
    internal long FontSize { get; set; }
    internal bool IsBold { get; set; }
    internal bool IsItalic { get; set; }
    internal bool IsUnderlined { get; set; }
    internal bool IsSubscript { get; set; }
    internal bool IsSuperscript { get; set; }
    internal bool IsWordStart { get; set; }
    internal bool IsSuspicious { get; set; }
    internal bool IsProofed { get; set; }
    internal long Bottom { get; set; }
    internal long Left { get; set; }
    internal long Right { get; set; }
    internal long Top { get; set; }
    internal long Spacing { get; set; }
  }
}
