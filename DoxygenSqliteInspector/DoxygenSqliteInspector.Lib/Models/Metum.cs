using System;
using System.Collections.Generic;

namespace DoxygenSqliteInspector.Lib.Models;

public partial class Metum
{
    public string DoxygenVersion { get; set; } = null!;

    public string SchemaVersion { get; set; } = null!;

    public string GeneratedAt { get; set; } = null!;

    public string GeneratedOn { get; set; } = null!;

    public string ProjectName { get; set; } = null!;

    public string? ProjectNumber { get; set; }

    public string? ProjectBrief { get; set; }
}
