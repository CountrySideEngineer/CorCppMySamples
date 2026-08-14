using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class MemberdefParam
{
    public int Rowid { get; set; }

    public int MemberdefId { get; set; }

    public int ParamId { get; set; }

    public virtual Memberdef Memberdef { get; set; } = null!;

    public virtual Param Param { get; set; } = null!;
}
