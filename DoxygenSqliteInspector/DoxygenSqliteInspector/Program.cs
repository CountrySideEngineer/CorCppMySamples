using System.Text.Json;
using DoxygenSqliteInspector;
using DoxygenSqliteInspector.Models;
using DoxygenSqliteInspector.Repositories;
using DoxygenSqliteInspector.Services;

Console.WriteLine("Doxygen SQLite DB Inspector using EF Core");

var hasFunction = args.Any(a => string.Equals(a, "--function", StringComparison.OrdinalIgnoreCase));
var hasFile = args.Any(a => string.Equals(a, "--file", StringComparison.OrdinalIgnoreCase));
var hasJson = args.Any(a => string.Equals(a, "--json", StringComparison.OrdinalIgnoreCase));
var hasShortF = args.Any(a => string.Equals(a, "-f", StringComparison.OrdinalIgnoreCase));

int idxFunction = Array.FindIndex(args, a => string.Equals(a, "--function", StringComparison.OrdinalIgnoreCase));
int idxShortF = Array.FindIndex(args, a => string.Equals(a, "-f", StringComparison.OrdinalIgnoreCase));
int idxFile = Array.FindIndex(args, a => string.Equals(a, "--file", StringComparison.OrdinalIgnoreCase));
int idxJson = Array.FindIndex(args, a => string.Equals(a, "--json", StringComparison.OrdinalIgnoreCase));

var efHasFunction = hasFunction || hasShortF;

static bool TryGetArgumentValue(string[] args, string[] names, out string? value)
{
    value = null;
    for (int i = 0; i < args.Length; i++)
    {
        foreach (var name in names)
        {
            if (string.Equals(args[i], name, StringComparison.OrdinalIgnoreCase))
            {
                if (i + 1 < args.Length)
                {
                    value = args[i + 1];
                    return true;
                }

                return false;
            }
        }
    }

    return false;
}

if (hasJson && !efHasFunction && !hasFile && !hasShortF)
{
    Console.Error.WriteLine("Argument error: 'json' must be used with either --file or --function.");
    return;
}

if (efHasFunction && hasFile)
{
    Console.Error.WriteLine("Argument error: --file and --function cannot be used together.");
    return;
}

if (hasJson)
{
    var primaryIndex = efHasFunction ? (hasFunction ? idxFunction : idxShortF) : idxFile;
    if (idxJson <= primaryIndex)
    {
        Console.Error.WriteLine("Argument error: 'json' must be specified after --file or --function.");
        return;
    }
}

if (!TryGetArgumentValue(args, new[] { "--db", "--database" }, out var dbPath) || string.IsNullOrWhiteSpace(dbPath))
{
    Console.Error.WriteLine("Argument error: --db <path> or --database <path> is required.");
    return;
}

if (!File.Exists(dbPath))
{
    Console.Error.WriteLine($"Database file not found: {dbPath}");
    return;
}

try
{
    var clientOptions = new DoxygenInspectorOptions { DatabasePath = dbPath };
    var inspectorClient = new DoxygenInspectorClient(clientOptions);

    var meta = await inspectorClient.GetMetaAsync();
    Console.WriteLine("Meta information:");
    Console.WriteLine($"  Project: {meta.ProjectName}");
    Console.WriteLine($"  Version: {meta.ProjectNumber}");
    Console.WriteLine($"  Doxygen: {meta.DoxygenVersion}");
    Console.WriteLine($"  Schema: {meta.SchemaVersion}");
    Console.WriteLine($"  Generated: {meta.GeneratedAt} {meta.GeneratedOn}");
    Console.WriteLine();

    if (hasFile)
    {
        var files = await inspectorClient.GetFilesAsync();

        Console.WriteLine($"File count: {files.Count}");
        foreach (var file in files)
        {
            Console.WriteLine($"  - {file.Name}");
        }

        if (hasJson)
        {
            var json = JsonSerializer.Serialize(files.Select(p => new { p.Rowid, p.Name }), new JsonSerializerOptions { WriteIndented = true });
            var outName = "file.json";
            await File.WriteAllTextAsync(outName, json);
            Console.WriteLine($"Wrote JSON to {outName}");
        }

        return;
    }

    var functions = await inspectorClient.GetFunctionDetailsAsync();

    if (efHasFunction)
    {
        Console.WriteLine($"Function count: {functions.Count}");
        foreach (var function in functions)
        {
            Console.WriteLine($"  - {GetDisplayName(function)}");
        }

        if (hasJson)
        {
            var outList = functions.Select(function => new
            {
                Rowid = function.Rowid,
                Name = function.Name,
                Definition = function.Definition,
                ReturnType = function.ReturnType,
                DeclaredIn = function.DeclaredIn,
                ImplementedIn = function.ImplementedIn,
                Scope = function.Scope,
                Line = function.Line,
                Parameters = function.Parameters.Select(p => new { Type = p.Type, Name = p.Name }),
                Callees = function.Callees,
                Callers = function.Callers
            }).ToList();

            var json = JsonSerializer.Serialize(outList, new JsonSerializerOptions { WriteIndented = true });
            var outName = "--function.json";
            if (hasShortF && idxShortF >= 0 && idxJson == idxShortF + 1)
            {
                outName = "function_detail.json";
            }

            await File.WriteAllTextAsync(outName, json);
            Console.WriteLine($"Wrote JSON to {outName}");
        }

        return;
    }

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
        Console.WriteLine($"  Return type: {function.ReturnType ?? "<unknown>"}");
        Console.WriteLine($"  Declared in file: {function.DeclaredIn ?? "<unknown>"}");
        Console.WriteLine($"  Implementation file: {function.ImplementedIn ?? "<unknown>"}");
        Console.WriteLine($"  Scope: {function.Scope ?? "<none>"}");
        Console.WriteLine($"  Line: {function.Line}");

        if (function.Parameters.Count > 0)
        {
            Console.WriteLine("  Parameters:");
            foreach (var param in function.Parameters)
            {
                Console.WriteLine($"    - Type: {param.Type ?? "<unknown>"}, Name: {param.Name ?? "<unnamed>"}");
            }
        }
        else
        {
            Console.WriteLine("  Parameters: <none>");
        }

        Console.WriteLine($"  Calls: {function.Callees.Count}");
        foreach (var callee in function.Callees)
        {
            Console.WriteLine($"    - {callee.Name ?? "<unknown>"} (context={callee.Context ?? "<unknown>"})");
        }

        Console.WriteLine($"  Called by: {function.Callers.Count}");
        foreach (var caller in function.Callers)
        {
            Console.WriteLine($"    - {caller.Name ?? "<unknown>"} (context={caller.Context ?? "<unknown>"})");
        }

        Console.WriteLine();
    }

    Console.WriteLine("Function inspection complete.");
}
catch (InvalidOperationException ex)
{
    Console.WriteLine(ex.Message);
    return;
}

static string GetDisplayName(DoxygenFunctionDetail function)
{
    if (!string.IsNullOrWhiteSpace(function.Definition))
    {
        return function.Definition;
    }

    var args = function.Parameters.Count > 0
        ? string.Join(", ", function.Parameters.Select(p => FormatParam(p.Type, p.Name)))
        : string.Empty;

    return $"{function.ReturnType ?? "<unknown>"} {function.Name}({args})".Trim();
}

static string FormatParam(string? type, string? name)
{
    var paramName = name ?? "<unnamed>";
    return $"{type ?? "<unknown>"} {paramName}".Trim();
}