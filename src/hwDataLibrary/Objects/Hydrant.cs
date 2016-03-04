using System;
using HydrantWiki.Library.Helpers;
using TreeGecko.Library.Geospatial.Extensions;
using TreeGecko.Library.Common.Interfaces;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Geospatial.Attributes;
using TreeGecko.Library.Geospatial.Objects;

namespace HydrantWiki.Library.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class Hydrant : AbstractTGObject, IPositionable
    {
        public Hydrant()
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
        public Guid OriginalTagUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime OriginalTagDateTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? OriginalReviewerUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? LastReviewerUserGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid? PrimaryImageGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Properties Properties { get; set; }

        /// <summary>
        /// Gets or sets the creation date time (Time the record was added to the system)
        /// </summary>
        /// <value>
        /// The creation date time.
        /// </value>
        public DateTime CreationDateTime { get; set; }

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
        		        
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = base.GetTGSerializedObject();

            obj.Add("Position", Position);
            obj.Add("PrimaryImageGuid", PrimaryImageGuid);
            obj.Add("OriginalTagUserGuid", OriginalTagUserGuid);
            obj.Add("OriginalTagDateTime", OriginalTagDateTime);
            obj.Add("CreationDateTime", CreationDateTime);
            obj.Add("OriginalReviewerUserGuid", OriginalReviewerUserGuid);
            obj.Add("LastReviewerUserGuid", LastReviewerUserGuid);
            obj.Add("Properties", Properties.ToString());
            
            return obj;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            OriginalTagUserGuid = _tgs.GetGuid("OriginalTagUserGuid");
            OriginalTagDateTime = _tgs.GetDateTime("OriginalTagDateTime");
            Position = _tgs.GetGeoPoint("Position");
            CreationDateTime = _tgs.GetDateTime("CreationDateTime");
            PrimaryImageGuid = _tgs.GetNullableGuid("PrimaryImageGuid");
            OriginalReviewerUserGuid = _tgs.GetNullableGuid("OriginalReviewerUserGuid");
            LastReviewerUserGuid = _tgs.GetNullableGuid("LastReviewerUserGuid");

            string temp = _tgs.GetString("Properties");

            if (temp != null)
            {
                Properties = TGSerializedObject.GetTGSerializable<Properties>(temp);
            }
        }
    }
}
