namespace DoxygenSqliteInspector.Lib.Models;

public sealed class DoxygenProjectMeta
{
    public string DoxygenVersion { get; set; } = string.Empty;

    public string SchemaVersion { get; set; } = string.Empty;

    public string GeneratedAt { get; set; } = string.Empty;

    public string GeneratedOn { get; set; } = string.Empty;

    public string ProjectName { get; set; } = string.Empty;

    public string? ProjectNumber { get; set; }
}

public sealed class DoxygenFileSummary
{
    public int Rowid { get; set; }

    public string Name { get; set; } = string.Empty;
}

public sealed class DoxygenFunctionParameter
{
    public string? Type { get; set; }

    public string? Name { get; set; }
}

public sealed class DoxygenFunctionCall
{
    public string? Name { get; set; }

    public string? Context { get; set; }
}

public sealed class DoxygenFunctionSummary
{
    public int Rowid { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Definition { get; set; }

    public string? ReturnType { get; set; }

    public string? DeclaredIn { get; set; }

    public string? ImplementedIn { get; set; }

    public string? Scope { get; set; }

    public int Line { get; set; }
}

public sealed class DoxygenFunctionDetail
{
    public int Rowid { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Definition { get; set; }

    public string? ReturnType { get; set; }

    public string? DeclaredIn { get; set; }

    public string? ImplementedIn { get; set; }

    public string? Scope { get; set; }

    public int Line { get; set; }

    public IReadOnlyList<DoxygenFunctionParameter> Parameters { get; set; } = Array.Empty<DoxygenFunctionParameter>();

    public IReadOnlyList<DoxygenFunctionCall> Callees { get; set; } = Array.Empty<DoxygenFunctionCall>();

    public IReadOnlyList<DoxygenFunctionCall> Callers { get; set; } = Array.Empty<DoxygenFunctionCall>();
}
