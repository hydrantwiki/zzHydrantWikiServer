using HydrantWiki.Library.DAOs;
using TreeGecko.Library.Mongo.Managers;
using TreeGecko.Library.Net.DAOs;
using TreeGecko.Library.Net.Objects;

namespace HydrantWiki.Library.Managers
{
    public class HydrantWikiStructureManager: AbstractMongoManager
    {
        public HydrantWikiStructureManager()
            : base("HW")
        {
           
        }

        public void BuildDB()
        {
            HydrantDAO hydrantDAO = new HydrantDAO(MongoDB);
            hydrantDAO.BuildTable();

            HydrantImageDAO hydrantImageDAO = new HydrantImageDAO(MongoDB);
            hydrantImageDAO.BuildTable();

            TagDAO tagDAO = new TagDAO(MongoDB);
            tagDAO.BuildTable();

            UserDAO userDAO = new UserDAO(MongoDB);
            userDAO.BuildTable();

            UserStatsDAO userStatsDAO = new UserStatsDAO(MongoDB);
            userStatsDAO.BuildTable();

            CannedEmailDAO cannedEmailDAO = new CannedEmailDAO(MongoDB);
            cannedEmailDAO.BuildTable();

            SystemEmailDAO systemEmailDAO = new SystemEmailDAO(MongoDB);
            systemEmailDAO.BuildTable();

            TGUserAuthorizationDAO tgUserAuthorizationDAO = new TGUserAuthorizationDAO(MongoDB);
            tgUserAuthorizationDAO.BuildTable();

            TGUserEmailValidationDAO tgUserEmailValidationDAO = new TGUserEmailValidationDAO(MongoDB);
            tgUserEmailValidationDAO.BuildTable();

            TGUserPasswordDAO tgUserPasswordDAO = new TGUserPasswordDAO(MongoDB);
            tgUserPasswordDAO.BuildTable();

            WebLogEntryDAO webLogEntryDAO = new WebLogEntryDAO(MongoDB);
            webLogEntryDAO.BuildTable();

        }
    }
}
