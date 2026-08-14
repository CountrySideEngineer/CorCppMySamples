using DoxygenSqliteInspector.Lib.Models;
using DoxygenSqliteInspector.Lib.Repositories;
using DoxygenSqliteInspector.Lib.Services;

namespace DoxygenSqliteInspector.Lib;

public sealed class DoxygenInspectorClient
{
    private readonly IDoxygenInspectorService _service;
    private readonly string _databasePath;

    public DoxygenInspectorClient()
        : this(new DoxygenInspectorService(new DoxygenFunctionRepository()), new DoxygenInspectorOptions())
    {
    }

    public DoxygenInspectorClient(DoxygenInspectorOptions options)
        : this(new DoxygenInspectorService(new DoxygenFunctionRepository()), options)
    {
    }

    public DoxygenInspectorClient(IDoxygenInspectorService service)
        : this(service, new DoxygenInspectorOptions())
    {
    }

    public DoxygenInspectorClient(IDoxygenInspectorService service, DoxygenInspectorOptions options)
    {
        _service = service ?? throw new ArgumentNullException(nameof(service));
        ArgumentNullException.ThrowIfNull(options);

        if (string.IsNullOrWhiteSpace(options.DatabasePath))
        {
            throw new ArgumentException("DatabasePath is required.", nameof(options));
        }

        _databasePath = options.DatabasePath;
    }

    public string DatabasePath => _databasePath;

    public Task<DoxygenProjectMeta> GetMetaAsync(CancellationToken cancellationToken = default)
        => _service.GetMetaAsync(_databasePath, cancellationToken);

    public Task<IReadOnlyList<DoxygenFileSummary>> GetFilesAsync(CancellationToken cancellationToken = default)
        => _service.GetFilesAsync(_databasePath, cancellationToken);

    public Task<IReadOnlyList<DoxygenFunctionSummary>> GetFunctionsAsync(CancellationToken cancellationToken = default)
        => _service.GetFunctionsAsync(_databasePath, cancellationToken);

    public Task<IReadOnlyList<DoxygenFunctionDetail>> GetFunctionDetailsAsync(CancellationToken cancellationToken = default)
        => _service.GetFunctionDetailsAsync(_databasePath, cancellationToken);
}
