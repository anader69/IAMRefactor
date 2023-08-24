using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IAMRefactor.Common.Enums
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
  //  [XmlType(Namespace = Saml2Constants.PROTOCOL)]
    public enum AuthnContextComparisonType
    {
        /// <summary>
        /// 
        /// </summary>
        exact,


        /// <summary>
        /// 
        /// </summary>
        minimum,


        /// <summary>
        /// 
        /// </summary>
        maximum,


        /// <summary>
        /// 
        /// </summary>
        better,
    }
}
