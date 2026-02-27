using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Domain
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Group { get; set; } = string.Empty;
    }
}
