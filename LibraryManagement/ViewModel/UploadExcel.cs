using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class UploadExcel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }

        [NotMapped]
        public DataTable DetailsDT { get; set; }
    }
}
