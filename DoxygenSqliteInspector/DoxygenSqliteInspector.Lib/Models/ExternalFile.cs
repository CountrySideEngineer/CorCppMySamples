using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class ExternalFile
{
    public int? Rowid { get; set; }

    public int? Found { get; set; }

    public string? Name { get; set; }
}
