using DoxygenSqliteInspector.Models;
using DoxygenSqliteInspector.Repositories;

namespace DoxygenSqliteInspector.Services;

public sealed class DoxygenInspectorService : IDoxygenInspectorService
{
    private readonly IDoxygenFunctionRepository _repository;

    public DoxygenInspectorService(IDoxygenFunctionRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<DoxygenProjectMeta> GetMetaAsync(string dbPath, CancellationToken cancellationToken = default)
    {
        var meta = await _repository.GetMetaAsync(dbPath, cancellationToken);
        if (meta is null)
        {
            throw new InvalidOperationException("No meta information found in the database.");
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

    public Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(string dbPath, CancellationToken cancellationToken = default)
        => _repository.GetFilesAsync(dbPath, cancellationToken);

    public Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(string dbPath, CancellationToken cancellationToken = default)
        => _repository.GetFunctionsAsync(dbPath, cancellationToken);

    public Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(string dbPath, CancellationToken cancellationToken = default)
        => _repository.GetFunctionDetailsAsync(dbPath, cancellationToken);
}
