[![](https://img.shields.io/nuget/v/soenneker.kiota.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.util/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.kiota.util/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.kiota.util/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.kiota.util.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.kiota.util/)

# Soenneker.Kiota.Util

A utility library for Kiota and OpenAPI related operations.

## Install

```bash
dotnet add package Soenneker.Kiota.Util
```

## Quick start

```csharp
using Soenneker.Kiota.Util.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddKiotaUtilAsSingleton();
```

Adds `IKiotaUtil` as a singleton service.

## What you get

- `IKiotaUtil` — A utility library for Kiota and OpenAPI related operations.
- `KiotaUtilRegistrar` — A utility library for Kiota and OpenAPI related operations.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `IKiotaUtil.Generate(fixedPath, clientName, libraryName, targetDir, cancellationToken)` | Generates kiota. | A task that completes when the generate operation is complete. |
| `KiotaUtilRegistrar.AddKiotaUtilAsSingleton(services)` | Adds `IKiotaUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `KiotaUtilRegistrar.AddKiotaUtilAsScoped(services)` | Adds `IKiotaUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Cancellation stops pending work; it does not undo work that has already completed.
