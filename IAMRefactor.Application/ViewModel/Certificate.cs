using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IAMRefactor.Application.ViewModel
{
    public class Certificate
    {
        public string Thumbprint { get; set; }

        public string FileName { get; set; }

        public string Password { get; set; }

        public string X509KeyStorageFlags { get; set; } = "PersistKeySet";

        public string StoreLocation { get; set; } = "LocalMachine";

        public string StoreName { get; set; } = "My";

        public X509KeyStorageFlags GetKeyStorageFlags()
        {
            if (Enum.TryParse(X509KeyStorageFlags, out X509KeyStorageFlags flags))
            {
                return flags;
            }

            throw new InvalidOperationException("Invalid X509KeyStorageFlags");
        }

        public StoreLocation GetStoreLocation()
        {
            if (Enum.TryParse(StoreLocation, out StoreLocation storeLocation))
            {
                return storeLocation;
            }

            throw new InvalidOperationException("Invalid StoreLocation");
        }

        public StoreName GetStoreName()
        {
            if (Enum.TryParse(StoreName, out StoreName storeName))
            {
                return storeName;
            }

            throw new InvalidOperationException("Invalid StoreName");
        }
    }
}
