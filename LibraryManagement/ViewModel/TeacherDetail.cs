using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class TeacherDetail
    {
        public int Id { get; set; }
        public string TeacherId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? Department { get; set; }
        public string Email { get; set; }
        public string ContactNumber { get; set; }
        public string ImagePath { get; set; }
    }
}
