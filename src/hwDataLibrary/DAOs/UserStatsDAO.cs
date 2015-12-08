using System;
using HydrantWiki.Library.Objects;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;

namespace HydrantWiki.Library.DAOs
{
    internal class UserStatsDAO: AbstractMongoDAO<UserStats>
    {
        public UserStatsDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "UserStats"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildUniqueIndex("UserGuid", "USERGUID");
        }

        public UserStats GetByUser(Guid _userGuid)
        {
            return GetOneItem<UserStats>("UserGuid", _userGuid.ToString());
        }
    }
}
