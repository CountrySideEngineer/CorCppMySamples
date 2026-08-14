using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Rel
{
    public int? Rowid { get; set; }

    public int? Reimplemented { get; set; }

    public int? Reimplements { get; set; }

    public int? Innercompounds { get; set; }

    public int? Outercompounds { get; set; }

    public int? Innerpages { get; set; }

    public int? Outerpages { get; set; }

    public int? Innerdirs { get; set; }

    public int? Outerdirs { get; set; }

    public int? Innerfiles { get; set; }

    public int? Outerfiles { get; set; }

    public int? Innerclasses { get; set; }

    public int? Outerclasses { get; set; }

    public int? Innernamespaces { get; set; }

    public int? Outernamespaces { get; set; }

    public int? Innergroups { get; set; }

    public int? Outergroups { get; set; }

    public int? Members { get; set; }

    public int? Compounds { get; set; }

    public int? Subclasses { get; set; }

    public int? Superclasses { get; set; }

    public int? LinksIn { get; set; }

    public int? LinksOut { get; set; }

    public int? ArgumentLinksIn { get; set; }

    public int? ArgumentLinksOut { get; set; }

    public int? InitializerLinksIn { get; set; }

    public int? InitializerLinksOut { get; set; }
}
