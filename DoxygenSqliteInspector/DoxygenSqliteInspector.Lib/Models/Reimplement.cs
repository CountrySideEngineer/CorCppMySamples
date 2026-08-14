using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Reimplement
{
    public int Rowid { get; set; }

    public int MemberdefRowid { get; set; }

    public int ReimplementedRowid { get; set; }

    public virtual Memberdef MemberdefRow { get; set; } = null!;

    public virtual Memberdef ReimplementedRow { get; set; } = null!;
}
