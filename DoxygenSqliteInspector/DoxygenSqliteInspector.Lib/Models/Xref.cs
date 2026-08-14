using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Xref
{
    public int Rowid { get; set; }

    public int SrcRowid { get; set; }

    public int DstRowid { get; set; }

    public string Context { get; set; } = null!;

    public virtual Refid DstRow { get; set; } = null!;

    public virtual Refid SrcRow { get; set; } = null!;
}
