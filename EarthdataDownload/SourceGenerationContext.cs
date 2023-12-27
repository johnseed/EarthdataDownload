using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EarthdataDownload
{
    [JsonSourceGenerationOptions(WriteIndented = true)]
    [JsonSerializable(typeof(FeedRoot))]
    internal partial class SourceGenerationContext : JsonSerializerContext
    {
    }
}
