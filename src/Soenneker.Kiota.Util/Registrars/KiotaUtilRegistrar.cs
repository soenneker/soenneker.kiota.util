using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Kiota.Util.Abstract;
using Soenneker.Utils.Directory.Registrars;
using Soenneker.Utils.Process.Registrars;

namespace Soenneker.Kiota.Util.Registrars;

/// <summary>
/// A utility library for Kiota and OpenAPI related operations
/// </summary>
public static class KiotaUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="IKiotaUtil"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddKiotaUtilAsSingleton(this IServiceCollection services)
    {
        services.AddDirectoryUtilAsSingleton()
                .AddProcessUtilAsSingleton();

        services.TryAddSingleton<IKiotaUtil, KiotaUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="IKiotaUtil"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddKiotaUtilAsScoped(this IServiceCollection services)
    {
        services.AddDirectoryUtilAsScoped()
                .AddProcessUtilAsScoped();

        services.TryAddScoped<IKiotaUtil, KiotaUtil>();

        return services;
    }
}