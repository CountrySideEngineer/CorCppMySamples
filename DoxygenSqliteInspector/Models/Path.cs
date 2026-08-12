using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class Path
{
    public int Rowid { get; set; }

    public int Type { get; set; }

    public int Local { get; set; }

    public int Found { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Compounddef> CompounddefFiles { get; set; } = new List<Compounddef>();

    public virtual ICollection<Compounddef> CompounddefHeaders { get; set; } = new List<Compounddef>();

    public virtual ICollection<Include> IncludeDsts { get; set; } = new List<Include>();

    public virtual ICollection<Include> IncludeSrcs { get; set; } = new List<Include>();

    public virtual ICollection<Memberdef> MemberdefBodyfiles { get; set; } = new List<Memberdef>();

    public virtual ICollection<Memberdef> MemberdefFiles { get; set; } = new List<Memberdef>();
}
