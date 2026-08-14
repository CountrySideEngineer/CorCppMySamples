using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class InitializerXref
{
    public int? Rowid { get; set; }

    public int? SrcRowid { get; set; }

    public int? DstRowid { get; set; }
}
