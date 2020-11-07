using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace DailyExpenditure.DAL.DTO
{
    public class UserDetailsDto
    {
        public int UserId { get; set; }
        [Required(ErrorMessage = "Please Enter Name.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Please Enter EmailId.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        public string EmailId { get; set; }
        [Required(ErrorMessage = "Please Enter PhoneNo.")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNo { get; set; }
        [Required(ErrorMessage = "Please Enter UserName.")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Please Enter Password.")]
        public string Password { get; set; }
        //[Required(ErrorMessage = "Please Select UserImage.")]
        public string UserImage { get; set; }
        public DateTime CreatedDate { get; set; }
        public int IsActive { get; set; }

    }
    public class UserMessage
    {
        public string nullErrorMsg = "Please Enter UserName and Password..!";
        public string invalidUserMsg = "UserName and Password Is Incorrect.";
        public string successMsg = "You Have Completed Registration Successfully.Please SignIn..!";
        public string imgMessage = "Please choose only Image file";
        public string nonImgMessage = "Please Select Image file";
        public string userCheckMessage = "UserName alredy Exist!";
    }
}
