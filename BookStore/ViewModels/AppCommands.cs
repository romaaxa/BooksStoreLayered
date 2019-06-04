using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;

namespace BookStore
{
    public class AppCommands : INotifyPropertyChanged
    {
        private Book selectedBook;

        IFileService fileService;
        IDialogService dialogService;
        public ObservableCollection<Book> Books { get; set; }

        //public Book bk(author)
        //{

        //}

        private ExecCommands addCommand;
        public ExecCommands AddCommand
        {
            get
            {
                //?? null
                return addCommand ??
                  (addCommand = new ExecCommands(obj =>
                  {
                      Book book = new Book();
                      Books.Insert(0, book);
                      SelectedBook = book;
                  }));
            }
        }

        private ExecCommands saveCommand;
        public ExecCommands SaveCommand
        {
            get
            {
                return saveCommand ??
                  (saveCommand = new ExecCommands(obj =>
                  {
                      try
                      {
                          if (dialogService.SaveFileDialog() == true)
                          {
                              fileService.Save(dialogService.FilePath, Books.ToList());
                              dialogService.ShowMessage("          Файл сохранен!        ");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        
        private ExecCommands openCommand;
        public ExecCommands OpenCommand
        {
            get
            {
                return openCommand ??
                  (openCommand = new ExecCommands(obj =>
                  {
                      try
                      {
                          if (dialogService.OpenFileDialog() == true)
                          {
                              var books = fileService.Open(dialogService.FilePath);
                              Books.Clear();
                              foreach (var _book in books)
                                  Books.Add(_book);
                              dialogService.ShowMessage("          Файл открыт!        ");
                          }
                      }
                      catch (Exception ex)
                      {
                          dialogService.ShowMessage(ex.Message);
                      }
                  }));
            }
        }

        private ExecCommands searchCommand;

        private ExecCommands removeCommand;
        public ExecCommands RemoveCommand
        {
            get
            {
                return removeCommand ??
                  (removeCommand = new ExecCommands(obj =>
                  {
                      Book book = obj as Book;
                      if (book != null)
                      {
                          Books.Remove(book);
                      }
                  },
                 (obj) => Books.Count > 0));
            }
        }
        private ExecCommands search;

        
        public Book SelectedBook
        {
            get { return selectedBook; }
            set
            {
                selectedBook = value;
                OnPropertyChanged("SelectedBook");
            }
        }


        public AppCommands(IDialogService dialogService, IFileService fileService)
        {
            this.dialogService = dialogService;
            this.fileService = fileService;
            Books = new ObservableCollection<Book>
            {
                new Book {Title="Война и мир", Author="Толстой", Genre="MMO", OriginalLanguage="Русский", Price=300, Year=1337 },
                new Book {Title="Математика за 3 часа", Author="Grin'", Genre="Научный", OriginalLanguage="Русский", Price=2000000, Year=2500 },
                new Book {Title="Теория Ибрагимов", Author="Жига", Genre="RPG", OriginalLanguage="Русский", Price=228, Year=1488 },
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
