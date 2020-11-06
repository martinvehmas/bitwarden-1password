using System;
using System.Collections.Generic;

namespace bitwarden_1password
{
    public class BitwardenModel{
        public List<Folder> folders { get; set; } 
        public List<Item> items { get; set; } 

        public class Folder{
            public string id { get; set; } 
            public string name { get; set; } 
        }

        public class Uri{
            public object match { get; set; } 
            public string uri { get; set; } 
        }

        public class Login{
            public List<Uri> uris { get; set; } 
            public string username { get; set; } 
            public string password { get; set; } 
        }

        public class SecureNote{
            public int type { get; set; } 
        }

        public class Item    {
            public string id { get; set; } 
            public object organizationId { get; set; } 
            public string folderId { get; set; } 
            public int type { get; set; } 
            public string name { get; set; } 
            public string notes { get; set; } 
            public bool favorite { get; set; } 
            public Login login { get; set; } 
            public object collectionIds { get; set; } 
            public SecureNote secureNote { get; set; } 
        }
    }
}
