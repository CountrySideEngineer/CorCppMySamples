using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Compoundref
{
    public int Rowid { get; set; }

    public int BaseRowid { get; set; }

    public int DerivedRowid { get; set; }

    public int Prot { get; set; }

    public int Virt { get; set; }

    public virtual Compounddef BaseRow { get; set; } = null!;

    public virtual Compounddef DerivedRow { get; set; } = null!;
}
