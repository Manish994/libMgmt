using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryManagement.ViewModel
{
    public partial class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool Role { get; set; }
        public bool Blocked { get; set; }
    }
}
