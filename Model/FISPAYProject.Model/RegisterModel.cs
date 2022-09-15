using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FISPAYProject.Model
{
    public class RegisterModel
    {
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string UserType { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdatePersonModel
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
    }
}
