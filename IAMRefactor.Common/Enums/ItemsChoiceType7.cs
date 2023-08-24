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
  // [XmlType(Namespace = Saml2Constants.PROTOCOL, IncludeInSchema = false)]
    public enum ItemsChoiceType7
    {
        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextClassRef")] AuthnContextClassRef,


        /// <summary>
        /// 
        /// </summary>
        [XmlEnum("urn:oasis:names:tc:SAML:2.0:assertion:AuthnContextDeclRef")] AuthnContextDeclRef,
    }
}
