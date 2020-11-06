using System;
using System.Collections.Generic;

namespace bitwarden_1password
{
    public class OnePasswordModel{
        public string uuid { get; set; } 
        public string typeName { get; set; } 
        public string title { get; set; } 
        public string location { get; set; } 
        public OpenContents openContents { get; set; } 
        public SecureContents secureContents { get; set; } 

        public class OpenContents{
            public List<string> tags { get; set; } 
        }

        public class URL{
            public string label { get; set; } 
            public string url { get; set; } 
        }

        public class Field{
            public string designation { get; set; } 
            public string name { get; set; } 
            public string type { get; set; } 
            public string value { get; set; } 
        }

        public class SecureContents{
            public List<URL> URLs { get; set; } 
            public string notesPlain { get; set; } 
            public List<Field> fields { get; set; } 
        }
    }   
}