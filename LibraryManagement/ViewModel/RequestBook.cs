﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class RequestBook
    {
        public int Id { get; set; }
        public string CollegeRollNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DepartmentName { get; set; }
        public string Email { get; set; }
        public string BookId { get; set; }
        public string BookName { get; set; }
        public string BookAuthor { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public bool? Action { get; set; }

        [NotMapped]
        public DataTable DetailsDT { get; set; }
    }
}
