using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class BookDetail
    {
        public BookDetail()
        {
            StudentDetails = new HashSet<StudentDetail>();
        }

        public int Id { get; set; }
        public string BookId { get; set; }
        public string AuthorName { get; set; }
        public string BookName { get; set; }
        public string ImagePath { get; set; }

        public virtual ICollection<StudentDetail> StudentDetails { get; set; }
    }
}
