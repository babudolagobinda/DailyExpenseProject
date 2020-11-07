using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace DailyExpenditure.UI.Models
{
    public class UserDetailsModel
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter EmailId.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please Enter PhoneNo.")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please Enter UserName.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password.")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Please Select UserImage.")]
        public string UserImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }
    }
}