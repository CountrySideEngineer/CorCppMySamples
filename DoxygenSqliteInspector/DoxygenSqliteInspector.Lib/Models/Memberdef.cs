using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Memberdef
{
    public int Rowid { get; set; }

    public string Name { get; set; } = null!;

    public string? Definition { get; set; }

    public string? Type { get; set; }

    public string? Argsstring { get; set; }

    public string? Scope { get; set; }

    public string? Initializer { get; set; }

    public string? Bitfield { get; set; }

    public string? Read { get; set; }

    public string? Write { get; set; }

    public int? Prot { get; set; }

    public int? Static { get; set; }

    public int? Extern { get; set; }

    public int? Const { get; set; }

    public int? Explicit { get; set; }

    public int? Inline { get; set; }

    public int? Final { get; set; }

    public int? Sealed { get; set; }

    public int? New { get; set; }

    public int? Optional { get; set; }

    public int? Required { get; set; }

    public int? Volatile { get; set; }

    public int? Virt { get; set; }

    public int? Mutable { get; set; }

    public int? Initonly { get; set; }

    public int? Attribute { get; set; }

    public int? Property { get; set; }

    public int? Readonly { get; set; }

    public int? Bound { get; set; }

    public int? Constrained { get; set; }

    public int? Transient { get; set; }

    public int? Maybevoid { get; set; }

    public int? Maybedefault { get; set; }

    public int? Maybeambiguous { get; set; }

    public int? Readable { get; set; }

    public int? Writable { get; set; }

    public int? Gettable { get; set; }

    public int? Privategettable { get; set; }

    public int? Protectedgettable { get; set; }

    public int? Settable { get; set; }

    public int? Privatesettable { get; set; }

    public int? Protectedsettable { get; set; }

    public int? Accessor { get; set; }

    public int? Addable { get; set; }

    public int? Removable { get; set; }

    public int? Raisable { get; set; }

    public string Kind { get; set; } = null!;

    public int? Bodystart { get; set; }

    public int? Bodyend { get; set; }

    public int? BodyfileId { get; set; }

    public int FileId { get; set; }

    public int Line { get; set; }

    public int Column { get; set; }

    public string? Detaileddescription { get; set; }

    public string? Briefdescription { get; set; }

    public string? Inbodydescription { get; set; }

    public virtual Path? Bodyfile { get; set; }

    public virtual Path File { get; set; } = null!;

    public virtual ICollection<MemberdefParam> MemberdefParams { get; set; } = new List<MemberdefParam>();

    public virtual ICollection<Member> Members { get; set; } = new List<Member>();

    public virtual ICollection<Reimplement> ReimplementMemberdefRows { get; set; } = new List<Reimplement>();

    public virtual ICollection<Reimplement> ReimplementReimplementedRows { get; set; } = new List<Reimplement>();

    public virtual Refid Row { get; set; } = null!;
}
