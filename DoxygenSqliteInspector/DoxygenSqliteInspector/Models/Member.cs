using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class Member
{
    public int Rowid { get; set; }

    public int ScopeRowid { get; set; }

    public int MemberdefRowid { get; set; }

    public int Prot { get; set; }

    public int Virt { get; set; }

    public virtual Memberdef MemberdefRow { get; set; } = null!;

    public virtual Compounddef ScopeRow { get; set; } = null!;
}
