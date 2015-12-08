using System;
using TreeGecko.Library.Geospatial.Objects;

namespace HydrantWiki.Library.Objects
{
    public class NearbyHydrant
    {
        public Guid HydrantGuid { get; set; }

        public string Thumbnail { get; set; }

        public string DistanceInFeet { get; set; }

        public string Location { get; set; }

        public GeoPoint Position { get; set; }

        public string MatchButton { get; set; }

        public string Color { get; set; }

        public string Symbol { get; set; }

        public string Title { get; set; }
    }
}
