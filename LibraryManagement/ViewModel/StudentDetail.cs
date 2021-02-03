using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class StudentDetail
    {
        public int Id { get; set; }
        public int? CollegeRollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? DepartmentId { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string Address { get; set; }
        public string ImageName { get; set; }
        public int? BookId { get; set; }

        public virtual BookDetail Book { get; set; }
        public virtual Department Department { get; set; }
    }
}
