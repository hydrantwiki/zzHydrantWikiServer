using System;

namespace HydrantWiki.Mobile.Api.Objects 
{
    public class HydrantHeader
    {
        public Guid HydrantGuid { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ImageUrl { get; set; }

        public Double? DistanceInFeet { get; set; }

        public GeoLocation Position { get; set; }

        public string Username { get; set; }
    }
}