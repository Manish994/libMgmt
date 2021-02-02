using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class StudentDetail
    {
        public int Id { get; set; }
        public int? StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Branch { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
    }
}
