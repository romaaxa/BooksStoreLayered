using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore
{
    public interface IFileService
    {
        List<Book> Open(string filename);
        void Save(string filename, List<Book> booksList);
    }
}
