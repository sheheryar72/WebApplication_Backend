using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebDevelopmentBackend.Models
{
    public class User
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }

    }
}