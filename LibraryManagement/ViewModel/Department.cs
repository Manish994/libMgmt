using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class Department
    {
        public Department()
        {
            TeacherDetails = new HashSet<TeacherDetail>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<TeacherDetail> TeacherDetails { get; set; }
    }
}
