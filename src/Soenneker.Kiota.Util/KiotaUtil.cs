using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Soenneker.Extensions.ValueTask;
using Soenneker.Kiota.Util.Abstract;
using Soenneker.Utils.Directory.Abstract;
using Soenneker.Utils.Process.Abstract;

namespace Soenneker.Kiota.Util;

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
        if (!IsIdentifier(clientName))
            throw new InvalidOperationException($"'{clientName}' is not a valid C# client class name.");

        if (!IsQualifiedIdentifier(libraryName))
            throw new InvalidOperationException($"'{libraryName}' is not a valid C# namespace.");

        await _directoryUtil.Create(targetDir, cancellationToken: cancellationToken)
                            .NoSync();

        var outputDir = $"src/{libraryName}";
        string arguments = $"generate -l CSharp -d {QuoteArgument(fixedPath)} -o {QuoteArgument(outputDir)} -c {QuoteArgument(clientName)} -n {QuoteArgument(libraryName)} --ebc --co --cc";

        await _processUtil.Start("kiota", targetDir, arguments, waitForExit: true, cancellationToken: cancellationToken)
                          .NoSync();
    }

    public async ValueTask EnsureInstalled(CancellationToken cancellationToken = default)
    {
        await _processUtil.Start("dotnet", null, "tool update --global Microsoft.OpenApi.Kiota", waitForExit: true, cancellationToken: cancellationToken);
    }

    private static bool IsQualifiedIdentifier(string value)
    {
        ReadOnlySpan<char> remaining = value.AsSpan();

        while (!remaining.IsEmpty)
        {
            int separator = remaining.IndexOf('.');
            ReadOnlySpan<char> segment = separator < 0 ? remaining : remaining[..separator];

            if (!IsIdentifier(segment))
                return false;

            if (separator < 0)
                return true;

            remaining = remaining[(separator + 1)..];
        }

        return false;
    }

    private static bool IsIdentifier(string value) => IsIdentifier(value.AsSpan());

    private static bool IsIdentifier(ReadOnlySpan<char> value)
    {
        if (value.IsEmpty || !(char.IsLetter(value[0]) || value[0] == '_'))
            return false;

        for (var i = 1; i < value.Length; i++)
        {
            if (!(char.IsLetterOrDigit(value[i]) || value[i] == '_'))
                return false;
        }

        return true;
    }

    private static string QuoteArgument(string value)
    {
        var result = new StringBuilder(value.Length + 2).Append('"');
        var backslashes = 0;

        foreach (char character in value)
        {
            if (character == '\\')
            {
                backslashes++;
                continue;
            }

            if (character == '"')
            {
                result.Append('\\', backslashes * 2 + 1);
                result.Append('"');
            }
            else
            {
                result.Append('\\', backslashes);
                result.Append(character);
            }

            backslashes = 0;
        }

        result.Append('\\', backslashes * 2);
        return result.Append('"').ToString();
    }
}
