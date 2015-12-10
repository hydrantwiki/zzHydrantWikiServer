using System.Collections.Generic;
using HydrantWiki.Library.Objects;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TreeGecko.Library.Geospatial.Objects;
using TreeGecko.Library.Mongo.DAOs;

namespace HydrantWiki.Library.DAOs
{
    internal class HydrantDAO: AbstractMongoDAO<Hydrant>
    {
        public HydrantDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Hydrant"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildGeospatialIndex("Position", "POSITION");
        }

        public List<Hydrant> GetHydrants(GeoBox _geoBox)
        {
            IMongoQuery query = Query.WithinRectangle("Position",
                _geoBox.MinLongitude(), _geoBox.MinLatitude(), 
                _geoBox.MaxLongitude(), _geoBox.MaxLatitude());

            return GetList(query);
        }

        public List<Hydrant> GetHydrants(GeoBox _geoBox, int _quantity)
        {
            IMongoQuery query = Query.WithinRectangle("Position",
                _geoBox.MinLongitude(), _geoBox.MinLatitude(),
                _geoBox.MaxLongitude(), _geoBox.MaxLatitude());

            MongoCursor cursor = GetCursor(query).SetLimit(_quantity);

            return GetList(cursor);
        }
    }
}
