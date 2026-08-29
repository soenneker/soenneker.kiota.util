using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Kiota.Util.Abstract;

/// <summary>
/// A utility library for Kiota and OpenAPI related operations
/// </summary>
public interface IKiotaUtil
{
    /// <summary>
    /// Generates kiota.
    /// </summary>
    /// <param name="fixedPath">Path of the fixed to use.</param>
    /// <param name="clientName">client Name used to communicate with the external service.</param>
    /// <param name="libraryName">Name of the library to load.</param>
    /// <param name="targetDir">Target Dir for the generate operation.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the generate operation is complete.</returns>
    ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default);

    /// <summary>
    /// Ensures installed for the kiota.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the ensure installed operation is complete.</returns>
    ValueTask EnsureInstalled(CancellationToken cancellationToken = default);
}
