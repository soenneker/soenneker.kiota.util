using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.Util.Abstract;
using Soenneker.Utils.Directory.Abstract;
using Soenneker.Utils.Process.Abstract;

namespace Soenneker.Kiota.Util;

/// <inheritdoc cref="IKiotaUtil"/>
public sealed class KiotaUtil : IKiotaUtil
{
    private readonly IDirectoryUtil _directoryUtil;
    private readonly IProcessUtil _processUtil;

    public KiotaUtil(IDirectoryUtil directoryUtil, IProcessUtil processUtil)
    {
        _directoryUtil = directoryUtil;
        _processUtil = processUtil;
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