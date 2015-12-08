
using System.Text;
using TreeGecko.Library.Common.Attributes;
using TreeGecko.Library.Common.Objects;

namespace HydrantWiki.Library.Objects
{
    /// <summary>
    /// 
    /// </summary>
    public class Address : AbstractTGObject  
    {
        /// <summary>
        /// 
        /// </summary>
        public string Address1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Country { get; set; }

        [Identifier]
        public string Identifier
        {
            get { return Country + "|" + PostalCode; }
        }


        public override TGSerializedObject GetTGSerializedObject()
        {
            TGSerializedObject obj = base.GetTGSerializedObject();

            obj.Add("Address1", Address1);
            obj.Add("Address2", Address2);
            obj.Add("City", City);
            obj.Add("State", State);
            obj.Add("PostalCode", PostalCode);
            obj.Add("Country", Country);

            return obj;
        }


        public override void LoadFromTGSerializedObject(TGSerializedObject _tgs)
        {
            base.LoadFromTGSerializedObject(_tgs);

            Address1 = _tgs.GetString("Address1");
            Address2 = _tgs.GetString("Address2");
            City = _tgs.GetString("City");
            State = _tgs.GetString("State");
            PostalCode = _tgs.GetString("PostalCode");
            Country = _tgs.GetString("Country");
        }
		
		public override string ToString ()
		{
			StringBuilder sb = new StringBuilder();
			
			if (!string.IsNullOrEmpty(Address1))
			{
				sb.AppendLine(Address1);
			}
			
			if (!string.IsNullOrEmpty(Address2))
			{
			sb.AppendLine(Address2);
			}
			
			sb.AppendFormat("{0}, {1} {2}", City, State, PostalCode);
			
			return sb.ToString();
		}
    }
}
