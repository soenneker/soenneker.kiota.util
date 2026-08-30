[![](https://img.shields.io/nuget/v/soenneker.kiota.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.util/build-and-test.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.util/actions/workflows/build-and-test.yml)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.kiota.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.util/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.util/actions/workflows/codeql.yml)

# Soenneker.Kiota.Util

A small wrapper for installing Kiota and generating C# clients from OpenAPI documents.

## Install

```bash
dotnet add package Soenneker.Kiota.Util
```

## Usage

```csharp
using Soenneker.Kiota.Util.Abstract;
using Soenneker.Kiota.Util.Registrars;

services.AddKiotaUtilAsSingleton();

IKiotaUtil kiota = serviceProvider.GetRequiredService<IKiotaUtil>();

await kiota.EnsureInstalled(cancellationToken);
await kiota.Generate(
    fixedPath: "openapi.json",
    clientName: "OrdersClient",
    libraryName: "Example.Orders.Client",
    targetDir: repositoryDirectory,
    cancellationToken);
```

`Generate` runs Kiota with `targetDir` as its working directory and writes the client beneath `targetDir/src/<libraryName>`. `clientName` must be a C# identifier and `libraryName` must be a dot-separated C# namespace. Paths containing spaces are supported.

Generation cleans Kiota's output directory before writing it. Keep hand-written files outside that directory. A non-zero tool exit, installation failure, or cancellation is surfaced to the caller.

Use `AddKiotaUtilAsScoped()` when its directory and process dependencies should follow a DI scope; otherwise use the singleton registration.
