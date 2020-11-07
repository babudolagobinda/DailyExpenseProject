using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyExpenditure.DAL.DTO;
using DailyExpenditure.DAL.Entity;
using DailyExpenditure.DAL.DataConnection;

namespace DailyExpenditure.DAL.Factory
{
    public class UserDetailsFactory
    {
        DataContext dataContext = new DataContext();
        public UserDetailsDto UserSignIn(UserDetailsDto userDetailsDto)
        {
            UserDetailsDto userDetails = dataContext.UserDetail
                .Where(u => u.UserName == userDetailsDto.UserName && u.Password == userDetailsDto.Password)
                .Select(x => new UserDetailsDto()
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    Name = x.Name,
                    PhoneNo = x.PhoneNo,
                    EmailId = x.EmailId,
                    UserImage = x.UserImage
                }).SingleOrDefault();

            return userDetails;
        }
        public string CheckUserName(string userName)
        {
            var chkUserName = dataContext.UserDetail
                .Where(u => u.UserName == userName).Select(p => p.UserName).SingleOrDefault();
            return chkUserName;
        }
        public int CheckUserEmailId(int UserId,string EmailId)
        {
            var item = dataContext.UserDetail.Where(p =>p.UserId== UserId && p.EmailId == EmailId).Select(p => p.UserId).SingleOrDefault();
            return item;
        }
        public int UserSignUp(UserDetailsDto userDetailsDto)
        {
            UserDetail userDetail = new UserDetail();
            userDetail.Name = userDetailsDto.Name;
            userDetail.EmailId = userDetailsDto.EmailId;
            userDetail.PhoneNo = userDetailsDto.PhoneNo;
            userDetail.UserImage = userDetailsDto.UserImage;
            userDetail.UserName = userDetailsDto.UserName;
            userDetail.Password = userDetailsDto.Password;
            userDetail.CreatedDate = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            userDetail.IsActive = Convert.ToInt32(1);
            dataContext.UserDetail.Add(userDetail);
            int i = dataContext.SaveChanges();
            return i;
        }
        public UserDetailsDto GetUserDetails(int UserId)
        {
            UserDetailsDto userDetails = dataContext.UserDetail
                .Where(u => u.UserId == UserId)
                .Select(x => new UserDetailsDto()
                {
                    UserId = x.UserId,
                    UserName = x.UserName,
                    Name = x.Name,
                    PhoneNo = x.PhoneNo,
                    EmailId = x.EmailId,
                    UserImage = x.UserImage
                }).SingleOrDefault();

            return userDetails;
        }
    }
}
