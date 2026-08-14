using DoxygenSqliteInspector.Lib.Models;

namespace DoxygenSqliteInspector.Lib.Services;

public interface IDoxygenInspectorService
{
    Task<DoxygenProjectMeta> GetMetaAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(string dbPath, CancellationToken cancellationToken = default);
}
