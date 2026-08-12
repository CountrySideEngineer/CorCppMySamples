using DoxygenSqliteInspector.Data;
using DoxygenSqliteInspector.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Doxygen SQLite DB Inspector using EF Core");

var showFunctions = args.Contains("--function", StringComparer.OrdinalIgnoreCase);
var showFiles = args.Contains("--file", StringComparer.OrdinalIgnoreCase);

await using var context = new DoxygenContext();

var meta = await context.Meta.AsNoTracking().FirstOrDefaultAsync();
if (meta is null)
{
    Console.WriteLine("No meta information found in the database.");
    return;
}

Console.WriteLine("Meta information:");
Console.WriteLine($"  Project: {meta.ProjectName}");
Console.WriteLine($"  Version: {meta.ProjectNumber}");
Console.WriteLine($"  Doxygen: {meta.DoxygenVersion}");
Console.WriteLine($"  Schema: {meta.SchemaVersion}");
Console.WriteLine($"  Generated: {meta.GeneratedAt} {meta.GeneratedOn}");
Console.WriteLine();

if (showFiles)
{
    var files = await context.Paths
        .AsNoTracking()
        .OrderBy(p => p.Name)
        .ToListAsync();

    Console.WriteLine($"File count: {files.Count}");
    foreach (var file in files)
    {
        Console.WriteLine($"  - {file.Name}");
    }
    return;
}

var functions = await context.Memberdefs
    .AsNoTracking()
    .Where(m => m.Kind == "function")
    .Include(m => m.File)
    .Include(m => m.Bodyfile)
    .Include(m => m.MemberdefParams).ThenInclude(mp => mp.Param)
    .OrderBy(m => m.Name)
    .ToListAsync();

if (showFunctions)
{
    Console.WriteLine($"Function count: {functions.Count}");
    foreach (var function in functions)
    {
        Console.WriteLine($"  - {GetDisplayName(function)}");
    }
    return;
}

var xrefs = await context.Xrefs
    .AsNoTracking()
    .Include(x => x.SrcRow).ThenInclude(r => r.Memberdef)
    .Include(x => x.DstRow).ThenInclude(r => r.Memberdef)
    .ToListAsync();

Console.WriteLine($"Function count: {functions.Count}");
Console.WriteLine();

if (!functions.Any())
{
    Console.WriteLine("No functions found in memberdef table.");
    return;
}

foreach (var function in functions)
{
    Console.WriteLine($"Function: {GetDisplayName(function)}");
    Console.WriteLine($"  Function name: {function.Name}");
    Console.WriteLine($"  Definition: {function.Definition ?? "<none>"}");
    Console.WriteLine($"  Return type: {function.Type ?? "<unknown>"}");
    Console.WriteLine($"  Declared in file: {function.File?.Name ?? "<unknown>"}");
    Console.WriteLine($"  Implementation file: {function.Bodyfile?.Name ?? function.File?.Name ?? "<unknown>"}");
    Console.WriteLine($"  Scope: {function.Scope ?? "<none>"}");
    Console.WriteLine($"  Line: {function.Line}");

    var parameters = function.MemberdefParams
        .Select(mp => mp.Param)
        .Where(p => p is not null)
        .ToList();

    if (parameters.Count > 0)
    {
        Console.WriteLine("  Parameters:");
        foreach (var param in parameters)
        {
            Console.WriteLine($"    - Type: {param.Type ?? "<unknown>"}, Name: {param.Declname ?? param.Defname ?? "<unnamed>"}");
        }
    }
    else if (!string.IsNullOrWhiteSpace(function.Argsstring))
    {
        Console.WriteLine($"  Parameters: {function.Argsstring}");
    }
    else
    {
        Console.WriteLine("  Parameters: <none>");
    }

    var callees = xrefs
        .Where(x => x.SrcRow.Memberdef != null && x.SrcRow.Memberdef.Rowid == function.Rowid)
        .Select(x => new { x.DstRow.Memberdef, x.Context })
        .Where(x => x.Memberdef != null)
        .Select(x => new { Memberdef = x.Memberdef!, x.Context })
        .ToList();

    var callers = xrefs
        .Where(x => x.DstRow.Memberdef != null && x.DstRow.Memberdef.Rowid == function.Rowid)
        .Select(x => new { x.SrcRow.Memberdef, x.Context })
        .Where(x => x.Memberdef != null)
        .Select(x => new { Memberdef = x.Memberdef!, x.Context })
        .ToList();

    Console.WriteLine($"  Calls: {callees.Count}");
    foreach (var x in callees)
    {
        Console.WriteLine($"    - {GetDisplayName(x.Memberdef)} (context={x.Context})");
    }

    Console.WriteLine($"  Called by: {callers.Count}");
    foreach (var x in callers)
    {
        Console.WriteLine($"    - {GetDisplayName(x.Memberdef)} (context={x.Context})");
    }

    Console.WriteLine();
}

Console.WriteLine("Function inspection complete.");

static string GetDisplayName(Memberdef memberdef)
{
    if (!string.IsNullOrWhiteSpace(memberdef.Definition))
    {
        return memberdef.Definition;
    }

    var args = !string.IsNullOrWhiteSpace(memberdef.Argsstring)
        ? memberdef.Argsstring
        : string.Join(", ", memberdef.MemberdefParams.Select(mp => FormatParam(mp.Param)));

    return $"{memberdef.Type ?? "<unknown>"} {memberdef.Name}({args})".Trim();
}

static string FormatParam(Param? param)
{
    if (param is null)
    {
        return "<unknown>";
    }

    var name = param.Declname ?? param.Defname ?? "<unnamed>";
    return $"{param.Type ?? "<unknown>"} {name}".Trim();
}