using System;
using TreeGecko.Library.Common.Objects;

namespace HydrantWiki.Library.Objects
{
    public class HydrantImage : AbstractTGObject
    {
        /// <summary>
        /// 
        /// </summary>
        public Guid HydrantGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Guid ImageGuid { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject bcn = base.GetTGSerializedObject();

            bcn.Add("HydrantGuid", HydrantGuid);
            bcn.Add("ImageGuid", ImageGuid);

            return bcn;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            HydrantGuid = _tgs.GetGuid("HydrantGuid");
            ImageGuid = _tgs.GetGuid("ImageGuid");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            TGSerializedObject obj = GetTGSerializedObject();

            return obj.ToString();
        }
    }
}
