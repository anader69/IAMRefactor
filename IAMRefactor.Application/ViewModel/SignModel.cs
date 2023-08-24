using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.ViewModel
{
    public class SignModel
    {
        public string SignHashUrl { get; set; }
        public int RectangleX1 { get; set; }
        public int RectangleX2 { get; set; }
        public int RectangleY1 { get; set; }
        public int RectangleY2 { get; set; }
        public int PageNumber { get; set; }
        public string PersonFirstName { get; set; }
        public string PersonSecondName { get; set; }
        public string Reason { get; set; }

        public bool RTL { get; set; }
        public string SignHashRequestAdssUrl { get; set; }
        public string ClientID { get; set; }
        public string MutualAuthCertSerial { get; set; }
        public bool EnableMutualAuth { get; set; }
        public string SigningProfile { get; set; }
        public string Alias { get; set; }
        public string PrivateKeyPassword { get; set; }
        public string TSAurl { get; set; }
        public string PdfFile { get; set; }
        public string BackImage { get; set; }
    }
}
