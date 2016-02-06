using System;

namespace HydrantWiki.Mobile.Api.Objects
{
    public class Tag
    {
        public Tag()
        {
            
        }

        public Tag(Library.Objects.Tag _source)
        {
            ImageGuid = _source.ImageGuid;
            TagDateTime = _source.DeviceDateTime;

            if (_source.Position != null)
            {
                Position = new Position
                {
                    DeviceDateTime = _source.DeviceDateTime,
                    Latitude = _source.Position.Y,
                    Longitude = _source.Position.X
                };
            }
        }

        public Guid? ImageGuid { get; set; }
        public Position Position { get; set; }
        public DateTime TagDateTime { get; set; }
    }
}