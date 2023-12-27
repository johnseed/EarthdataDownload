using System.Globalization;
using System;
using System.Net.Http.Headers;
using X.Common.Helper;
using System.CommandLine.Invocation;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json;

namespace EarthdataDownload
{
    internal class Program
    {
        static async Task<int> Main(string[] args)
        {
            if (args.Length == 0)
            {
                args = ["--help"];
            }
            var rootCommand = new RootCommand
            {
                new Option<string>([ "--datatype", "-t" ], "Data type."),
                new Option<string>([ "--output", "-o" ], "Output folder."),
                new Option<DateTime>([ "--start", "-s" ], "Start time."),
                new Option<DateTime>([ "--end", "-e" ], "End time.")
            };
            rootCommand.Description = "Earthdata download\n\n" +
                                    "Examples:\n" +
                                    "  EarthdataDownload -t 海流 -o output -s 2022-12-31T00:00:02.000Z -e 2022-12-31T20:00:02.000Z \n" +
                                    "  EarthdataDownload -t 海面风场 -o output -s 2022-12-31T00:00:02.000Z -e 2022-12-31T20:00:02.000Z \n" +
                                    "";
            rootCommand.Handler = CommandHandler.Create<string, string, DateTime, DateTime>(HandleParameters);

            return await rootCommand.InvokeAsync(args);
            //return rootCommand.Invoke(args);
        }

        static async Task HandleParameters(string datatype, string output, DateTime start, DateTime end)
        {
            // 解析枚举类型
            if (!Enum.TryParse(datatype, out DataType dataTypeEnum))
            {
                Console.WriteLine($"Invalid data type: {datatype}");
                return;
            }

            var urls = await Search(start, end, dataTypeEnum);
            foreach (var url in urls)
            {
                Download(url, output);
                Console.WriteLine(url);
            }
            Console.WriteLine("Hello, World!");
        }

        public enum DataType
        {
            海流,
            海面风场
        }

        [RequiresUnreferencedCode("Calls DynamicBehavior.")]
        public static async Task<List<string>> Search(DateTime startDate, DateTime endDate, DataType dataType)
        {
            Dictionary<DataType, string> dataDict = new()
            {
                { DataType.海流, "C2102958977" },
                { DataType.海面风场, "C2075141638" },
            };
            string start = startDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture); // 2022-12-31T20:00:02.000Z
            string end = endDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            using HttpClient client = new();

            using HttpRequestMessage request = new(HttpMethod.Post, "https://cmr.earthdata.nasa.gov/search/granules.json");

            request.Headers.Add("authority", "cmr.earthdata.nasa.gov");
            request.Headers.Add("accept", "application/json, text/plain, */*");
            request.Headers.Add("accept-language", "zh-CN,zh;q=0.9");
            request.Headers.Add("client-id", "eed-edsc-prod-serverless-client");
            request.Headers.Add("cmr-request-id", "b66f8bdd-2588-48ca-bbae-4710ce072592");
            request.Headers.Add("origin", "https://search.earthdata.nasa.gov");
            request.Headers.Add("referer", "https://search.earthdata.nasa.gov/");
            request.Headers.Add("sec-ch-ua", "\"Microsoft Edge\";v=\"119\", \"Chromium\";v=\"119\", \"Not?A_Brand\";v=\"24\"");
            request.Headers.Add("sec-ch-ua-mobile", "?0");
            request.Headers.Add("sec-ch-ua-platform", "\"Windows\"");
            request.Headers.Add("sec-fetch-dest", "empty");
            request.Headers.Add("sec-fetch-mode", "cors");
            request.Headers.Add("sec-fetch-site", "same-site");
            request.Headers.Add("user-agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/119.0.0.0 Safari/537.36 Edg/119.0.0.0");

            request.Content = new StringContent($"echo_collection_id={dataDict[dataType]}-POCLOUD&page_num=1&page_size=900&temporal={start},{end}&sort_key=-start_date");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            using HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();

            //FeedRoot feed = JsonConvert.DeserializeObject<FeedRoot>(responseBody);
            FeedRoot feed = JsonSerializer.Deserialize(responseBody, SourceGenerationContext.Default.FeedRoot);
            // new JsonSerializerOptions() { PropertyNameCaseInsensitive = true},
            List<string> urls = feed.Feed.Entry.Select(x => x.Links.Single(l => l.Href.StartsWith("https://") && l.Href.EndsWith(".nc")).Href).ToList();
            return urls;
        }

        public static void Download(string url, string outputFolder = ".")
        {
            if (!Directory.Exists(outputFolder)) { Directory.CreateDirectory(outputFolder); }
            string outputFile = Path.GetFileName(url);
            string outputPath = Path.Combine(outputFolder, outputFile);
            // curl --insecure --proxy http://192.168.3.134:8888 -f -b "cookies.pwIASwSVAV" -c "cookies.pwIASwSVAV" -L --netrc-file ".netrc" -g -o "ascat_20221231_224800_metopc_21537_eps_o_250_3301_ovw.l2.nc" -- "https://archive.podaac.earthdata.nasa.gov/podaac-ops-cumulus-protected/ASCATC-L2-25km/ascat_20221231_224800_metopc_21537_eps_o_250_3301_ovw.l2.nc"
            CommandHelper.Execute("curl", $"--insecure -f -b \"cookies.pwIASwSVAV\" -c \"cookies.pwIASwSVAV\" -L --netrc-file \".netrc\" -g -o \"{outputPath}\" -- \"{url}\"");
        }
    }
}