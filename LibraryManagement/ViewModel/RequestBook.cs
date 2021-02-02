using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class RequestBook
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public int? BookId { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
    }
}
