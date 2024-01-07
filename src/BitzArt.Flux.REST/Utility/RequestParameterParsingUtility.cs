using Microsoft.Extensions.Logging;
using System.Text;
using System.Text.RegularExpressions;

namespace BitzArt.Flux;

internal partial class RequestParameterParsingUtility
{
    public static RequestUrlParameterParsingResult ParseRequestUrl(ILogger logger, string path, object[]? parameters)
    {
        var logBuilder = new StringBuilder();

        var matches = ParameterRegex().Matches(path);
        if (!matches.Any()) return new RequestUrlParameterParsingResult(path, string.Empty);

        if (parameters is null) throw new ParametersNotFoundException();

        var requiredCount = matches.Count;
        var foundCount = parameters.Length;

        if (requiredCount != foundCount) throw new ParameterCountDidNotMatchException(foundCount, requiredCount);

        var result = path;
        logBuilder.Append('\n');

        for (int counter = 0; counter < requiredCount; counter++)
        {
            var match = matches[counter];
            var paramName = match.Groups[1].Value;
            var value = parameters[counter].ToString();
            result = result.Replace(match.Value, value);

            if (counter > 0) logBuilder.Append("; ");
            logBuilder.Append($"{paramName}: {value}");
        }

        return new RequestUrlParameterParsingResult(result, logBuilder.ToString());
    }

    [GeneratedRegex("{(.*?)}")]
    private static partial Regex ParameterRegex();
}

file class ParametersNotFoundException : Exception
{
    public ParametersNotFoundException()
        : base("Parameters are specified in endpoint configuration but not found in the request.")
    { }
}

file class ParameterCountDidNotMatchException : Exception
{
    public ParameterCountDidNotMatchException(int found, int required)
        : base($"Number of parameters in a request ({found}) did not match number of required parameters ({required}) for this endpoint.")
    { }
}
