using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BubbleTea.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string ASP_Id { get; set; }
    }
}
