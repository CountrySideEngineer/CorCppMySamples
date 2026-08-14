using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Param
{
    public int Rowid { get; set; }

    public string? Attributes { get; set; }

    public string? Type { get; set; }

    public string? Declname { get; set; }

    public string? Defname { get; set; }

    public string? Array { get; set; }

    public string? Defval { get; set; }

    public string? Briefdescription { get; set; }

    public virtual ICollection<MemberdefParam> MemberdefParams { get; set; } = new List<MemberdefParam>();
}
