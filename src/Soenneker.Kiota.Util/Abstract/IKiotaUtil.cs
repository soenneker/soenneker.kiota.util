using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Kiota.Util.Abstract;

/// <summary>
/// A utility library for Kiota and OpenAPI related operations
/// </summary>
public interface IKiotaUtil
{
    ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default);

    ValueTask EnsureInstalled(CancellationToken cancellationToken = default);
}