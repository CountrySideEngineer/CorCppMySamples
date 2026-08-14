using DoxygenSqliteInspector.Lib.Models;

namespace DoxygenSqliteInspector.Lib.Repositories;

public interface IDoxygenFunctionRepository
{
    Task<DoxygenProjectMeta?> GetMetaAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(string dbPath, CancellationToken cancellationToken = default);
}
