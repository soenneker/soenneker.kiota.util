using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Kiota.Util.Abstract;

/// <summary>
/// A utility library for Kiota and OpenAPI related operations
/// </summary>
public interface IKiotaUtil
{
    ValueTask ConvertOpenApi2To3(string sourcePath, string destinationPath, CancellationToken cancellationToken = default);

    ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default);

    ValueTask EnsureInstalled(CancellationToken cancellationToken = default);
}