using DoxygenSqliteInspector.Models;

namespace DoxygenSqliteInspector.Repositories;

public interface IDoxygenFunctionRepository
{
    Task<Metum?> GetMetaAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(string dbPath, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(string dbPath, CancellationToken cancellationToken = default);
}
