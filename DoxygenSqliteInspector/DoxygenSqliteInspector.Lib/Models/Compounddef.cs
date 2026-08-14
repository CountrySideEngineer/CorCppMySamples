using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Compounddef
{
    public int Rowid { get; set; }

    public string Name { get; set; } = null!;

    public string? Title { get; set; }

    public string Kind { get; set; } = null!;

    public int? Prot { get; set; }

    public int FileId { get; set; }

    public int Line { get; set; }

    public int Column { get; set; }

    public int? HeaderId { get; set; }

    public string? Detaileddescription { get; set; }

    public string? Briefdescription { get; set; }

    public virtual ICollection<Compoundref> CompoundrefBaseRows { get; set; } = new List<Compoundref>();

    public virtual ICollection<Compoundref> CompoundrefDerivedRows { get; set; } = new List<Compoundref>();

    public virtual ICollection<Contain> ContainInnerRows { get; set; } = new List<Contain>();

    public virtual ICollection<Contain> ContainOuterRows { get; set; } = new List<Contain>();

    public virtual Path File { get; set; } = null!;

    public virtual Path? Header { get; set; }

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual Refid Row { get; set; } = null!;
}
