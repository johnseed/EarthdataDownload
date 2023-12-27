using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarthdataDownload
{
    public class OrbitCalculatedSpatialDomain
    {
        public string OrbitNumber { get; set; }
    }

    public class Link
    {
        public string Rel { get; set; }
        public string Title { get; set; }
        public string Hreflang { get; set; }
        public string Href { get; set; }
        public string Type { get; set; }
        public bool? Inherited { get; set; }
        public string Length { get; set; }
    }

    public class Entry
    {
        public List<string> Boxes { get; set; }
        public DateTime TimeStart { get; set; }
        public DateTime Updated { get; set; }
        public List<OrbitCalculatedSpatialDomain> OrbitCalculatedSpatialDomains { get; set; }
        public string DatasetId { get; set; }
        public string DataCenter { get; set; }
        public string Title { get; set; }
        public string CoordinateSystem { get; set; }
        public string DayNightFlag { get; set; }
        public DateTime TimeEnd { get; set; }
        public string Id { get; set; }
        public string OriginalFormat { get; set; }
        public string GranuleSize { get; set; }
        public bool BrowseFlag { get; set; }
        public List<List<string>> Polygons { get; set; }
        public string CollectionConceptId { get; set; }
        public bool OnlineAccessFlag { get; set; }
        public List<Link> Links { get; set; }
    }

    public class Feed
    {
        public DateTime Updated { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public List<Entry> Entry { get; set; }
    }

    public class FeedRoot
    {
        public Feed Feed { get; set; }
    }

}
