using System.Globalization;
using System;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using X.Common.Helper;

namespace EarthdataDownload
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var urls = await Search(DateTime.Now.AddDays(-10), DateTime.Now, DataType.海面风场);
            foreach (var url in urls)
            {
                Download(urls[0]);
                Console.WriteLine(url);
            }
            Console.WriteLine("Hello, World!");
        }
        public enum DataType
        {
            海流,
            海面风场

        }
        public static async Task<List<string>> Search(DateTime startDate, DateTime endDate, DataType dataType)
        {
            Dictionary<DataType, string> dataDict = new()
            {
                { DataType.海流, "C2102958977" },
                { DataType.海面风场, "C2075141638" },
            };
            string start = startDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture); // 2022-12-31T20:00:02.000Z
            string end = endDate.ToString("yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture);
            HttpClient client = new();

            HttpRequestMessage request = new(HttpMethod.Post, "https://cmr.earthdata.nasa.gov/search/granules.json");

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

            request.Content = new StringContent($"echo_collection_id={dataDict[dataType]}-POCLOUD&page_num=1&page_size=200&temporal={start},{end}&sort_key=-start_date");
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");

            HttpResponseMessage response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            FeedRoot feed = JsonConvert.DeserializeObject<FeedRoot>(responseBody);
            List<string> urls = feed.Feed.Entry.Select(x => x.Links[0].Href).ToList();
            return urls;
        }

        public static void Download(string url)
        {
            string outputFile = Path.GetFileName(url);
            // curl --insecure --proxy http://192.168.3.134:8888 -f -b "cookies.pwIASwSVAV" -c "cookies.pwIASwSVAV" -L --netrc-file ".netrc" -g -o "ascat_20221231_224800_metopc_21537_eps_o_250_3301_ovw.l2.nc" -- "https://archive.podaac.earthdata.nasa.gov/podaac-ops-cumulus-protected/ASCATC-L2-25km/ascat_20221231_224800_metopc_21537_eps_o_250_3301_ovw.l2.nc"
            CommandHelper.Execute("curl", $"--insecure -f -b \"cookies.pwIASwSVAV\" -c \"cookies.pwIASwSVAV\" -L --netrc-file \".netrc\" -g -o \"{outputFile}\" -- \"{url}\"");
        }
    }
}