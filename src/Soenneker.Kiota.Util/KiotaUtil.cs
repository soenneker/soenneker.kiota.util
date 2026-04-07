using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Reader;
using Soenneker.Extensions.Task;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.Util.Abstract;
using Soenneker.Utils.Directory.Abstract;
using Soenneker.Utils.File.Abstract;
using Soenneker.Utils.Process.Abstract;

namespace Soenneker.Kiota.Util;

/// <inheritdoc cref="IKiotaUtil"/>
public sealed class KiotaUtil : IKiotaUtil
{
    private readonly IDirectoryUtil _directoryUtil;
    private readonly IFileUtil _fileUtil;
    private readonly IProcessUtil _processUtil;

    public KiotaUtil(IDirectoryUtil directoryUtil, IFileUtil fileUtil, IProcessUtil processUtil)
    {
        _directoryUtil = directoryUtil;
        _fileUtil = fileUtil;
        _processUtil = processUtil;
    }

    public async ValueTask ConvertOpenApi2To3(string sourcePath, string destinationPath, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(sourcePath);
        ArgumentException.ThrowIfNullOrWhiteSpace(destinationPath);

        await using MemoryStream sourceStream = await _fileUtil.ReadToMemoryStream(sourcePath, log: false, cancellationToken).NoSync();

        ReadResult readResult = await OpenApiModelFactory.LoadAsync(sourceStream, settings: new OpenApiReaderSettings(), cancellationToken: cancellationToken).NoSync();

        if (readResult.Document is null)
            throw new InvalidOperationException($"Unable to load OpenAPI document from '{sourcePath}'.");

        if (readResult.Diagnostic?.Errors?.Count > 0)
        {
            string errors = string.Join("; ", readResult.Diagnostic.Errors.Select(static error => error.Message));
            throw new InvalidOperationException($"Unable to parse OpenAPI document '{sourcePath}': {errors}");
        }

        string? directory = Path.GetDirectoryName(destinationPath);

        if (!string.IsNullOrWhiteSpace(directory))
        {
            await _directoryUtil.Create(directory, cancellationToken: cancellationToken)
                                .NoSync();
        }

        string extension = Path.GetExtension(destinationPath);

        if (extension.Equals(".json", StringComparison.OrdinalIgnoreCase))
        {
            string json = await readResult.Document.SerializeAsJsonAsync(OpenApiSpecVersion.OpenApi3_0, cancellationToken).NoSync();
            await _fileUtil.Write(destinationPath, json, log: false, cancellationToken).NoSync();
            return;
        }

        if (extension.Equals(".yaml", StringComparison.OrdinalIgnoreCase) || extension.Equals(".yml", StringComparison.OrdinalIgnoreCase))
        {
            string yaml = await readResult.Document.SerializeAsYamlAsync(OpenApiSpecVersion.OpenApi3_0, cancellationToken).NoSync();
            await _fileUtil.Write(destinationPath, yaml, log: false, cancellationToken).NoSync();
            return;
        }

        throw new ArgumentException($"Unsupported destination extension '{extension}'. Expected .json, .yaml, or .yml.", nameof(destinationPath));
    }

    public async ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default)
    {
        await _directoryUtil.Create(targetDir, cancellationToken: cancellationToken)
                            .NoSync();

        var outputDir = $"src/{libraryName}";

        await _processUtil.Start("kiota", targetDir, $"generate -l CSharp -d \"{fixedPath}\" -o {outputDir} -c {clientName} -n {libraryName} --ebc --cc",
                              waitForExit: true, cancellationToken: cancellationToken)
                          .NoSync();
    }

    public async ValueTask EnsureInstalled(CancellationToken cancellationToken = default)
    {
        await _processUtil.Start("dotnet", null, "tool update --global Microsoft.OpenApi.Kiota", waitForExit: true, cancellationToken: cancellationToken);
    }
}