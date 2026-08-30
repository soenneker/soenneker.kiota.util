using System.Threading;
using System.Threading.Tasks;

namespace Soenneker.Kiota.Util.Abstract;

/// <summary>
/// Generates C# API clients with the Kiota command-line tool.
/// </summary>
public interface IKiotaUtil
{
    /// <summary>
    /// Generates a C# client from an OpenAPI document.
    /// </summary>
    /// <param name="fixedPath">Path or URL to the OpenAPI document.</param>
    /// <param name="clientName">C# class name for the generated client.</param>
    /// <param name="libraryName">C# namespace for the generated client and its output directory under <c>src</c>.</param>
    /// <param name="targetDir">Working directory in which generation runs.</param>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the generate operation is complete.</returns>
    ValueTask Generate(string fixedPath, string clientName, string libraryName, string targetDir, CancellationToken cancellationToken = default);

    /// <summary>
    /// Installs or updates the global Kiota .NET tool.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task that completes when the ensure installed operation is complete.</returns>
    ValueTask EnsureInstalled(CancellationToken cancellationToken = default);
}
