using System;
using HydrantWiki.Library.Helpers;
using TreeGecko.Library.Geospatial.Objects;

namespace HydrantWiki.Library.Objects
{
    public class TagRow 
    {
        private Guid? m_ImageGuid;

        public TagRow()
        {
        }

        public TagRow(Tag _tag)
        {
            UserGuid = _tag.UserGuid;
            Position = _tag.Position;
            DeviceDateTime = _tag.DeviceDateTime;
            ImageGuid = _tag.ImageGuid;
            TagGuid = _tag.Guid;
        }

        public Guid TagGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid UserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime DeviceDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ImageGuid
        {
            get
            {
                return m_ImageGuid;
            }
            set
            {
                m_ImageGuid = value;

                ImageUrl = this.GetUrl(false);
                ThumbImageUrl = this.GetUrl(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public GeoPoint Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PositionText
        {
            get
            {
                if (Position != null)
                {
                    return Position.ToString("####.####");
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? Latitude
        {
            get
            {
                if (Position != null)
                {
                    return Position.Y;
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public double? Longitude
        {
            get
            {
                if (Position != null)
                {
                    return Position.X;
                }

                return null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public string ThumbImageUrl { get; private set; }

        
    }
}
