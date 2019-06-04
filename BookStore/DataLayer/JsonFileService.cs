using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace BookStore
{
    public class JsonFileService : IFileService
    {
        public List<Book> Open(string filename)
        {
            List<Book> books = new List<Book>();
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(filename, FileMode.OpenOrCreate))
            {
                books = jsonFormatter.ReadObject(fs) as List<Book>;
            }

            return books;
        }

        public void Save(string filename, List<Book> booksList)
        {
            DataContractJsonSerializer jsonFormatter =
                new DataContractJsonSerializer(typeof(List<Book>));
            using (FileStream fs = new FileStream(filename, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, booksList);
            }
        }
    }
}
