using System;
using HydrantWiki.Library.Helpers;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Attributes;
using TreeGecko.Library.Geospatial.Extensions;
using TreeGecko.Library.Geospatial.Objects;


namespace HydrantWiki.Library.Objects
{
    public class Tag : AbstractTGObject, IPositionable
    {
        public Tag()
        {
            Properties = new Properties();
        }

        /// <summary>
        /// 
        /// </summary>
        [Geometry]
        public GeoPoint Position { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExternalIdentifier { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ExternalSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PositionText
        {
            get
            {
                if (Position != null)
                {
                    return Position.ToString("###.######");
                }

                return null;
            }
        }
        
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
        public string TagType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? ImageGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Properties Properties { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? HydrantGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Status { get; set; }
		
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = base.GetTGSerializedObject();

            obj.Add("UserGuid", UserGuid);
            obj.Add("DeviceDateTime", DeviceDateTime);
            obj.Add("TagPosition", Position);
            obj.Add("TagType", TagType);
            obj.Add("ImageGuid", ImageGuid);
            obj.Add("ExternalIdentifier", ExternalIdentifier);
            obj.Add("ExternalSource", ExternalSource);
            obj.Add("Properties", Properties.ToString());
            obj.Add("HydrantGuid", HydrantGuid);
            obj.Add("Status", Status);

            return obj;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserGuid = _tgs.GetGuid("UserGuid");
            DeviceDateTime = _tgs.GetDateTime("DeviceDateTime");
            Position = _tgs.GetGeoPoint("TagPosition");
            TagType = _tgs.GetString("TagType");
            ImageGuid = _tgs.GetNullableGuid("ImageGuid");
            ExternalIdentifier = _tgs.GetString("ExternalIdentifier");
            ExternalSource = _tgs.GetString("ExternalSource");
            HydrantGuid = _tgs.GetNullableGuid("HydrantGuid");

            string temp = _tgs.GetString("Properties");

            if (temp != null)
            {
                Properties = TGSerializedObject.GetTGSerializable<Properties>(temp);
            }

            Status = _tgs.GetString("Status");
        }

        /// <summary>
        /// 
        /// </summary>
        public string ThumbnailUrl
        {
            get
            {
                return this.GetUrl(true);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public string ImageUrl
        {
            get
            {
                return this.GetUrl(false);
            }
        }
    }
}
