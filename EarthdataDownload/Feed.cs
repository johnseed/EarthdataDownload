using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace EarthdataDownload
{
    public class OrbitCalculatedSpatialDomain
    {
        [JsonPropertyName("orbit_number")]
        public string OrbitNumber { get; set; }
    }

    public class Link
    {
        [JsonPropertyName("rel")]
        public string Rel { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("hreflang")]
        public string Hreflang { get; set; }

        [JsonPropertyName("href")]
        public string Href { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

        [JsonPropertyName("inherited")]
        public bool? Inherited { get; set; }

        [JsonPropertyName("length")]
        public string Length { get; set; }
    }

    public class Entry
    {
        [JsonPropertyName("boxes")]
        public List<string> Boxes { get; set; }

        [JsonPropertyName("time_start")]
        public DateTime TimeStart { get; set; }

        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("orbit_calculated_spatial_domains")]
        public List<OrbitCalculatedSpatialDomain> OrbitCalculatedSpatialDomains { get; set; }

        [JsonPropertyName("dataset_id")]
        public string DatasetId { get; set; }

        [JsonPropertyName("data_center")]
        public string DataCenter { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("coordinate_system")]
        public string CoordinateSystem { get; set; }

        [JsonPropertyName("day_night_flag")]
        public string DayNightFlag { get; set; }

        [JsonPropertyName("time_end")]
        public DateTime TimeEnd { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("original_format")]
        public string OriginalFormat { get; set; }

        [JsonPropertyName("granule_size")]
        public string GranuleSize { get; set; }

        [JsonPropertyName("browse_flag")]
        public bool BrowseFlag { get; set; }

        [JsonPropertyName("polygons")]
        public List<List<string>> Polygons { get; set; }

        [JsonPropertyName("collection_concept_id")]
        public string CollectionConceptId { get; set; }

        [JsonPropertyName("online_access_flag")]
        public bool OnlineAccessFlag { get; set; }

        [JsonPropertyName("links")]
        public List<Link> Links { get; set; }
    }

    public class Feed
    {
        [JsonPropertyName("updated")]
        public DateTime Updated { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("title")]
        public string Title { get; set; }

        [JsonPropertyName("entry")]
        public List<Entry> Entry { get; set; }
    }

    public class FeedRoot
    {
        [JsonPropertyName("feed")]
        public Feed Feed { get; set; }
    }

}
