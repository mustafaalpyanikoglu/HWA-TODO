using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Core.CrossCuttingConcerns.Exceptions.HttpProblemDetails;

public static class ProblemDetailsExtensions
{
    public static string AsJson(this ProblemDetails details) => JsonConvert.SerializeObject(details);
}