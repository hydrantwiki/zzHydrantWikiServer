using System;
using TreeGecko.Library.Common.Objects;

namespace HydrantWiki.Library.Objects
{
    public class UserStats : AbstractTGObject 
    {
        public Guid UserGuid { get; set; }

        public int RejectedTagCount { get; set; }

        public int ApprovedTagCount { get; set; }

        public int PendingTagCount { get; set; }

        public int UserRanking { get; set; }

        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject tgs = base.GetTGSerializedObject();

            tgs.Add("UserGuid", UserGuid);
            tgs.Add("RejectedTagCount", RejectedTagCount);
            tgs.Add("ApprovedTagCount", ApprovedTagCount);
            tgs.Add("PendingTagCount", PendingTagCount);
            tgs.Add("UserRanking", UserRanking);

            return tgs;                
        }

        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            UserGuid = _tgs.GetGuid("UserGuid");
            RejectedTagCount = _tgs.GetInt32("RejectedTagCount");
            ApprovedTagCount = _tgs.GetInt32("ApprovedTagCount");
            PendingTagCount = _tgs.GetInt32("PendingTagCount");
            UserRanking = _tgs.GetInt32("UserRanking");
        }

        public override string ToString()
        {
            TGSerializedObject obj = GetTGSerializedObject();

            return obj.ToString();
        }
    }
}
