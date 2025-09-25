using System.Net.NetworkInformation;
using Newtonsoft.Json;

namespace Birthsys.Identity.Domain.Abstractions
{
    public class Error(int code, string type, List<string> details)
    {
        public int Code { get; } = code;
        public string Type { get; } = $"Error.{type}";
        public List<string> Details { get; } = details;
        public DateTime OccurredOn { get; } = DateTime.UtcNow;
        public override string ToString() => $"{Type} - ({Code}): {string.Join(", ", Details)}";
        public string ToJsonString() => JsonConvert.SerializeObject(this, new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        });
        public static Error ReferenceNull => Build(500, "ReferenceNull", "The specified reference cannot be null.");
        public static Error None => Build(200, "None", "No error.");
        public static Error Build(int code, string type, string detail) => new(code, type, [detail]);
        public static Error Build(int code, string type, List<string> details) => new(code, type, details);

    }
}