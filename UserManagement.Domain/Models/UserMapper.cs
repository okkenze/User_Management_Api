using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Models
{
    public class UserMapper
    {
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression(@"[\w+]+$", ErrorMessage = "Invalid Character in First Name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression(@"[\w]+$", ErrorMessage = "Invalid Character in Last Name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Phone Number is required")]
        [RegularExpression(@"\d{11}", ErrorMessage = "Invalid Phone Number.Must be 11 digits")]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Gender is required")]
        [RegularExpression(@"[Male Female]+$", ErrorMessage = "Gender must be either Male Or Female")]
        public string Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        [Required(ErrorMessage = "Nationality is required")]
        [RegularExpression(@"[\w+]+$", ErrorMessage = "Invalid Character in Nationality")]
        public string Nationality { get; set; }
        [Required(ErrorMessage = "Role Id is required")]
        [RegularExpression(@"\d{1}", ErrorMessage = "Invalid Character in First Name")]
        public int RoleId { get; set; }
    }
}
