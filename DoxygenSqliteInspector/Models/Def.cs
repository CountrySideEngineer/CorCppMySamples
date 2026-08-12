using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class Def
{
    public int? Rowid { get; set; }

    public string? Refid { get; set; }

    public string? Kind { get; set; }

    public string? Name { get; set; }

    public string? Summary { get; set; }
}
