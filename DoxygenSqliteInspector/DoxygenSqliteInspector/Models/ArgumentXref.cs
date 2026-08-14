using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class ArgumentXref
{
    public int? Rowid { get; set; }

    public int? SrcRowid { get; set; }

    public int? DstRowid { get; set; }
}
