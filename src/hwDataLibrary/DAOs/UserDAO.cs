using System.Collections.Generic;
using System.Collections.Specialized;
using HydrantWiki.Library.Objects;
using MongoDB.Driver;
using TreeGecko.Library.Mongo.DAOs;

namespace HydrantWiki.Library.DAOs
{
    internal class UserDAO: AbstractMongoDAO<User>
    {
        public UserDAO(MongoDatabase _mongoDB)
            : base(_mongoDB)
        {
            HasParent = false;
        }

        public override string TableName
        {
            get { return "User"; }
        }

        public override void BuildTable()
        {
            base.BuildTable();

            List<string> columns = new List<string> {"UserSource", "Username"};
            BuildUniqueIndex(columns, "USERSOURCE_USERNAME");

            BuildUniqueSparceIndex("EmailAddress", "EMAIL");
        }

        public User Get(string _userSource, string _userName)
        {
            NameValueCollection nvc = new NameValueCollection
            {
                {"UserSource", _userSource}, 
                {"Username", _userName}
            };

            return GetOneItem<User>(nvc);
        }

        public User GetByEmail(string _userSource, string _emailAddress)
        {
            NameValueCollection nvc = new NameValueCollection
            {
                {"UserSource", _userSource},
                {"EmailAddress", _emailAddress}
            };

            return GetOneItem<User>(nvc);
        }

        public List<User> GetPrimaryUsers()
        {
            return GetList("ParentGuid", null);
        }

    }
}
