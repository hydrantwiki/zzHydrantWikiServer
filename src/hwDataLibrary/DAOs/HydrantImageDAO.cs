using HydrantWiki.Library.Objects;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;

namespace HydrantWiki.Library.DAOs
{
    internal class HydrantImageDAO : AbstractMongoDAO<HydrantImage>
    {
        public HydrantImageDAO(MongoDatabase _mongoDB) : base(_mongoDB)
        {
        }

        public override string TableName
        {
            get { return "HydrantImage"; }
        }
    }
}
