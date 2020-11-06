using System;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace bitwarden_1password
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter path to folder");
            string folderPath = Console.ReadLine();
            Console.WriteLine("Enter filename of Bitwarden JSON file");
            string bitwardenFileName = Console.ReadLine();
            BitwardenModel bitwarden = JsonSerializer.Deserialize<BitwardenModel>(System.IO.File.ReadAllText(System.IO.Path.Combine(folderPath, bitwardenFileName)));

            if (bitwarden.items.Where(i => i.type > 2).Count() > 0)
            {
                Console.WriteLine("Warning: Only supports Logins and Notes.");
            }
           
            string onePifString = string.Empty;
            foreach (BitwardenModel.Item item in bitwarden.items.Where(i => i.type <= 2).ToList())
            {

                var newItem = new OnePasswordModel();
                newItem.uuid = Guid.NewGuid().ToString().Replace("-", "");
                newItem.title = item.name;
                
                if (item.folderId != null)
                {
                    string folder = bitwarden.folders.Where(f => f.id == item.folderId).Select(f => f.name).First();
                    newItem.openContents = new OnePasswordModel.OpenContents() {tags = new List<string>() {folder}};
                }

                newItem.secureContents = new OnePasswordModel.SecureContents();
                newItem.secureContents.notesPlain = item.notes;

                if (item.login?.uris != null && item.login.uris.Count() > 0)
                {
                    newItem.location = item.login.uris[0].uri;
                    newItem.secureContents.URLs = item.login.uris.Select(x => new OnePasswordModel.URL() { label = "website", url = x.uri }).ToList();
                }
                
                if (item.type == 1)
                {
                    newItem.typeName = "webforms.WebForm";
                    newItem.secureContents.fields = new List<OnePasswordModel.Field>();
                    newItem.secureContents.fields.Add(new OnePasswordModel.Field() { designation = "username", name = "username", type = "T", value = item.login?.username });
                    newItem.secureContents.fields.Add(new OnePasswordModel.Field() { designation = "password", name = "password", type = "P", value = item.login?.password });
                }

                if (item.type == 2)
                {
                    newItem.typeName = "securenotes.SecureNote";
                }

                onePifString += JsonSerializer.Serialize(newItem);
                onePifString += "\r\n***5642bee8-a5ff-11dc-8314-0800200c9a66***\r\n";
            }

            System.IO.File.WriteAllText(System.IO.Path.Combine(folderPath, $"data{DateTime.Now.ToString("yyyy-MM-dd-HHmmss")}.1pif"), onePifString);
        }
    }
}
