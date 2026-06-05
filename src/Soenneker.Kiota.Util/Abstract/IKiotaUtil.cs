using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Kiota.Util.Abstract;

/// <summary>
/// A utility library for Kiota and OpenAPI related operations
/// </summary>
public interface IKiotaUtil
{
    /// <summary>
    /// Executes the generate operation.
    /// </summary>
    /// <param name="fixedPath">The fixed path.</param>
    /// <param name="clientName">The client name.</param>
    /// <param name="libraryName">The library name.</param>
    /// <param name="targetDir">The target dir.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default);

    /// <summary>
    /// Executes the ensure installed operation.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    ValueTask EnsureInstalled(CancellationToken cancellationToken = default);
}