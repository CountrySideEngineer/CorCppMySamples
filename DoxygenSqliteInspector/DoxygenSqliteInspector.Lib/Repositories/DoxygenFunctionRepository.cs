using DoxygenSqliteInspector.Data;
using DoxygenSqliteInspector.Lib.Models;
using DoxygenSqliteInspector.Models;
using Microsoft.EntityFrameworkCore;

namespace DoxygenSqliteInspector.Lib.Repositories;

public sealed class DoxygenFunctionRepository : IDoxygenFunctionRepository
{
    public async Task<DoxygenProjectMeta?> GetMetaAsync(string dbPath, CancellationToken cancellationToken = default)
    {
        await using var context = new DoxygenContext(dbPath);
        var meta = await context.Meta.AsNoTracking().FirstOrDefaultAsync(cancellationToken);
        if (meta is null)
        {
            return null;
        }

        return new DoxygenProjectMeta
        {
            DoxygenVersion = meta.DoxygenVersion,
            SchemaVersion = meta.SchemaVersion,
            GeneratedAt = meta.GeneratedAt,
            GeneratedOn = meta.GeneratedOn,
            ProjectName = meta.ProjectName,
            ProjectNumber = meta.ProjectNumber
        };
    }

    public async Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(string dbPath, CancellationToken cancellationToken = default)
    {
        await using var context = new DoxygenContext(dbPath);
        var files = await context.Paths
            .AsNoTracking()
            .OrderBy(p => p.Name)
            .Select(p => new DoxygenFileSummary
            {
                Rowid = p.Rowid,
                Name = p.Name
            })
            .ToListAsync(cancellationToken);

        return files;
    }

    public async Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(string dbPath, CancellationToken cancellationToken = default)
    {
        await using var context = new DoxygenContext(dbPath);
        var functions = await context.Memberdefs
            .AsNoTracking()
            .Where(m => m.Kind == "function")
            .Include(m => m.File)
            .Include(m => m.Bodyfile)
            .OrderBy(m => m.Name)
            .Select(m => new DoxygenFunctionSummary
            {
                Rowid = m.Rowid,
                Name = m.Name,
                Definition = m.Definition,
                ReturnType = m.Type,
                DeclaredIn = m.File != null ? m.File.Name : null,
                ImplementedIn = m.Bodyfile != null ? m.Bodyfile.Name : m.File != null ? m.File.Name : null,
                Scope = m.Scope,
                Line = m.Line
            })
            .ToListAsync(cancellationToken);

        return functions;
    }

    public async Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(string dbPath, CancellationToken cancellationToken = default)
    {
        await using var context = new DoxygenContext(dbPath);

        var functions = await context.Memberdefs
            .AsNoTracking()
            .Where(m => m.Kind == "function")
            .Include(m => m.File)
            .Include(m => m.Bodyfile)
            .Include(m => m.MemberdefParams).ThenInclude(mp => mp.Param)
            .OrderBy(m => m.Name)
            .ToListAsync(cancellationToken);

        var xrefs = await context.Xrefs
            .AsNoTracking()
            .Include(x => x.SrcRow).ThenInclude(r => r.Memberdef)
            .Include(x => x.DstRow).ThenInclude(r => r.Memberdef)
            .ToListAsync(cancellationToken);

        var result = new List<DoxygenFunctionDetail>(functions.Count);

        foreach (var function in functions)
        {
            var parameters = function.MemberdefParams
                .Select(mp => new DoxygenFunctionParameter
                {
                    Type = mp.Param?.Type,
                    Name = mp.Param?.Declname ?? mp.Param?.Defname
                })
                .ToList();

            var callees = xrefs
                .Where(x => x.SrcRow.Memberdef != null && x.SrcRow.Memberdef.Rowid == function.Rowid)
                .Select(x => new DoxygenFunctionCall
                {
                    Name = x.DstRow.Memberdef?.Definition ?? x.DstRow.Memberdef?.Name,
                    Context = x.Context
                })
                .ToList();

            var callers = xrefs
                .Where(x => x.DstRow.Memberdef != null && x.DstRow.Memberdef.Rowid == function.Rowid)
                .Select(x => new DoxygenFunctionCall
                {
                    Name = x.SrcRow.Memberdef?.Definition ?? x.SrcRow.Memberdef?.Name,
                    Context = x.Context
                })
                .ToList();

            result.Add(new DoxygenFunctionDetail
            {
                Rowid = function.Rowid,
                Name = function.Name,
                Definition = function.Definition,
                ReturnType = function.Type,
                DeclaredIn = function.File?.Name,
                ImplementedIn = function.Bodyfile?.Name ?? function.File?.Name,
                Scope = function.Scope,
                Line = function.Line,
                Parameters = parameters,
                Callees = callees,
                Callers = callers
            });
        }

        return result;
    }
}
