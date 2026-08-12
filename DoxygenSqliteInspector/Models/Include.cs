using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class Include
{
    public int Rowid { get; set; }

    public int Local { get; set; }

    public int SrcId { get; set; }

    public int DstId { get; set; }

    public virtual Path Dst { get; set; } = null!;

    public virtual Path Src { get; set; } = null!;
}
