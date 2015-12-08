using TreeGecko.Library.Common.Enums;
using TreeGecko.Library.Common.Objects;
using TreeGecko.Library.Net.Objects;

namespace HydrantWiki.Library.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class User : TGUser
    {
        /// <summary>
        /// 
        /// </summary>
        public string UserSource { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public UserTypes UserType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = base.GetTGSerializedObject();

            obj.Add("UserSource", UserSource);
            obj.Add("UserType", UserType);

            return obj;
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserSource = _tgs.GetString("UserSource");
            UserType = (UserTypes)_tgs.GetEnum("UserType", typeof (UserTypes), UserTypes.User);
        }

        public string GetUsernameWithSource()
        {
            string source = "";
            switch (this.UserSource)
            {
                case "osm":
                    source = "OpenStreetMap";
                    break;
                case "yo":
                    source = "Yo";
                    break;
                default:
                    source = "HydrantWiki";
                    break;
            }

            return string.Format("{0} - {1}", source, this.Username);
        }
    }
}
