using System;
using System.Collections.Generic;
using HydrantWiki.Library.Constants;
using HydrantWiki.Library.Objects;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TreeGecko.Library.Mongo.DAOs;

namespace HydrantWiki.Library.DAOs
{
    internal class TagDAO: AbstractMongoDAO<Tag>
    {
        public TagDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "Tag"; }
        }

        public List<Tag> GetTagsForHydrant(Guid _hydrantGuid)
        {
            return GetList("HydrantGuid", _hydrantGuid.ToString());
        }

        /// <summary>
        /// Returns a count of tags for a user
        /// </summary>
        /// <param name="_userGuid"></param>
        /// <returns></returns>
        public int GetTagCount(Guid _userGuid)
        {
            IMongoQuery query = GetQuery("UserGuid", _userGuid.ToString());

            return (int)GetCount(query);
        }

        /// <summary>
        /// Returns a count of all tags in system.
        /// </summary>
        /// <returns></returns>
        public int GetTagCount()
        {
            return (int)MongoCollection.Count();
        }

        public List<Tag> GetTagsForUser(Guid _userGuid)
        {
            IMongoQuery query = GetQuery("UserGuid", _userGuid.ToString());
            MongoCursor cursor = GetCursor(query)
                .SetSortOrder(SortBy.Ascending("DeviceDateTime"));
            return GetList(cursor);
        }

        public Tag GetNextPendingTag()
        {
            IMongoQuery query = GetQuery("Status", TagStatus.Pending);
            MongoCursor cursor = GetCursor(query)
                .SetSortOrder(SortBy.Ascending("DeviceDateTime"))
                .SetBatchSize(1);
            return GetFirst(cursor);

        }

        public List<Tag> GetPendingTags()
        {
            IMongoQuery query = GetQuery("Status", TagStatus.Pending);
            MongoCursor cursor = GetCursor(query)
                .SetSortOrder(SortBy.Ascending("DeviceDateTime"));
            return GetList(cursor);

        }

        public override void BuildTable()
        {
            base.BuildTable();

            BuildNonuniqueIndex("HydrantGuid", "HYDRANTGUID");
            BuildNonuniqueIndex("UserGuid", "USERGUID");
            BuildGeospatialIndex("TagPosition", "TAGPOSITION");
        }
    }
}
