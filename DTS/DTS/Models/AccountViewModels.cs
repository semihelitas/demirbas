using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DTS.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Zorunlu Alan")]
        [Display(Name = "Username")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Zorunlu Alan")]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}