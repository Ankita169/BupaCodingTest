using System.Collections.Generic;

namespace BookOwner.ViewModels
{
    public class BookOwnerViewModel
    {
        public List<Book> AdultBooks { get; set; }
        public List<Book> Childbooks { get; set; }
    }

    public class Book
    {
        public string Name { get; set; }
        public string Type { get; set; }
    }
}