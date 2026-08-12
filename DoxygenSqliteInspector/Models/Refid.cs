using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Models;

public partial class Refid
{
    public int Rowid { get; set; }

    public string Refid1 { get; set; } = null!;

    public virtual Compounddef? Compounddef { get; set; }

    public virtual Memberdef? Memberdef { get; set; }

    public virtual ICollection<Xref> XrefDstRows { get; set; } = new List<Xref>();

    public virtual ICollection<Xref> XrefSrcRows { get; set; } = new List<Xref>();
}
