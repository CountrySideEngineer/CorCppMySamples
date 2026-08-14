using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Contain
{
    public int Rowid { get; set; }

    public int InnerRowid { get; set; }

    public int OuterRowid { get; set; }

    public virtual Compounddef InnerRow { get; set; } = null!;

    public virtual Compounddef OuterRow { get; set; } = null!;
}
